using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class UIMouseKeybord : MonoBehaviour
{
    private UIInputActions _uiInputActions; //Inputsystem取得

    //設定画面を表示、BGM、SE、マウス感度、マイク、コントローラー説明、キーボード説明、タイトルに戻る画面を表示
    [SerializeField] GameObject menyu, settingPanel1, settingPanel2, settingPanel3;

    //ゲーム内設定を変更する画面へ遷移するボタン、操作説明画面へ遷移するボタン、タイトル画面へ遷移するボタン
    [SerializeField] GameObject settingButton, operationExplanationButton,titleButton;
    Image imageSettingButton, imageOperationExplanationButton, imageTitleButton;

    //ゲームシーンの文字、コントローラー説明画面、キーボードの文字、キーボード説明画面
    [SerializeField] GameObject gamePadSettingButton, controllerUI, keyBoardSettingButton, keyboardUI;
    Image imageGamePadSettingButton, imageKeyBoardSettingButton;

    //マイクスライダー、BGMスライダー、SEスライダー、マウススライダー
    [SerializeField] Slider micSlider, bgmSlider,seSlider, mouseSlider; 　

    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] GameObject micObject;

    public float volume, level1, volume2, volume3;
    public CinemachineFreeLook VCamera;
    public float ButtonCount;
    public bool ButtonON;

    private Gamepad gamepad;

    float isStartButton;//メニュー画面の表示　ON＝１、OFF＝０；
    float mainSelectCount;//コントローラーのメニュー画面移動

    //UI選択時の色
    Color whiteColor = Color.white;
    Color yellowColor = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        menyu.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);

        imageSettingButton = settingButton.GetComponent<Image>();
        imageOperationExplanationButton = operationExplanationButton.GetComponent<Image>();
        imageTitleButton = titleButton.GetComponent<Image>();

        imageGamePadSettingButton = gamePadSettingButton.GetComponent<Image>();
        imageKeyBoardSettingButton = keyBoardSettingButton.GetComponent<Image>();

        //InputSystemのインスタンス化
        _uiInputActions = new UIInputActions();
        _uiInputActions.Enable();

        // 最初に接続されているゲームパッドを取得
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("ゲームパッドが接続されていません。");
        }

    }

    // Update is called once per frame
    void Update()
    {
        OnSettingMenu();
    }

    void SetBGM(float volume2)
    {
        _audioMixer.SetFloat("BGM", volume2);
    }

    void SetSE(float volume3)
    {
        _audioMixer.SetFloat("SE", volume3);
    }

    void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = micSlider.value;
    }

    void SetMouse(float level1)
    {
        VCamera.m_YAxis.m_MaxSpeed = mouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = mouseSlider.value * 50;
    }

    //マウス・キーボード設定
    #region
    //歯車マークを押した時
    public void OnSettingMenuButton()
    {
        menyu.SetActive(true);
        imageSettingButton.color = yellowColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnSettingButton()
    {
        imageSettingButton.color = yellowColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
    }

    public void OnOperationExplanationButton()
    {
        imageSettingButton.color = whiteColor;
        imageOperationExplanationButton.color = yellowColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(false);
        settingPanel2.SetActive(true);
        settingPanel3.SetActive(false);

        imageGamePadSettingButton.color = whiteColor;
        imageKeyBoardSettingButton.color = yellowColor;
        controllerUI.SetActive(false);
        keyboardUI.SetActive(true);
    }
    
    public void OnimageTitleButton()
    {
        imageSettingButton.color = whiteColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = yellowColor;

        settingPanel1.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(true);
    }

    public void OnGamePadSettingButton()
    {
        imageGamePadSettingButton.color = yellowColor;
        imageKeyBoardSettingButton.color=whiteColor;

        controllerUI.SetActive(true);
        keyboardUI.SetActive(false);
    }

    public void OnKeyBoardSettingButton()
    {
        imageGamePadSettingButton.color = whiteColor;
        imageKeyBoardSettingButton.color = yellowColor;

        controllerUI.SetActive(false);
        keyboardUI.SetActive(true);

    }

    public void closeKeyBButton()
    {
        menyu.SetActive(false);
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

    //コントローラー設定
    #region

    public void OnSettingMenu()
    {
        if (_uiInputActions.ControllerUI.StartButton.triggered) 
        {
            if (isStartButton == 0)
            {
                menyu.SetActive(true);
                imageSettingButton.color = yellowColor;
                imageOperationExplanationButton.color = whiteColor;
                imageTitleButton.color = whiteColor;
                settingPanel1.SetActive(true);
                Time.timeScale = 0;
                isStartButton = 1;
            }
            else if(isStartButton == 1)
            {
                menyu.SetActive(false);
                settingPanel1.SetActive(true);
                settingPanel2.SetActive(false);
                settingPanel3.SetActive(false);
                Time.timeScale = 1;
                isStartButton = 0;
            }
        }
    }

    public void OnMainSelect()
    {
       
    }



    #endregion
}

