using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 5f;

    public GameObject cameraObject; // �J������Transform
    public float cameraRotationSpeed = 100f; // �J�����̉�]���x

    private GameInputSystem inputActions;
    private Vector2 moveInput;
    private Vector2 moveCameraInput;
    private bool isRightClickHeld;
    private bool isShiftClickHeld;
    private bool isSpaceClickHeld;
    private bool isEClickHeld;

    public bool isWalk;

    private void Awake()
    {
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

        // Input System�̃C���X�^���X���쐬
        inputActions = new GameInputSystem();

        // Move�̓��͂�o�^
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        //// MoveCamera�̓��͂�o�^
        //inputActions.Player.MoveCamera.performed += ctx => moveCameraInput = ctx.ReadValue<Vector2>();
        //inputActions.Player.MoveCamera.canceled += ctx => moveCameraInput = Vector2.zero;

        // �E�N���b�N�̓��͂�o�^
        inputActions.Player.RightClick.performed += ctx => isRightClickHeld = true;
        inputActions.Player.RightClick.canceled += ctx => isRightClickHeld = false;

        //�V�t�g�̓��͂�o�^
        inputActions.Player.ShiftClick.performed += ctx => isShiftClickHeld = true;
        inputActions.Player.ShiftClick.canceled += ctx => isShiftClickHeld = false;

        ////�X�y�[�X�̓��͂�o�^
        //inputActions.Player.SpaceClick.performed += ctx => isSpaceClickHeld = true;
        //inputActions.Player.SpaceClick.canceled += ctx => isSpaceClickHeld = false;

        ////E�L�[�̓��͂�o�^
        //inputActions.Player.EClick.performed += ctx => isEClickHeld = true;
        //inputActions.Player.EClick.canceled += ctx => isEClickHeld = false;

        // �X�y�[�X�̓��͂�o�^ (�����ꂽ�u�Ԃ������O��\��)
        inputActions.Player.SpaceClick.performed += ctx =>
        {
            Debug.Log("���W�I�𗎂Ƃ�����");
        };

        // E�L�[�̓��͂�o�^ (�����ꂽ�u�Ԃ������O��\��)
        inputActions.Player.EClick.performed += ctx =>
        {
            Debug.Log("���W�I���E����");
        };
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
            moveSpeed = 15.0f;
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
            moveSpeed = 5.0f;

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

        //if (isSpaceClickHeld)
        //{
        //    Debug.Log("���W�I�𗎂Ƃ�����");
        //}

        //if (isEClickHeld)
        //{
        //    Debug.Log("���W�I���E����");
        //}

        Move();
        RotatePlayer();
    }

    /// <summary>
    /// �J�����̌����Ɋ�Â����ړ�
    /// </summary>
    private void Move()
    {
        //Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (cameraObject == null || moveInput.magnitude < 0.1f) return;

        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
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
}   