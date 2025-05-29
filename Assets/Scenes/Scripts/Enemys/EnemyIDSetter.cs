#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EnemyIDSetter : MonoBehaviour
{
    [MenuItem("Tools/Set Unique Enemy IDs")]
    static void AssignUniqueIDs()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].characterID = i;
            EditorUtility.SetDirty(enemies[i]); // シーン変更をUnityに通知
        }

        Debug.Log("すべてのEnemyにユニークなIDを割り当てました");
    }
}
#endif
