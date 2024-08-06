using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrototypeController4 : MonoBehaviour
{

    //課題
    /*1音
     2物の可視化
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
    public GameObject VisualizationBox;//物の可視化の当たり判定
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //サウンド
    AudioSource audioSourse;
    public AudioClip FootstepsSound;// 足音のオーディオクリップ
    public AudioClip VisualizationSound;// 可視化時のオーディオクリップ
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;
    public AudioSource audioSource1;// オーディオソース
    public AudioSource audioSource2;// オーディオソース

    //前後判定
    public Transform TargetPlayer;
    public bool FrontorBack;//(前： true/後: false)

    //Playerを追跡
    float ChaseSpeed = 0.05f;//Playerを追いかけるスピード
    bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    private bool TouchWall;
    float WallONOFF = 0.0f;

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject Player;
    public GameObject Prototype;

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
            audioSource1.clip = FootstepsSound;//足音のオーディオクリップをオーディオソースに入れる
            audioSource1.enabled = true;
            GameOverBoxCapsuleCollider.enabled = false;//当たり判定OFF
            VisualizationBox.SetActive(false);//物の可視化判定OFF
            //3DモデルのRendererを見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//ランダムで出された値より大きかったら見えるようにする
            {
                audioSource1.enabled = false;
                ONOFF = 1;
                ONTime = 0;
            }
        }
        else if (ONOFF == 1)//見えているとき
        {
            audioSource2.clip = VisualizationSound;// 可視化時のオーディオクリップをオーディオソースに入れる
            audioSource2.enabled = true;
            GameOverBoxCapsuleCollider.enabled = true;//当たり判定ON
            VisualizationBox.SetActive(true);//物の可視化判定ON
            //3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            if (FrontorBack == true)//前方
            {
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
                    float distance = 50;    // Rayを飛ばす距離

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
                            Debug.Log("プレイヤー発見");
                            PS.onoff = 1;  //見えているから1
                            foreach (var playerParts in childTransforms)
                            {
                                //タグが"PlayerParts"である子オブジェクトを見えるようにする
                                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                            }
                        }

                        if (hit.collider.gameObject.CompareTag("Wall") || (hit.collider.gameObject.CompareTag("InWall")))
                        {
                            Debug.Log("プレイヤーとの間に壁がある");
                        }

                    }
                }

            }

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10秒以上経ったら見えなくする
            {
                audioSource2.enabled = false;
                ONOFF = 0;
                OFFTime = 0;
            }
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (ChaseONOFF == false)
        {
            animator.SetBool("Run", true);
        }

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
            Chase();
            /*float Wall= Vector3.Distance(transform.position, TargetPlayer.position);
              if (Wall <= 30f)//壁が検知範囲に入ったら
              {

              }*/
            FrontorBack = true;
        }
        else if (isBack)// ターゲットが自身の後方にあるなら
        {
            FrontorBack = false;
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
            audioSource1.enabled = false;
            audioSource2.clip = VisualizationSound;// 可視化時のオーディオクリップをオーディオソースに入れる
            audioSource2.enabled = true;
            GameOverBoxCapsuleCollider.enabled = true;//当たり判定ON
           　//3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            if (FrontorBack == false)
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

                if (detectionPlayer <= 7f)//プレイヤーが検知範囲に入ったら
                {
                    DestroyONOFF = true;
                }
            }
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
