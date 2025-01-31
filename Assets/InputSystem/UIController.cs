using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject Menyu;
    [SerializeField] GameObject SettingPanel1;
    [SerializeField] GameObject SettingPanel2;

    [SerializeField] GameObject SettingButton;                 //ゲーム内設定を変更する画面へ遷移するボタン
    [SerializeField] GameObject OperationExplanationButton;    //操作説明画面へ遷移するボタン
    [SerializeField] GameObject TitleButton;                   //タイトル画面へ遷移するボタン

    bool mainSelect;
    int mainSelectCount;

    //[SerializeField] GameObject Cursors;
    //[SerializeField] GameObject Cursors1;
    //[SerializeField] GameObject Cursors2;
    //[SerializeField] GameObject Cursors3;
    //[SerializeField] GameObject RBButton;
    //[SerializeField] GameObject RBButton1;
    //[SerializeField] GameObject RBButton2;
    //[SerializeField] GameObject RBButton3;

    bool setting;
    bool menyu;
    int settingCount;

    [SerializeField] GameObject GamePadSettingButton;
    [SerializeField] GameObject KeyBoardSettingButton;
    [SerializeField] GameObject KeyboardUI;
    [SerializeField] GameObject ControllerUI;

    bool operatinginstructions;
    int operatinginstructionsCount;

   //マイクスライダー
   [SerializeField] Slider MicSlider;
    //BGMスライダー
    [SerializeField] Slider BGMSlider;
    //SEスライダー
    [SerializeField] Slider SESlider;
    //マウススライダー
    [SerializeField] Slider MouseSlider;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;
    public float level1;
    public float volume2;
    public float volume3;
    public CinemachineFreeLook VCamera;
    public float ButtonCount;
    public bool ButtonON;

    [SerializeField] GameObject SettingPanel3;


    // Start is called before the first frame update
    void Start()
    {

        Menyu.SetActive(false);
        SettingPanel1.SetActive(false);
        SettingPanel2.SetActive(false);

        mainSelect = false;
        mainSelectCount = 0;

        operatinginstructions = false;
        operatinginstructionsCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //if (_uiInputActions.SettingUI.Pouse.triggered)
        //{
        //    Menyu.SetActive(true);
        //    mainSelect = true;
        //    Time.timeScale = 0;
        //}

        //if (mainSelect)
        //{
        //    operatinginstructions = false;
        //    setting = false;
        //    MainImage();
        //    if (_uiInputActions.SettingUI.MainSelsectUp.triggered)
        //    {
        //        mainSelectCount++;
        //        if (mainSelectCount > 3)
        //        {
        //            mainSelectCount =2;
        //        }
        //    }
        //    else if (_uiInputActions.SettingUI.MainSelsectDown.triggered)
        //    {
        //        mainSelectCount--;
        //        if (mainSelectCount < 0)
        //        {
        //            mainSelectCount = 0;
        //        }
        //    }

        //    if (Input.GetKeyDown("joystick button 1"))
        //    {
        //        Menyu.SetActive(false);
        //        mainSelect = false;
        //        Time.timeScale = 1;
        //    }
        //}

        //if (setting)
        //{
        //    operatinginstructions = false;
        //    SettingPanel1Image();
        //    if (_uiInputActions.SettingUI.MainSelectRight.triggered)
        //    {
        //        menyu = true;
        //        settingCount = 1;
        //    }
        //    else if (_uiInputActions.SettingUI.MainSelectLeft.triggered)
        //    {
        //        menyu=false;
        //        settingCount = 0;
        //        mainSelect = true;
        //    }

        //    if (menyu)
        //    {
        //        mainSelect = false;
        //        if (_uiInputActions.SettingUI.MainSelsectUp.triggered)
        //        {
        //            settingCount--;
        //            if (settingCount < 0)
        //            {
        //                settingCount = 1;
        //            }

        //        }
        //        else if (_uiInputActions.SettingUI.MainSelsectDown.triggered)
        //        {
                

        //            settingCount++;
        //            if (settingCount > 5)
        //            {
        //                settingCount = 4;
        //            }
        //        }
        //    }

        //     if (Input.GetKeyDown("joystick button 0"))
        //    {
        //        menyu = false;
        //        settingCount = 0;
        //        mainSelect = true;
        //    }
        //}

        //if (operatinginstructions)
        //{
        //    mainSelect = false;
        //    setting = false;
        //    SettingPanel2Image();
        //    if (_uiInputActions.SettingUI.MainSelectRight.triggered)
        //    {
        //        operatinginstructionsCount++;
        //        if (operatinginstructionsCount > 3)
        //        {
        //            operatinginstructionsCount = 2;
        //        }
        //    }
        //    else if (_uiInputActions.SettingUI.MainSelectLeft.triggered)
        //    {
        //        operatinginstructionsCount--;
        //        if (operatinginstructionsCount < 0)
        //        {
        //            operatinginstructionsCount = 0;
        //        }
        //    }
        //}


    }


    void MainImage()
    {
        Color whiteColor = Color.white;
        Color yellowColor = Color.yellow;

        Image imageSettingButton = SettingButton.GetComponent<Image>();
        Image imageOperationExplanationButton = OperationExplanationButton.GetComponent<Image>();
        Image imageTitleButton = TitleButton.GetComponent<Image>();

        if (mainSelectCount == 2)
        {
            imageSettingButton.color = yellowColor;
            imageOperationExplanationButton.color = whiteColor;
            imageTitleButton.color = whiteColor;
            setting = true;
            operatinginstructions = false;
            SettingPanel1.SetActive(true);
            SettingPanel2.SetActive(false);
            SettingPanel3.SetActive(false);
        }
        else if (mainSelectCount == 1)
        {
            imageSettingButton.color = whiteColor;
            imageOperationExplanationButton.color = yellowColor;
            imageTitleButton.color = whiteColor;
            setting = false;
            operatinginstructions = true;
            SettingPanel1.SetActive(false);
            SettingPanel2.SetActive(true);
            SettingPanel3.SetActive(false);
        }
        else if (mainSelectCount == 0)
        {
            imageSettingButton.color = whiteColor;
            imageOperationExplanationButton.color = whiteColor;
            imageTitleButton.color = yellowColor;
            operatinginstructions = false;
            setting = false;
            SettingPanel1.SetActive(false);
            SettingPanel2.SetActive(false);
            SettingPanel3.SetActive(true);
            if (Input.GetKeyDown("joystick button 0"))
            {
                BackTitleButton();
            }
        }
    }
    void SettingPanel1Image()
    {
        Color whiteColor = Color.white;
        Color yellowColor = Color.yellow;

        Image imageSettingButton = SettingButton.GetComponent<Image>();

        //Image imageCursorsButton = Cursors.GetComponent<Image>();
        //Image imageCursors1Button = Cursors1.GetComponent<Image>();
        //Image imageCursors2Button = Cursors2.GetComponent<Image>();
        //Image imageCursors3Button = Cursors3.GetComponent<Image>();

        if (settingCount == 0)
        {
            mainSelect = true;
            imageSettingButton.color = yellowColor;
            //Cursors.SetActive(false);
            //Cursors1.SetActive(false);
            //Cursors2.SetActive(false);
            //Cursors3.SetActive(false);
            //RBButton.SetActive(false);
            //RBButton1.SetActive(false);
            //RBButton2.SetActive(false);
            //RBButton3.SetActive(false);

        }
        else if (settingCount == 1)
        {
            //imageSettingButton.color = whiteColor;
            //imageCursorsButton.color = yellowColor;
            //imageCursors1Button.color = whiteColor;
            //imageCursors2Button.color = whiteColor;
            //imageCursors3Button.color = whiteColor;

            //Cursors.SetActive(true);
            //Cursors1.SetActive(false);
            //Cursors2.SetActive(false);
            //Cursors3.SetActive(false);
            //RBButton.SetActive(true);
            //RBButton1.SetActive(false);
            //RBButton2.SetActive(false);
            //RBButton3.SetActive(false);

            if (Input.GetKey("joystick button 5"))
            {
                if (volume2 < 0)
                {
                    volume2 += 5f;
                }
                BGMSlider.value = volume2;
                SetMic(volume2);
            }
            else if (Input.GetKey("joystick button 4"))
            {
                if (volume2 > -80 && volume != 0)
                {
                    volume2 -= 5f;
                }
                BGMSlider.value = volume2;
                SetMic(volume2);
            }
        }
        else if(settingCount ==2)
        {
            //imageSettingButton.color = whiteColor;
            //imageCursorsButton.color = whiteColor;
            //imageCursors1Button.color = yellowColor;
            //imageCursors2Button.color = whiteColor;
            //imageCursors3Button.color = whiteColor;

            //Cursors.SetActive(false);
            //Cursors1.SetActive(true);
            //Cursors2.SetActive(false);
            //Cursors3.SetActive(false);
            //RBButton.SetActive(false);
            //RBButton1.SetActive(true);
            //RBButton2.SetActive(false);
            //RBButton3.SetActive(false);

            if (Input.GetKey("joystick button 5"))
            {
                if (volume3 < 0)
                {
                    volume3 += 5f;
                }
                SESlider.value = volume3;
                SetMic(volume3);
            }
            else if (Input.GetKey("joystick button 4"))
            {
                if (volume3 > -80 && volume != 0)
                {
                    volume3 -= 5f;
                }
                SESlider.value = volume3;
                SetMic(volume3);
            }
        }
        else if( settingCount==3)
        {
            //imageSettingButton.color = whiteColor;
            //imageCursorsButton.color = whiteColor;
            //imageCursors1Button.color = whiteColor;
            //imageCursors2Button.color = yellowColor;
            //imageCursors3Button.color = whiteColor;

            //Cursors.SetActive(false);
            //Cursors1.SetActive(false);
            //Cursors2.SetActive(true);
            //Cursors3.SetActive(false);
            //RBButton.SetActive(false);
            //RBButton1.SetActive(false);
            //RBButton2.SetActive(true);
            //RBButton3.SetActive(false);

            if (Input.GetKey("joystick button 5"))
            {
                if (volume < 1)
                {
                    volume += 0.1f;
                }
                MicSlider.value = volume;
                SetMic(volume);
            }
            else if (Input.GetKey("joystick button 4"))
            {
                if (volume > 0 && volume != 0)
                {
                    volume -= 0.1f;
                }
                MicSlider.value = volume;
                SetMic(volume);
            }
        }
        else if(settingCount==4)
        {
            //imageSettingButton.color = whiteColor;
            //imageCursorsButton.color = whiteColor;
            //imageCursors1Button.color = whiteColor;
            //imageCursors2Button.color = whiteColor;
            //imageCursors3Button.color = yellowColor;

            //Cursors.SetActive(false);
            //Cursors1.SetActive(false);
            //Cursors2.SetActive(false);
            //Cursors3.SetActive(true);
            //RBButton.SetActive(false);
            //RBButton1.SetActive(false);
            //RBButton2.SetActive(false);
            //RBButton3.SetActive(true);

            if (Input.GetKey("joystick button 5"))
            {
                if (level1 < 5)
                {
                    level1 += 0.5f;
                }
                MouseSlider.value = level1;
                SetMouse(level1);
            }
            else if (Input.GetKey("joystick button 4"))
            {
                if (level1 > 0 && level1 != 0)
                {
                    level1 -= 0.5f;
                }
                MouseSlider.value = level1;
                SetMouse(level1);
            }
        }
    }

    void SettingPanel2Image()
    {
        Color whiteColor = Color.white;
        Color yellowColor = Color.yellow;

        Image imageGamePadSettingButton = GamePadSettingButton.GetComponent<Image>();
        Image imageKeyBoardSettingButton = KeyBoardSettingButton.GetComponent<Image>();
        Image imageOperationExplanationButton = OperationExplanationButton.GetComponent<Image>();

        if (operatinginstructionsCount == 0)
        {
            imageOperationExplanationButton.color = yellowColor;

            imageGamePadSettingButton.color = whiteColor;
            imageKeyBoardSettingButton.color = whiteColor;

            mainSelect = true;
        }
        else if (operatinginstructionsCount == 1)
        {
            imageOperationExplanationButton.color = whiteColor;

            imageGamePadSettingButton.color = whiteColor;
            imageKeyBoardSettingButton.color = yellowColor;
            ControllerUI.SetActive(false);
            KeyboardUI.SetActive(true);

        }
        else if (operatinginstructionsCount == 2)
        {
            imageOperationExplanationButton.color = whiteColor;

            imageGamePadSettingButton.color = yellowColor;
            imageKeyBoardSettingButton.color = whiteColor;
            ControllerUI.SetActive(true);
            KeyboardUI.SetActive(false);
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
    public void BackTitleButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }

    public void OnSettingMenuButton()
    {
        Debug.Log("1");
        Menyu.SetActive(true);
    }
}
