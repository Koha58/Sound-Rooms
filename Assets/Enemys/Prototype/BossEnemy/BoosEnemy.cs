using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BoosEnemy : MonoBehaviour
{

/*ボス敵について
ボス敵の設定↓
//・エリア内に常駐している
//・通常敵と同じく、可視化⇔不可視化の2つの状態がある
・音を出して可視化する範囲が通常より広く(エリア全域の1/4くらい)、この敵の音に当たるとライフが1減る。
//・ボス敵の出す音は敵、Player両方を一定時間可視化する。
→なので、一定時間内は一定範囲内の敵が全て見えている状態になるので、倒しやすくなる。範囲外の敵は見えないままなので、油断してると、そちらに襲われる可能性もあるまま。
//・ボス敵は通常敵が倒される度に音を出す(可視化する)。
・壁に張り付いて素早く移動しているため、倒すのは困難。
//・通常敵を倒す度にボス敵の可視化範囲及び速度が遅くなり倒しやすくなる。
//・ボス敵の音に当たらないようにするためには障害物を利用して隠れる事が必要(敵を倒したあと10秒ほどボス敵が音を出すまでに時間がある)。
//・ボス敵は通常敵より大きい。*/
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    public float MoveSpeed =2.0f; // 動く速度
    private int CurrentPointIndex = 0; // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    AudioSource audioSource;
    public AudioClip BossMove;
    public AudioClip BossIdle;

    //前後判定
    public Transform TargetPlayer;

    //Playerを追跡
    public float VisualizationPlayer;
    public float ChaseSpeed = 0.5f;//Playerを追いかけるスピード
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
            if (ChasePlayer <= 20f)//プレイヤーが検知範囲に入ったら
            {
                if (PS.onoff == 1)//プレイヤーが可視化していたら
                {
                    ONOFF = 1;
                    animator.SetBool("Move", true);
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
    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//見えない状態
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSource = GetComponent<AudioSource>();

        //3DモデルのRendererを見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

        Items = GameObject.FindGameObjectsWithTag("Object");
    }

    // Update is called once per frame
    private void Update()
    {
        if (ChaseONOFF == false)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Move", true);
        }

        Visualization();
        TouchWalls();

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
            DestroyONOFF = false;
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

            if (VisualizationPlayer <= 25f)//プレイヤーが検知範囲に入ったら
            {
                Chase();

                if (ONOFF == 0)
                {
                    ChaseONOFF = false;
                }

                Ray ray;
                RaycastHit hit;
                Vector3 direction;   // Rayを飛ばす方向
                float distance = 25;    // Rayを飛ばす距離

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

                    if (hit.collider.gameObject.CompareTag("Object"))
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
    void Idle()
    {
        audioSource.PlayOneShot(BossIdle);
    }

    void Move()
    {
        audioSource.PlayOneShot(BossMove);
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

    private void OnCollisionEnter(Collision collision)
    {
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (collision.gameObject.tag == "LeftWall")
        {
            localAngle.z = 90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(10f, 0, 0);
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            localAngle.z = -90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(-10f, 0, 0);
        }
        else if (collision.gameObject.tag == "Ceiling")
        {
            localAngle.z = 180f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(0, 10f, 0);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            localAngle.z = 0f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(0, -10f, 0);
        }
    }

}
