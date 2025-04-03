using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputSystem���g�����v���C���[�̈ړ��Ǘ��N���X
/// </summary>
public class MovePlayer : MonoBehaviour
{
    // �A�j���[�^�[�̎Q�� (�A�j���[�V��������p)
    [SerializeField] private Animator animator;

    // �v���C���[�̈ړ����x
    private float moveSpeed;
    private float defaultSpeed = 4.0f; // �����Ƃ�
    private float runSpeed = 7.0f;�@�@ // ����Ƃ�
    private float crouchSpeed = 2.0f;�@// ���Ⴊ�ݕ�������Ƃ�

    // Rigidbody�R���|�[�l���g���i�[����ϐ�
    private Rigidbody rb;

    //�J����
    public GameObject cameraObject;          // �v���C���[�������Ă���J������Transform
    public float cameraRotationSpeed = 100f; // �J������]���x

    // InputSystem
    private GameInputSystem inputActions;�@// ���͊Ǘ��V�X�e��
    private Vector2 moveInput;             // �ړ��̓��́i���Əc�̎��j
    private bool isRightClickHeld;         // �E�N���b�N��������Ă��邩�̃t���O
    private bool isShiftClickHeld;         // �V�t�g�L�[��������Ă��邩�̃t���O

    //�����蔻��
    private float wallCheckDistance = 1.0f; // �ǂƂ̃`�F�b�N����
    [SerializeField] private LayerMask wallLayerMask;        // �ǂ̃��C���[�}�X�N

    private float ObjectCheckDistance = 1.0f; //���Ƃ̃`�F�b�N����
    [SerializeField] private LayerMask ObjectLayerMask;        // ���̃��C���[�}�X�N


    // �����������iAwake�̓V�[�������[�h�����O�ɌĂ΂��j
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Rigidbody�R���|�[�l���g���擾
        animator = GetComponent<Animator>();   // Animator�R���|�[�l���g���擾

        // Input System�̃C���X�^���X���쐬
        inputActions = new GameInputSystem();

        // Move�̓��͂�o�^
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // �E�N���b�N�̓��͂�o�^
        inputActions.Player.RightClick.performed += ctx => isRightClickHeld = true;
        inputActions.Player.RightClick.canceled += ctx => isRightClickHeld = false;

        //�V�t�g�̓��͂�o�^
        inputActions.Player.ShiftClick.performed += ctx => isShiftClickHeld = true;
        inputActions.Player.ShiftClick.canceled += ctx => isShiftClickHeld = false;
    }

    // ���̓A�N�V������L���ɂ���i�Q�[���J�n���j
    private void OnEnable()
    {
        inputActions.Enable();
    }

    // ���̓A�N�V�����𖳌��ɂ���i�Q�[���I�����j
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // �E�N���b�N�������Ă���Ԃ́A�����ԂɕύX
        if (isRightClickHeld)
        {
            moveSpeed = runSpeed;                         // �ړ����x�𑝉�
            animator.SetBool("Walking", false);       // ���s�A�j���[�V�����𖳌���
            animator.SetBool("Running", true);        // ���s�A�j���[�V������L����
            animator.SetBool("Squatting", false);     // ���Ⴊ�݃A�j���[�V�����𖳌���
            animator.SetBool("CrouchWalking", false); // ���Ⴊ��ŕ����A�j���[�V�����𖳌���
        }
        // �V�t�g�L�[�������Ă���Ԃ́A���Ⴊ��ŕ�����ԂɕύX
        else if (isShiftClickHeld)
        {
            moveSpeed = crouchSpeed;                        // �ړ����x������
            animator.SetBool("Walking", false);      // ���s�A�j���[�V�����𖳌���
            animator.SetBool("Running", false);      // ���s�A�j���[�V�����𖳌���
            animator.SetBool("Squatting", false);    // ���Ⴊ�݃A�j���[�V�����𖳌���
            animator.SetBool("CrouchWalking", true); // ���Ⴊ��ŕ����A�j���[�V������L����
        }
        // ����ȊO�͒ʏ�̕��s���
        else
        {
            moveSpeed = defaultSpeed; // �ʏ�̈ړ����x

            // �ړ����͂�����ꍇ�A���s�A�j���[�V�������Đ�
            if (moveInput.magnitude > 0.1f)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            animator.SetBool("Running", false); // ���s�A�j���[�V�����𖳌���
            animator.SetBool("Squatting", false); // ���Ⴊ�݃A�j���[�V�����𖳌���
            animator.SetBool("CrouchWalking", false); // ���Ⴊ�ݕ��s�A�j���[�V�����𖳌���s
        }

        Move(); // �v���C���[�̈ړ�����
        RotatePlayer(); // �v���C���[�̉�]����
    }

    /// <summary>
    /// �J�����̌����Ɋ�Â��ăv���C���[���ړ�������
    /// </summary>
    private void Move()
    {
        // �J�����I�u�W�F�N�g���ݒ肳��Ă��Ȃ��A�܂��͈ړ����͂��Ȃ��ꍇ�A�ړ����Ȃ�
        if (cameraObject == null || moveInput.magnitude < 0.1f) return;

        // �J�����̑O�����ƉE�������擾���A���ʏ�Ő��K��
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        // �v���C���[�̈ړ��������v�Z�i�J�����̌����Ɋ�Â��j
        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // �ǂ�I�u�W�F�N�g���O���ɂȂ��ꍇ�Ɉړ������s
        if (!IsWallInFront(moveDirection) && !IsObjectInFront(moveDirection))
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// �v���C���[�̌������ړ������ɍ��킹��
    /// </summary>
    private void RotatePlayer()
    {
        // �ړ����͂��Ȃ��ꍇ�A��]���Ȃ�
        if (moveInput.magnitude < 0.1f) return;

        // �J�����̑O�����ƉE�������擾���A���ʏ�Ő��K��
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        // �v���C���[�̈ړ��������v�Z�i�J�����̌����Ɋ�Â��j
        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // �ړ���������ɉ�]���s��
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // �X���[�Y�ɉ�]
    }

    // �ǂ��O���ɂ��邩���`�F�b�N����
    private bool IsWallInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        // �v���C���[����O����Ray���΂��A�ǂ����邩���m�F
        if (Physics.Raycast(transform.position, moveDirection, out hit, wallCheckDistance, wallLayerMask))
        {
            return true; // �ǂ��O���ɂ���
        }
        return false; // �ǂ��Ȃ�
    }

    // �v���C���[�̑O���ɏ�Q�������邩���`�F�b�N����
    private bool IsObjectInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        // �v���C���[����O����Ray���΂��A�I�u�W�F�N�g�����邩���m�F
        if (Physics.Raycast(transform.position, moveDirection, out hit, ObjectCheckDistance, ObjectLayerMask))
        {
            return true; // ��Q�����O���ɂ���
        }
        return false; // ��Q�����Ȃ�
    }
}   