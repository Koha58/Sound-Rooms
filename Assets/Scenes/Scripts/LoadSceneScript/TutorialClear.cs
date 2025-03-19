using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// チュートリアルクリア(脱出判定)クラス
/// このクラスは、プレイヤーが出口に衝突した際に、ステージ1クリアシーンに遷移する役割を持つ。
/// </summary>
public class TutorialClear : MonoBehaviour
{
    // 衝突が発生したときに呼ばれる
    void OnCollisionEnter(Collision other)
    {
        // "EnemyAttackArea"という名前のゲームオブジェクトをシーンから取得
        GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");

        // "ImpactOnObjects"スクリプトを取得（ゲームオブジェクトのスクリプト参照）
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>();

        // 衝突したオブジェクトが「ExitDoor」なら
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjectsスクリプト内のcountが1の場合
            if (impactObjects.count == 1)
            {
                // シーンを「TutorialClear」に変更
                SceneManager.LoadScene("TutorialClear");
            }
        }
    }
}
