using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 0.3f;                    // 動く速度
    private int CurrentPointIndex = 0;                 // 現在の巡回ポイントのインデックス
    private float NextTime;                            //次のポイントに向かうまでの時間
　　private bool Front;                                //ポイントにたどり着いたときに返す

    //可視化
    public float ONOFF = 0;                               　　　 //(0が見えない；１が見える状態）
    private float OFFTime;                                　　　 //プレイヤーを見失ってから時間を
    float VisualizationRandom;                             　　　//可視化時間をランダム
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer; //3DモデルのRenderer

    //サウンド
   [SerializeField] AudioSource audioSourse;
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;

    //前後判定
    public Transform TargetPlayer;　　　　　　　　　　 //プレイヤーの位置を取得

    //Playerを追跡
    float ChaseSpeed = 0.08f;                           //Playerを追いかける速度
    [SerializeField] bool ChaseONOFF;                  //(ChaseON： true/ChaseOFF: false)


    //Destroyの判定
    public bool DestroyONOFF;                          //(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWallONOFF;　　　　　　　　　　　 //(TouchWallON： true/ TouchWallOFF: false)

    //アニメーション
    [SerializeField] Animator animator;　　　　　　　  //アニメーター取得

    public GameObject Player;                          //プレイヤーオブジェクト取得

    //プレイヤーが一定の範囲に入った時に返す
    private bool INPlayerONOFF;                        //(INPlayerON： true/ INPlayerOFF: false)

    //ピアノの部屋に関すること
    public bool piano;
    int pianocnt;
    public bool zero;
    AudioSetting AS;
    bool PianoRoom;

    private bool IdleONOFF;
    private bool SeenAreaONOFF;
    private float VisualizationTime;
    [SerializeField] GameObject HitBox;

    private void MoveEnemy()
    {
        if (ChaseONOFF == false && TouchWallONOFF == false)
        {
            if (Front == false)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);              //次のポイントの方向を向く

                if (this.transform.position == PatrolPoints[CurrentPointIndex].position)  // 次の巡回ポイントへのインデックスを更新
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Walk", false);
                    Front = true;
                }
            }
            else if (Front == true)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
                NextTime += Time.deltaTime;
                if (NextTime >= 5.0f)
                {
                    Front = false;
                    NextPatrolPoint();
                    NextTime = 0;
                }
            }
        }
    }

    private void Chase()//プレイヤーを追いかける
    {
        GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position); //プレイヤーと敵の位置の計算
        if (ChasePlayer <= 5f) //プレイヤーが検知範囲に入ったら
        {
            if ((PS.onoff == 1 && ONOFF == 1 && TouchWallONOFF == false)) //プレイヤーが可視化していたら
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                ONOFF = 1; //自分自身を可視化
                HitBox.SetActive(true);
                ChaseONOFF = true; //追跡中
                transform.LookAt(TargetPlayer.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed; //プレイヤーの方向に向かう
                PS.Visualization = true;
                PS.onoff = 1;  //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else if (ChasePlayer > 5 && ChaseONOFF == true)
        {
            PS.Visualization = false;
            PS.onoff = 0;
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えるようにする
                playerParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            SeenAreaONOFF = false;
            ChaseONOFF = true;
            IdleONOFF = true;
            ONOFF = 1;
            HitBox.SetActive(true);

            if (IdleONOFF == true)
            {
                OFFTime += Time.deltaTime;
                if (OFFTime >= 3.0f)
                {
                    ChaseONOFF = false;
                    IdleONOFF = false;
                    ONOFF = 0;
                    HitBox.SetActive(false);
                    PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
                    NextTime = 0;
                    OFFTime = 0;
                }
            }
        }
    }

    private void NextPatrolPoint()  　　　　　　　　　　//ポイントの更新                 
    {
        CurrentPointIndex++;                         　//次のポイント
        //巡回ポイントが最後まで行ったら最初に戻る
        if (CurrentPointIndex >= PatrolPoints.Length){CurrentPointIndex = 0;}
    }

    private void Visualization()　　　　　　　　　　　　　　　　　//自身の可視化のON OFF
    {
        if (PianoRoom == false)                                 　//ピアノの部屋じゃない
        {
            if (Front == false)                                 　//ポイントにつくまでは見えない状態
            {
                ONOFF = 0;
                HitBox.SetActive(false);
                PrototypeBodySkinnedMeshRenderer.enabled = false;　//3DモデルのRendererを見えない状態                                    
            }
            if (Front == true)                                     //ポイントについたので見える状態
            {
                PrototypeBodySkinnedMeshRenderer.enabled = true;　//3DモデルのRendererを見える状態
                ONOFF = 1;
            }
        }

        if (SeenAreaONOFF == true)
        {
            GameObject obj = GameObject.Find("Player");                                                                                 //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            ONOFF = 1;
            HitBox.SetActive(true);
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3DモデルのRendererを見える状態
            Front = true;
            if (ChaseONOFF == false)
            {
                VisualizationTime += Time.deltaTime;
                if (VisualizationTime >= 5.0f)
                {
                    PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
                    ONOFF = 0;                                         //見えない状態
                    HitBox.SetActive(false);
                    VisualizationTime = 0;
                    SeenAreaONOFF = false;
                }
            }
            else
            {
                float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position); //プレイヤーと敵の位置の計算
                if (ChasePlayer > 5 && ChaseONOFF == true)
                {
                    PS.Visualization = false;
                    PS.onoff = 0;
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    SeenAreaONOFF = false;
                    ChaseONOFF = true;
                    IdleONOFF = true;
                    ONOFF = 1;
                    HitBox.SetActive(true);

                    if (IdleONOFF == true)
                    {
                        ONOFF = 1;
                        HitBox.SetActive(true);
                        OFFTime += Time.deltaTime;
                        if (OFFTime >= 3.0f)
                        {
                            ChaseONOFF = false;
                            IdleONOFF = false;
                            ONOFF = 0;
                            HitBox.SetActive(false);
                            PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
                            NextTime = 0;
                            OFFTime = 0;
                        }
                    }
                }
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player");                                                                                 //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);                                    //プレイヤーと敵の位置の計算
        if (VisualizationPlayer <=5f)                                                                                              //プレイヤーが検知範囲に入ったら
        {
            Chase();
            Ray ray;
            RaycastHit hit;
            Vector3 direction;                                                                                                      // Rayを飛ばす方向
            float distance =5.0f;                                                                                                  // Rayを飛ばす距離

            // Rayを飛ばす方向を計算
            Vector3 temp = Player.transform.position - transform.position;
            direction = temp.normalized;

            ray = new Ray(transform.position, direction);                                                                           // Rayを飛ばす
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);                                                         // Rayをシーン上に描画

            // Rayが最初に当たった物体を調べる
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Wall") )
                {
                    PS.Visualization = false;
                    PS.onoff = 0;                                                                                                   //見えているから1
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }

                if (hit.collider.CompareTag("Player"))
                {
                    Chase();
                    PS.Visualization = true;
                    PS.onoff = 1;  //見えているから1
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }
    }
    
    
    private void Ray2()
    {
        if (PianoRoom != true)
        {
            if (TouchWallONOFF == false)
            {
                GameObject obj = GameObject.Find("Player");                                                                                 //Playerオブジェクトを探す
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //付いているスクリプトを取得
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);                                    //プレイヤーと敵の位置の計算
                if (VisualizationPlayer <= 5f)                                                                                              //プレイヤーが検知範囲に入ったら
                {
                    Ray ray;
                    RaycastHit hit;
                    Vector3 direction;                                                                                                      // Rayを飛ばす方向
                    float distance = 5f;                                                                                                  // Rayを飛ばす距離

                    // Rayを飛ばす方向を計算
                    Vector3 temp = Player.transform.position - transform.position;
                    direction = temp.normalized;

                    ray = new Ray(transform.position, direction);                                                                           // Rayを飛ばす
                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);                                                         // Rayをシーン上に描画

                    // Rayが最初に当たった物体を調べる
                    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Wall"))
                        {
                            if (ChaseONOFF == false)
                            {
                                audioSourse.maxDistance = 1;
                            }
                            else
                            {
                                audioSourse.maxDistance = 3.5f;
                            }
                        }
                        if (hit.collider.CompareTag("Player"))
                        {
                            if (PS.onoff == 1)
                            {

                                transform.LookAt(TargetPlayer.transform);                                                                         //プレイヤーの方向にむく
                                transform.position += transform.forward * 0.05f;                                                             //プレイヤーの方向に向かう
                            }
                        }
                    }
                }
            }
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;                                　　　　//見えない状態
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3DモデルのRendererを見えない状態
        ChaseONOFF = false;                       　　　　//追跡中じゃない
        TouchWallONOFF = false;
        INPlayerONOFF = false;
        animator = GetComponent<Animator>();      　　　　//アニメーターコントローラーからアニメーションを取得する
        audioSourse = GetComponent<AudioSource>();
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    private void Update()
    {
        TouchWallONOFF = false;
        float Player = Vector3.Distance(transform.position, TargetPlayer.position);   //プレイヤーと敵の位置の計算
        if (Player <= 0.8f)
        {
            if (ONOFF == 1)
            {
                Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
                bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
                bool isBack = Vector3.Dot(Position, transform.forward) < 0;   // ターゲットが自身の後方にあるかどうか判定

                if (isFront)
                {
                    GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
                    PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得
                    var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                    PS.Visualization = true;
                    PS.onoff = 1;
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    ONOFF = 1;
                    HitBox.SetActive(true);
                    PrototypeBodySkinnedMeshRenderer.enabled = true;                         //3DモデルのRendererを見える状態
                    ChaseONOFF = false;
                    INPlayerONOFF = true;
                }
                else if (isBack)
                {
                    GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
                    PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得
                    var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                    PS.Visualization = false;
                    PS.onoff = 0;
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }

                    ONOFF = 1;
                    HitBox.SetActive(true);
                    PrototypeBodySkinnedMeshRenderer.enabled = true;                         //3DモデルのRendererを見える状態
                    ChaseONOFF = false;
                    INPlayerONOFF = true;
                }
            }
        }
        else if (Player >= 2f) { INPlayerONOFF = false; }

        if (INPlayerONOFF == false)
        {
           
            Visualization();
            MoveEnemy();

            Vector3 Position = TargetPlayer.position - transform.position;                          // ターゲットの位置と自身の位置の差を計算
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;                            // ターゲットが自身の前方にあるかどうか判定
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;                             // ターゲットが自身の後方にあるかどうか判定

            if (isFront)                                                                            //ターゲットが自身の前方にあるなら
            {
                Chase();
                Ray2();
                if (ONOFF == 0) { ChaseONOFF = false; } else { Ray(); }
                DestroyONOFF = false;
            }
            else if (isBack)                                                                         // ターゲットが自身の後方にあるなら
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position); //プレイヤーと敵の位置の計算

                //プレイヤーが検知範囲に入ったら
                if (detectionPlayer <= 5f) { DestroyONOFF = true; }
            }
        }

        //ピアノ部屋挙動
        if (piano)
        {
            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>();
            if (AS.BGMSlider.value == -80)
            {
                zero = true;
                piano = false;
                PianoRoom = false;
                Visualization();
            }
            else
            {
                piano = true;
                zero = false;
                ONOFF = 1;
                PianoRoom = true;
                PrototypeBodySkinnedMeshRenderer.enabled = true;                　　　　　　　　　　　//3DモデルのRendererを見える状態
            }
        }
        else
        {
            zero = false;
            if (pianocnt % 2 != 0 && AS.BGMSlider.value != -80) { piano = true; }
        }
    }

    void Idle(){audioSourse.PlayOneShot(EnemySearch);}

    void Run(){ audioSourse.PlayOneShot(EnemyRun);}

    void Walk(){audioSourse.PlayOneShot(EnemyWalk);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))//タグ（"SeenArea"）接触し続けていたら
        {
            ONOFF = 1;
            HitBox.SetActive(true);
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3DモデルのRendererを見える状態
            SeenAreaONOFF = true;
        }

        if (other.CompareTag("Wall"))
        {
            TouchWallONOFF = true;
            NextPatrolPoint();
            NextTime = 0;
            Front = false;
        }

        if (other.CompareTag("PianoRoom"))
        {
            PianoRoom = true;
            pianocnt++;
            if (!zero)
            {
                piano = true;
                if (pianocnt % 2 == 0){piano = false;}
            }
        }

        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (ONOFF == 0)
            {

                PS.Visualization = false;
                PS.onoff = 0;                                                             //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
}