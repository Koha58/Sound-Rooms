using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Memo2 : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingMenu;
    public GameObject explanationMenu;
    public GameObject goTitleSelectParent;

    public Slider bgmSlider;
    public Slider seSlider;
    public Slider micSlider;
    public Slider mouseSlider;

    public GameObject keyboardPanel;
    public GameObject gameControllerPanel;

    private bool isMenuOpen = false;
    private bool isInSettingMenu = false;
    private bool isInExplanationMenu = false;

    // Input Action Assetの参照
    [SerializeField] private InputActionAsset inputActions;

    private InputAction openSettingAction;
    private InputAction moveAction;
    private InputAction backAction;
    private InputAction decisionAction;
    private InputAction sliderAction;

    void Start()
    {
        // メニューの初期状態設定
        settingMenu.SetActive(false);
        explanationMenu.SetActive(false);
        goTitleSelectParent.SetActive(false);
    }

    void OnEnable()
    {
        // InputActionAssetからUIアクションを取得
        var uiMap = inputActions.FindActionMap("UI");

        openSettingAction = uiMap.FindAction("OpenSetting");
        //moveAction = uiMap.FindAction("Move");
        backAction = uiMap.FindAction("Return");
        decisionAction = uiMap.FindAction("Decision");
        //sliderAction = uiMap.FindAction("Slider");

        // アクションの有効化
        openSettingAction.Enable();
        moveAction.Enable();
        backAction.Enable();
        decisionAction.Enable();
        sliderAction.Enable();
    }

    void OnDisable()
    {
        // アクションの無効化
        openSettingAction.Disable();
        moveAction.Disable();
        backAction.Disable();
        decisionAction.Disable();
        sliderAction.Disable();
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // メニューを開く（コントローラーまたはキーボードで選択）
        if (openSettingAction.triggered)
        {
            ToggleMenu();
        }

        // メニュー内での操作
        if (isInSettingMenu)
        {
            HandleSettingMenu();
        }

        if (isInExplanationMenu)
        {
            HandleExplanationMenu();
        }

        // 戻るボタン
        if (backAction.triggered)
        {
            GoBack();
        }

        // 決定ボタン（Aボタンなどで選択）
        if (decisionAction.triggered)
        {
            // タイトルに戻る処理
            if (isInSettingMenu || isInExplanationMenu)
            {
                GoToTitleSelect();
            }
        }
    }

    // メニューの開閉
    private void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        mainMenu.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            settingMenu.SetActive(false);
            explanationMenu.SetActive(false);
            goTitleSelectParent.SetActive(true);
        }
        else
        {
            goTitleSelectParent.SetActive(false);
        }
    }

    // Settingメニューの操作
    private void HandleSettingMenu()
    {
        // スライダー操作 (Aボタンでスライダーを動かす)
        if (sliderAction.triggered)
        {
            // スライダー操作のロジック（ボタンで値を増やす）
            if (bgmSlider.interactable)
                bgmSlider.value += 0.1f;
            if (seSlider.interactable)
                seSlider.value += 0.1f;
            if (micSlider.interactable)
                micSlider.value += 0.1f;
            if (mouseSlider.interactable)
                mouseSlider.value += 0.1f;
        }
    }

    // Explanationメニューの操作
    private void HandleExplanationMenu()
    {
        Vector2 stickInput = moveAction.ReadValue<Vector2>();

        // スティックの横移動でパネルを切り替え
        if (stickInput.x > 0.5f) // GameControllerに移動
        {
            keyboardPanel.SetActive(false);
            gameControllerPanel.SetActive(true);
        }
        else if (stickInput.x < -0.5f) // Keyboardに移動
        {
            gameControllerPanel.SetActive(false);
            keyboardPanel.SetActive(true);
        }
    }

    // Backボタンでメニューに戻る
    private void GoBack()
    {
        if (isInSettingMenu)
        {
            settingMenu.SetActive(false);
            isInSettingMenu = false;
            mainMenu.SetActive(true);
        }
        else if (isInExplanationMenu)
        {
            explanationMenu.SetActive(false);
            isInExplanationMenu = false;
            mainMenu.SetActive(true);
        }
    }

    // タイトルに戻る処理
    private void GoToTitleSelect()
    {
        // タイトル選択画面へ遷移（GoTitleSelectParentを有効にする）
        goTitleSelectParent.SetActive(true);
        mainMenu.SetActive(false);
    }
}
