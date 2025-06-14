using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵が巡回するためのルート情報を定義するクラス。
/// それぞれの巡回ルートは、Transform（位置情報）を複数保持します。
/// </summary>
[System.Serializable]
public class EnemyPatrolRoute
{
    // 巡回ポイント（Transform）の配列。
    // 敵はこの順に巡回していきます。
    public Transform[] patrolPoints;
}

/// <summary>
/// ゲーム全体の巡回ポイント情報を管理するマネージャー。
/// 各敵に対して対応する巡回ポイントをセットアップします。
/// </summary>
public class GameManager : MonoBehaviour
{
    // 巡回ポイントを管理する外部マネージャー（シーン上の別オブジェクトにアタッチされている想定）
    [SerializeField] private PatrolPointManager patrolPointManager;

    // 各敵に対応する親オブジェクトのリスト。
    // 各親オブジェクトの子オブジェクトとして巡回ポイントが配置されています。
    [SerializeField] private List<Transform> enemyPatrolParents;

    // 敵ごとの巡回ルート情報を格納するリスト。
    private List<EnemyPatrolRoute> enemyPatrolRoutes = new List<EnemyPatrolRoute>();

    /// <summary>
    /// ゲーム開始時に各敵の巡回ルートを初期化・構築する。
    /// </summary>
    private void Awake()
    {
        // 巡回ルートリストをクリア（再初期化）
        enemyPatrolRoutes.Clear();

        // 各敵（親オブジェクト）ごとに巡回ルートを作成
        for (int i = 0; i < enemyPatrolParents.Count; i++)
        {
            // 対象の親Transformを取得
            Transform parent = enemyPatrolParents[i];

            // 新しい巡回ルートオブジェクトを作成
            EnemyPatrolRoute route = new EnemyPatrolRoute();

            // 親オブジェクトの子の数を取得（巡回ポイント数）
            int childCount = parent.childCount;

            // 子オブジェクト（巡回ポイント）をTransform配列に格納
            route.patrolPoints = new Transform[childCount];
            for (int j = 0; j < childCount; j++)
            {
                route.patrolPoints[j] = parent.GetChild(j);
            }

            // 作成したルートをリストに追加
            enemyPatrolRoutes.Add(route);

            // 巡回ポイントマネージャーに現在の巡回ルートを登録（敵IDごとに）
            patrolPointManager.AddPatrolPoints(i, new List<Transform>(route.patrolPoints));

            // デバッグログ：登録完了の確認
            Debug.Log($"Enemy ID {i} に {childCount} 個の巡回ポイントを登録しました。");
        }
    }
}
