using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の巡回ルートを定義するクラス
/// </summary>
[System.Serializable]
public class EnemyPatrolRoute
{
    // 巡回ポイントの配列（Transformで位置情報を持つ）
    public Transform[] patrolPoints;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private PatrolPointManager patrolPointManager;

    // 親オブジェクト（敵ごとの巡回ポイント群が子オブジェクトとして存在）
    [SerializeField] private List<Transform> enemyPatrolParents;

    // 敵の巡回ルートリスト（EnemyPatrolRouteの配列）
    private List<EnemyPatrolRoute> enemyPatrolRoutes = new List<EnemyPatrolRoute>();

    private void Awake()
    {
        enemyPatrolRoutes.Clear();

        // 親オブジェクトリストを使ってEnemyPatrolRouteを作る
        for (int i = 0; i < enemyPatrolParents.Count; i++)
        {
            Transform parent = enemyPatrolParents[i];
            EnemyPatrolRoute route = new EnemyPatrolRoute();

            // 親の子オブジェクト全てを配列にセット
            int childCount = parent.childCount;
            route.patrolPoints = new Transform[childCount];
            for (int j = 0; j < childCount; j++)
            {
                route.patrolPoints[j] = parent.GetChild(j);
            }

            enemyPatrolRoutes.Add(route);

            // PatrolPointManagerに登録
            patrolPointManager.AddPatrolPoints(i, new List<Transform>(route.patrolPoints));

            Debug.Log($"Enemy ID {i} に {childCount} 個の巡回ポイントを登録しました。");
        }
    }
}
