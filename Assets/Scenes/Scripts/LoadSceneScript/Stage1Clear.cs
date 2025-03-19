using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームクリア(脱出判定)クラス
/// このクラスは、プレイヤーが出口に衝突した際に、ステージ1クリアシーンに遷移する役割を持つ。
/// </summary>
public class Stage1Clear : MonoBehaviour
{
    // 衝突が発生したときに呼ばれる
    void OnCollisionEnter(Collision other)
    {
        // ImpactOnObjectsAreaという名前のGameObjectを探す
        GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

        // ImpactOnObjectsスクリプトを取得
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjectsスクリプトのインスタンスを取得

        // 衝突したオブジェクトの名前が"ExitDoor"の場合
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjectsスクリプト内のcountが1のときにステージクリア処理を実行
            if (impactObjects.count == 1)
            {
                // "Stage1Clear"シーンに遷移
                SceneManager.LoadScene("Stage1Clear");
            }
        }
    }
}