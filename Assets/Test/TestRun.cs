using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    //移動用の変数
    float x, z;

    //歩くスピード調整用の変数
    float Walkspeed = 1f/100;
    //走るスピード調整用の変数
    float Runspeed = 2f / 50;

    Animator Animator;

    // 最大の回転角速度[deg/s]
    [SerializeField] private float _maxAngularSpeed = Mathf.Infinity;

    // 進行方向に向くのにかかるおおよその時間[s]
    [SerializeField] private float _smoothTime = 0.1f;

    private Transform _transform;

    // 前フレームのワールド位置
    private Vector3 _prevPosition;

    bool Move;

    private Transform CamPos;
    private Vector3 Camforward;
    private Vector3 ido;
    private Vector3 Animdir = Vector3.zero;

    float runspeed = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

        _transform = transform;

        _prevPosition = _transform.position;

        if (Camera.main != null)
        {
            CamPos = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Idle();
        Controller();
        if (Move == true)
        {
            Rotation();
            PlayerWalk();
            if (Input.GetKey("joystick button 5")) { PlayerRun(); }
        }
    }


    private void Idle()
    {
        if(Input.GetAxisRaw("Horizontal")==0&&Input.GetAxisRaw("Vertical")==0)
        {
            Animator.SetBool("Walking", false);
            Animator.SetBool("Running", false);
            Animator.SetBool("Squatting", false);
            Animator.SetBool("CrouchWalking", false);
            Move = false;
        }
        else {Move = true;}
    }

    private void PlayerWalk()
    {
        x = Input.GetAxisRaw("Horizontal") * Walkspeed;
        z = Input.GetAxisRaw("Vertical") * Walkspeed;
        Animator.SetBool("Walking", true);
        Animator.SetBool("Running", false);
        transform.position += new Vector3(x*-1 , 0, z);
    }

    private void PlayerRun()
    {
        x = Input.GetAxisRaw("Horizontal") * Runspeed;
        z = Input.GetAxisRaw("Vertical") * Runspeed;
        Animator.SetBool("Walking", false);
        Animator.SetBool("Running", true);

        transform.position += new Vector3(x*-1, 0, z);
    }

    private void Rotation()
    {
        // 現在フレームのワールド位置
        var position = _transform.position;

        // 移動量を計算
        var delta = position - _prevPosition;

        // 次のUpdateで使うための前フレーム位置更新
        _prevPosition = position;

        // 静止している状態だと、進行方向を特定できないため回転しない
        if (delta == Vector3.zero)
            return;

        // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        // オブジェクトの回転に反映
        _transform.rotation = rotation;
    }

    private void Controller()
    {
        //キーボード数値取得。プレイヤーの方向として扱う
        float h = Input.GetAxis("Horizontal");//横
        float v = Input.GetAxis("Vertical");//縦

        //カメラのTransformが取得されてれば実行
        if (CamPos != null)
        {
            //2つのベクトルの各成分の乗算(Vector3.Scale)。単位ベクトル化(.normalized)
            Camforward = Vector3.Scale(CamPos.forward, new Vector3(1, 0, 1)).normalized;
            //移動ベクトルをidoというトランスフォームに代入
            ido = (v*-1) * Camforward * runspeed + h * CamPos.right * runspeed;
            //Debug.Log(ido);
        }

        //現在のポジションにidoのトランスフォームの数値を入れる
        transform.position = new Vector3(
        transform.position.x + ido.x,
        0,
        transform.position.z + ido.z);

        //方向転換用Transform

        Vector3 AnimDir = ido;
        AnimDir.y = 0;
        //方向転換
        if (AnimDir.sqrMagnitude > 0.001)
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, AnimDir, 5f * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
