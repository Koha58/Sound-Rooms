using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BoosEnemy : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    public float MoveSpeed = 15.0f; // 動く速度
    private int CurrentPointIndex = 0;// 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //前後判定
    public Transform TargetPlayer;

    //Playerを追跡
    public float ChaseSpeed = 2.0f;//Playerを追いかけるスピード
    [SerializeField] bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;
    private bool UpON = false;
    private float NextTime;
    private bool Front;

    [SerializeField] Quaternion rotation;

    public GameObject VisualizationBoss;

    public SphereCollider SphereCollider;

    float Count;

    private void Chase()//プレイヤーを追いかける
    {
        GameObject gobj = GameObject.Find("Player");//Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (TouchWall == false)
        {
            if (ChaseONOFF==true)//プレイヤーが検知範囲に入ったら
            {
                // Run();
                //ONOFF = 1;//自分自身を可視化
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
                ChaseONOFF = true;//追跡中
                transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed; //プレイヤーの方向に向かう

                Transform myTransform = this.transform;
                Vector3 localAngle = myTransform.localEulerAngles;

                localAngle.x = 0f;
                localAngle.z = 0f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
            }
            else { ChaseONOFF = false; }//追跡中じゃない
        }
    }

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        //巡回ポイントが最後まで行ったら最初に戻る
        if (CurrentPointIndex >= PatrolPoints.Length) { CurrentPointIndex = 0;}
    }

    private void Visualization()//自身の可視化のON OFF
    {

        if (ONOFF == 0)//見えないとき
        {
            //3DモデルのRendererを見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            //ONOFF = 1;//見える
            VisualizationBoss.SetActive(false);
        }
        else if (ONOFF == 1)//見えているとき
        {
            //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            ONTime += Time.deltaTime;
            if (ONTime>=10.0f)
            {
                //3DモデルのRendererを見える状態
                PrototypeBodySkinnedMeshRenderer.enabled =false;
                ONOFF = 0;//見えない
                VisualizationBoss.SetActive(false);
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        Chase();
        Ray ray;
        RaycastHit hit;
        Vector3 direction;   // Rayを飛ばす方向
        float distance = 15.0f;    // Rayを飛ばす距離

        // Rayを飛ばす方向を計算
        Vector3 temp = Player.transform.position - transform.position;
        direction = temp.normalized;

        ray = new Ray(transform.position, direction);  // Rayを飛ばす
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Rayをシーン上に描画

        // Rayが最初に当たった物体を調べる
        if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
        {
            /*
            if (hit.collider.CompareTag("Player"))
            {
                if (ONOFF == 1)
                {
                    PS.onoff = 1;  //見えているから1
                    PS.Visualization = true;
                    ONOFF = 1;
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }*/
        }
        else
        {
            OFFTime += Time.deltaTime;
            if (OFFTime >= 15.0f)
            {
                ONOFF = 0;
                OFFTime = 0;
                PS.Visualization = false;
                PS.onoff = 0;  //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えなくする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    private void Chase2()
    {
        GameObject gobj = GameObject.Find("Player");//Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (ChasePlayer <= 15)
        {
            transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
            GameObject soundobj = GameObject.Find("SoundVolume");
            LevelMeter levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
            if (levelMeter.nowdB > 0.0f)
            {
                transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
                transform.position += transform.forward * (MoveSpeed * 0.09f); //プレイヤーの方向に向かう
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//見えない状態
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3DモデルのRendererを見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        ChaseONOFF = false;//追跡中じゃない

        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
    }

    // Update is called once per frame
    private void Update()
    {
        if(PlayerRun.CrouchOn==false) {  VisualizationBoss.SetActive(true);}
        else{VisualizationBoss.SetActive(false);}

        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (Player <= 3.5f)
        {
            if (ONOFF == 1)
            {
                GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
                PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
                animator.SetBool("Idle", true);
                animator.SetBool("Move", false);
                PS.Visualization = true;
                PS.onoff = 1;  //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }

                UpON = true;
                Count += Time.deltaTime;
                if (Count <= 10.0f)
                {
                    TouchWall = true;
                    animator.SetBool("Run", true);
                    CurrentPointIndex--;
                    //巡回ポイントが最後まで行ったら最初に戻る
                    if (CurrentPointIndex <= PatrolPoints.Length) { CurrentPointIndex = 0; }
                }
                else
                {
                    Count = 0;
                }
            }
        }
        else if (Player >= 4f){UpON = false;}

        if (UpON == false)
        {
            Chase2();

            if (ChaseONOFF == false)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
            }

            Visualization();

            if (ChaseONOFF == false)
            {
                if (Front == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                    transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

                    if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
                    {
                        animator.SetBool("Move", true);
                        Front = true;
                    }
                }
                else
                {
                    animator.SetBool("Move", true);
                    NextTime += Time.deltaTime;
                    if (NextTime >= 5.0f)
                    {
                        NextPatrolPoint();
                        NextTime = 0;
                        Front = false;
                        TouchWall = false;
                    }
                }
            }

            Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

            if (isFront) //ターゲットが自身の前方にあるなら
            {
                if (ONOFF == 0) {ChaseONOFF = false;}
                DestroyONOFF =false;
                if (Front == true) { if (PlayerRun.CrouchOn == false){Ray();}}
            }
            else if (isBack)// ターゲットが自身の後方にあるなら
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

                //プレイヤーが検知範囲に入ったら
                if (detectionPlayer <= 7f){DestroyONOFF = true;}
            }
        }
    }
    void Idle(){audioSourse.PlayOneShot(BossIdle);}

    void Move(){audioSourse.PlayOneShot(BossMove);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
           //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            ChaseONOFF = true;
        }

        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (other.gameObject.tag == "LeftWall")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("1");
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(10f, 0, 0);
            }
        }
        else if (other.gameObject.tag == "RightWall")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("2");
                localAngle.x = 0f;
                localAngle.z = 90f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                //Physics.gravity = new Vector3(-10f, 0, 0);
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("3");
                localAngle.x = 0f;
                localAngle.y = 0f;
                localAngle.z = 180f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, 10f, 0);
            }
        }
        else if (other.gameObject.tag == "Floor")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("4");
                localAngle.x = 0f;
                localAngle.z = 0f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }
        else if (other.gameObject.tag == "RW2")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("5");
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }
        else if (other.gameObject.tag == "LW2")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("6");
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }

        if (other.CompareTag("RoomIN"))
        {
            TouchWall = true;
            animator.SetBool("Run", true);
            CurrentPointIndex--;
            //巡回ポイントが最後まで行ったら最初に戻る
            if (CurrentPointIndex <= PatrolPoints.Length) { CurrentPointIndex = 0; }
        }
    }
}
