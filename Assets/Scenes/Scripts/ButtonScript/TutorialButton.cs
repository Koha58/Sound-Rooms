using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TutorialSceneでゲームオーバー時のシーン遷移を管理するクラス
/// </summary>
public class TutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 現状では初期化処理はない
    }

    // チュートリアルシーンに遷移するボタン処理
    public void ButtonC()
    {
        // "TutorialScene" シーンをロードする
        SceneManager.LoadScene("TutorialScene");
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
            // "TutorialScene" シーンをロードする
            SceneManager.LoadScene("TutorialScene");
        }

        // Bキーまたはゲームパッドのボタン1（通常はBボタン）が押された時
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // "StartScene" シーンをロードする（タイトル画面に遷移）
            SceneManager.LoadScene("StartScene");
        }
    }
}