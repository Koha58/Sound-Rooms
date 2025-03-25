using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
    public Transform[] patrolPoints;  // 巡回ポイントの配列

    // 現在の巡回ポイントインデックス
    private int currentPatrolIndex = 0;

    // キャラクターIDに基づいて巡回ポイントを取得
    public Transform GetNextPatrolPoint(int characterID)
    {
        if (patrolPoints.Length == 0)
        {
            return null;
        }

        // キャラクターIDを無視して、単純に次の巡回ポイントを取得
        // この方法では、すべてのキャラクターが同じ順番で巡回するようになります
        Transform nextPatrolPoint = patrolPoints[currentPatrolIndex];

        // 次の巡回ポイントに移動するため、インデックスを更新
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

        return nextPatrolPoint;
    }
}
