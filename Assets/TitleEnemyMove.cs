using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TitleEnemyMove : MonoBehaviour
{
    //移動
    [SerializeField] private Transform[] PatrolPoints; // 巡回ポイントの配列
    private float MoveSpeed = 0.5f;                    // 動く速度
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

    //アニメーション
    [SerializeField] Animator animator;

    public GameObject VisualizationGameObject;
    private bool UpON = false;
    private float NextTime;
    private bool Front;

    private void NextPatrolPoint() //次のポイント
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//巡回ポイントが最後まで行ったら最初に戻る
            CurrentPointIndex = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//見えない状態
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3DモデルのRendererを見えない状態
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
    }

    // Update is called once per frame
    private void Update()
    {

        if (ONOFF == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//次のポイントの方向を向く
            animator.SetBool("Walk", true);
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            VisualizationGameObject.SetActive(false);
            if (transform.position == PatrolPoints[CurrentPointIndex].position)// 次の巡回ポイントへのインデックスを更新
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                ONOFF = 1;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            VisualizationGameObject.SetActive(true);
            NextTime += Time.deltaTime;
            if (NextTime >= 5.0f)
            {
                NextPatrolPoint();
                NextTime = 0;
                ONOFF = 0;
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
}
