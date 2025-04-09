using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// ゲーム中に設定（BGMやSE、操作説明など）を制御するスクリプト
/// </summary>
public class UIController : MonoBehaviour
{
    // 定数の定義
    private const int CursorBGMIndex = 0; // BGMのカーソルインデックス
    private const int CursorSEIndex = 1; // SEのカーソルインデックス
    private const int CursorMicIndex = 2; // マイクのカーソルインデックス
    private const int CursorMouseIndex = 3; // マウスのカーソルインデックス

    private const float MouseMaxSpeedDivisor = 50f; // Y軸のスピード設定の割り算に使う定数
    private const float MouseMaxSpeedMultiplier = 50f; // X軸のスピード設定の掛け算に使う定数

    private const float TimeScalePaused = 0f;  // ゲーム進行を一時停止するための定数
    private const float TimeScaleRunning = 1f; // ゲーム進行を再開するための定数

    // フィールドの定義（UI要素）
    [SerializeField] GameObject originSettingButton, menu, settingPanel, explanationPanel, goGamePanel, goTitlePanel;
    // originSettingButton: 設定画面に移動するボタン
    // menu: メニューパネル（設定や操作説明などの画面を含む）
    // settingPanel: 設定画面（音量調整などが行えるパネル）
    // explanationPanel: 操作説明画面（ゲームの操作方法やヒントを表示）
    // goGamePanel: ゲームに戻るパネル
    // goTitlePanel: タイトルに戻るパネル（タイトル画面に戻るための確認パネル）

    [SerializeField] GameObject settingButton, operationExplanationButton, gameButton, backTitleButton;
    // settingButton: 設定画面に移動するためのボタン
    // operationExplanationButton: 操作説明画面に移動するためのボタン
    // gemeButton: ゲーム画面に戻るためのボタン
    // backTitleButton: タイトル画面に戻るためのボタン

    [SerializeField] GameObject keyBoardMoveSettingSelect, gamePadMoveSettingSelect, keyBoardButton, gamePadButton, keyBoard, gamePad;
    // keyBoardMoveSettingSelect: キーボード移動設定を選択するためのUI要素
    // gamePadMoveSettingSelect: ゲームパッド移動設定を選択するためのUI要素
    // keyBoardButton: キーボード設定を選択するためのボタン
    // gamePadButton: ゲームパッド設定を選択するためのボタン
    // keyBoard: キーボード設定用のパネル
    // gamePad: ゲームパッド設定用のパネル

    [SerializeField] GameObject MicSliderGameObject, BGMSliderGameObject, SESliderGameObject, MouseSliderGameObject, switchButton, backButton;
    // MicSliderGameObject: マイク音量を調整するためのスライダー
    // BGMSliderGameObject: BGM音量を調整するためのスライダー
    // SESliderGameObject: SE音量を調整するためのスライダー
    // MouseSliderGameObject: マウス感度を調整するためのスライダー
    // switchButton: コントローラーの時に表示させるボタン/ボタンA/各スライダーの変更を開始するときに使用する
    // backButton:コントローラーの時に表示させるボタン/ボタンB/各スライダーの変更を決定・終了するときに使用する

    [SerializeField] GameObject[] Cursor;
    // Cursor: 複数のカーソル（選択中の項目に表示される）を保持する配列

    [SerializeField] GameObject[] parameterCursor;
    // parameterCursor: 操作中のスライダーのカーソルを保持する配列

    // 各種スライダー
    [SerializeField] Slider MicSlider, BGMSlider, SESlider, MouseSlider;

    // その他の必要なオブジェクト
    [SerializeField] AudioMixer audioMixer;  // オーディオミキサー
    [SerializeField] GameObject micObject;    // マイクオブジェクト
    public CinemachineFreeLook VCamera;       // Cinemachineカメラ

    // 入力管理
    private GameInputSystem inputActions;     // 入力管理システム
    private Vector2 navigateInput;            // 移動の入力（2Dベクトル）
    private bool isMenuButton;                // メニューボタンが押されているか
    private bool isBButton;                   // Bボタンが押されているか
    private bool isAButton;                   // Aボタンが押されているか

    // パネル管理用リスト（パネルの表示非表示を管理する）
    private List<GameObject> panels;

    // デバイス（Xbox/Keyboard）のチェックフラグ
    bool deviceCheck;

