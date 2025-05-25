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

/// <summary>
/// ゲーム全体の管理を行うGameManagerクラス
/// 敵の巡回ルートを初期化してPatrolPointManagerに登録する
/// </summary>
public class GameManager : MonoBehaviour
{
    // 巡回ポイントの管理を行うマネージャークラスの参照（Inspectorからアサイン）
    [SerializeField] private PatrolPointManager patrolPointManager;

    // 各敵ごとの巡回ルート情報を格納するリスト（Inspectorから設定）
    [SerializeField] private List<EnemyPatrolRoute> enemyPatrolRoutes = new List<EnemyPatrolRoute>();

    /// <summary>
    /// ゲーム開始時に呼ばれる初期化処理
    /// 各敵に対応する巡回ポイントをPatrolPointManagerに登録する
    /// </summary>
    private void Start()
    {
        // 各敵ごとの巡回ルートを処理
        for (int i = 0; i < enemyPatrolRoutes.Count; i++)
        {
            int enemyId = i + 1; // 敵IDを1から開始（0からではなく）

            // 巡回ポイントをListに変換してPatrolPointManagerに追加
            patrolPointManager.AddPatrolPoints(enemyId, new List<Transform>(enemyPatrolRoutes[i].patrolPoints));
        }
    }
}