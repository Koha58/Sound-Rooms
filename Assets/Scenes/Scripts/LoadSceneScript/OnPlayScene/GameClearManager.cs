using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームクリア時の処理をまとめたクラス
/// </summary>
public class GameClearManager : MonoBehaviour
{
    private string currentScene;  // 現在のシーン名

    // 衝突が発生したときに呼ばれる
    void OnCollisionEnter(Collision other)
    {
       
        // 衝突したオブジェクトの名前が"ExitDoor"の場合
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjectsAreaという名前のGameObjectを探す
            GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

            // ImpactOnObjectsスクリプトを取得
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjectsスクリプトのインスタンスを取得

            // ImpactOnObjectsスクリプト内のcountが1のときにステージクリア処理を実行
            if (impactObjects.count == 1)
            {
                // プレイヤーのライフが0になったとき、現在のシーン名を保存してGameClearSceneに遷移
                // 現在のシーン名を保存
                PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                // 指定されたシーンに遷移
                SceneManager.LoadScene("GameClearScene");
            }
        }
    }
}
