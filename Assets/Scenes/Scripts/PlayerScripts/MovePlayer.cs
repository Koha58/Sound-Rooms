using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 5f;

    private Rigidbody rb;  // Rigidbody��ǉ�

    public GameObject cameraObject; // �J������Transform
    public float cameraRotationSpeed = 100f; // �J�����̉�]���x

    private GameInputSystem inputActions;
    private Vector2 moveInput;
    private bool isRightClickHeld;
    private bool isShiftClickHeld;

    public bool isWalk;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbody���擾

        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

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

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // �E�N���b�N�������Ă���Ԃ݈̂ړ�
        if (isRightClickHeld)
        {
            moveSpeed = 7.5f;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
        }
        else if (isShiftClickHeld)
        {
            moveSpeed = 2.5f;
            animator.SetBool("Walking",false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting",false);
            animator.SetBool("CrouchWalking", true);
        }
        else 
        {
            moveSpeed = 4.0f;

            // �ړ����͂������Walking�A�j���[�V�������Đ�
            if (moveInput.magnitude > 0.1f)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
        }

        Move();
        RotatePlayer();
    }

    /// <summary>
    /// �J�����̌����Ɋ�Â����ړ�
    /// </summary>
    private void Move()
    {
        if (cameraObject == null || moveInput.magnitude < 0.1f) return;

        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // Rigidbody���g���Ĉړ�
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �v���C���[�̌������ړ������ɍ��킹��
    /// </summary>
    private void RotatePlayer()
    {
        if (moveInput.magnitude < 0.1f) return;

        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
           
        }
    }
}   