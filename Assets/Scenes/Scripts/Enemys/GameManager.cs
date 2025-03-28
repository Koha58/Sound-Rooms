using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Enemyの巡回ポイントをIDで管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    public PatrolPointManager patrolPointManager; // PatrolPointManagerへの参照 (巡回ポイントを管理)
    public Transform[] enemy1PatrolPoints;        // 敵1用の巡回ポイント
    public Transform[] enemy2PatrolPoints;        // 敵2用の巡回ポイント
    public Transform[] enemy3PatrolPoints;        // 敵3用の巡回ポイント
    public Transform[] enemy4PatrolPoints;        // 敵4用の巡回ポイント
    public Transform[] enemy5PatrolPoints;        // 敵5用の巡回ポイント
    public Transform[] enemy6PatrolPoints;        // 敵6用の巡回ポイント
    public Transform[] enemy7PatrolPoints;        // 敵7用の巡回ポイント
    public Transform[] enemy8PatrolPoints;        // 敵8用の巡回ポイント

    private void Start()
    {
        // 敵1の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(1, new List<Transform>(enemy1PatrolPoints));

        // 敵2の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(2, new List<Transform>(enemy2PatrolPoints));

        // 敵3の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(3, new List<Transform>(enemy3PatrolPoints));

        // 敵4の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(4, new List<Transform>(enemy4PatrolPoints));

        // 敵5の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(5, new List<Transform>(enemy5PatrolPoints));

        // 敵6の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(6, new List<Transform>(enemy6PatrolPoints));

        // 敵7の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(7, new List<Transform>(enemy7PatrolPoints));

        // 敵8の巡回ポイントを追加
        patrolPointManager.AddPatrolPoints(8, new List<Transform>(enemy8PatrolPoints));
    }
}
