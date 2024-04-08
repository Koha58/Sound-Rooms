using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] patrolPoints; // 巡回ポイントの配列
    public float patrolInterval = 2f; // 巡回の間隔
    public float chaseSpeed = 5f; // Playerを追いかける速度

    private int currentPointIndex = 0; // 現在の巡回ポイントのインデックス
    private Transform target; // Playerの位置
    private bool isPatrolling = true; // 巡回中かどうか

    private Animator animator; // アニメーターコンポーネント

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextPatrolPoint();
    }

    void Update()
    {
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

