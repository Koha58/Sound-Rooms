using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;

/// <summary>
/// Playerの移動を管理するクラス
/// </summary>

public class PlayerRun : MonoBehaviour
{
    public int moving = 0; // 移動中かどうかを示すフラグ（0は停止、1は移動中）
    [SerializeField]
    float moveSpeedIn; // プレイヤーの移動速度を設定する変数

    private Animator animator; // プレイヤーのアニメーター（アニメーションを制御）

    Rigidbody playerRb; // プレイヤーのRigidbody（物理演算を行うため）

    Vector3 moveSpeed; // プレイヤーの移動速度（ベクトル形式）

    Vector3 currentPos; // プレイヤーの現在位置

    Vector3 pastPos; // プレイヤーの過去の位置（移動の前後比較用）

    Vector3 delta; // プレイヤーの移動量（位置の変化量）

    Quaternion playerRot; // プレイヤーの進行方向を向くクォータニオン（回転を表現）

    float currentAngularVelocity; // プレイヤーの現在の回転角速度

    [SerializeField]
    float maxAngularVelocity = Mathf.Infinity; // 最大の回転角速度[deg/s]（制限値）

    [SerializeField]
    float smoothTime = 0.1f; // 進行方向にかかるおおよその時間[s]（回転を滑らかにする）

    float diffAngle; // 現在の向きと進行方向の角度差

    float rotAngle; // 現在の回転する角度

    Quaternion nextRot; // 次に回転する角度（補間に使う）

    public static bool walk = false; // 歩いているかどうかを示すフラグ（デフォルトは歩いていない）

    public static bool run = false; // 走っているかどうかを示すフラグ（デフォルトは走っていない）

    public static bool crouch = false; // しゃがんでいるかどうかを示すフラグ（デフォルトはしゃがんでいない）

    public bool crouchWalk = false; // しゃがんで歩いているかどうかを示すフラグ（しゃがみながらの歩行）

    private Rigidbody rb; // プレイヤーのRigidbody（物理演算を行うため）

    // しゃがむとき
    public static bool CrouchOn = false; // しゃがみ状態を管理するフラグ（しゃがみ中ならtrue）



    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを取得して、物理挙動を扱う
        animator = GetComponent<Animator>(); // アニメーターコントローラーを取得してアニメーションを制御
        pastPos = transform.position; // プレイヤーの初期位置を記録

        Application.targetFrameRate = 60; // アプリケーションのターゲットフレームレートを60に設定（滑らかな動き）

