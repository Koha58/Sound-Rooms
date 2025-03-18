using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 5f;

    public GameObject cameraObject; // カメラのTransform
    public float cameraRotationSpeed = 100f; // カメラの回転速度

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
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

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

            // 移動入力があればWalkingアニメーションを再生
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
        //    Debug.Log("ラジオを落としたよ");
        //}

        //if (isEClickHeld)
        //{
        //    Debug.Log("ラジオを拾うよ");
        //}

        Move();
        RotatePlayer();
    }

    /// <summary>
    /// カメラの向きに基づいた移動
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
    /// プレイヤーの向きを移動方向に合わせる
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