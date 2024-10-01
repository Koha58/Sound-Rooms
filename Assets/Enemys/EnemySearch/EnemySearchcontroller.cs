using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySearchcontroller : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 0.5f; // 動く速度
    private int CurrentPointIndex = 0; // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    [SerializeField] AudioSource audioSourse;
    public AudioClip TrickEnemyLaugh;
    public AudioClip TrickEnemyRun;
    public AudioClip TrickEnemyIdle;

    //前後判定
    public Transform TargetPlayer;

    //Playerを追跡
    float ChaseSpeed = 0.01f;//Playerを追いかけるスピード
    bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;

    [SerializeField]
    private Transform Pos;

    [SerializeField] Quaternion rotation;
    private bool UpON = false;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;

    int Count = 0;
    float CountTime;

    [SerializeField]
    private Vector3 initialTransform;

    private bool IdleONOFF;
    private bool SeenAreaONOFF;
    private float VisualizationTime;
    [SerializeField] GameObject HitBox;

    private void MoveEnemy()
    {
        if (ChaseONOFF == false || TouchWall == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く
                                                                        // 次の巡回ポイントへのインデックスを更新
            if (transform.position == PatrolPoints[CurrentPointIndex].position) { NextPatrolPoint(); }
        }
    }

    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

        if (ChasePlayer <= 6f)//プレイヤーが検知範囲に入ったら
        {
            if (PS.onoff == 1&&ONOFF==1)//プレイヤーが可視化していたら
            {
                ONOFF = 1;
                HitBox.SetActive(true);
                PrototypeBodySkinnedMeshRenderer.enabled = true;
                audioSourse.enabled = true;
                animator.SetBool("StandUp", true);
                animator.SetBool("Run", true);
                ChaseONOFF = true;
                transform.LookAt(TargetPlayer.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }
        }
        else if(ChasePlayer >= 6f &&ChaseONOFF==true)
        {
            animator.SetBool("Run", true);
            ChaseONOFF = false;
            PS.Visualization = false;
            PS.onoff = 0;//見えているから1
        }
        
    }

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length){CurrentPointIndex = 0; }  //巡回ポイントが最後まで行ったら最初に戻る
    }

    private void Visualization()//自身の可視化のON OFF
    {
        if (ChaseONOFF == false)
        {
            if (ONOFF == 0)//見えないとき
            {
                //3DモデルのRendererを見えない状態
                PrototypeBodySkinnedMeshRenderer.enabled = false;
                ONTime += Time.deltaTime;
                if (Count == 0)
                {
                    if (ONTime >= VisualizationRandom)//ランダムで出された値より大きかったら見えるようにする
                    {
                        if (VisualizationRandom <= 5.0f) { Count = 0; }
                        else { Count = 1; }
                        ONOFF = 1;
                        ONTime = 0;
                    }
                }
            }
            else if (ONOFF == 1)//見えているとき
            {
                PrototypeBodySkinnedMeshRenderer.enabled = true;

                OFFTime += Time.deltaTime;
                if (OFFTime >= 5.0f)//10秒以上経ったら見えなくする
                {
                    ONOFF = 0;
                    HitBox.SetActive(false);
                    OFFTime = 0;
                }
            }

            if (Count == 1)
            {
                CountTime += Time.deltaTime;
                if (CountTime >= 20.0f)
                {
                    CountTime = 0;
                    Count = 0;
                }
            }
        }

        if (SeenAreaONOFF == true)
        {
            GameObject obj = GameObject.Find("Player");                                                                                 //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //付いているスクリプトを取得

            ONOFF = 1;
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3DモデルのRendererを見える状態
            if (ChaseONOFF == false)
            {
                VisualizationTime += Time.deltaTime;
                if (VisualizationTime >= 5.0f)
                {
                    VisualizationTime = 0;
                    PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
                    ONOFF = 0;                                         //見えない状態
                    HitBox.SetActive(false);
                }
            }
            else
            {
                float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position); //プレイヤーと敵の位置の計算
                if (ChasePlayer > 6 && ChaseONOFF == true)
                {
                    PS.Visualization = false;
                    PS.onoff = 0;
                    animator.SetBool("StandUp", false);
                    animator.SetBool("Run",true);
                    SeenAreaONOFF = false;
                    ChaseONOFF = true;
                    IdleONOFF = true;
                    ONOFF = 1;
                    HitBox.SetActive(true);

                    if (IdleONOFF == true)
                    {
                        ONOFF = 1;
                        OFFTime += Time.deltaTime;
                        if (OFFTime >= 3.0f)
                        {
                            ChaseONOFF = false;
                            IdleONOFF = false;
                            ONOFF = 0;
                            HitBox.SetActive(false);
                            PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
                            OFFTime = 0;
                        }
                    }
                }
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

        if (VisualizationPlayer <= 6f)//プレイヤーが検知範囲に入ったら
        {
            Ray ray;
            RaycastHit hit;
            Vector3 direction;   // Rayを飛ばす方向
            float distance =6;    // Rayを飛ばす距離

            // Rayを飛ばす方向を計算
            Vector3 temp = Player.transform.position - transform.position;
            direction = temp.normalized;

            ray = new Ray(transform.position, direction);  // Rayを飛ばす
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Rayをシーン上に描画

            // Rayが最初に当たった物体を調べる
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    PS.Visualization = true;
                    PS.onoff = 1;  //見えているから1
                }

                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    PS.Visualization = false;
                    PS.onoff = 0;//見えているから1
                }
            }
        }
        else
        {
            if (ChaseONOFF == true)
            {
                PS.Visualization = false;
                PS.onoff = 0;//見えているから1
                ChaseONOFF = false;
            }
        }
    }
   
    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//見えない状態
        HitBox.SetActive(false);
        VisualizationRandom = Random.Range(4.0f, 6.0f);
        audioSourse = GetComponent<AudioSource>();
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
        ChaseONOFF = false;
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
        initialTransform = transform.position; // 初期位置の取得（のつもり）
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (Player <= 0.2f)
        {
            if (ONOFF == 1)
            {
                ChaseONOFF = true;
                animator.SetBool("Run", true);
                PS.Visualization = true;
                PS.onoff = 1;  //見えているから1
                UpON = true;
            }
        }
        else if (Player > 0.2f) { UpON = false; }

        if (UpON == false)
        {
            if (this.transform.position == Pos.transform.position)
            {
                animator.SetBool("StandUp", false);
                animator.SetBool("Run", false);
                this.transform.localRotation = rotation;
                TouchWall = false;
                ChaseONOFF = false;
            }

            if (ONOFF==0)
            {
                transform.position = initialTransform;
            }

            Visualization();
            MoveEnemy();

            Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

            if (isFront) //ターゲットが自身の前方にあるなら
            {
                Chase();
                if (ONOFF == 0) { ChaseONOFF = false; }else{ Ray(); }
                DestroyONOFF = false;
            }
            else if (isBack)// ターゲットが自身の後方にあるなら
            {
                DestroyONOFF = true;
                if (ONOFF == 0) { ChaseONOFF = false; }
                if (ChaseONOFF==true) 
                {
                    animator.SetBool("Run", true);
                    ChaseONOFF = false;
                    PS.Visualization = false;
                    PS.onoff = 0;//見えているから1
                }
            }
        }
    }

    void Laugh(){audioSourse.PlayOneShot(TrickEnemyLaugh);}

    void Idle(){audioSourse.PlayOneShot(TrickEnemyIdle);}

    void Run(){audioSourse.PlayOneShot(TrickEnemyRun);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;//当たり判定ON
            PrototypeBodySkinnedMeshRenderer.enabled = true;　//3DモデルのRendererを見える状態
            SeenAreaONOFF = true;
        }

        if ( other.CompareTag("Wall"))
        {
            TouchWall = true;
            CurrentPointIndex--;
            //巡回ポイントが最後まで行ったら最初に戻る
            if (CurrentPointIndex <= PatrolPoints.Length){ CurrentPointIndex = 0; }
        }

        if (other.CompareTag("RoomOut"))
        {
            TouchWall = true;
            animator.SetBool("Run", true);
            CurrentPointIndex--;
            //巡回ポイントが最後まで行ったら最初に戻る
            if (CurrentPointIndex <= PatrolPoints.Length) { CurrentPointIndex = 0; }
        }

        if (other.CompareTag("Player"))
        {
            if (ONOFF == 0)
            {
                GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得
            }
        }
    }
}