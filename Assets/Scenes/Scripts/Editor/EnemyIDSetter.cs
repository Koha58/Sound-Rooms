#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EnemyIDSetter : MonoBehaviour
{
    [MenuItem("Tools/Set Unique Enemy IDs")]
    static void AssignUniqueIDs()
    {
        // 全ての敵を取得して合計数をカウント
        EnemyController[] normalEnemies = FindObjectsOfType<EnemyController>();
        TrickEnemyController[] trickEnemies = FindObjectsOfType<TrickEnemyController>();
        BossEnemyController[] bossEnemies = FindObjectsOfType<BossEnemyController>();

        int totalCount = normalEnemies.Length + trickEnemies.Length + bossEnemies.Length;
        int idCounter = totalCount - 1; // 降順で開始

        // BossEnemyController に ID を付ける（順番は任意ですがここでは先にしています）
        foreach (BossEnemyController enemy in bossEnemies)
        {
            enemy.characterID = idCounter--;
            EditorUtility.SetDirty(enemy);
        }

        // TrickEnemyController に ID を付ける
        foreach (TrickEnemyController enemy in trickEnemies)
        {
            enemy.characterID = idCounter--;
            EditorUtility.SetDirty(enemy);
        }

        // EnemyController に ID を付ける
        foreach (EnemyController enemy in normalEnemies)
        {
            enemy.characterID = idCounter--;
            EditorUtility.SetDirty(enemy);
        }

        Debug.Log("すべてのEnemy（EnemyController, TrickEnemyController, BossEnemyController）にユニークなID（降順）を割り当てました");
    }
}
#endif
