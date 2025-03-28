using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static InputDeviceManager;

/// <summary>
/// シーン遷移を管理するスキップボタンのスクリプト
/// </summary>
public class SkipButton : MonoBehaviour
{
    // スキップボタンのUIオブジェクト（ボタンの表示/非表示を制御）
    public GameObject Sikp;

    // 新しいInput Systemでの入力管理用のインスタンス
    private GameInputSystem inputActions;

    // デバイス（Xbox/Keyboard）のチェックフラグ
    bool deviceCheck;

    // Xボタンが押されたかどうかを管理するフラグ
    private bool isXButton;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Awake()
    {
        // Input Systemのインスタンスを生成
        inputActions = new GameInputSystem();

        // Xボタンが押されたときにフラグを立てる
        inputActions.UI.XButton.performed += ctx => isXButton = true;
        inputActions.UI.XButton.canceled += ctx => isXButton = false;
    }

    /// <summary>
    /// Startメソッド
    /// </summary>
    // Startはゲーム開始時に1度だけ呼ばれる
    void Start()
    {
        // 特に処理は書かれていないが、必要であればここに初期化処理を書くことができる
    }

    /// <summary>
    /// 入力を有効化する
    /// </summary>
    private void OnEnable()
    {
        // 新しい入力システムを有効にする
        inputActions.Enable();
    }

    /// <summary>
    /// 入力を無効化する
    /// </summary>
    private void OnDisable()
    {
        // 新しい入力システムを無効にする
        inputActions.Disable();
    }

    /// <summary>
    /// フレーム毎の処理
    /// </summary>
    void Update()
    {
        // 現在使用している入力デバイスをチェック
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            // Xboxコントローラーが使用されている場合
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            // キーボードが使用されている場合
            deviceCheck = false;
        }

        // 入力デバイスがキーボードの場合はスキップボタンを非表示にし、Xboxの場合は表示する
        if (!deviceCheck)
        {
            // キーボードの場合、スキップボタンを非表示
            Sikp.SetActive(false);
        }
        else
        {
            // Xboxの場合、スキップボタンを表示
            Sikp.SetActive(true);
        }

        // Xボタンが押された場合、シーン遷移を行う
        if (isXButton == true)
        {
            // シーン名「TutorialScene」に遷移する
            SceneManager.LoadScene("TutorialScene");
        }
    }

    /// <summary>
    /// ボタンがクリックされたときに呼ばれるメソッド
    /// </summary>
    public void OnClick()
    {
        // 「TutorialScene」シーンに遷移
        SceneManager.LoadScene("TutorialScene");
    }

}