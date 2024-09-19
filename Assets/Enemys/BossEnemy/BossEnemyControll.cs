using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossEnemyControll : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    public float MoveSpeed = 15.0f;                    // 動く速度
    private int CurrentPointIndex = 0;                 // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0; //(0が見えない；１が見える状態）
    private float OFFTime;  //(見えない状態になるまでの時間)
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer; //3DモデルのRendererのONOFF

    //サウンド
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //前後判定
    public Transform TargetPlayer;//プレイヤーの位置を取得

    //Playerを追跡
    public float ChaseSpeed = 2f;      //Playerを追いかけるスピード
    [SerializeField] bool ChaseONOFF;　//(ChaseON:true/ChaseOFF:false)

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //アニメーション
    [SerializeField] Animator animator;

    private float NextTime;　　//そのポイントの待機時間
    private bool Front;        //ポイントについたかどうか

    public GameObject VisualizationBoss;   //ボスの可視化の音(球体)
    public SphereCollider SphereCollider; //可視化時の音を小さくする

    private void Chase()//プレイヤーを追いかける
    {
        GameObject gobj = GameObject.Find("Player");     //Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (ChaseONOFF == true)                     //プレイヤーが検知範囲に入ったら
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

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length){CurrentPointIndex = 0;}//巡回ポイントが最後まで行ったら最初に戻る
    }

    private void MoveBossEnemy()
    {
        if (ChaseONOFF == false)
        {
            if (Front == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

                if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
                {
                    Front = true;
                }
            }
            else
            {
                NextTime += Time.deltaTime;
                if (NextTime >= 5.0f)
                {
                    NextPatrolPoint();
                    NextTime = 0;
                    Front = false;
                }
            }

        }
    }

    private void Visualization()//自身の可視化のON OFF
    {
        if (ONOFF == 0)//見えないとき
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Move", true);
            VisualizationBoss.SetActive(false);              //可視化の音(円)を見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;//3DモデルのRendererを見えない状態
            audioSourse.maxDistance = 5;                     //音が聞こえる範囲
        }
        else if (ONOFF == 1)//見えているとき
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            GameObject gobj = GameObject.Find("Player");     //Playerオブジェクトを探す
            PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
  
            VisualizationBoss.SetActive(true);               　//可視化の音(円)を見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true; 　//3DモデルのRendererを見える状態
            audioSourse.maxDistance = 300;                  　//音が聞こえる範囲

            OFFTime += Time.deltaTime;
            if (OFFTime >= 7.0f)
            {
                VisualizationBoss.SetActive(false);               //可視化の音(円)を見えない状態
                PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見える状態
                ONOFF = 0;                                        //見えない状態
                PS.Visualization = false;                         //Playerリング制限
                PS.onoff = 0;                                     //Player見えない状態
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えなくする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
                Front = false;                                    //次のポイント
                OFFTime = 0;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;                                       //見えない状態
        VisualizationBoss.SetActive(false);              //可視化の音(円)を見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;//3DモデルのRendererを見えない状態
        ChaseONOFF = false;                              //追跡中じゃない
        animator = GetComponent<Animator>();             //アニメーターコントローラーからアニメーションを取得する
        audioSourse = GetComponent<AudioSource>();　　　 //オーディオソースを取得
    }

    // Update is called once per frame
    private void Update()
    {
        Visualization();
        MoveBossEnemy();
        Chase();

        if (EnemyAttack.SoundON == true)
        {
            Front = true;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
        }
        if (EnemyAttack.SoundOFF == true)
        {
            ONOFF = 1;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            Idle();
        }

        Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

        if (isFront) //ターゲットが自身の前方にあるなら
        {
            DestroyONOFF = false;
            float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
            if (ONOFF == 0|| ChasePlayer > 5) { ChaseONOFF = false; }
            else if (ONOFF == 1 && ChasePlayer <= 5) { ChaseONOFF = true; }
           
        }
        else if (isBack)// ターゲットが自身の後方にあるなら
        {
            float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
            if (detectionPlayer <= 7f) { DestroyONOFF = true; }//プレイヤーが検知範囲に入ったら倒せる
        }
    }
    void Idle() { audioSourse.PlayOneShot(BossIdle); }

    void Move() { audioSourse.PlayOneShot(BossMove); }

    private void OnTriggerStay(Collider other)
    {
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (other.gameObject.tag == "LeftWall")
        {
            localAngle.x = 0f;
            localAngle.z = -90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
        }
        else if (other.gameObject.tag == "RightWall")
        {
            localAngle.x = 0f;
            localAngle.z = 90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            localAngle.x = 0f;
            localAngle.y = 0f;
            localAngle.z = 180f;
            myTransform.localEulerAngles = localAngle;
        }
        else if (other.gameObject.tag == "Floor")
        {
            localAngle.x = 0f;
            localAngle.z = 0f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
        }
        else if (other.gameObject.tag == "RW2")
        {
            localAngle.x = 0f;
            localAngle.z = -90f;
            localAngle.y = -90f;
            myTransform.localEulerAngles = localAngle;
        }
        else if (other.gameObject.tag == "LW2")
        {
            localAngle.x = 0f;
            localAngle.z = -90f;
            localAngle.y = -90f;
            myTransform.localEulerAngles = localAngle;
        }

        if (other.CompareTag("RoomOut"))
        {
            NextPatrolPoint();
        }
    }
}