    private void Awake()
    {
        // Input Systemのインスタンスを作成
        // GameInputSystemは、ゲーム内での入力管理を行うクラス
        inputActions = new GameInputSystem();

        // メニュー画面でのスティック操作を監視
        // スティック操作が行われると、navigateInputにVector2の値が設定される
        inputActions.UI.Navigate.performed += ctx => navigateInput = ctx.ReadValue<Vector2>();
        inputActions.UI.Navigate.canceled += ctx => navigateInput = Vector2.zero; // スティック操作がキャンセルされたときは、navigateInputをゼロにリセット

        // メニューボタンの入力を監視
        // メニューボタンが押された場合、isMenuButtonがtrueに設定される
        inputActions.UI.MenuButton.performed += ctx => isMenuButton = true;
        inputActions.UI.MenuButton.canceled += ctx => isMenuButton = false; // メニューボタンが離されたとき、isMenuButtonをfalseにリセット

        // Bボタンの入力を監視
        // Bボタンが押された場合、isBButtonがtrueに設定される
        inputActions.UI.BButton.performed += ctx => isBButton = true;
        inputActions.UI.BButton.canceled += ctx => isBButton = false;

        // Aボタンの入力を監視
        // Aボタンが押された場合、isBButtonがtrueに設定される
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
        panels = new List<GameObject> { menu, settingPanel, explanationPanel , goGamePanel, goTitlePanel };

        // 全てのパネルを非表示にする
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        keyBoardMoveSettingSelect.SetActive(true);
        gamePadMoveSettingSelect.SetActive(false);
    }

    // Update is called once per frame
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

