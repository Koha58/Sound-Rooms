using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Animator animator;

    public float moveSpeed = 5f;

    private Rigidbody rb;  // Rigidbodyを追加

    public GameObject cameraObject; // カメラのTransform
    public float cameraRotationSpeed = 100f; // カメラの回転速度

    private GameInputSystem inputActions;
    private Vector2 moveInput;
    private bool isRightClickHeld;
    private bool isShiftClickHeld;

    public float wallCheckDistance = 1.0f; // 壁とのチェック距離
    public LayerMask wallLayerMask; // 壁のレイヤーマスク

    public float ObjectCheckDistance = 1.0f; // 壁とのチェック距離
    public LayerMask ObjectLayerMask; // 壁のレイヤーマスク

    public bool isWall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyを取得

        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

        // Input Systemのインスタンスを作成
        inputActions = new GameInputSystem();

        // Moveの入力を登録
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // 右クリックの入力を登録
        inputActions.Player.RightClick.performed += ctx => isRightClickHeld = true;
        inputActions.Player.RightClick.canceled += ctx => isRightClickHeld = false;

        //シフトの入力を登録
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
        // 右クリックを押している間のみ移動
        if (isRightClickHeld)
        {
            moveSpeed = 7.0f;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
        }
        else if (isShiftClickHeld)
        {
            moveSpeed = 2.0f;
            animator.SetBool("Walking",false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting",false);
            animator.SetBool("CrouchWalking", true);
        }
        else 
        {
            moveSpeed = 4.0f;

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

            Move();
        RotatePlayer();
    }

    /// <summary>
    /// カメラの向きに基づいた移動
    /// </summary>
    private void Move()
    {
        if (cameraObject == null || moveInput.magnitude < 0.1f) return;

        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // 壁との衝突をチェック
        if (!IsWallInFront(moveDirection)&&!IsObjectInFront(moveDirection))
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
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

    // 壁が前方にあるかをチェックする
    private bool IsWallInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, wallCheckDistance, wallLayerMask))
        {
            return true; // 壁が前方にある
        }
         return false; // 壁がない
    }

    // 壁が前方にあるかをチェックする
    private bool IsObjectInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, ObjectCheckDistance, ObjectLayerMask))
        {
            return true; // 壁が前方にある
        }
        return false; // 壁がない
    }
}   