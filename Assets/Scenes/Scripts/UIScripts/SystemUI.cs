using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions; // Input Systemのアクションアセット

    private InputAction openSettingAction; // 設定メニューを開くアクション
    private InputAction moveAction;        // メニューカーソルを上下に動かすアクション
    private InputAction backAction;        // 戻るアクション
    private InputAction decisionAction;    // 決定アクション

    // 設定メニューに関連するUIオブジェクト
    [SerializeField] private GameObject MainMenu;      // 設定メニュー全体
    [SerializeField] private Image buttonBui;          // BボタンUI（戻る）
    [SerializeField] private Image buttonAui;          // AボタンUI（決定）
    [SerializeField] private Image settingCursorUI;    // カーソル用画像

    [SerializeField] private GameObject KeyboardPanel;        // キーボード操作パネル
    [SerializeField] private GameObject GameControllerPanel;  // ゲームコントローラ操作パネル

    public List<GameObject> MenuSelects; // カーソルで選択するメニュー項目のリスト
    public List<GameObject> Menus;       // （未使用）別のメニュー要素用？
    public List<GameObject> cursor;
    public List<Slider> slider;

    // スライダーごとの変更量（たとえば、BGM 0.5f、SE 0.2f）
    [SerializeField] private List<float> sliderSteps;

    private int currentIndex = 0;        // 現在カーソルが指しているメニューのインデックス
    private float moveCooldown = 0.2f;   // カーソル移動のクールタイム（連打防止）
    private float moveTimer = 0f;

    private const float TimeScalePaused = 0f;   // メニュー表示中：ゲーム停止
    private const float TimeScaleRunning = 1f;  // メニュー非表示中：ゲーム再開

    private bool IsSettingActive;// メニューが開いているかどうかを表すフラグ

    private float settingToggleCooldown = 0.5f; // メニュー開閉のクールタイム
    private float settingToggleTimer = 0f;

    private bool isControllingSlider = false;
    private int currentSliderIndex = 0;

    private bool isInDeviceSelectMode = false; // パネル切り替えモード中
    private int devicePanelIndex = 0; // 0: KeyboardPanel, 1: GameControllerPanel

    // アクションの有効化
    private void OnEnable()
    {
        var uiMap = inputActions.FindActionMap("UI");

        openSettingAction = uiMap.FindAction("OpenSetting");
        moveAction = uiMap.FindAction("SelectMenu");
        backAction = uiMap.FindAction("Return");
        decisionAction = uiMap.FindAction("Decision");

        openSettingAction.Enable();
        moveAction.Enable();
        backAction.Enable();
        decisionAction.Enable();
    }

    // アクションの無効化
    private void OnDisable()
    {
        openSettingAction.Disable();
        moveAction.Disable();
        backAction.Disable();
        decisionAction.Disable();
    }

    private void Start()
    {
        MainMenu.SetActive(false);
        KeyboardPanel.SetActive(true);
        GameControllerPanel.SetActive(false);
        // ▼ カーソルの表示切り替え
        for (int i = 0; i < 4; i++)
        {
            cursor[i].SetActive(i == currentSliderIndex);
        }
        // MenuSelectに合わせてMenusを表示非表示にする
        for (int i = 1; i < 4; i++)
        {
            Menus[i].SetActive(i == currentIndex);
        }
        // ▼ カーソルの表示切り替え
        for (int i = 0; i < 4; i++)
        {
            cursor[i].SetActive(false);
        }
    }

    private void Update()
    {
        // クールタイムタイマーを減少
        if (settingToggleTimer > 0f)
        {
            settingToggleTimer -= Time.unscaledDeltaTime;
        }

        // メニューの開閉トグル（クールタイム中は無効）
        if (openSettingAction.WasPressedThisFrame() && settingToggleTimer <= 0f)
        {
            if (!IsSettingActive)
            {
                OpenSettingMenu();
            }
            else
            {
                CloseSettingMenu();
            }

            settingToggleTimer = settingToggleCooldown;
        }
        // メニューがアクティブなときのみ操作可能
        if (IsSettingActive)
        {
            moveTimer -= Time.unscaledDeltaTime;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // ▼ デバイス切り替えモード
            if (isInDeviceSelectMode)
            {
                // 左右入力でパネル切り替え
                if (moveInput.x > 0.5f && moveTimer <= 0f)
                {
                    devicePanelIndex = 1; // GameControllerPanel
                    UpdateDevicePanel();
                    moveTimer = moveCooldown;
                }
                else if (moveInput.x < -0.5f && moveTimer <= 0f)
                {
                    devicePanelIndex = 0; // KeyboardPanel
                    UpdateDevicePanel();
                    moveTimer = moveCooldown;
                }

                // Bで戻る
                if (backAction.WasPressedThisFrame())
                {
                    isInDeviceSelectMode = false;
                    KeyboardPanel.SetActive(true);
                    GameControllerPanel.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(MenuSelects[currentIndex]);
                }
            }
            // ▼ スライダー操作モード
            if (isControllingSlider)
            {
                // ▼ カーソルの表示切り替え
                for (int i = 0; i < cursor.Count; i++)
                {
                    cursor[i].SetActive(i == currentSliderIndex);
                }

                // スライダーの値を変更
                if (moveInput.x > 0.5f && moveTimer <= 0f)
                {
                    var targetSlider = slider[currentSliderIndex];
                    float step = sliderSteps.Count > currentSliderIndex ? sliderSteps[currentSliderIndex] : 0.1f; // 安全対策
                    targetSlider.value = Mathf.Clamp(targetSlider.value + step, targetSlider.minValue, targetSlider.maxValue);
                    moveTimer = moveCooldown;
                }
                else if (moveInput.x < -0.5f && moveTimer <= 0f)
                {
                    var targetSlider = slider[currentSliderIndex];
                    float step = sliderSteps.Count > currentSliderIndex ? sliderSteps[currentSliderIndex] : 0.1f;
                    targetSlider.value = Mathf.Clamp(targetSlider.value - step, targetSlider.minValue, targetSlider.maxValue);
                    moveTimer = moveCooldown;
                }

                // 上下でスライダーの切り替え
                if (moveInput.y > 0.5f && moveTimer <= 0f)
                {
                    currentSliderIndex = (currentSliderIndex - 1 + slider.Count) % slider.Count;
                    EventSystem.current.SetSelectedGameObject(slider[currentSliderIndex].gameObject);
                    moveTimer = moveCooldown;
                }
                else if (moveInput.y < -0.5f && moveTimer <= 0f)
                {
                    currentSliderIndex = (currentSliderIndex + 1) % slider.Count;
                    EventSystem.current.SetSelectedGameObject(slider[currentSliderIndex].gameObject);
                    moveTimer = moveCooldown;
                }

                // Bでスライダー操作からメニュー戻る
                if (backAction.WasPressedThisFrame())
                {
                    isControllingSlider = false;

                    // ▼ スライダー用カーソルをすべて非表示にする
                    foreach (var c in cursor)
                    {
                        c.SetActive(false);
                    }

                    EventSystem.current.SetSelectedGameObject(MenuSelects[currentIndex]);
                }
            }
            // ▼ メニューモード
            else
            {
                // 上下でメニュー移動
                if (moveInput.y > 0.5f && moveTimer <= 0f)
                {
                    MoveCursor(-1);
                }
                else if (moveInput.y < -0.5f && moveTimer <= 0f)
                {
                    MoveCursor(1);
                }

                // 決定で処理
                if (decisionAction.WasPressedThisFrame())
                {
                    // ▼ MenuSelects[1]が選ばれていたらパネル切り替えモードへ
                    if (currentIndex == 1)
                    {
                        isInDeviceSelectMode = true;
                        devicePanelIndex = 0;
                        UpdateDevicePanel();
                        EventSystem.current.SetSelectedGameObject(null); // UIフォーカス解除
                    }
                    else if (currentIndex == 0 && slider.Count > 0)
                    {
                        isControllingSlider = true;
                        currentSliderIndex = 0;
                        EventSystem.current.SetSelectedGameObject(slider[currentSliderIndex].gameObject);
                    }
                    else
                    {
                        PressCurrentButton();
                    }
                }

                // Bボタン
                if (backAction.WasPressedThisFrame())
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }

                // カーソルUIの位置更新
                if (settingCursorUI != null && currentIndex >= 0 && currentIndex < MenuSelects.Count)
                {
                    settingCursorUI.rectTransform.position = MenuSelects[currentIndex].GetComponent<RectTransform>().position;
                }
            }
        }
    }

    // カーソルを上下に移動させる処理
    private void MoveCursor(int direction)
    {
        currentIndex += direction;

        // インデックスが範囲外にならないようループ
        if (currentIndex < 0) currentIndex = MenuSelects.Count - 1;
        else if (currentIndex >= MenuSelects.Count) currentIndex = 0;

        // EventSystemで選択対象を更新
        EventSystem.current.SetSelectedGameObject(MenuSelects[currentIndex]);
        moveTimer = moveCooldown;

        // MenuSelectに合わせてMenusを表示非表示にする
        for (int i = 0; i < Menus.Count; i++)
        {
            Menus[i].SetActive(i == currentIndex);
        }
    }

    // 現在選択されているボタンのクリック処理を呼び出す
    private void PressCurrentButton()
    {
        var selected = MenuSelects[currentIndex].GetComponent<Button>();
        if (selected != null)
        {
            selected.onClick.Invoke();
        }
    }

    // 設定メニューを開く処理
    public void OpenSettingMenu()
    {
        MainMenu.SetActive(true);
        Time.timeScale = TimeScalePaused;
        IsSettingActive = true;
        currentIndex = 0; // 最初の項目にカーソルを合わせる
        EventSystem.current.SetSelectedGameObject(MenuSelects[currentIndex]);
    }

    // 設定メニューを閉じる処理
    public void CloseSettingMenu()
    {
        MainMenu.SetActive(false);
        Time.timeScale = TimeScaleRunning;
        IsSettingActive = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateDevicePanel()
    {
        KeyboardPanel.SetActive(devicePanelIndex == 0);
        GameControllerPanel.SetActive(devicePanelIndex == 1);
    }

    // タイトルに戻るを表示し、シーンをロード
    public void GoTitlePanel()
    {
        CloseSettingMenu();
        SceneManager.LoadScene("StartScene"); // 新しいシーンを読み込む
    }
}