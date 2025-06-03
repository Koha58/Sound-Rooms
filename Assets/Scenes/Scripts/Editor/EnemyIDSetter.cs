#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EnemyIDSetter : MonoBehaviour
{
    [MenuItem("Tools/Set Unique Enemy IDs")]
    static void AssignUniqueIDs()
    {
        int idCounter = 0;

        // EnemyController に ID を付ける
        EnemyController[] normalEnemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in normalEnemies)
        {
            enemy.characterID = idCounter++;
            EditorUtility.SetDirty(enemy);
        }

        // TrickEnemyController に ID を付ける
        TrickEnemyController[] trickEnemies = FindObjectsOfType<TrickEnemyController>();
        foreach (TrickEnemyController enemy in trickEnemies)
        {
            enemy.characterID = idCounter++;
            EditorUtility.SetDirty(enemy);
        }

        // BossEnemyController に ID を付ける
        BossEnemyController[] bossEnemies = FindObjectsOfType<BossEnemyController>();
        foreach (BossEnemyController enemy in bossEnemies)
        {
            enemy.characterID = idCounter++;
            EditorUtility.SetDirty(enemy);
        }

        Debug.Log("すべてのEnemy（EnemyController, TrickEnemyController, BossEnemyController）にユニークなIDを割り当てました");
    }
}
#endif
