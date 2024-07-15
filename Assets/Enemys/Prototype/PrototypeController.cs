using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PrototypeController : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 2f; // 動く速度
    private int CurrentPointIndex = 0; // 現在の巡回ポイントのインデックス

    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    private float ONTime;
    private float OFFTime;
    public CapsuleCollider PrototypeCapsuleCollider;//当たり判定のONOFF
    float VisualizationRandom;//可視化時間をランダム

    //3DモデルのRendererのONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;
    public SkinnedMeshRenderer PrototypeKeySkinnedMeshRenderer;
    public SkinnedMeshRenderer PrototypeRingSkinnedMeshRenderer;
    public MeshRenderer Ear;
    public MeshRenderer Eey;

    //サウンド
    public AudioClip FootstepsSound;// 足音のオーディオクリップ
    public AudioClip VisualizationSound;// 可視化時のオーディオクリップ
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
    //public GameObject Wall;

    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= 7f)//プレイヤーが検知範囲に入ったら
        {
            if (PS.onoff == 1)//プレイヤーが可視化していたら
            {
                Debug.Log("追跡");
                ChaseONOFF = true;
                transform.LookAt(TargetPlayer.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }
            else if (PS.onoff == 0 || ONOFF == 0)
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

    private void Visualization()//自身の可視化のON OFF
    {
        if (ONOFF == 0)//見えないとき
        {
            audioSource1.clip = FootstepsSound;//足音のオーディオクリップをオーディオソースに入れる
            audioSource1.enabled = true;
            PrototypeCapsuleCollider.enabled = false;//当たり判定OFF
            //3DモデルのRendererを見えない状態
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            PrototypeKeySkinnedMeshRenderer.enabled = false;
            PrototypeRingSkinnedMeshRenderer.enabled = false;
            Ear.GetComponent<MeshRenderer>().enabled = false;
            Eey.GetComponent<MeshRenderer>().enabled = false;

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
            PrototypeCapsuleCollider.enabled = true;//当たり判定ON
           　//3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            PrototypeKeySkinnedMeshRenderer.enabled = true;
            PrototypeRingSkinnedMeshRenderer.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;
            Eey.GetComponent<MeshRenderer>().enabled = true;

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
        PrototypeCapsuleCollider.enabled = false;//当たり判定OFF
        VisualizationRandom = Random.Range(5.0f, 10.0f);

        //3DモデルのRendererを見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;
        PrototypeKeySkinnedMeshRenderer.enabled = false;
        PrototypeRingSkinnedMeshRenderer.enabled = false;
        Ear.GetComponent<MeshRenderer>().enabled = false;
        Eey.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    private void Update()
    {
        Visualization();

        if (ChaseONOFF == false)
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
            FrontorBack = true;
        }
        else if (isBack)// ターゲットが自身の後方にあるなら
        {
            FrontorBack = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            if (FrontorBack==false)
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

                if (detectionPlayer <= 5f)//プレイヤーが検知範囲に入ったら
                {
                    DestroyONOFF = true;
                }
            }
            else
               DestroyONOFF = false;
        }


      /*if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            //EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();

            if (FrontorBack == true)
            {
                if (PS.onoff == 0)
                {
                    float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

                    if (detectionPlayer <= 5f)//プレイヤーが検知範囲に入ったら
                    {
                        PS.onoff = 1;  //見えているから1
                        foreach (var playerParts in childTransforms)
                        {
                            //タグが"PlayerParts"である子オブジェクトを見えるようにする
                            playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                        }
                    }
                }
            }
        }*/
    }
}