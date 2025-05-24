using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPatrolRoute
{
    public Transform[] patrolPoints; // 敵の巡回ポイント
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PatrolPointManager patrolPointManager; // 巡回ポイント管理

    // 敵の巡回ルートをまとめたリスト
    [SerializeField] private List<EnemyPatrolRoute> enemyPatrolRoutes = new List<EnemyPatrolRoute>();

    private void Start()
    {
        for (int i = 0; i < enemyPatrolRoutes.Count; i++)
        {
            int enemyId = i + 1;
            patrolPointManager.AddPatrolPoints(enemyId, new List<Transform>(enemyPatrolRoutes[i].patrolPoints));
        }
    }
}