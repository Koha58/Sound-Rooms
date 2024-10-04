using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossTutoriaru : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    public float MoveSpeed =1.0f;                      // 動く速度
    private float NextTime;                            //次のポイントに向かうまでの時間
    private int CurrentPointIndex = 0;                 // 現在の巡回ポイントのインデックス
    private bool Front;                                //ポイントにたどり着いたときに返す

    //可視化
    public float ONOFF = 0;    //(0が見えない；１が見える状態）
    private float ONTime;
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;//3DモデルのRendererのONOFF

    //サウンド
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //前後判定
    public Transform TargetPlayer;　//プレイヤーの位置を取得

    //Playerを追跡
    public float ChaseSpeed = 0.07f;　　//Playerを追いかけるスピード
    [SerializeField] bool ChaseONOFF;　//(ChaseON： true/ChaseOFF: false)

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;　//プレイヤーオブジェクト取得

    public GameObject VisualizationBoss;   //ボスの可視化の音(球体)
    public SphereCollider SphereCollider; //可視化時の音を小さくする

    public GameObject gravity;
    public GameObject Blur;

    private　void MoveBossEnemy()
    {
        if (ChaseONOFF == false)
        {
            if (Front == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

                if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
                {
                    if (ONOFF == 0)
                    {
                        animator.SetBool("Idle", false);
                        animator.SetBool("Move", true);
                    }
                    else
                    {
                        animator.SetBool("Idle", true);
                        animator.SetBool("Move", false);
                    }
                    Front = true;
                }
            }
            else
            {
                if (ONOFF == 0)
                {
                    animator.SetBool("Idle", false);
                    animator.SetBool("Move", true);
                }
                else
                {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Move", false);
                }
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
    }

    private void Chase()//プレイヤーを追いかける
    {
        GameObject gobj = GameObject.Find("Player");        //Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>();    //付いているスクリプトを取得

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (TouchWall == false)
        {
            if (ChaseONOFF == true)//プレイヤーが検知範囲に入ったら
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);

                ChaseONOFF = true;                                    //追跡中
                transform.LookAt(TargetPlayer.transform);             //プレイヤーの方向にむく
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
        if (CurrentPointIndex >= PatrolPoints.Length) {CurrentPointIndex = 0;}
    }

    private void Visualization()//自身の可視化のON OFF
    {
        if (ONOFF == 0)//見えないとき
        {
            gravity.SetActive(false);
            Blur.SetActive(false);
            PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
            audioSourse.maxDistance = 5;                      //音が聞こえる範囲
        }
        else if (ONOFF == 1)//見えているとき
        {
            GameObject gobj = GameObject.Find("Player");     //Playerオブジェクトを探す
            PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

            gravity.SetActive(true);
            Blur.SetActive(true);
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            VisualizationBoss.SetActive(true);              //可視化の音(円)を見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3DモデルのRendererを見える状態
            audioSourse.maxDistance = 300;                 //音が聞こえる範囲
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;                                        //見えない状態
        VisualizationBoss.SetActive(false);               //可視化の音(円)を見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
        ChaseONOFF = false;                               //追跡中じゃない
        animator = GetComponent<Animator>();              //アニメーターコントローラーからアニメーションを取得する
        audioSourse = GetComponent<AudioSource>();　     //オーディオソースを取得
    }

    // Update is called once per frame
    private void Update()
    {
        Visualization();
        if (EnemyAttack.OFF == true)
        {
            ONTime += Time.deltaTime;
            if (ONTime >= 30.0f)
            {
                GameObject gobj = GameObject.Find("Player");        //Playerオブジェクトを探す
                PlayerSeen PS = gobj.GetComponent<PlayerSeen>();    //付いているスクリプトを取得
               
                PS.Visualization = false;
                PS.onoff = 0;

                ONTime = 0;
                VisualizationBoss.SetActive(false);              //可視化の音(円)を見えない状態
                ONOFF = 0;                                       //見えない
                PrototypeBodySkinnedMeshRenderer.enabled = false;//3DモデルのRendererを見える状態
                EnemyAttack.OFF = false;
            }
        }

        if (EnemyAttack.SoundON == true)
        {
            Front = true;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            VisualizationBoss.SetActive(true);
        }
        else if (EnemyAttack.SoundON == false)
        {
            ONOFF = 0;
            PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
            MoveBossEnemy();
            VisualizationBoss.SetActive(false);
        }

        Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

        if (isFront) //ターゲットが自身の前方にあるなら
        {
            Chase();
            float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
            if (ONOFF == 0) { ChaseONOFF = false;}
            if (ONOFF == 1 && ChasePlayer <= 5) { ChaseONOFF = true; }
            DestroyONOFF = false;
        }
        else if (isBack){DestroyONOFF = true;}
    }
    void Idle() { audioSourse.PlayOneShot(BossIdle); }

    void Move() { audioSourse.PlayOneShot(BossMove); }

    private void OnTriggerStay(Collider other)
    {
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (other.gameObject.tag == "LeftWall")
        {
            if (ChaseONOFF == false)
            {
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
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }

        if (other.CompareTag("RoomOut"))
        {
            TouchWall = true;
            NextPatrolPoint();
        }
    }
}
