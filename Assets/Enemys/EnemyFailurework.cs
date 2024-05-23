using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] patrolPoints; // 巡回ポイントの配列
    public float patrolInterval = 2f; // 巡回の間隔
    public float chaseSpeed = 5f; // Playerを追いかける速度

    private int currentPointIndex = 0; // 現在の巡回ポイントのインデックス
    private Transform target; // Playerの位置
    private bool isPatrolling = true; // 巡回中かどうか

    public float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public float SoundTime;//経過時間
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    public Animator animator; //アニメーションの格納

    [SerializeField]
    private AudioClip SoundAttck;     //音を出すのオーディオクリップ
    [SerializeField]
    private AudioClip footstepSound;     // 足音のオーディオクリップ
    [SerializeField]
    private AudioSource audioSource;     // オーディオソース
    [SerializeField]

    public bool Soundonoff = true;

    private void Sound()
    {
        if (ONoff == 0)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = footstepSound;
                audioSource.Play();
            }
        }
        if (ONoff == 1)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    private void AttackSiund()
    {
        if (ONoff == 1)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = SoundAttck;
                audioSource.Play();
            }
        }
        if (ONoff == 0)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    private  void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextPatrolPoint();
    }

    private  void Update()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 1)//見えないとき
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//音波非表示→表示
            }
        }
        if (ONoff == 0)//見えているとき
        {
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

        if (target != null)
        {
            // Playerがいる場合は追いかける
            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
        else if (isPatrolling)
        {
            // 巡回中の場合は巡回ポイントに向かう
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, chaseSpeed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                // 巡回ポイントに到達したら一定時間停止し、次の巡回ポイントに移動する
               // animator.SetTrigger("ShakeHead");
                isPatrolling = false;
                Invoke("MoveToNextPatrolPoint", patrolInterval);
            }
        }

     
    }


    void MoveToNextPatrolPoint()
    {
        // 次の巡回ポイントへのインデックスを更新
        currentPointIndex++;
        if (currentPointIndex >= patrolPoints.Length)
        {
            currentPointIndex = 0;
        }

        // 巡回中に戻る
        isPatrolling = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Playerを検知したら追いかける
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Playerが範囲外に出たら追跡をやめる
            target = null;
        }
    }
}

