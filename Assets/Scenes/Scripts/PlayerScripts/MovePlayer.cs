using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InputSystemを使ったプレイヤーの移動管理クラス
/// </summary>
public class MovePlayer : MonoBehaviour
{
    // アニメーターの参照 (アニメーション制御用)
    [SerializeField] private Animator animator;

    // プレイヤーの移動速度
    private float moveSpeed;
    private const float DEFAULT_SPEED = 4.0f;  // 歩くとき
    private const float RUN_SPEED = 7.0f;      // 走るとき
    private const float CROUCH_SPEED = 2.0f;   // しゃがみ歩きするとき

    // 回転速度
    private const float ROTATION_SPEED = 10.0f;

    // 移動入力を受け付ける最小入力値
    private const float MAGNITUDE_CHECK = 0.1f;

    // Rigidbodyコンポーネントを格納する変数
    private Rigidbody rb;

    // カメラ設定
    public GameObject cameraObject;            // プレイヤーが向いているカメラのTransform
    public float cameraRotationSpeed = 100f;   // カメラ回転速度

    // 入力管理システム
    private GameInputSystem inputActions;      // 入力管理システム
    private Vector2 moveInput;                 // 移動の入力（横と縦の軸）
    private bool isRightClickHeld;             // 右クリックが押されているかのフラグ
    private bool isShiftClickHeld;             // シフトキーが押されているかのフラグ

    // 当たり判定用設定
    private const float WALL_CHECK_DISTANCE = 1.0f;  // 壁とのチェック距離
    [SerializeField] private LayerMask wallLayerMask;  // 壁のレイヤーマスク

    private const float OBJECT_CHECK_DISTANCE = 1.0f; // 物とのチェック距離
    [SerializeField] private LayerMask objectLayerMask; // 物のレイヤーマスク


    // 初期化処理（Awakeはシーンがロードされる前に呼ばれる）
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        // Rigidbodyコンポーネントを取得
        animator = GetComponent<Animator>();   // Animatorコンポーネントを取得

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

    // 入力アクションを有効にする（ゲーム開始時）
    private void OnEnable()
    {
        inputActions.Enable();
    }

    // 入力アクションを無効にする（ゲーム終了時）
    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // 右クリックを押している間は、走る状態に変更
        if (isRightClickHeld)
        {
            moveSpeed = RUN_SPEED;                         // 移動速度を増加
            animator.SetBool("Walking", false);       // 歩行アニメーションを無効化
            animator.SetBool("Running", true);        // 走行アニメーションを有効化
            animator.SetBool("Squatting", false);     // しゃがみアニメーションを無効化
            animator.SetBool("CrouchWalking", false); // しゃがんで歩くアニメーションを無効化
        }
        // シフトキーを押している間は、しゃがんで歩く状態に変更
        else if (isShiftClickHeld)
        {
            moveSpeed = CROUCH_SPEED;                        // 移動速度を減少
            animator.SetBool("Walking", false);      // 歩行アニメーションを無効化
            animator.SetBool("Running", false);      // 走行アニメーションを無効化
            animator.SetBool("Squatting", false);    // しゃがみアニメーションを無効化
            animator.SetBool("CrouchWalking", true); // しゃがんで歩くアニメーションを有効化
        }
        // それ以外は通常の歩行状態
        else
        {
            moveSpeed = DEFAULT_SPEED; // 通常の移動速度

            // 移動入力がある場合、歩行アニメーションを再生
            if (moveInput.magnitude > MAGNITUDE_CHECK)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            animator.SetBool("Running", false); // 走行アニメーションを無効化
            animator.SetBool("Squatting", false); // しゃがみアニメーションを無効化
            animator.SetBool("CrouchWalking", false); // しゃがみ歩行アニメーションを無効化s
        }

        Move(); // プレイヤーの移動処理
        RotatePlayer(); // プレイヤーの回転処理
    }

    /// <summary>
    /// カメラの向きに基づいてプレイヤーを移動させる
    /// </summary>
    private void Move()
    {
        // カメラオブジェクトが設定されていない、または移動入力がない場合、移動しない
        if (cameraObject == null || moveInput.magnitude < MAGNITUDE_CHECK) return;

        // カメラの前方向と右方向を取得し、平面上で正規化
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        // プレイヤーの移動方向を計算（カメラの向きに基づく）
        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // 壁やオブジェクトが前方にない場合に移動を実行
        if (!IsWallInFront(moveDirection) && !IsObjectInFront(moveDirection))
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// プレイヤーの向きを移動方向に合わせる
    /// </summary>
    private void RotatePlayer()
    {
        // 移動入力がない場合、回転しない
        if (moveInput.magnitude < 0.1f) return;

        // カメラの前方向と右方向を取得し、平面上で正規化
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(cameraObject.transform.right, new Vector3(1, 0, 1)).normalized;

        // プレイヤーの移動方向を計算（カメラの向きに基づく）
        Vector3 moveDirection = (cameraRight * moveInput.x + cameraForward * moveInput.y).normalized;

        // 移動方向を基に回転を行う
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * ROTATION_SPEED); // スムーズに回転
    }

    // 壁が前方にあるかをチェックする
    private bool IsWallInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        // プレイヤーから前方にRayを飛ばし、壁があるかを確認
        if (Physics.Raycast(transform.position, moveDirection, out hit, WALL_CHECK_DISTANCE, wallLayerMask))
        {
            return true; // 壁が前方にある
        }
        return false; // 壁がない
    }

    // プレイヤーの前方に障害物があるかをチェックする
    private bool IsObjectInFront(Vector3 moveDirection)
    {
        RaycastHit hit;
        // プレイヤーから前方にRayを飛ばし、オブジェクトがあるかを確認
        if (Physics.Raycast(transform.position, moveDirection, out hit, OBJECT_CHECK_DISTANCE, objectLayerMask))
        {
            return true; // 障害物が前方にある
        }
        return false; // 障害物がない
    }
}   