using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] PatrolPoints; // 巡回ポイントの配列
    private float patrolInterval = 2f; // 巡回の間隔
    private float chaseSpeed = 2f; // Playerを追いかける速度
    private float MoveSpeed = 2f; // 動く速度

    public int CurrentPointIndex = 0; // 現在の巡回ポイントのインデックス
    private Transform target; // Playerの位置
    private bool isPatrolling = true; // 巡回中かどうか

    public float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public float SoundTime;//経過時間
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;
    public Transform Player;//プレイヤーを参照

    private float TargetTime;

    public Animator animator; //アニメーションの格納
   
    public bool Soundonoff ;


    float time;
   
    private void Start()
    {
        //tagが"EnemyParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //タグが"EnemyParts"である子オブジェクトを見えなくする
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }

        //animator = GetComponent<Animator>();
        NextPatrolPoint();

       GameObject Chase = GameObject.FindWithTag("Chase");
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
    }

    private  void Update()
    {
        Switch();

        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);

        


        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        if (target != null)
        {
            if (PS.onoff == 1 && ONoff == 1)
            {
                animator.SetBool("EnemyWalk", false);
                animator.SetBool("EnemyRun", true);
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            }
        }
        else if (isPatrolling )
        {
      
                // 巡回中の場合は巡回ポイントに向かう
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            if (transform.position == PatrolPoints[CurrentPointIndex].position)
            {
                // 巡回ポイントに到達したら一定時間停止し、次の巡回ポイントに移動する
                // animator.SetTrigger("ShakeHead");
                time += Time.deltaTime;
                if (time >= 2f)
                {
                    isPatrolling = false;
                    Invoke("NextPatrolPoint", patrolInterval);
                    transform.LookAt(PatrolPoints[CurrentPointIndex].transform);
                    animator.SetBool("EnemyRun", false);
                    time = 0;
                }
            }
        }
    }

    void Switch()
    {
        float randomTime = Random.Range(5f, 10f);
        TargetTime = randomTime;
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//見えないとき
        {
            Soundonoff = true;
            SoundTime += Time.deltaTime;
            if (SoundTime >= TargetTime)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//音波非表示→表示
                GameObject Chase = GameObject.FindWithTag("Chase");
                EnemyChase EC = Chase.GetComponent<EnemyChase>(); //EnemyFailurework付いているスクリプトを取得

                if (EC.Chase == false)
                {
                    target = null;  
                }
            }
        }
        if (ONoff == 1)//見えているとき
        {
            Soundonoff = false;
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えなくする
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
                Sphere.SetActive(false);//音波表示→非表示
            }
        }
    }

    void NextPatrolPoint()
    {
        // 次の巡回ポイントへのインデックスを更新
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)
        {
            CurrentPointIndex = 0;
        }
        // 巡回中に戻る
        isPatrolling = true;
        animator.SetBool("EnemyRun", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           GameObject Chase = GameObject.FindWithTag("Chase");
           EnemyChase EC = Chase.GetComponent<EnemyChase>(); 

            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            /*
            if (EC.Chase == true &&EC.Wall==false&& PS.onoff == 1)
            {
                target = other.transform;  // Playerを検知したら追いかける
            }
            */
        }
    }
}

