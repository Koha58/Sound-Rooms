using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy2controller : MonoBehaviour
{
    /*//・普段は不可視化状態
         //・定期的に音を出す(可視化する)
         ┗この時、Playerと同じで音の範囲内の物を不可視化させてほしい。
         //・Playerが音の範囲に入る、又はPlayerの音の範囲に入る(背後、左右以外)とPlayerを可視化させ、Playerを追いかける
         ・Playerと接触したらPlayerのライフが1減る(ライフが0になるとゲームオーバー)
         ・Playerが音を出すと音源周辺の敵は音源に向かう
         ┗自動ドアが開く音とかにも反応して欲しいけど、そもそもPlayerが自動ドア前で可視化しないと自動ドアがPlayerを認識せずに開かない仕組みにするから、Playerが音を出したとき、一定の範囲内にいる(Playerの音の範囲とは別)敵だけ音源に向かう感じでいいかな(別に追われる訳では無い)。
         後、Enemyが音を出す頻度もっと少なくてもいいかな(Playerが音を出す意味があんまり無くなってしまうので)。
       */

    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 0.2f; // 動く速度
    private int CurrentPointIndex = 0; // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    public CapsuleCollider GameOverBoxCapsuleCollider;//当たり判定のONOFF
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    AudioSource audioSourse;
    public AudioClip TrickEnemyLaugh;
    public AudioClip TrickEnemyRun;
    public AudioClip TrickEnemyIdle;

    //前後判定
    public Transform TargetPlayer;

    //Playerを追跡
    float ChaseSpeed = 0.5f;//Playerを追いかけるスピード
    bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;
    float WallONOFF = 0.0f;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;
    public GameObject[] Items;
    public GameObject ItemGameObject;

    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (TouchWall == false)
        {
            if (ChasePlayer <= 30f)//プレイヤーが検知範囲に入ったら
            {
                if (PS.onoff == 1)//プレイヤーが可視化していたら
                {
                    animator.SetBool("Run", true);
                    ChaseONOFF = true;
                    transform.LookAt(TargetPlayer.transform); //プレイヤーの方向にむく
                    transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
                }
                else if (ONOFF == 0)
                {
                    ChaseONOFF = false;
                }
            }
            else
            {
                ChaseONOFF = false;
            }
        }
    }

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//巡回ポイントが最後まで行ったら最初に戻る
            CurrentPointIndex = 0;
    }

    private void TouchWalls()
    {
        if (TouchWall == true)
        {
            WallONOFF += Time.deltaTime;
            if (WallONOFF > 3f)
                TouchWall = false;//Wall判定
        }

    }

    private void Visualization()//自身の可視化のON OFF
    {
        if (ONOFF == 0)//見えないとき
        {
            GameOverBoxCapsuleCollider.enabled = false;//当たり判定OFF
            //3DモデルのRendererを見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//ランダムで出された値より大きかったら見えるようにする
            {
                ONOFF = 1;
                ONTime = 0;
                ItemGameObject.SetActive(true);
            }
        }
        else if (ONOFF == 1)//見えているとき
        {
            GameOverBoxCapsuleCollider.enabled = true;//当たり判定ON
            //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10秒以上経ったら見えなくする
            {
                ONOFF = 0;
                OFFTime = 0;
                ItemGameObject.SetActive(false);
            }
        }
    }

    private void ItemVisualization()//自身の可視化のON OFF
    {
        GameObject gameObject = GameObject.FindWithTag("Object");
        ItemObject itemObject = gameObject.AddComponent<ItemObject>();
        foreach (var itms in Items)
        {
            float VisualizationItems = Vector3.Distance(transform.position, itms.transform.position);//プレイヤーと敵の位置の計算
            if (VisualizationItems >= 3)
            {
                // itemObject.VisualizationON = true;
            }
            //else 
            //itemObject.VisualizationON = false;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//見えない状態
        GameOverBoxCapsuleCollider.enabled = false;//当たり判定OFF
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3DモデルのRendererを見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

        Items = GameObject.FindGameObjectsWithTag("Object");
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetBool("Run", true);
        Visualization();
        TouchWalls();

        if (ChaseONOFF == false || TouchWall == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

            if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
                NextPatrolPoint();
        }

        Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

        if (isFront) //ターゲットが自身の前方にあるなら
        {
            ItemVisualization();
            DestroyONOFF = false;
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

            if (VisualizationPlayer <= 30f)//プレイヤーが検知範囲に入ったら
            {

                Ray ray;
                RaycastHit hit;
                Vector3 direction;   // Rayを飛ばす方向
                float distance = 30;    // Rayを飛ばす距離

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
                        Chase();
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
            else
            {
                OFFTime += Time.deltaTime;
                if (OFFTime >= 10.0f)//10秒以上経ったら見えなくする
                {
                    ONOFF = 0;
                    OFFTime = 0;
                    PS.Visualization = false;
                }
            }
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

    void Laugh()
    {
        audioSourse.PlayOneShot(TrickEnemyLaugh);
    }

    void Idle()
    {
        audioSourse.PlayOneShot(TrickEnemyIdle);
    }

    void Run()
    {
        audioSourse.PlayOneShot(TrickEnemyRun);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            GameOverBoxCapsuleCollider.enabled = true;//当たり判定ON
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
