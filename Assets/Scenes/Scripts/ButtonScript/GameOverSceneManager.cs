using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバーシーンでリトライやタイトルに戻る処理をまとめたクラス
/// </summary>
public class GameOverSceneManager : MonoBehaviour
{
    private string previousScene;  // 前のシーン名を格納する変数

    // Startメソッドで、前のシーンを取得しておく
    void Start()
    {
        // プレイヤープレファレンスから前のシーン名を取得
        previousScene = PlayerPrefs.GetString("PreviousScene", "StartScene"); // デフォルト値を"StartScene"に設定
    }

    // リトライボタンが押されたときの処理
    public void RetryButton()
    {
        // リトライ先のシーンに遷移
        SceneManager.LoadScene(previousScene);
    }

    // タイトル画面に戻る処理
    public void TitleButton()
    {
        // タイトルシーンに遷移
        SceneManager.LoadScene("StartScene");
    }

    // Updateメソッドでキー入力を受け取る
    void Update()
    {
        // Aキーまたはジョイスティックのボタン0（通常はAボタン）が押された時
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            // リトライボタンを押したときの処理
            RetryButton();
        }

        // Bキーまたはジョイスティックのボタン1（通常はBボタン）が押された時
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // タイトル画面に戻る
            TitleButton();
        }
    }
}
