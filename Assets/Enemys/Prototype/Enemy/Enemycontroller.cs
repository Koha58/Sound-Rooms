using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 1.0f;                    // 動く速度
    private int CurrentPointIndex = 0;                 // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;                            //(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;                         //可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    AudioSource audioSourse;
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;

    //前後判定
    public Transform TargetPlayer;

    //Playerを追跡
    float ChaseSpeed = 0.1f;                           //Playerを追いかけるスピード
    [SerializeField] bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;                           //(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;
    public GameObject VisualizationGameObject;
    private bool UpON=false;


    private void Chase()//プレイヤーを追いかける
    {
        GameObject gobj = GameObject.Find("Player");//Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (TouchWall == false)
        {
            if (ChasePlayer <= 7f)//プレイヤーが検知範囲に入ったら
            {
                if (PS.onoff == 1)//プレイヤーが可視化していたら
                {
                   // Run();
                    ONOFF = 1;//自分自身を可視化
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    ChaseONOFF = true;//追跡中
                    transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
                    transform.position += transform.forward * ChaseSpeed;　//プレイヤーの方向に向かう
                }
                else if (ONOFF == 0)
                {
                    ChaseONOFF = false;//追跡中じゃない
                }
            }
            else { ChaseONOFF = false; }//追跡中じゃない
        }
    }

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//巡回ポイントが最後まで行ったら最初に戻る
            CurrentPointIndex = 0;
    }

    private void Visualization()//自身の可視化のON OFF
    {
        if (ONOFF == 0)//見えないとき
        {
            //3DモデルのRendererを見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//ランダムで出された値より大きかったら見えるようにする
            {
                ONOFF = 1;//見える
                ONTime = 0;
                VisualizationGameObject.SetActive(true);//物を不可視化する判定をON
            }
        }
        else if (ONOFF == 1)//見えているとき
        {
            //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10秒以上経ったら見えなくする
            {
                ONOFF = 0;//見えない
                OFFTime = 0;
                VisualizationGameObject.SetActive(false);//物を不可視化する判定をOFF
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (VisualizationPlayer <= 10f)//プレイヤーが検知範囲に入ったら
        {
            Chase();
 
            Ray ray;
            RaycastHit hit;
            Vector3 direction;   // Rayを飛ばす方向
            float distance =7;    // Rayを飛ばす距離

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
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }

                if (hit.collider.gameObject.CompareTag("Wall") || (hit.collider.gameObject.CompareTag("InWall")))
                {
                    PS.Visualization = false;
                    PS.onoff = 0;  //見えているから1
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えるようにする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }
        else if (VisualizationPlayer >= 11f)
        {
            OFFTime += Time.deltaTime;
            if (OFFTime >= 5.0f)//10秒以上経ったら見えなくする
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
        if(ChasePlayer<=10)
        {
            if (PS.onoff == 1)
            {
                transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
                transform.position += transform.forward * (MoveSpeed*0.09f); //プレイヤーの方向に向かう
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
        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (Player<= 0.65f)
        {
            //Idle();
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            transform.LookAt(TargetPlayer.transform);//プレイヤーの方向にむく
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            PS.Visualization = true;
            PS.onoff = 1;  //見えているから1
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えるようにする
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            UpON = true;
        }
        else if(Player >= 1.5f) { UpON = false; }

        if (UpON == false)
        {
            Chase2();

            if (ChaseONOFF == false)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
               // Walk();
            }

            Visualization();

            if (ChaseONOFF == false || TouchWall == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

                if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
                {
                    NextPatrolPoint();
                }
            }


            Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

            if (isFront) //ターゲットが自身の前方にあるなら
            {
                if (ONOFF == 0) { ChaseONOFF = false; }
                DestroyONOFF = false;
                Ray();
            }
            else if (isBack)// ターゲットが自身の後方にあるなら
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

                if (detectionPlayer <= 7f)//プレイヤーが検知範囲に入ったら
                {
                    DestroyONOFF = true;
                }
            }
        }
    }

    void Idle()
    {
        audioSourse.PlayOneShot(EnemySearch);
    }

    void Run()
    {
        audioSourse.PlayOneShot(EnemyRun);
    }

    void Walk()
    {
        audioSourse.PlayOneShot(EnemyWalk);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

        if (other.CompareTag("InWall") || other.CompareTag("Wall"))
        {
            TouchWall = true;
            CurrentPointIndex--;
            if (CurrentPointIndex <= PatrolPoints.Length)//巡回ポイントが最後まで行ったら最初に戻る
                CurrentPointIndex = 0;
        }
    }
}