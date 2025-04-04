using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;      

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Camera Settings")]
    public Transform cameraTransform; // カメラのTransform
    public float cameraRotationSpeed = 100f; // カメラの回転速度

    private GameInputSystem inputActions;
    private Vector2 moveInput;
    private Vector2 moveCameraInput;
    private bool isRightClickHeld;
    private bool isShiftClickHeld;
    private bool isSpaceClickHeld;
    private bool isEClickHeld;

    private void Awake()
    {
        // Input Systemのインスタンスを作成
        inputActions = new GameInputSystem();

        // Moveの入力を登録
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        //// MoveCameraの入力を登録
        //inputActions.Player.MoveCamera.performed += ctx => moveCameraInput = ctx.ReadValue<Vector2>();
        //inputActions.Player.MoveCamera.canceled += ctx => moveCameraInput = Vector2.zero;

        // 右クリックの入力を登録
        inputActions.Player.RightClick.performed += ctx => isRightClickHeld = true;
        inputActions.Player.RightClick.canceled += ctx => isRightClickHeld = false;

        //シフトの入力を登録
        inputActions.Player.ShiftClick.performed += ctx => isShiftClickHeld = true;
        inputActions.Player.ShiftClick.canceled += ctx => isShiftClickHeld = false;

        ////スペースの入力を登録
        //inputActions.Player.SpaceClick.performed += ctx => isSpaceClickHeld = true;
        //inputActions.Player.SpaceClick.canceled += ctx => isSpaceClickHeld = false;

        ////Eキーの入力を登録
        //inputActions.Player.EClick.performed += ctx => isEClickHeld = true;
        //inputActions.Player.EClick.canceled += ctx => isEClickHeld = false;

        // スペースの入力を登録 (押された瞬間だけログを表示)
        inputActions.Player.SpaceClick.performed += ctx =>
        {
            Debug.Log("ラジオを落としたよ");
        };

        // Eキーの入力を登録 (押された瞬間だけログを表示)
        inputActions.Player.EClick.performed += ctx =>
        {
            Debug.Log("ラジオを拾うよ");
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
        // 右クリックを押している間のみ移動
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
        //    Debug.Log("ラジオを落としたよ");
        //}

        //if (isEClickHeld)
        //{
        //    Debug.Log("ラジオを拾うよ");
        //}

        Move();

        //RotateCamera();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    //private void RotateCamera()
    //{
    //    if (cameraTransform == null) return;

    //    // カメラ回転の入力 (右スティック)
    //    float rotationX = moveCameraInput.x * cameraRotationSpeed * Time.deltaTime;
    //    float rotationY = moveCameraInput.y * cameraRotationSpeed * Time.deltaTime;

    //    // プレイヤーを中心にカメラを水平回転
    //    cameraTransform.RotateAround(transform.position, Vector3.up, rotationX);

     
    //}
}