        rb = GetComponent<Rigidbody>(); // Rigidbodyコンポーネントを再度取得（無駄な重複ですが、別の用途の可能性あり）
    }

    void Update()
    {
        // Animation管理（プレイヤーのアニメーション状態を更新）
        Animation();

        // 移動メソッドで、プレイヤーに力を加える
        Move();

        // 慣性の管理やしゃがみ状態を処理
        Inertia();

        // プレイヤーの回転処理（進行方向に合わせて回転）
        Rotation();
    }

    /// <summary>
    /// プレイヤーの移動方向に力を加える処理
    /// </summary>
    private void Move()
    {
        // 歩いている場合、通常の移動速度で力を加える
        if (walk == true)
        {
            playerRb.velocity = moveSpeed; // 歩く速度で移動（velocityを設定して力を加える）
        }

        // 走っている場合、速度を2倍にして力を加える
        if (run == true)
        {
            playerRb.velocity = moveSpeed * 2; // 走るときは移動速度を倍にする
        }

        // しゃがみながら歩く場合、移動速度を1/5にして力を加える
        if (crouchWalk == true)
        {
            playerRb.velocity = moveSpeed / 5; // しゃがんでいるときの移動速度を遅くする
        }
    }


    /// <summary>
    /// Animation管理
    /// </summary>
    private void Animation()
    {
        //------プレイヤーの移動------

        //カメラに対して前を取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //カメラに対して右を取得
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        //moveVelocityを0で初期化する
        moveSpeed = Vector3.zero;

        //歩くとき
        if (!crouch && Input.GetKey(KeyCode.W)) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.A)) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.S)) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetKey(KeyCode.D)) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //左前方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }


        //歩くとき(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
            CrouchOn = false;
        }

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            walk = true;
            run = false;
            crouch = false;
            crouchWalk = false;
        }

        //走るとき
        if (!crouch && Input.GetKey(KeyCode.W) && Input.GetMouseButton(1)) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.A) && Input.GetMouseButton(1)) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.S) && Input.GetMouseButton(1)) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetKey(KeyCode.D) && Input.GetMouseButton(1)) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1)) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) && Input.GetMouseButton(1)) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) && Input.GetMouseButton(1)) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) && Input.GetMouseButton(1))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;

        }

        //走るとき(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0 && Input.GetKey("joystick button 5")) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0 && Input.GetKey("joystick button 5")) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0 && Input.GetKey("joystick button 5")) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0 && Input.GetKey("joystick button 5")) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0) && Input.GetKey("joystick button 5")) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5")) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0) && Input.GetKey("joystick button 5")) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }
        if (!crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0) && Input.GetKey("joystick button 5"))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = false;
        }

        //しゃがみ歩き
        if (crouch && Input.GetKey(KeyCode.W)) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.A)) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.S)) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetKey(KeyCode.D)) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            walk = false;
            run = false;
            crouchWalk = true;
        }


        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;

        }


        //しゃがみ歩き(Xbox)
        if (crouch && Input.GetAxisRaw("Vertical") < 0) //前方向に移動
        {
            moveSpeed = moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") < 0) //左方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = true;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Vertical") > 0) //後方向に移動
        {
            moveSpeed = -moveSpeedIn * cameraForward;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") > 0) //右方向に移動
        {
            moveSpeed = moveSpeedIn * cameraRight;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }

        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)) //右後ろ方向に移動
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)) //右前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)) //左前方向に移動
        {
            moveSpeed = (moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
        if (CrouchOn == true && !crouch && (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0))
        {
            moveSpeed = (-moveSpeedIn * cameraForward) + (-moveSpeedIn * cameraRight).normalized;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", true);
            moving = 1;
            run = false;
            walk = false;
            crouch = false;
            crouchWalk = true;
        }
    }

    /// <summary>
    /// 慣性、しゃがんだ際の動きの調整
    /// </summary>
    private void Inertia()
    {
        //慣性を消す
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)
            || Input.GetKeyUp("left shift") || Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", false);
            animator.SetBool("CrouchWalking", false);
            moving = 0;
            walk = false;
            run = false;
            crouch = false;
            crouchWalk = false;
        }

        //しゃがむとき
        if (Input.GetKey("left shift"))
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", true);
            moving = 0;
            walk = false;
            run = false;
            crouch = true;

            CrouchOn = true;
        }
        //しゃがむとき(Xbox)
        if (Input.GetKey("joystick button 4"))//LB
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Squatting", true);
            moving = 0;
            walk = false;
            run = false;
            crouch = true;

            CrouchOn = true;
        }
    }

    /// <summary>
    /// Playerの回転
    /// </summary>
    private void Rotation()
    {
        //------プレイヤーの回転------

        //現在の位置
        currentPos = transform.position;

        //移動量計算
        delta = currentPos - pastPos;
        delta.y = 0;

        //過去の位置の更新
        pastPos = currentPos;

        if (delta == Vector3.zero)
            return;

        playerRot = Quaternion.LookRotation(delta, Vector3.up);

        diffAngle = Vector3.Angle(transform.forward, delta);

        //Vector3.SmoothDampはVector3型の値を徐々に変化させる
        //Vector3.SmoothDamp (現在地, 目的地, ref 現在の速度, 遷移時間, 最高速度);
        rotAngle = Mathf.SmoothDampAngle(0, diffAngle, ref currentAngularVelocity, smoothTime, maxAngularVelocity);

        nextRot = Quaternion.RotateTowards(transform.rotation, playerRot, rotAngle);

        transform.rotation = nextRot;
    }
    
}
