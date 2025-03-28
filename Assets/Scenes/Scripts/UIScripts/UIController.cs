using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// ゲーム中に設定（BGMやSE、操作説明など）を制御するスクリプト
/// </summary>
public class UIController : MonoBehaviour
{
    //歯車ボタン（設定画面に行くボタン）,メニュー画面,設定画面,操作説明画面,タイトルに戻る画面
    [SerializeField] GameObject originSettingButton, menu, settingPanel1,settingPanel2,settingPanel3;

    //ゲーム内設定を変更する画面へ遷移するボタン,操作説明画面へ遷移するボタン,タイトル画面へ遷移するボタン
    [SerializeField] GameObject settingButton,qperationExplanationButton,backTitleButton;

    //
    [SerializeField] GameObject keyBoardMoveSettingSelect, gamePadMoveSettingSelect, keyBoardButton, gamePadButton, keyBoard, gamePad;

    //
    [SerializeField] GameObject MicSliderGameObject, BGMSliderGameObject, SESliderGameObject, MouseSliderGameObject, closeKey;

    [SerializeField] GameObject[] Cursor;

    /// <summary>
    /// 各種スライダーを管理するためのSlider型変数
    /// </summary>
    //マイクスライダー,BGMスライダー,SEスライダー,マウススライダー
    [SerializeField] Slider MicSlider, BGMSlider, SESlider, MouseSlider;

    [SerializeField] AudioMixer audioMixer; // オーディオミキサー
    [SerializeField] GameObject micObject;　// マイクオブジェクト
    public CinemachineFreeLook VCamera;     // Cinemachineカメラ

    //InputSystem
    private GameInputSystem inputActions;  // 入力管理システム
    private Vector2 navigateInput;         // 移動の入力（2Dベクトル）

    private bool isMenuButton; // メニューボタンが押されているか
    private bool isBButton;    // Bボタンが押されているか
    private bool isAButton;    // Aボタンが押されているか


    // パネル管理用リスト（パネルの表示非表示を管理する）
    private List<GameObject> panels;

    private void Awake()
    {
        // Input Systemのインスタンスを作成
        inputActions = new GameInputSystem();

        //メニュ画面のスティック操作
        inputActions.UI.Navigate.performed += ctx => navigateInput = ctx.ReadValue<Vector2>();
        inputActions.UI.Navigate.canceled += ctx => navigateInput = Vector2.zero;

        //メニュボタン
        inputActions.UI.MenuButton.performed += ctx => isMenuButton = true;
        inputActions.UI.MenuButton.canceled += ctx => isMenuButton = false;

        //Bボタン
        inputActions.UI.BButton.performed += ctx => isBButton = true;
        inputActions.UI.BButton.canceled += ctx => isBButton = false;

        //Aボタン
        inputActions.UI.AButton.performed += ctx => isAButton = true;
        inputActions.UI.AButton.canceled += ctx => isAButton = false;
    }

    // 入力アクションを有効にする（ゲーム開始時）
    private void OnEnable()
    {
        inputActions.Enable();
    }

    // 入力アクションを無効にする（ゲーム終了時）
    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // パネルリストを作成
        panels = new List<GameObject> { menu, settingPanel1, settingPanel2, settingPanel3 };

        // 全てのパネルを非表示にする
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // カーソルを非表示にする
        for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Controller();// コントローラーの入力処理を管理
    }

    // メニューを表示する処理
    public void Menu(GameObject menuPanel)
    {
        menuPanel.SetActive(true); // メニューパネルを表示
        closeKey.SetActive(true);  // 閉じるキーを表示
        settingPanel1.SetActive(true); // 設定パネル1を表示
        settingPanel2.SetActive(false); // 設定パネル2を非表示
        settingPanel3.SetActive(false); // 設定パネル3を非表示

        Time.timeScale = 0; // ゲームの進行を一時停止
    }

    // メニューを閉じる処理
    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false); // メニューを非表示
        closeKey.SetActive(false);  // 閉じるキーを非表示

        Time.timeScale = 1; // ゲームの進行を再開
    }
    // 設定パネル1を表示
    public void SettingPanel()
    {
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
    }

    // 設定パネル2を表示
    public void SettingPanel1()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(true);
        settingPanel3.SetActive(false);
    }

    // 設定パネル3を表示し、シーンをロード
    public void SettingPanel2()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(true);
        SceneManager.LoadScene("StartScene"); // 新しいシーンを読み込む
    }

    // キーボード設定パネルを表示
    public void KeyBoardPanel()
    {
        keyBoard.SetActive(true);
        gamePad.SetActive(false); // ゲームパッド設定を非表示
    }

    // ゲームパッド設定パネルを表示
    public void GamePadPanel()
    {
        keyBoard.SetActive(false); // キーボード設定を非表示
        gamePad.SetActive(true); // ゲームパッド設定を表示
    }

    // コントローラーの入力に基づいてUIを操作する
    public void Controller()
    {
        // 現在選択されているUIオブジェクトを取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        // メニューボタンが押された場合
        if (isMenuButton == true)
        {
            menu.SetActive(true);
            settingPanel1.SetActive(true);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(false);
            Time.timeScale = 0; // ゲームを一時停止
        }
        // Bボタンが押された場合（メニューを閉じる）
        else if (isBButton == true)
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false); // すべてのパネルを非表示
            }

            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false); // カーソルを非表示

            Time.timeScale = 1; // ゲームを再開
        }

        // 各UIオブジェクトに応じて設定パネルを切り替える処理
        if (selectedGameObject == settingButton)
        {
            settingPanel1.SetActive(true);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(false);
            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
        }
        else if (selectedGameObject == qperationExplanationButton)
        {
            settingPanel1.SetActive(false);
            settingPanel2.SetActive(true);
            settingPanel3.SetActive(false);

            keyBoard.SetActive(true); // キーボード設定を表示
            keyBoardMoveSettingSelect.SetActive(false); // キーボード移動設定を非表示
            gamePadMoveSettingSelect.SetActive(false); // ゲームパッド移動設定を非表示
            gamePad.SetActive(false); // ゲームパッド設定を非表示
        }
        else if (selectedGameObject == backTitleButton)
        {
            settingPanel1.SetActive(false);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(true);
        }
        // その他のUIオブジェクトに応じて設定を切り替える処理（スライダーやボタンの選択）
        else if (selectedGameObject == keyBoardButton)
        {
            keyBoardMoveSettingSelect.SetActive(true);
            gamePadMoveSettingSelect.SetActive(false);
            keyBoard.SetActive(true);
            gamePad.SetActive(false);
        }
        else if (selectedGameObject == gamePadButton)
        {
            keyBoardMoveSettingSelect.SetActive(false);
            gamePadMoveSettingSelect.SetActive(true);
            keyBoard.SetActive(false);
            gamePad.SetActive(true);
        }
        else if (selectedGameObject == BGMSliderGameObject)
        {
            Cursor[0].SetActive(true);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == SESliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(true);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == MicSliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(true);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == MouseSliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(true);
        }
        // 何も選択されていない場合、settingButtonにフォーカスを当てる
        else if (selectedGameObject == null)
        {
            // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(settingButton);
        }
    }

    public void SetBGM(float volume2)
    {
        audioMixer.SetFloat("BGM", volume2);
    }

    public void SetSE(float volume3)
    {
        audioMixer.SetFloat("SE", volume3);
    }

    public void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = MicSlider.value;
    }

    public void SetMouse(float level1)
    {
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value * 50;
    }
}