        // 入力デバイスがXboxの場合の処理
        if (deviceCheck)
        {
            Controller();// コントローラーの入力処理を管理
        }
        
    }

    // メニューを表示する処理
    public void Menu(GameObject menuPanel)
    {
        menuPanel.SetActive(true); // メニューパネルを表示
        switchButton.SetActive(false);  // Aボタンを非表示
        backButton.SetActive(false);  //Bボタンを非表示
        settingPanel.SetActive(true); // 設定パネル1を表示
        explanationPanel.SetActive(false); // 設定パネル2を非表示
        goGamePanel.SetActive(false);
        goTitlePanel.SetActive(false); // 設定パネル3を非表示
        for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
        for (int i = 0; i < parameterCursor.Length; i++) parameterCursor[i].SetActive(false);

        Time.timeScale = TimeScalePaused; // ゲームの進行を一時停止
    }

    // メニューを閉じる処理
    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false); // メニューを非表示

        Time.timeScale = TimeScaleRunning; // ゲームの進行を再開
    }
    // 設定画面を表示
    public void SettingPanel()
    {
        settingPanel.SetActive(true);
        switchButton.SetActive(false);  // Aボタンを非表示
        backButton.SetActive(false);  //Bボタンを非表示
        explanationPanel.SetActive(false);
        goGamePanel.SetActive(false);
        goTitlePanel.SetActive(false);
    }

    // 操作説明画面を表示
    public void ExplanationPanel()
    {
        settingPanel.SetActive(false);
        explanationPanel.SetActive(true);
        goGamePanel.SetActive(false);
        goTitlePanel.SetActive(false);
    }

    // キーボード用の操作説明画面を表示
    public void KeyboardControl()
    {
        keyBoardMoveSettingSelect.SetActive(true);
        gamePadMoveSettingSelect.SetActive(false);
        keyBoard.SetActive(true);
        gamePad.SetActive(false);
    }

    // ゲームパッド用の操作説明画面を表示
    public void GamepadControl()
    {
        keyBoardMoveSettingSelect.SetActive(false);
        gamePadMoveSettingSelect.SetActive(true);
        keyBoard.SetActive(false);
        gamePad.SetActive(true);
    }

    // タイトルに戻るを表示し、シーンをロード
    public void GoTitlePanel()
    {
        settingPanel.SetActive(false);
        explanationPanel.SetActive(false);
        goGamePanel.SetActive(false);
        goTitlePanel.SetActive(true);
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
            settingPanel.SetActive(true);
            explanationPanel.SetActive(false);
            goGamePanel.SetActive(false);
            goTitlePanel.SetActive(false);
            Time.timeScale = TimeScalePaused; // ゲームの進行を一時停止
        }


        // 各UIオブジェクトに応じて設定パネルを切り替える処理
        if (selectedGameObject == settingButton)
        {
            settingPanel.SetActive(true);
            explanationPanel.SetActive(false);
            goGamePanel.SetActive(false);
            goTitlePanel.SetActive(false);
            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
            for (int i = 0; i < parameterCursor.Length; i++) parameterCursor[i].SetActive(false);
        }
        else if (selectedGameObject == operationExplanationButton)
        {
            settingPanel.SetActive(false);
            explanationPanel.SetActive(true);
            goGamePanel.SetActive(false);
            goTitlePanel.SetActive(false);

            keyBoard.SetActive(true); // キーボード設定を表示
            keyBoardMoveSettingSelect.SetActive(false); // キーボード移動設定を非表示
            gamePadMoveSettingSelect.SetActive(false); // ゲームパッド移動設定を非表示
            gamePad.SetActive(false); // ゲームパッド設定を非表示
        }
        else if (selectedGameObject == gameButton)
        {    
            settingPanel.SetActive(false);
            explanationPanel.SetActive(false);
            goGamePanel.SetActive(true);
            goTitlePanel.SetActive(false);
            if (isAButton)
            {
                foreach (GameObject panel in panels)
                {
                    panel.SetActive(false); // すべてのパネルを非表示
                }

                Time.timeScale = TimeScaleRunning; // ゲームの進行を再開
            }
            
        }
        else if (selectedGameObject == backTitleButton)
        {
            settingPanel.SetActive(false);
            explanationPanel.SetActive(false);
            goGamePanel.SetActive(false);
            goTitlePanel.SetActive(true);
        }
        // 選択されているスライダーに応じて設定を切り替える処理
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
            Cursor[CursorBGMIndex].SetActive(true); // BGMのカーソルを表示
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(false);

            if(isAButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(true); // BGMのカーソルを表示
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(false);
                
                isBButton = false;
            }
            else if(isBButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false); // BGMのカーソルを非表示
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(false);

                inputActions.UI.Navigate.performed += ctx => navigateInput = ctx.ReadValue<Vector2>();
                isAButton = false;
            }
        }
        else if (selectedGameObject == SESliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(true); // SEのカーソルを表示
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(false);

            if (isAButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(true); // SEのカーソルを表示
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(false);

                isBButton = false;
            }
            else if (isBButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(false); // SEのカーソルを非表示
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(false);

                isAButton = false;
            }
        }
        else if (selectedGameObject == MicSliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(true); // マイクのカーソルを表示
            Cursor[CursorMouseIndex].SetActive(false);

            if (isAButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(true); // マイクのカーソルを表示
                parameterCursor[CursorMouseIndex].SetActive(false);

                isBButton = false;
            }
            else if (isBButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(false); // マイクのカーソルを非表示
                parameterCursor[CursorMouseIndex].SetActive(false);

                isAButton = false;
            }
        }
        else if (selectedGameObject == MouseSliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(true); // マウスのカーソルを表示

            if (isAButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(true); // マウスのカーソルを表示

                isBButton = false;
            }
            else if (isBButton)
            {
                parameterCursor[CursorBGMIndex].SetActive(false);
                parameterCursor[CursorSEIndex].SetActive(false);
                parameterCursor[CursorMicIndex].SetActive(false);
                parameterCursor[CursorMouseIndex].SetActive(false); // マウスのカーソルを非表示

                isAButton = false;
            }
        }
        // 何も選択されていない場合、settingButtonにフォーカスを当てる
        else if (selectedGameObject == null)
        {
            // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(settingButton);
        }

        if (selectedGameObject == BGMSliderGameObject || SESliderGameObject || MicSliderGameObject || MouseSliderGameObject)
        {
            switchButton.SetActive(true);  // Aボタンを表示
            backButton.SetActive(true);  //Bボタンを表示
        }

        // 何も選択されていない場合やコントローラー操作でない場合
        if (selectedGameObject == null ||
            (selectedGameObject != BGMSliderGameObject && selectedGameObject != SESliderGameObject &&
             selectedGameObject != MicSliderGameObject && selectedGameObject != MouseSliderGameObject))
        {
            // コントローラー操作でない場合は、ボタンを非表示にする
            switchButton.SetActive(false);  // Aボタンを非表示
            backButton.SetActive(false);  // Bボタンを非表示
        }
    }

    // BGMの音量を設定するメソッド
    // 引数として渡されたbgmVolumeを使用して、オーディオミキサーでBGMの音量を調整する
    public void SetBGM(float bgmVolume)
    {
        // BGMの音量を設定
        audioMixer.SetFloat("BGM", bgmVolume); // オーディオミキサーで"BGM"の音量を設定
    }

    // SEの音量を設定するメソッド
    // 引数として渡されたseVolumeを使用して、オーディオミキサーでSEの音量を調整する
    public void SetSE(float seVolume)
    {
        // SEの音量を設定
        audioMixer.SetFloat("SE", seVolume); // オーディオミキサーで"SE"の音量を設定
    }

    // マイクの音量を設定するメソッド
    // 引数として渡されたmicVolumeを使用して、マイクの音量を調整する
    public void SetMic(float micVolume)
    {
        // マイク音量を設定
        AudioSource micAudioSource = micObject.GetComponent<AudioSource>(); // micObjectからAudioSourceコンポーネントを取得
        micAudioSource.volume = micVolume; // 引数のmicVolumeに基づいて、マイクの音量を設定
    }

    // マウス感度を設定するメソッド
    // 引数として渡されたmouseSensitivityを基に、CinemachineカメラのX軸とY軸の感度を調整する
    public void SetMouse(float mouseSensitivity)
    {
        // マウスの感度を設定（Y軸の移動速度）
        VCamera.m_YAxis.m_MaxSpeed = mouseSensitivity / MouseMaxSpeedDivisor; // Y軸の感度を設定（割り算を使用）

        // マウスの感度を設定（X軸の移動速度）
        VCamera.m_XAxis.m_MaxSpeed = mouseSensitivity * MouseMaxSpeedMultiplier; // X軸の感度を設定（掛け算を使用）
    }

}