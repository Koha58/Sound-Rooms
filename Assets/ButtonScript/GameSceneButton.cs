using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class GameSceneButton : MonoBehaviour
{
    //設定背景画面
    [SerializeField] GameObject BackGround;
    //操作設定画面
    [SerializeField] GameObject SettingMenu;
    //タイトルの「設定」文字
    [SerializeField] GameObject SettingTitle;
    //各種設定画面背景
    [SerializeField] GameObject SettingBack;
    //設定選択目印
    [SerializeField] GameObject Select;
    //設定選択目印
    [SerializeField] GameObject Select1;
    //設定選択目印
    [SerializeField] GameObject Select2;
    //操作設定画面カーソル
    [SerializeField] GameObject MenuCursor;
    //操作設定画面カーソル
    [SerializeField] GameObject MenuCursor1;
    //操作設定画面カーソル
    [SerializeField] GameObject MenuCursor2;
    //操作設定画面カーソル
    [SerializeField] GameObject MenuCursor3;
    //設定を閉じるボタン
    [SerializeField] GameObject CloseButton;
    //設定を閉じるボタン(コントローラー)
    [SerializeField] GameObject CloseButtonB;
    //ゲーム内設定を変更する画面へ遷移するボタン
    [SerializeField] GameObject SettingButton;
    //操作説明画面へ遷移するボタン
    [SerializeField] GameObject OperationExplanationButton;
    //タイトル画面へ遷移するボタン
    [SerializeField] GameObject TitleButton;
    //操作説明画面上の白い線
    [SerializeField] GameObject WhiteLine;
    //操作設定画面の「キーボード」文字ボタン
    [SerializeField] GameObject KeyboardcharaButton;
    //操作設定画面の「ゲームパッド」文字ボタン
    [SerializeField] GameObject GamepadcharaButton;
    //操作設定画面のコントローラー設定画面
    [SerializeField] GameObject ControllerSetting;
    //操作設定画面のキーボード設定画面
    [SerializeField] GameObject KeyboardSetting;
    //操作説明画面内選択目印
    [SerializeField] GameObject OperationExplanationSelect;
    //操作説明画面内選択目印
    [SerializeField] GameObject OperationExplanationSelect1;
    //マイクスライダー
    [SerializeField] Slider MicSlider;
    //BGMスライダー
    [SerializeField] Slider BGMSlider;
    //SEスライダー
    [SerializeField] Slider SESlider;
    //マウススライダー
    [SerializeField] Slider MouseSlider;

    [SerializeField]GameObject decisionA;

    [SerializeField] GameObject Cursor;

    //カーソル(コントローラー用)
    // [SerializeField] GameObject Cursor;
    bool deviceCheck;

    private float MainSettingOriginPositionX = -773;
    private float MainSettingOriginPositionY = 317;
    private float MainSettingOriginPositionZ = 0;

    private float MainSettingChangePositionY;

    private float OperationExplanationOriginPositionX = -236;
    private float OperationExplanationOriginPositionY = 317;
    private float OperationExplanationOriginPositionZ = 0;

    private float OperationExplanationChangePositionX;

    private float CursorOriginPositionX = 627;
    private float CursorOriginPositionY = 73;
    private float CursorOriginPositionZ = 0;

    private float CursorChangePositionY;

    int MainSelectPosition = 0;

    int CursorPosition = 0;

    bool Continue;

    bool ExplainController;

    //メインの設定をいじっているか
    bool Main;

    bool MainSelectPositionSelect;

    bool NoUI;

    bool ExplanationSelect;
    float ExplanationSelectCount;

    float SelectCount;

    bool NotSelect;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;
    public float level1;
    public float volume2;
    public float volume3;
    public CinemachineFreeLook VCamera;
    public float ButtonCount;
    public bool ButtonON;

    bool Reset;
    bool MenuSelect;
    float MenuSelectCount;

    bool Reset1;
    bool ExplainSelect;
    float ExplainSelectCount;

    bool MenuSelectIN;
    bool MenuSelectOUT;
    float MenuSelectOUTCount;

    float Timer;
    bool TimeON;

    float Timervh;
    bool VH;
    int i = 0;

    [SerializeField] GameObject[] RBLB;

    // Start is called before the first frame update
    void Start()
    {
        BackGround.GetComponent<Image>().enabled = false;

        SettingMenu.GetComponent<Image>().enabled = false;

        SettingTitle.GetComponent<Image>().enabled = false;

        SettingBack.GetComponent<Image>().enabled = false;

        Select.GetComponent<Image>().enabled = false;

        Select1.GetComponent<Image>().enabled = false;

        Select2.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;
        MenuCursor1.GetComponent<Image>().enabled = false;
        MenuCursor2.GetComponent<Image>().enabled = false;
        MenuCursor3.GetComponent<Image>().enabled = false;

        CloseButton.GetComponent<Image>().enabled = false;

        CloseButtonB.GetComponent<Image>().enabled = false;

        SettingButton.GetComponent<Image>().enabled = false;

        OperationExplanationButton.GetComponent<Image>().enabled = false;

        TitleButton.GetComponent<Image>().enabled = false;

        WhiteLine.GetComponent<Image>().enabled = false;

        KeyboardcharaButton.GetComponent<Image>().enabled = false;

        GamepadcharaButton.GetComponent<Image>().enabled = false;

        ControllerSetting.GetComponent<Image>().enabled = false;

        KeyboardSetting.GetComponent<Image>().enabled = false;

        OperationExplanationSelect.GetComponent<Image>().enabled = false;

        OperationExplanationSelect1.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);

        decisionA.gameObject.SetActive(false);

        Cursor.gameObject.SetActive(false);

        TutorialManager.ON = false;


        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        OperationExplanationSelectTransform.transform.localPosition = new Vector3(OperationExplanationOriginPositionX, OperationExplanationOriginPositionY, OperationExplanationOriginPositionZ);
        OperationExplanationChangePositionX = OperationExplanationOriginPositionX;

        MainSelectPosition = 0;
        CursorPosition = 0;
        Continue = false;
        ExplainController = false;
        Main = true;

        ExplanationSelectCount = 0;
        ExplanationSelect = false;

        SelectCount = 0;

        NoUI = false;
        ButtonON = false;
        volume += 0.1f;
        SelectCount = 0;

        RBLB[0].gameObject.SetActive(false);
        RBLB[1].gameObject.SetActive(false);
        RBLB[2].gameObject.SetActive(false);
        RBLB[3].gameObject.SetActive(false);
        RBLB[4].gameObject.SetActive(false);
        RBLB[5].gameObject.SetActive(false);
        RBLB[6].gameObject.SetActive(false);
        RBLB[7].gameObject.SetActive(false);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (NoUI == true)
        {
           Controller();
        }

        if (Input.GetKeyDown("joystick button 7"))//メニュー ボタン 
        {
            TutorialManager.ON=true;

            SettingButton.GetComponent<Image>().enabled = true;

            BackGround.GetComponent<Image>().enabled = true;

            SettingMenu.GetComponent<Image>().enabled = true;

            SettingTitle.GetComponent<Image>().enabled = true;

            SettingBack.GetComponent<Image>().enabled = true;

            Select.GetComponent<Image>().enabled = true;

            OperationExplanationButton.GetComponent<Image>().enabled = true;

            TitleButton.GetComponent<Image>().enabled = true;

            MicSlider.gameObject.SetActive(true);

            BGMSlider.gameObject.SetActive(true);

            SESlider.gameObject.SetActive(true);

            MouseSlider.gameObject.SetActive(true);

            if (deviceCheck)
            {
                CloseButtonB.GetComponent<Image>().enabled = true;
            }
            else
            {
                CloseButton.GetComponent<Image>().enabled = true;
            }

            MainSelectPosition = 0;
            CursorPosition = 0;

            MainSettingChangePositionY = MainSettingOriginPositionY;

            CursorChangePositionY = CursorOriginPositionY;

            Main = true;

            Time.timeScale = 0;

            NoUI = true;

            MainSelectPositionSelect = true;
        }
  

        if (Input.GetKeyDown("joystick button 1"))//B
        {
            BackGround.GetComponent<Image>().enabled = false;

            SettingMenu.GetComponent<Image>().enabled = false;

            SettingTitle.GetComponent<Image>().enabled = false;

            SettingBack.GetComponent<Image>().enabled = false;

            SettingButton.GetComponent<Image>().enabled = false;

            Select.GetComponent<Image>().enabled = false;
            Select1.GetComponent<Image>().enabled = false;
            Select2.GetComponent<Image>().enabled = false;

            MenuCursor.GetComponent<Image>().enabled = false;
            MenuCursor1.GetComponent<Image>().enabled = false;
            MenuCursor2.GetComponent<Image>().enabled = false;
            MenuCursor3.GetComponent<Image>().enabled = false;

            CloseButton.GetComponent<Image>().enabled = false;

            CloseButtonB.GetComponent<Image>().enabled = false;

            OperationExplanationButton.GetComponent<Image>().enabled = false;

            TitleButton.GetComponent<Image>().enabled = false;

            WhiteLine.GetComponent<Image>().enabled = false;

            KeyboardcharaButton.GetComponent<Image>().enabled = false;

            GamepadcharaButton.GetComponent<Image>().enabled = false;

            ControllerSetting.GetComponent<Image>().enabled = false;

            KeyboardSetting.GetComponent<Image>().enabled = false;

            OperationExplanationSelect.GetComponent<Image>().enabled = false;
            OperationExplanationSelect1.GetComponent<Image>().enabled = false;

            MicSlider.gameObject.SetActive(false);

            BGMSlider.gameObject.SetActive(false);

            SESlider.gameObject.SetActive(false);

            MouseSlider.gameObject.SetActive(false);
            TutorialManager.ON =false;
            //SettingBack.SetActive(false);
            Time.timeScale = 1;
            SelectCount = 0;
            NoUI = false;
        }

    }

    public void SettingButtonON()
    {
        Transform MainSettingSelectTransform = Select.transform;
        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
        MainSettingChangePositionY = MainSettingOriginPositionY;

        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        OperationExplanationSelectTransform.transform.localPosition = new Vector3(OperationExplanationOriginPositionX, OperationExplanationOriginPositionY, OperationExplanationOriginPositionZ);
        OperationExplanationChangePositionX = OperationExplanationOriginPositionX;

        BackGround.GetComponent<Image>().enabled = true;

        SettingMenu.GetComponent<Image>().enabled = true;

        SettingTitle.GetComponent<Image>().enabled = true;

        SettingBack.GetComponent<Image>().enabled = true;

        SettingButton.GetComponent<Image>().enabled = true;

        Select.GetComponent<Image>().enabled = true;

        OperationExplanationButton.GetComponent<Image>().enabled = true;

        TitleButton.GetComponent<Image>().enabled = true;

        MicSlider.gameObject.SetActive(true);

        BGMSlider.gameObject.SetActive(true);

        SESlider.gameObject.SetActive(true);

        MouseSlider.gameObject.SetActive(true);

        if (deviceCheck)
        {
            CloseButtonB.GetComponent<Image>().enabled = true;
        }
        else
        {
            CloseButton.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void CloseSetting()
    {
        BackGround.GetComponent<Image>().enabled = false;

        SettingMenu.GetComponent<Image>().enabled = false;

        SettingTitle.GetComponent<Image>().enabled = false;

        SettingBack.GetComponent<Image>().enabled = false;

        Select.GetComponent<Image>().enabled = false;

        SettingButton.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

        CloseButton.GetComponent<Image>().enabled = false;

        CloseButtonB.GetComponent<Image>().enabled = false;

        OperationExplanationButton.GetComponent<Image>().enabled = false;

        TitleButton.GetComponent<Image>().enabled = false;

        WhiteLine.GetComponent<Image>().enabled = false;

        KeyboardcharaButton.GetComponent<Image>().enabled = false;

        GamepadcharaButton.GetComponent<Image>().enabled = false;

        ControllerSetting.GetComponent<Image>().enabled = false;

        KeyboardSetting.GetComponent<Image>().enabled = false;

        OperationExplanationSelect.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);
        Time.timeScale = 1;
        //SettingBack.SetActive(true);

    }

    public void OnSettingMenuButton()
    {
        Transform MainSettingSelectTransform = Select.transform;

        SettingMenu.GetComponent<Image>().enabled = true;

        Select.GetComponent<Image>().enabled = true;

        SettingButton.GetComponent<Image>().enabled = true;

        MicSlider.gameObject.SetActive(true);

        BGMSlider.gameObject.SetActive(true);

        SESlider.gameObject.SetActive(true);

        MouseSlider.gameObject.SetActive(true);

        WhiteLine.GetComponent<Image>().enabled = false;

        KeyboardcharaButton.GetComponent<Image>().enabled = false;

        GamepadcharaButton.GetComponent<Image>().enabled = false;

        ControllerSetting.GetComponent<Image>().enabled = false;

        KeyboardSetting.GetComponent<Image>().enabled = false;

        OperationExplanationSelect.GetComponent<Image>().enabled = false;
        OperationExplanationSelect1.GetComponent<Image>().enabled = false;

        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);

        if (deviceCheck)
        {
            CloseButtonB.GetComponent<Image>().enabled = true;
        }
        else
        {
            CloseButton.GetComponent<Image>().enabled = true;
        }
        ButtonON = false;
        Time.timeScale = 0;
    }

    public void OnOperationExplanationButton()
    {
        Transform MainSettingSelectTransform = Select.transform;
        MainSettingChangePositionY = 212;
        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);

        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        OperationExplanationSelectTransform.transform.localPosition = new Vector3(OperationExplanationOriginPositionX, OperationExplanationOriginPositionY, OperationExplanationOriginPositionZ);

        Select.GetComponent<Image>().enabled = true;

        WhiteLine.GetComponent<Image>().enabled = true;

        KeyboardcharaButton.GetComponent<Image>().enabled = true;

        GamepadcharaButton.GetComponent<Image>().enabled = true;

        ControllerSetting.GetComponent<Image>().enabled = false;

        KeyboardSetting.GetComponent<Image>().enabled = true;

        OperationExplanationSelect.GetComponent<Image>().enabled = true;

        SettingMenu.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);

        if (deviceCheck)
        {
            CloseButtonB.GetComponent<Image>().enabled = true;
        }
        else
        {
            CloseButton.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void OnGamePadExplanationButton()
    {
        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        OperationExplanationChangePositionX = 22;
        OperationExplanationSelectTransform.transform.localPosition = new Vector3(OperationExplanationChangePositionX, OperationExplanationOriginPositionY, OperationExplanationOriginPositionZ);

        Select.GetComponent<Image>().enabled = true;

        WhiteLine.GetComponent<Image>().enabled = true;

        KeyboardcharaButton.GetComponent<Image>().enabled = true;

        GamepadcharaButton.GetComponent<Image>().enabled = true;

        ControllerSetting.GetComponent<Image>().enabled = true;

        KeyboardSetting.GetComponent<Image>().enabled = false;

        OperationExplanationSelect.GetComponent<Image>().enabled = true;

        SettingMenu.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);

        if (deviceCheck)
        {
            CloseButtonB.GetComponent<Image>().enabled = true;
        }
        else
        {
            CloseButton.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void BackTitleButton()
    {
        Time.timeScale = 1;
        Transform MainSettingSelectTransform = Select.transform;
        MainSettingChangePositionY = 113;
        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
        SceneManager.LoadScene("StartScene");
    }

    private void Controller()
    {
        if (NoUI == true)
        {
            if (Input.GetAxis("Vertical") == 0 && MainSelectPositionSelect == true)
            {
                MainSelectPositionSelect = false;
            }

            if (Input.GetAxis("Horizontal") == 0 && ExplanationSelect == true)
            {
                ExplanationSelect = false;
            }

            if (NotSelect == false)
            {
                if (MainSelectPositionSelect == false && MainSelectPosition == -1)
                {
                    MainSelectPosition = 2;
                    MainSelectPositionSelect = true;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && MainSelectPositionSelect == false && MainSelectPosition == 0)
                {
                    MainSelectPosition--;
                    MainSelectPositionSelect = true;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && MainSelectPositionSelect == false && MainSelectPosition == 1)
                {
                    MainSelectPosition--;
                    MainSelectPositionSelect = true;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && MainSelectPositionSelect == false && MainSelectPosition == 2)
                {
                    MainSelectPosition--;
                    MainSelectPositionSelect = true;
                }

                if (Input.GetAxisRaw("Vertical") > 0 && MainSelectPositionSelect == false && MainSelectPosition == 0)
                {
                    MainSelectPosition++;
                    MainSelectPositionSelect = true;
                }
                else if (Input.GetAxisRaw("Vertical") > 0 && MainSelectPositionSelect == false && MainSelectPosition == 1)
                {
                    MainSelectPosition++;
                    MainSelectPositionSelect = true;
                }
                else if (Input.GetAxisRaw("Vertical") > 0 && MainSelectPositionSelect == false && MainSelectPosition == 2)
                {
                    MainSelectPosition++;
                    MainSelectPositionSelect = true;
                }
                else if (MainSelectPositionSelect == false && MainSelectPosition == 3)
                {
                    MainSelectPosition = 0;
                    MainSelectPositionSelect = true;
                }
            }

            if (MainSelectPosition == 0)
            {
                WhiteLine.GetComponent<Image>().enabled = false;
                KeyboardcharaButton.GetComponent<Image>().enabled = false;
                GamepadcharaButton.GetComponent<Image>().enabled = false;
                ControllerSetting.GetComponent<Image>().enabled = false;
                KeyboardSetting.GetComponent<Image>().enabled = false;
                SettingMenu.GetComponent<Image>().enabled = true;
                MicSlider.gameObject.SetActive(true);
                BGMSlider.gameObject.SetActive(true);
                SESlider.gameObject.SetActive(true);
                MouseSlider.gameObject.SetActive(true);
                Select.GetComponent<Image>().enabled = true;
                Select1.GetComponent<Image>().enabled = false;
                Select2.GetComponent<Image>().enabled = false;
                OperationExplanationSelect.GetComponent<Image>().enabled = false;
                OperationExplanationSelect1.GetComponent<Image>().enabled = false;
                ExplanationSelectCount = 0;

                if ( SelectCount == 0)
                {
                    NotSelect = true;
                    if (Input.GetAxisRaw("Vertical") > 0 && MainSelectPositionSelect == false)
                    {
                        SelectCount = 1;
                        MainSelectPositionSelect = true;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") > 0  && SelectCount == 1 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount = 2;
                }
                else if (Input.GetAxisRaw("Vertical") > 0  && SelectCount == 2 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount =3;
                }
                else if (Input.GetAxisRaw("Vertical") > 0 && SelectCount == 3 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount = 4;
                }
                else if (Input.GetAxisRaw("Vertical") > 0 && SelectCount == 4 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                }


                if (Input.GetAxisRaw("Vertical") < 0 && SelectCount ==0 && MainSelectPositionSelect == false)
                {
                    SelectCount = -1;
                    MenuCursor.GetComponent<Image>().enabled = false;
                    MenuCursor1.GetComponent<Image>().enabled = false;
                    MenuCursor2.GetComponent<Image>().enabled = false;
                    MenuCursor3.GetComponent<Image>().enabled = false;
                    ButtonCount = 0;
                    ButtonON = false;
                    ExplanationSelect = true;
                    NotSelect = false;
                    ButtonON = false;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && SelectCount == 1 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount =0;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && SelectCount == 2 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount =1;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && SelectCount == 3 && MainSelectPositionSelect == false)
                { 
                    MainSelectPositionSelect = true;
                    SelectCount = 2;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && SelectCount == 4 && MainSelectPositionSelect == false)
                {
                    MainSelectPositionSelect = true;
                    SelectCount = 3;
                }

                if (Input.GetAxis("Horizontal") > 0 && SelectCount == -1 && ExplanationSelect == false)
                {
                    SelectCount = 0;
                    ExplanationSelect = true;
                    NotSelect = false;
                }

                if (SelectCount == 0)
                {
                    MenuCursor.GetComponent<Image>().enabled = true;
                    MenuCursor1.GetComponent<Image>().enabled = false;
                    MenuCursor2.GetComponent<Image>().enabled = false;
                    MenuCursor3.GetComponent<Image>().enabled = false;

                    if (Input.GetKeyDown("joystick button 5"))
                    {
                        if (volume2 < 0)
                        {
                            volume2 += 1f;
                        }
                        BGMSlider.value = volume2;
                        SetMic(volume2);
                    }
                    else if (Input.GetKeyDown("joystick button 4"))
                    {
                        if (volume2 > -80 && volume != 0)
                        {
                            volume2 -= 1f;
                        }
                        BGMSlider.value = volume2;
                        SetMic(volume2);
                    }
                }
                else if (SelectCount == 1)
                {
                    MenuCursor.GetComponent<Image>().enabled = false;
                    MenuCursor1.GetComponent<Image>().enabled = true;
                    MenuCursor2.GetComponent<Image>().enabled = false;
                    MenuCursor3.GetComponent<Image>().enabled = false;

                    if (Input.GetKeyDown("joystick button 5"))
                    {
                        if (volume3 < 0)
                        {
                            volume3 += 1f;
                        }
                        SESlider.value = volume3;
                        SetMic(volume3);
                    }
                    else if (Input.GetKeyDown("joystick button 4"))
                    {
                        if (volume3 > -80 && volume != 0)
                        {
                            volume3 -= 1f;
                        }
                        SESlider.value = volume3;
                        SetMic(volume3);
                    }
                }
                else if (SelectCount == 2)
                {
                    MenuCursor.GetComponent<Image>().enabled = false;
                    MenuCursor1.GetComponent<Image>().enabled = false;
                    MenuCursor2.GetComponent<Image>().enabled = true;
                    MenuCursor3.GetComponent<Image>().enabled = false;
                    if (Input.GetKeyDown("joystick button 5"))
                    {
                        if (volume < 1)
                        {
                            volume += 0.02f;
                        }
                        MicSlider.value = volume;
                        SetMic(volume);
                    }
                    else if (Input.GetKeyDown("joystick button 4"))
                    {
                        if (volume > 0 && volume != 0)
                        {
                            volume -= 0.02f;
                        }
                        MicSlider.value = volume;
                        SetMic(volume);
                    }
                }
                else if (SelectCount == 3)
                {
                    MenuCursor.GetComponent<Image>().enabled = false;
                    MenuCursor1.GetComponent<Image>().enabled = false;
                    MenuCursor2.GetComponent<Image>().enabled = false;
                    MenuCursor3.GetComponent<Image>().enabled = true;
                    if (Input.GetKeyDown("joystick button 5"))
                    {
                        if (level1 < 5)
                        {
                            level1 += 0.1f;
                        }
                        MouseSlider.value = level1;
                        SetMouse(level1);
                    }
                    else if (Input.GetKeyDown("joystick button 4"))
                    {
                        if (level1 > 0 && level1 != 0)
                        {
                            level1 -= 0.1f;
                        }
                        MouseSlider.value = level1;
                        SetMouse(level1);
                    }
                }
                else if (SelectCount == 4)
                {
                    MenuCursor.GetComponent<Image>().enabled = false;
                    MenuCursor1.GetComponent<Image>().enabled = false;
                    MenuCursor2.GetComponent<Image>().enabled = false;
                    MenuCursor3.GetComponent<Image>().enabled = false;

                    if (Input.GetAxisRaw("Vertical") > 0 && MainSelectPositionSelect == true)
                    {
                        NotSelect = false;
                        MainSelectPositionSelect = false;
                        MainSelectPosition = 0;
                    }
                }

                if (Input.GetKeyDown("joystick button 2"))
                {
                    MainSelectPosition = 2;
                    MainSelectPositionSelect = false;
                    ExplanationSelect = true;
                    NotSelect = false;
                }
            }
            else if (MainSelectPosition == 1)
            {
                MenuCursor.GetComponent<Image>().enabled = false;
                MenuCursor1.GetComponent<Image>().enabled = false;
                MenuCursor2.GetComponent<Image>().enabled = false;
                MenuCursor3.GetComponent<Image>().enabled = false;
                WhiteLine.GetComponent<Image>().enabled = true;
                KeyboardcharaButton.GetComponent<Image>().enabled = true;
                GamepadcharaButton.GetComponent<Image>().enabled =true;
                SettingMenu.GetComponent<Image>().enabled = false;
                MicSlider.gameObject.SetActive(false);
                BGMSlider.gameObject.SetActive(false);
                SESlider.gameObject.SetActive(false);
                MouseSlider.gameObject.SetActive(false);
                Select.GetComponent<Image>().enabled = false;
                Select1.GetComponent<Image>().enabled =true;
                Select2.GetComponent<Image>().enabled = false;
                SelectCount = 0;

                if ( ExplanationSelect == false&& ExplanationSelectCount == 0)
                {
                    ExplanationSelectCount=1;
                    OperationExplanationSelect.GetComponent<Image>().enabled = true;
                    OperationExplanationSelect1.GetComponent<Image>().enabled = false;
                    Select1.GetComponent<Image>().enabled = false;
                    KeyboardSetting.GetComponent<Image>().enabled = true;
                    ControllerSetting.GetComponent<Image>().enabled = false;
                    ExplanationSelect = true;
                }
                else if (Input.GetAxis("Horizontal") > 0 && ExplanationSelect==false&& ExplanationSelectCount == 1)
                {
                    ExplanationSelectCount = 2;
                    OperationExplanationSelect1.GetComponent<Image>().enabled = true;
                    OperationExplanationSelect.GetComponent<Image>().enabled = false;
                    Select1.GetComponent<Image>().enabled = false;
                    ControllerSetting.GetComponent<Image>().enabled =true;
                    KeyboardSetting.GetComponent<Image>().enabled = false;
                    ExplanationSelect = true;
                }
                else if (Input.GetAxis("Horizontal") < 0 && ExplanationSelect == false && ExplanationSelectCount == 2)
                {
                    ExplanationSelectCount = 0;
                    OperationExplanationSelect1.GetComponent<Image>().enabled = true;
                    OperationExplanationSelect.GetComponent<Image>().enabled = false;
                    Select1.GetComponent<Image>().enabled = false;
                    ControllerSetting.GetComponent<Image>().enabled = true;
                    KeyboardSetting.GetComponent<Image>().enabled = false;
                    ExplanationSelect = true;
                }

            }
            else
            {
                MenuCursor.GetComponent<Image>().enabled = false;
                MenuCursor1.GetComponent<Image>().enabled = false;
                MenuCursor2.GetComponent<Image>().enabled = false;
                MenuCursor3.GetComponent<Image>().enabled = false;
                WhiteLine.GetComponent<Image>().enabled = false;
                KeyboardcharaButton.GetComponent<Image>().enabled = false;
                GamepadcharaButton.GetComponent<Image>().enabled = false;
                ControllerSetting.GetComponent<Image>().enabled = false;
                KeyboardSetting.GetComponent<Image>().enabled = false;
                SettingMenu.GetComponent<Image>().enabled = false;
                MicSlider.gameObject.SetActive(false);
                BGMSlider.gameObject.SetActive(false);
                SESlider.gameObject.SetActive(false);
                MouseSlider.gameObject.SetActive(false);
                Select.GetComponent<Image>().enabled = false;
                Select1.GetComponent<Image>().enabled = false;
                Select2.GetComponent<Image>().enabled = true;
                ExplanationSelectCount = 0;
                OperationExplanationSelect.GetComponent<Image>().enabled = false;
                OperationExplanationSelect1.GetComponent<Image>().enabled = false;
                SelectCount = 0;

                if (Input.GetKeyDown("joystick button 0"))
                {
                    BackTitleButton();
                }
            }
        }
    }

    //private void Controller2()
    //{
    //    if (NoUI == true)
    //    {
    //        //MainSelectPositionSelect = true;
    //        //Debug.Log(MenuSelectCount);
    //        Timer = Timer + 0.01f;
    //        //Debug.Log(Timer);
    //        if (VH == true)
    //        {
    //            Timervh += 0.01f;
    //            if (Timervh >= 0.2f)
    //            {
    //                Timervh = 0;
    //                VH = false;
    //            }
    //        }

    //        if (TimeON == true)
    //        {
    //            if (/*Input.GetAxis("Vertical") == 0 */ Timer >= 0.1f && Reset == false)
    //            {
    //                Reset = true;
    //                Timer = 0;
    //                TimeON = false;

    //            }

    //            if (/*Input.GetAxis("Horizontal") == 0*/VH == false && Timer >= 0.1f && Reset1 == false)
    //            {
    //                Reset1 = true;
    //                Timer = 0;
    //                TimeON = false;
    //            }

    //        }

    //        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
    //        {
    //            Timer = 0;
    //        }



    //        if (MainSelectPositionSelect == true && Reset)
    //        {
    //            if ((Input.GetAxisRaw("Vertical") > 0))
    //            {
    //                MainSelectPosition++;
    //                Reset = false;
    //                TimeON = true;
    //                if (MainSelectPosition > 2)
    //                {
    //                    MainSelectPosition = 2;
    //                }
    //            }

    //            if ((Input.GetAxisRaw("Vertical") < 0))
    //            {
    //                MainSelectPosition--;
    //                Reset = false;
    //                TimeON = true;
    //                if (MainSelectPosition < 0)
    //                {
    //                    MainSelectPosition = 0;
    //                }
    //            }
    //        }

    //        if (MainSelectPosition == 0 && MainSelectPositionSelect)
    //        {
    //            WhiteLine.GetComponent<Image>().enabled = false;
    //            KeyboardcharaButton.GetComponent<Image>().enabled = false;
    //            GamepadcharaButton.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            SettingMenu.GetComponent<Image>().enabled = true;
    //            MicSlider.gameObject.SetActive(true);
    //            BGMSlider.gameObject.SetActive(true);
    //            SESlider.gameObject.SetActive(true);
    //            MouseSlider.gameObject.SetActive(true);
    //            Select.GetComponent<Image>().enabled = true;
    //            Select1.GetComponent<Image>().enabled = false;
    //            Select2.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            decisionA.gameObject.SetActive(true);
    //            ExplainSelect = false;
    //            ExplainSelectCount = 0;
    //            Reset1 = false;
    //        }
    //        else if (MainSelectPosition == 1 && MainSelectPositionSelect)
    //        {
    //            MenuCursor.GetComponent<Image>().enabled = false;
    //            MenuCursor1.GetComponent<Image>().enabled = false;
    //            MenuCursor2.GetComponent<Image>().enabled = false;
    //            MenuCursor3.GetComponent<Image>().enabled = false;
    //            WhiteLine.GetComponent<Image>().enabled = true;
    //            KeyboardcharaButton.GetComponent<Image>().enabled = true;
    //            GamepadcharaButton.GetComponent<Image>().enabled = true;
    //            ControllerSetting.GetComponent<Image>().enabled = true;
    //            KeyboardSetting.GetComponent<Image>().enabled = true;
    //            SettingMenu.GetComponent<Image>().enabled = true;
    //            MicSlider.gameObject.SetActive(false);
    //            BGMSlider.gameObject.SetActive(false);
    //            SESlider.gameObject.SetActive(false);
    //            MouseSlider.gameObject.SetActive(false);
    //            Select.GetComponent<Image>().enabled = false;
    //            Select1.GetComponent<Image>().enabled = true;
    //            Select2.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect.GetComponent<Image>().enabled = true;
    //            OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //            decisionA.gameObject.SetActive(false);
    //            Cursor.gameObject.SetActive(false);
    //            MenuSelect = false;
    //            MenuSelectCount = 0;
    //            RBLB[0].gameObject.SetActive(false);
    //            RBLB[1].gameObject.SetActive(false);
    //            RBLB[2].gameObject.SetActive(false);
    //            RBLB[3].gameObject.SetActive(false);
    //            RBLB[4].gameObject.SetActive(false);
    //            RBLB[5].gameObject.SetActive(false);
    //            RBLB[6].gameObject.SetActive(false);
    //            RBLB[7].gameObject.SetActive(false);

    //        }
    //        else if (MainSelectPosition == 2 && MainSelectPositionSelect)
    //        {
    //            MenuCursor.GetComponent<Image>().enabled = false;
    //            MenuCursor1.GetComponent<Image>().enabled = false;
    //            MenuCursor2.GetComponent<Image>().enabled = false;
    //            MenuCursor3.GetComponent<Image>().enabled = false;
    //            WhiteLine.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            KeyboardcharaButton.GetComponent<Image>().enabled = false;
    //            GamepadcharaButton.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            SettingMenu.GetComponent<Image>().enabled = false;
    //            MicSlider.gameObject.SetActive(false);
    //            BGMSlider.gameObject.SetActive(false);
    //            SESlider.gameObject.SetActive(false);
    //            MouseSlider.gameObject.SetActive(false);
    //            Select.GetComponent<Image>().enabled = false;
    //            Select1.GetComponent<Image>().enabled = false;
    //            Select2.GetComponent<Image>().enabled = true;
    //            decisionA.gameObject.SetActive(false);
    //            Cursor.gameObject.SetActive(false);
    //            ExplanationSelectCount = 0;
    //            OperationExplanationSelect.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //            decisionA.gameObject.SetActive(true);
    //            SelectCount = 0;
    //            RBLB[0].gameObject.SetActive(false);
    //            RBLB[1].gameObject.SetActive(false);
    //            RBLB[2].gameObject.SetActive(false);
    //            RBLB[3].gameObject.SetActive(false);
    //            RBLB[4].gameObject.SetActive(false);
    //            RBLB[5].gameObject.SetActive(false);
    //            RBLB[6].gameObject.SetActive(false);
    //            RBLB[7].gameObject.SetActive(false);


    //            if (Input.GetKeyDown("joystick button 0"))
    //            {
    //                BackTitleButton();
    //            }
    //        }

    //        if (MainSelectPosition == 0 && (Input.GetAxisRaw("Horizontal") > 0))
    //        {
    //            MenuSelect = true;
    //            MainSelectPositionSelect = false;

    //            TimeON = true;
    //        }

    //        if (MenuSelect == true && Reset)
    //        {
    //            if ((Input.GetAxisRaw("Vertical") > 0))
    //            {
    //                MenuSelectCount++;
    //                Reset = false;
    //                TimeON = true;
    //                VH = true;
    //                if (MenuSelectCount > 3)
    //                {
    //                    MenuSelectCount = 3;
    //                }
    //            }

    //            if ((Input.GetAxisRaw("Vertical") < 0))
    //            {
    //                MenuSelectCount--;
    //                Reset = false;
    //                TimeON = true;
    //                VH = true;
    //                if (MenuSelectCount < 0)
    //                {
    //                    MenuSelectCount = 0;
    //                }
    //            }
    //        }

    //        if (MenuSelect == true && MainSelectPositionSelect == false)
    //        {
    //            if (MenuSelectCount == 0)
    //            {
    //                MenuCursor.GetComponent<Image>().enabled = true;
    //                MenuCursor1.GetComponent<Image>().enabled = false;
    //                MenuCursor2.GetComponent<Image>().enabled = false;
    //                MenuCursor3.GetComponent<Image>().enabled = false;
    //                RBLB[0].gameObject.SetActive(true);
    //                RBLB[1].gameObject.SetActive(true);
    //                RBLB[2].gameObject.SetActive(false);
    //                RBLB[3].gameObject.SetActive(false);
    //                RBLB[4].gameObject.SetActive(false);
    //                RBLB[5].gameObject.SetActive(false);
    //                RBLB[6].gameObject.SetActive(false);
    //                RBLB[7].gameObject.SetActive(false);

    //                Cursor.gameObject.SetActive(false);
    //                MenuSelectOUTCount = 0;

    //                if (Input.GetKey("joystick button 5") && VH == false)
    //                {
    //                    if (volume2 < 0)
    //                    {
    //                        volume2 += 1f;
    //                    }
    //                    BGMSlider.value = volume2;
    //                    SetMic(volume2);

    //                }
    //                else if (Input.GetKey("joystick button 4") && VH == false)
    //                {
    //                    if (volume2 > -80 && volume != 0)
    //                    {
    //                        volume2 -= 1f;
    //                    }
    //                    BGMSlider.value = volume2;
    //                    SetMic(volume2);
    //                }

    //                if (Input.GetKeyDown("joystick button 0"))
    //                {
    //                    MenuSelect = false;
    //                    MainSelectPositionSelect = true;
    //                    Select.GetComponent<Image>().enabled = true;
    //                    MenuSelectCount = 0;
    //                }
    //            }
    //            else if (MenuSelectCount == 1)
    //            {
    //                MenuCursor.GetComponent<Image>().enabled = false;
    //                MenuCursor1.GetComponent<Image>().enabled = true;
    //                MenuCursor2.GetComponent<Image>().enabled = false;
    //                MenuCursor3.GetComponent<Image>().enabled = false;
    //                RBLB[0].gameObject.SetActive(false);
    //                RBLB[1].gameObject.SetActive(false);
    //                RBLB[2].gameObject.SetActive(true);
    //                RBLB[3].gameObject.SetActive(true);
    //                RBLB[4].gameObject.SetActive(false);
    //                RBLB[5].gameObject.SetActive(false);
    //                RBLB[6].gameObject.SetActive(false);
    //                RBLB[7].gameObject.SetActive(false);

    //                Cursor.gameObject.SetActive(false);
    //                MenuSelectOUTCount = 0;
    //                MenuSelectOUTCount = 0;

    //                if (Input.GetKey("joystick button 5") && VH == false)
    //                {
    //                    if (volume3 < 0)
    //                    {
    //                        volume3 += 1f;
    //                    }
    //                    SESlider.value = volume3;
    //                    SetMic(volume3);
    //                }
    //                else if (Input.GetKey("joystick button 4") && VH == false)
    //                {
    //                    if (volume3 > -80 && volume != 0)
    //                    {
    //                        volume3 -= 1f;
    //                    }
    //                    SESlider.value = volume3;
    //                    SetMic(volume3);
    //                }

    //                if (Input.GetKeyDown("joystick button 0"))
    //                {
    //                    MenuSelect = false;
    //                    MainSelectPositionSelect = true;
    //                    Select.GetComponent<Image>().enabled = true;
    //                    MenuSelectCount = 0;
    //                }
    //            }
    //            else if (MenuSelectCount == 2)
    //            {
    //                MenuCursor.GetComponent<Image>().enabled = false;
    //                MenuCursor1.GetComponent<Image>().enabled = false;
    //                MenuCursor2.GetComponent<Image>().enabled = true;
    //                MenuCursor3.GetComponent<Image>().enabled = false;
    //                RBLB[0].gameObject.SetActive(false);
    //                RBLB[1].gameObject.SetActive(false);
    //                RBLB[2].gameObject.SetActive(false);
    //                RBLB[3].gameObject.SetActive(false);
    //                RBLB[4].gameObject.SetActive(true);
    //                RBLB[5].gameObject.SetActive(true);
    //                RBLB[6].gameObject.SetActive(false);
    //                RBLB[7].gameObject.SetActive(false);
    //                Cursor.gameObject.SetActive(false);
    //                MenuSelectOUTCount = 0;

    //                if (Input.GetKey("joystick button 5") && VH == false)
    //                {
    //                    if (volume < 1)
    //                    {
    //                        volume += 0.02f;
    //                    }
    //                    MicSlider.value = volume;
    //                    SetMic(volume);
    //                }
    //                else if (Input.GetKey("joystick button 4") && VH == false)
    //                {
    //                    if (volume > 0 && volume != 0)
    //                    {
    //                        volume -= 0.02f;
    //                    }
    //                    MicSlider.value = volume;
    //                    SetMic(volume);
    //                }

    //                if (Input.GetKeyDown("joystick button 0"))
    //                {
    //                    MenuSelect = false;
    //                    MainSelectPositionSelect = true;
    //                    Select.GetComponent<Image>().enabled = true;
    //                    MenuSelectCount = 0;
    //                }
    //            }
    //            else if (MenuSelectCount == 3)
    //            {
    //                MenuCursor.GetComponent<Image>().enabled = false;
    //                MenuCursor1.GetComponent<Image>().enabled = false;
    //                MenuCursor2.GetComponent<Image>().enabled = false;
    //                MenuCursor3.GetComponent<Image>().enabled = true;
    //                RBLB[0].gameObject.SetActive(false);
    //                RBLB[1].gameObject.SetActive(false);
    //                RBLB[2].gameObject.SetActive(false);
    //                RBLB[3].gameObject.SetActive(false);
    //                RBLB[4].gameObject.SetActive(false);
    //                RBLB[5].gameObject.SetActive(false);
    //                RBLB[6].gameObject.SetActive(true);
    //                RBLB[7].gameObject.SetActive(true);

    //                Cursor.gameObject.SetActive(false);
    //                MenuSelectOUTCount = 0;

    //                if (Input.GetKey("joystick button 5") && VH == false)
    //                {
    //                    if (level1 < 5)
    //                    {
    //                        level1 += 0.1f;
    //                    }
    //                    MouseSlider.value = level1;
    //                    SetMouse(level1);
    //                }
    //                else if (Input.GetKey("joystick button 4") && VH == false)
    //                {
    //                    if (level1 > 0 && level1 != 0)
    //                    {
    //                        level1 -= 0.1f;
    //                    }
    //                    MouseSlider.value = level1;
    //                    SetMouse(level1);
    //                }

    //                if (Input.GetKeyDown("joystick button 0"))
    //                {
    //                    MenuSelect = false;
    //                    MainSelectPositionSelect = true;
    //                    Select.GetComponent<Image>().enabled = true;
    //                    MenuSelectCount = 0;
    //                }
    //            }

    //        }



    //        if (MainSelectPosition == 1 && (Input.GetAxisRaw("Horizontal") > 0))
    //        {
    //            MainSelectPositionSelect = false;
    //            ExplainSelect = true;
    //            TimeON = true;
    //            if (ExplainSelect == true && Reset1)
    //            {
    //                if ((Input.GetAxisRaw("Horizontal") > 0))
    //                {
    //                    ExplainSelectCount++;
    //                    Reset1 = false;
    //                    TimeON = true;

    //                    if (ExplainSelectCount > 2)
    //                    {
    //                        ExplainSelectCount = 2;
    //                    }
    //                }
    //                else if (Input.GetAxisRaw("Horizontal") < 0)
    //                {
    //                    ExplainSelectCount--;
    //                    Reset1 = false;
    //                    TimeON = true;

    //                    if (ExplainSelectCount < 0)
    //                    {
    //                        ExplainSelectCount = 0;
    //                    }
    //                }
    //            }
    //        }
    //        else if (MainSelectPosition == 1 && (Input.GetAxisRaw("Horizontal") < 0))
    //        {
    //            if (ExplainSelect == true && Reset1)
    //            {
    //                if (Input.GetAxisRaw("Horizontal") < 0)
    //                {
    //                    ExplainSelectCount--;
    //                    Reset1 = false;
    //                    TimeON = true;

    //                    if (ExplainSelectCount < 0)
    //                    {
    //                        ExplainSelectCount = 0;
    //                    }

    //                }
    //            }
    //        }

    //        if (ExplainSelect == true && MainSelectPositionSelect == false)
    //        {
    //            if (ExplainSelectCount == 0)
    //            {
    //                Select1.GetComponent<Image>().enabled = true;
    //                OperationExplanationSelect.GetComponent<Image>().enabled = true;
    //                OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //                KeyboardSetting.GetComponent<Image>().enabled = true;
    //                MainSelectPositionSelect = true;
    //            }
    //            else if (ExplainSelectCount == 1)
    //            {
    //                OperationExplanationSelect.GetComponent<Image>().enabled = true;
    //                OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //                ControllerSetting.GetComponent<Image>().enabled = false;
    //                KeyboardSetting.GetComponent<Image>().enabled = true;

    //            }
    //            else if (ExplainSelectCount == 2)
    //            {
    //                OperationExplanationSelect.GetComponent<Image>().enabled = false;
    //                OperationExplanationSelect1.GetComponent<Image>().enabled = true;
    //                ControllerSetting.GetComponent<Image>().enabled = true;
    //                KeyboardSetting.GetComponent<Image>().enabled = false;
    //            }
    //        }



    //        if (Input.GetKeyDown("joystick button 1"))
    //        {
    //            WhiteLine.GetComponent<Image>().enabled = false;
    //            KeyboardcharaButton.GetComponent<Image>().enabled = false;
    //            GamepadcharaButton.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            SettingMenu.GetComponent<Image>().enabled = false;
    //            MicSlider.gameObject.SetActive(false);
    //            BGMSlider.gameObject.SetActive(false);
    //            SESlider.gameObject.SetActive(false);
    //            MouseSlider.gameObject.SetActive(false);
    //            Select.GetComponent<Image>().enabled = false;
    //            Select1.GetComponent<Image>().enabled = false;
    //            Select2.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect.GetComponent<Image>().enabled = false;
    //            OperationExplanationSelect1.GetComponent<Image>().enabled = false;
    //            ControllerSetting.GetComponent<Image>().enabled = false;
    //            KeyboardSetting.GetComponent<Image>().enabled = false;
    //            decisionA.gameObject.SetActive(false);
    //            Cursor.gameObject.SetActive(false);
    //            MenuSelect = false;
    //            MenuSelectCount = 0;
    //            ExplainSelect = false;
    //            ExplainSelectCount = 0;
    //            Reset1 = false;
    //            RBLB[0].gameObject.SetActive(false);
    //            RBLB[1].gameObject.SetActive(false);
    //            RBLB[2].gameObject.SetActive(false);
    //            RBLB[3].gameObject.SetActive(false);
    //            RBLB[4].gameObject.SetActive(false);
    //            RBLB[5].gameObject.SetActive(false);
    //            RBLB[6].gameObject.SetActive(false);
    //            RBLB[7].gameObject.SetActive(false);

    //        }
    //    }
    //}

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
