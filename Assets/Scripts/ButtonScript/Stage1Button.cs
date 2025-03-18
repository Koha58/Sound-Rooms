using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stage1でゲームオーバー時のシーン遷移を管理するクラス
/// </summary>
public class Stage1Button : MonoBehaviour
{
    // ボタンが押された時に呼び出されるメソッド
    public void ButtonC()
    {
        // "Stage1" シーンをロードする
        SceneManager.LoadScene("Stage1");
    }

    // タイトル画面に戻るためのボタン処理
    public void TitleButton()
    {
        // "StartScene" シーンをロードする（タイトル画面に遷移）
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        // キーボードやゲームパッドで入力を受け取る処理

        // Aキーまたはゲームパッドのボタン0（通常はAボタン）が押された時
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            // "Stage1" シーンをロードする
            SceneManager.LoadScene("Stage1");
        }

        // Bキーまたはゲームパッドのボタン1（通常はBボタン）が押された時
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // "StartScene" シーンをロードする（タイトル画面に遷移）
            SceneManager.LoadScene("StartScene");
        }
    }
}