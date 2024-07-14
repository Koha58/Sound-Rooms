using System.Collections;
using System.Collections.Generic;
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
    public float onoff = 0;//(0が見えない；１が見える状態）
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

    //前後判定orPlayerを追いかける
    public Transform TargetPlayer;
    float ChaseSpeed = 0.05f;//Playerを追いかけるスピード
                             // bool FrontorBack;//(前： true/後: false)

    //Wallに当たった時
    // public Transform TransformWall;

    private void Chase()
    {
        float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= 10)//プレイヤーが検知範囲に入ったら
        {
            Debug.Log("追跡");
            transform.LookAt(TargetPlayer.transform); //プレイヤーの方向にむく
            transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
        }
    }

    private void Destroy()
    {
        float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//プレイヤーと敵の位置の計算
        if (detectionPlayer <= 5)//プレイヤーが検知範囲に入ったら
        {
         
        }
    }

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//巡回ポイントが最後まで行ったら最初に戻る
            CurrentPointIndex = 0;
    }

    private void Visualization()//可視化のON OFF
    {
        if (onoff == 0)//見えないとき
        {
            audioSource1.clip = FootstepsSound;//足音のオーディオクリップをオーディオソースに入れる
            audioSource1.enabled =true;
            audioSource1.PlayOneShot(FootstepsSound);
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
                onoff = 1;
                ONTime = 0;
            }
        }

        if (onoff == 1)//見えているとき
        {
            audioSource2.clip = VisualizationSound;// 可視化時のオーディオクリップをオーディオソースに入れる
            audioSource2.enabled = true;
            audioSource2.PlayOneShot(VisualizationSound);
            PrototypeCapsuleCollider.enabled = true;//当たり判定ON
           　//3DモデルのRendererを見える状態
            PrototypeBodySkinnedMeshRenderer.enabled =true;
            PrototypeKeySkinnedMeshRenderer.enabled =true;
            PrototypeRingSkinnedMeshRenderer.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;
            Eey.GetComponent<MeshRenderer>().enabled = true;

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10秒以上経ったら見えなくする
            {
                audioSource2.enabled = false;
                onoff = 0;
                OFFTime = 0;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        onoff = 0;//見えない状態
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
    private void FixedUpdate()
    {
        Visualization();

        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
        transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く

        if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
            NextPatrolPoint();

        Vector3 Position = TargetPlayer.position - transform.position; // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // ターゲットが自身の後方にあるかどうか判定

        if (isFront) //ターゲットが自身の前方にあるなら
        {
            Chase();
            //FrontorBack = true;
        }
        else if (isBack)// ターゲットが自身の後方にあるなら
        {
            Destroy();
            //FrontorBack=false;
        }
    }
}