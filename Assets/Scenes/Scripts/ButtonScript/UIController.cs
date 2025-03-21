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
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    //歯車ボタン（設定画面に行くボタン）,メニュー画面,設定画面,操作説明画面,タイトルに戻る画面
    [SerializeField] GameObject originSettingButton, menu, settingPanel1,settingPanel2,settingPanel3;

    //ゲーム内設定を変更する画面へ遷移するボタン,操作説明画面へ遷移するボタン,タイトル画面へ遷移するボタン
    [SerializeField] GameObject settingButton,qperationExplanationButton,backTitleButton;

    [SerializeField] GameObject keyBoardMoveSettingSelect, gamePadMoveSettingSelect, keyBoardButton, gamePadButton, keyBoard, gamePad;

    [SerializeField] GameObject MicSliderGameObject, BGMSliderGameObject, SESliderGameObject, MouseSliderGameObject, closeKey;

    [SerializeField] GameObject[] Cursor;

    //マイクスライダー,BGMスライダー,SEスライダー,マウススライダー
    [SerializeField] Slider MicSlider, BGMSlider, SESlider, MouseSlider;

    [SerializeField] AudioMixer audioMixer; //オーディオミキサー
    [SerializeField] GameObject micObject;　//
    public CinemachineFreeLook VCamera;     //
    private GameInputSystem inputActions;

    private Vector2 navigateInput;

    private bool isMenuButton;
    private bool isBButton;
    private bool isAButton;

    // パネル管理用リスト
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

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // パネルリストに追加
        panels = new List<GameObject> { menu, settingPanel1, settingPanel2, settingPanel3 };

        // 初期状態で全パネルを非表示
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

       for(int i=0;i<Cursor.Length;i++) Cursor[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    public void Menu(GameObject menuPanel)
    {
        menuPanel.SetActive(true);
        closeKey.SetActive(true);
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);

        Time.timeScale = 0;
    }

    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
        closeKey.SetActive(false);

        Time.timeScale = 1;
    }

    public void SettingPanel()
    {
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
    }
    public void SettingPanel1()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(true);
        settingPanel3.SetActive(false);
    }
    public void SettingPanel2()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(true);
        SceneManager.LoadScene("StartScene");
    }

    public void KeyBoardPanel()
    {
        keyBoard.SetActive(true);
        gamePad.SetActive(false);
    }

    public void GamePadPanel()
    {
        keyBoard.SetActive(false);
        gamePad.SetActive(true);
    }

    public void Controller()
    {
        // 選択中のUI取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (isMenuButton == true)
        {
            menu.SetActive(true);
            settingPanel1.SetActive(true);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(false);
            Time.timeScale = 0;
        }
        else if (isBButton==true)
        {
            // 初期状態で全パネルを非表示
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false);
            }

            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);

            Time.timeScale = 1;
        }

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

            keyBoard.SetActive(true);
            keyBoardMoveSettingSelect.SetActive(false);
            gamePadMoveSettingSelect.SetActive(false);
            gamePad.SetActive(false);
        }
        else if (selectedGameObject == backTitleButton)
        {
            settingPanel1.SetActive(false);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(true);
        }
        else if(selectedGameObject == keyBoardButton)
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
        else if(selectedGameObject == null)
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