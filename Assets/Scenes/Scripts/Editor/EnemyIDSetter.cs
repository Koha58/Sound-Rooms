#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

/// <summary>
/// エディタ拡張：全ての敵キャラクターに一意のIDを自動で割り当てるツール
/// Unityエディタのメニューから実行可能
/// </summary>
public class EnemyIDSetter : MonoBehaviour
{
    /// <summary>
    /// UnityEditor メニューに "Tools/Set Unique Enemy IDs" を追加し、クリックでこのメソッドを実行
    /// </summary>
    [MenuItem("Tools/Set Unique Enemy IDs")]
    static void AssignUniqueIDs()
    {
        //各敵タイプのインスタンスをシーン内から取得
        // 通常の敵（EnemyController）の取得
        EnemyController[] normalEnemies = FindObjectsOfType<EnemyController>();

        // トリック型の敵（TrickEnemyController）の取得
        TrickEnemyController[] trickEnemies = FindObjectsOfType<TrickEnemyController>();

        // ボス敵（BossEnemyController）の取得
        BossEnemyController[] bossEnemies = FindObjectsOfType<BossEnemyController>();

        //全体の敵の数を合計して、IDの初期値（降順の最大値）を決定
        int totalCount = normalEnemies.Length + trickEnemies.Length + bossEnemies.Length;

        // 降順でユニークなIDを振るため、最大数から開始
        int idCounter = totalCount - 1;

        //各敵にユニークなIDを割り当てる
        // まずボスにIDを割り当てる（順番は任意：重要な敵を先にしても良い）
        foreach (BossEnemyController enemy in bossEnemies)
        {
            enemy.characterID = idCounter--; // IDを割り当ててカウントをデクリメント
            EditorUtility.SetDirty(enemy);   // 変更を保存対象としてマーク（エディタ上で反映されるように）
        }

        // 次にトリック系の敵にIDを割り当て
        foreach (TrickEnemyController enemy in trickEnemies)
        {
            enemy.characterID = idCounter--;
            EditorUtility.SetDirty(enemy);
        }

        // 最後に通常の敵にIDを割り当て
        foreach (EnemyController enemy in normalEnemies)
        {
            enemy.characterID = idCounter--;
            EditorUtility.SetDirty(enemy);
        }

        // エディタのコンソールに処理完了ログを出力
        Debug.Log("すべてのEnemy（EnemyController, TrickEnemyController, BossEnemyController）にユニークなID（降順）を割り当てました");
    }
}
#endif
