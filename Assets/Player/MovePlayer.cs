using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Camera Settings")]
    public Transform cameraTransform; // �J������Transform
    public float cameraRotationSpeed = 100f; // �J�����̉�]���x

    private GameInputSystem inputActions;
    private Vector2 moveInput;
    private Vector2 moveCameraInput;
    private bool isRightClickHeld;
    private bool isShiftClickHeld;
    private bool isSpaceClickHeld;
    private bool isEClickHeld;

    private void Awake()
    {
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
        }
        else if (isShiftClickHeld)
        {
            moveSpeed = 2.5f;
        }
        else
        {
            moveSpeed = 5.0f;
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

        //RotateCamera();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void RotateCamera()
    {
        if (cameraTransform == null) return;

        // �J������]�̓��� (�E�X�e�B�b�N)
        float rotationX = moveCameraInput.x * cameraRotationSpeed * Time.deltaTime;
        float rotationY = moveCameraInput.y * cameraRotationSpeed * Time.deltaTime;

        // �v���C���[�𒆐S�ɃJ�����𐅕���]
        cameraTransform.RotateAround(transform.position, Vector3.up, rotationX);


    }
}