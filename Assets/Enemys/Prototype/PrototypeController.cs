using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AudioSource audioSource;// オーディオソース

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
            audioSource.clip = FootstepsSound;//足音のオーディオクリップをオーディオソースに入れる
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
                onoff = 1;
                ONTime = 0;
            }
        }

        if (onoff == 1)//見えているとき
        {
            audioSource.clip = VisualizationSound;// 可視化時のオーディオクリップをオーディオソースに入れる
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
        
        if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
            NextPatrolPoint();

    }

}
