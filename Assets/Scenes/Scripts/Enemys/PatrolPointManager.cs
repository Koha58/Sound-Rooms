using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointManager : MonoBehaviour
{
    // 各敵の巡回ポイントを管理するディクショナリ
    private Dictionary<int, List<Transform>> patrolPoints = new Dictionary<int, List<Transform>>();

    // 巡回ポイントを追加する関数
    public void AddPatrolPoints(int enemyID, List<Transform> points)
    {
        if (!patrolPoints.ContainsKey(enemyID))
        {
            patrolPoints.Add(enemyID, points);
        }
    }

    // 指定したIDの巡回ポイントを取得する関数
    public List<Transform> GetPatrolPoints(int enemyID)
    {
        if (patrolPoints.ContainsKey(enemyID))
        {
            return patrolPoints[enemyID];
        }
        return null;
    }
}
