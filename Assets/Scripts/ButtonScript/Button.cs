using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameSceneでゲームオーバー時のシーン遷移を管理するクラス
/// </summary>
public class Button : MonoBehaviour
{
    // "GameScene" に遷移するためのボタン処理
    public void ButtonC()
    {
        // "GameScene" シーンをロード
        SceneManager.LoadScene("GameScene");
    }

    // "StartScene" に遷移するためのボタン処理
    public void TitleButton()
    {
        // "StartScene" シーンをロード
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        // "A"キーまたはジョイスティックのボタン0が押された場合（通常のボタンA）
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0")) // A
        {
            // "GameScene" シーンをロード
            SceneManager.LoadScene("GameScene");
        }

        // "B"キーまたはジョイスティックのボタン1が押された場合（通常のボタンB）
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1")) // B
        {
            // "StartScene" シーンをロード
            SceneManager.LoadScene("StartScene");
        }
    }
}