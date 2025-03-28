using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの巡回を管理するクラス
/// </summary>
public class PatrolPointManager : MonoBehaviour
{
    // 各敵の巡回ポイントを管理するディクショナリ。
    // 敵のID（enemyID）をキーとして、対応する巡回ポイント（Transformのリスト）を格納。
    private Dictionary<int, List<Transform>> patrolPoints = new Dictionary<int, List<Transform>>();

    // 巡回ポイントを追加する関数
    // enemyID: 敵の識別子
    // points: 敵が巡回する地点のTransformリスト
    public void AddPatrolPoints(int enemyID, List<Transform> points)
    {
        // patrolPointsに敵のIDが既に存在するか確認
        if (!patrolPoints.ContainsKey(enemyID))
        {
            // まだ存在しない場合、新しく追加
            patrolPoints.Add(enemyID, points);
        }
    }

    // 指定したIDの巡回ポイントを取得する関数
    // enemyID: 取得したい敵の識別子
    // 敵IDに対応する巡回ポイントのリストを返す
    public List<Transform> GetPatrolPoints(int enemyID)
    {
        // patrolPointsに敵のIDが存在するか確認
        if (patrolPoints.ContainsKey(enemyID))
        {
            // 存在する場合、そのIDに対応する巡回ポイントリストを返す
            return patrolPoints[enemyID];
        }
        // 敵IDが見つからなかった場合、nullを返す
        return null;
    }
}
