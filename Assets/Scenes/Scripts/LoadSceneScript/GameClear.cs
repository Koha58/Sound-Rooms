using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームクリア(脱出判定)クラス
/// このクラスは、プレイヤーが出口に衝突した際に、ゲームクリアシーンに遷移する役割を持つ。
/// </summary>
public class GameClear : MonoBehaviour
{
    // 衝突が発生した時に呼ばれる
    void OnCollisionEnter(Collision other)
    {
        // "ImpactOnObjectsArea" という名前のオブジェクトを探す
        GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

        // "ImpactOnObjects" スクリプトを "impactObjectsArea" オブジェクトから取得
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); //付いているスクリプトを取得

        // 衝突したオブジェクトが "ExitDoor" という名前であった場合
        if (other.gameObject.name == "ExitDoor")
        {
            // "ImpactOnObjects" スクリプト内のカウントが1の場合にゲームクリアシーンへ遷移
            if (impactObjects.count == 1)
            {
                // ゲームクリアシーンに遷移
                SceneManager.LoadScene("GameClear");
            }
        }
    }
}