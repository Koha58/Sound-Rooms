using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;

//プレイヤーの移動

public class PlayerRun : MonoBehaviour
{ 
    public int moving = 0;
    [SerializeField]
    float moveSpeedIn;//プレイヤーの移動速度を入力
    private Animator animator;

    Rigidbody playerRb;//プレイヤーのRigidbody

    Vector3 moveSpeed;//プレイヤーの移動速度

    Vector3 currentPos;//プレイヤーの現在の位置
    Vector3 pastPos;//プレイヤーの過去の位置

    Vector3 delta;//プレイヤーの移動量

    Quaternion playerRot;//プレイヤーの進行方向を向くクォータニオン

    float currentAngularVelocity;//現在の回転各速度

    [SerializeField]
    float maxAngularVelocity = Mathf.Infinity;//最大の回転角速度[deg/s]

    [SerializeField]
    float smoothTime = 0.1f;//進行方向にかかるおおよその時間[s]

    float diffAngle;//現在の向きと進行方向の角度

    float rotAngle;//現在の回転する角度

    Quaternion nextRot;//どのくらい回転するか

    private bool walk = false;
    private bool run = false;
    public bool crouch = false;
    public bool crouchWalk = false;

    private Rigidbody rb;

    public float count1;
    public float count2;
    public float count3;
    public float count4;
    public float count5;
    public float count6;
    public float count7;
    public float count8;
    public float count9;
    public float count10;
    public float count11;
    public float count12;
    public int cond;

    //しゃがむとき
   public static bool CrouchOn=false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
        pastPos = transform.position;

        Application.targetFrameRate = 60;

        rb = GetComponent<Rigidbody>();

        count1 = 0;
        count2 = 0;
        count3 = 0;
        count4 = 0;
        count5 = 0;
        count6 = 0;
        count7 = 0;
        count8 = 0;
        count9 = 0;
        count10 = 0;
        count11 = 0;
        count12 = 0;
        cond = 0;
    }

    void Update()
    {
        //------プレイヤーの移動------

        //カメラに対して前を取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        //カメラに対して右を取得
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        //moveVelocityを0で初期化する
        moveSpeed = Vector3.zero;

        //歩くとき
        if (!crouch && Input.GetKey(KeyCode.W))
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

        if (!crouch && Input.GetKey(KeyCode.A))
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

        if (!crouch && Input.GetKey(KeyCode.S))
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

        if (!crouch && Input.GetKey(KeyCode.D))
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

        //歩くとき(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0)
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

            cond = 1;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0)
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

            cond = 2;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0)
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

            cond = 3;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0)
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

            cond = 4;
        }

        //走るとき
        if (!crouch && Input.GetKey(KeyCode.W) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.A) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.S) && Input.GetMouseButton(1))
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

        if (!crouch && Input.GetKey(KeyCode.D) && Input.GetMouseButton(1))
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
        
        //走るとき(Xbox)
        if (!crouch && Input.GetAxisRaw("Vertical") < 0 && Input.GetKey("joystick button 5"))
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

            cond = 5;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") < 0 && Input.GetKey("joystick button 5"))
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

            cond = 6;
        }

        if (!crouch && Input.GetAxisRaw("Vertical") > 0 && Input.GetKey("joystick button 5"))
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

            cond = 7;
        }

        if (!crouch && Input.GetAxisRaw("Horizontal") > 0 && Input.GetKey("joystick button 5"))
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

            cond = 8;
        }

        //しゃがみ歩き
        if (crouch && Input.GetKey(KeyCode.W))
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

        if (crouch && Input.GetKey(KeyCode.A))
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

        if (crouch && Input.GetKey(KeyCode.S))
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

        if (crouch && Input.GetKey(KeyCode.D))
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

        //しゃがみ歩き(Xbox)
        if (crouch && Input.GetAxisRaw("Vertical") < 0)
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

            cond = 9;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") < 0)
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

            cond = 10;
        }

        if (crouch && Input.GetAxisRaw("Vertical") > 0)
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

            cond = 11;
        }

        if (crouch && Input.GetAxisRaw("Horizontal") > 0)
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

            cond = 12;
        }

        //Moveメソッドで、力加えてもらう
        Move();

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

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            count1 = 0f;
            count2 = 0f;
            count3 = 0f;
            count4 = 0f;
            count5 = 0f;
            count6 = 0f;
            count7 = 0f;
            count8 = 0f;
            count9 = 0f;
            count10 = 0f;
            count11 = 0f;
            count12 = 0f;
            cond = 0;
        }

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

    /// <summary>
    /// 移動方向に力を加える
    /// </summary>
    private void Move()
    {
        //playerRb.AddForce(moveSpeed, ForceMode.Force);
        if (walk == true)
        {
            playerRb.velocity = moveSpeed;
        }

        if(run == true)
        {
            playerRb.velocity = moveSpeed*2;
        }

        if(crouchWalk == true)
        {
            playerRb.velocity = moveSpeed/5;
        }

        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            if (cond == 1)
            {
                count1 += Time.deltaTime;
                count2 = 0;
                count3 = 0f;
                count4 = 0f;
                count5 = 0;
                count6 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 2)
            {
                count2 += Time.deltaTime;
                count1 = 0;
                count3 = 0f;
                count4 = 0f;
                count5 = 0;
                count6 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 3)
            {
                count3 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count4 = 0f;
                count5 = 0;
                count6 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 4)
            {
                count4 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count5 = 0;
                count6 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 5)
            {
                count5 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count6 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 6)
            {
                count6 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count7 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 7)
            {
                count7 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count8 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 8)
            {
                count8 += Time.deltaTime;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count7 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 9)
            {
                count8 = 0;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count7 = 0f;
                count9 += Time.deltaTime;
                count10 = 0f;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 10)
            {
                count8 = 0;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count7 = 0f;
                count9 = 0;
                count10 += Time.deltaTime; ;
                count11 = 0f;
                count12 = 0f;
            }
            else if (cond == 11)
            {
                count8 = 0;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count7 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 += Time.deltaTime; ;
                count12 = 0f;
            }
            else if (cond == 12)
            {
                count8 = 0;
                count1 = 0;
                count2 = 0f;
                count3 = 0f;
                count4 = 0;
                count5 = 0f;
                count6 = 0f;
                count7 = 0f;
                count9 = 0;
                count10 = 0f;
                count11 = 0f;
                count12 += Time.deltaTime; ;
            }

            if (count1 < 0.5f && count1 != 0 || count2 < 0.5f && count2 != 0 || count3 < 0.5f && count3 != 0 || count4 < 0.5f && count4 != 0)
            {
                playerRb.velocity = moveSpeed * 1.5f;
            }
            else if (count5 < 0.5f && count5 != 0 || count6 < 0.5f && count6 != 0 || count7 < 0.5f && count7 != 0 || count8 < 0.5f && count8 != 0)
            {
                playerRb.velocity = moveSpeed * 2.5f;
            }
            else if (count9 < 0.5f && count9 != 0 || count10 < 0.5f && count10 != 0 || count11 < 0.5f && count11 != 0 || count12 < 0.5f && count12 != 0)
            {
                playerRb.velocity = moveSpeed * 1.2f;
            }
        }

    }

}
