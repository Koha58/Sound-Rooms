using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    //�ݒ�w�i���
    [SerializeField] GameObject BackGround;
    //����ݒ���
    [SerializeField] GameObject SettingMenu;
    //�^�C�g���́u�ݒ�v����
    [SerializeField] GameObject SettingTitle;
    //�e��ݒ��ʔw�i
    [SerializeField] GameObject SettingBack;
    //�ݒ�I��ڈ�
    [SerializeField] GameObject Select;
    //����ݒ��ʃJ�[�\��
    [SerializeField] GameObject MenuCursor;
    //�ݒ�����{�^��
    [SerializeField] GameObject CloseButton;
    //�ݒ�����{�^��(�R���g���[���[)
    [SerializeField] GameObject CloseButtonB;
    //�Q�[�����ݒ��ύX�����ʂ֑J�ڂ���{�^��
    [SerializeField] GameObject SettingButton;
    //���������ʂ֑J�ڂ���{�^��
    [SerializeField] GameObject OperationExplanationButton;
    //�^�C�g����ʂ֑J�ڂ���{�^��
    [SerializeField] GameObject TitleButton;
    //���������ʏ�̔�����
    [SerializeField] GameObject WhiteLine;
    //����ݒ��ʂ́u�L�[�{�[�h�v�����{�^��
    [SerializeField] GameObject KeyboardcharaButton;
    //����ݒ��ʂ́u�Q�[���p�b�h�v�����{�^��
    [SerializeField] GameObject GamepadcharaButton;
    //����ݒ��ʂ̃R���g���[���[�ݒ���
    [SerializeField] GameObject ControllerSetting;
    //����ݒ��ʂ̃L�[�{�[�h�ݒ���
    [SerializeField] GameObject KeyboardSetting;
    //���������ʓ��I��ڈ�
    [SerializeField] GameObject OperationExplanationSelect;
    //�}�C�N�X���C�_�[
    [SerializeField] Slider MicSlider;
    //BGM�X���C�_�[
    [SerializeField] Slider BGMSlider;
    //SE�X���C�_�[
    [SerializeField] Slider SESlider;
    //�}�E�X�X���C�_�[
    [SerializeField] Slider MouseSlider;

    //�J�[�\��(�R���g���[���[�p)
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

    //���C���̐ݒ���������Ă��邩
    bool Main;

    // Start is called before the first frame update
    void Start()
    {
        BackGround.GetComponent<Image>().enabled = false;

        SettingMenu.GetComponent<Image>().enabled = false;

        SettingTitle.GetComponent<Image>().enabled = false;

        SettingBack.GetComponent<Image>().enabled = false;

        Select.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

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

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);

        //Cursor.GetComponent<Image>().enabled = false;

        TutorialManager.ON = false;

        //SettingBack.SetActive(false);

        Transform MainSettingSelectTransform = Select.transform;
        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
        MainSettingChangePositionY = MainSettingOriginPositionY;

        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        OperationExplanationSelectTransform.transform.localPosition = new Vector3(OperationExplanationOriginPositionX, OperationExplanationOriginPositionY, OperationExplanationOriginPositionZ);
        OperationExplanationChangePositionX = OperationExplanationOriginPositionX;

        Transform CursorTransform = MenuCursor.transform;
        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorOriginPositionY, CursorOriginPositionZ);
        CursorChangePositionY = CursorOriginPositionY;

        MainSelectPosition = 0;
        CursorPosition = 0;
        Continue = false;
        ExplainController = false;
        Main = true;
    }

    // Update is called once per frame
    void Update()
    {
        Transform MainSettingSelectTransform = Select.transform;
        Transform OperationExplanationSelectTransform = OperationExplanationSelect.transform;
        Transform CursorTransform = MenuCursor.transform;
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && CloseButtonB != null)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && CloseButton != null)
        {
            deviceCheck = false;
        }

        if (Input.GetKeyDown("joystick button 7"))//���j���[ �{�^�� 
        {
            TutorialManager.ON = true;

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

            MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
            MainSettingChangePositionY = MainSettingOriginPositionY;

            CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorOriginPositionY, CursorOriginPositionZ);
            CursorChangePositionY = CursorOriginPositionY;

            Main = true;

            Time.timeScale = 0;
        }

        if (deviceCheck)
        {
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                Continue = false;
            }

            if (Input.GetAxisRaw("Vertical") < 0 && Continue == false && Main)
            {
                if (MainSelectPosition == 0)
                {
                    MainSelectPosition++;
                    MainSettingChangePositionY = 212;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
                }
                else if (MainSelectPosition == 1)
                {
                    MainSelectPosition++;
                    MainSettingChangePositionY = 113;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
                }
                else if (MainSelectPosition == 2)
                {
                    MainSelectPosition = 0;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
                    MainSettingChangePositionY = MainSettingOriginPositionY;
                }
                Continue = true;
            }
            else if (Input.GetAxisRaw("Vertical") > 0 && Continue == false && Main)
            {
                if (MainSelectPosition == 0)
                {
                    MainSelectPosition = 2;
                    MainSettingChangePositionY = 113;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
                }
                else if (MainSelectPosition == 1)
                {
                    MainSelectPosition--;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
                    MainSettingChangePositionY = MainSettingOriginPositionY;
                }
                else if (MainSelectPosition == 2)
                {
                    MainSettingChangePositionY = 212;
                    MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
                }
                Continue = true;
            }

            if (MainSelectPosition == 0)
            {
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
                MainSettingChangePositionY = MainSettingOriginPositionY;

                if (Input.GetAxisRaw("Horizontal") > 0 && Continue == false)
                {
                    Main = false;
                    MenuCursor.GetComponent<Image>().enabled = true;
                    CursorPosition = 0;
                    Continue = true;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && Continue == false)
                {
                    Main = true;
                    MenuCursor.GetComponent<Image>().enabled = false;
                    CursorPosition = 0;
                }

                if (Input.GetAxisRaw("Vertical") == 0)
                {
                    Continue = false;
                }

                if (Input.GetAxisRaw("Vertical") > 0 && Continue == false)
                {
                    if (CursorPosition == 0)
                    {
                        CursorPosition++;
                        CursorChangePositionY = -65;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }
                    else if (CursorPosition == 1)
                    {
                        CursorPosition++;
                        CursorChangePositionY = -195;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }
                    else if (CursorPosition == 2)
                    {
                        CursorPosition++;
                        CursorChangePositionY = -330;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }
                    else if (CursorPosition == 3)
                    {
                        CursorPosition = 0;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorOriginPositionY, CursorOriginPositionZ);
                        CursorChangePositionY = CursorOriginPositionY;
                    }

                    Continue = true;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && Continue == false)
                {
                    if (CursorPosition == 0)
                    {
                        CursorPosition = 3;
                        CursorChangePositionY = -330;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }
                    else if (CursorPosition == 1)
                    {
                        CursorPosition--;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorOriginPositionY, CursorOriginPositionZ);
                        CursorChangePositionY = CursorOriginPositionY;
                    }
                    else if (CursorPosition == 2)
                    {
                        CursorPosition--;
                        CursorChangePositionY = -65;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }
                    else if (CursorPosition == 3)
                    {
                        CursorPosition--;
                        CursorChangePositionY = -195;
                        CursorTransform.transform.localPosition = new Vector3(CursorOriginPositionX, CursorChangePositionY, CursorOriginPositionZ);
                    }

                    Continue = true;
                }

            }
            else if (MainSelectPosition == 1)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && Continue == false)
                {
                    Main = false;
                    if (ExplainController)
                    {
                        OnGamePadExplanationButton();
                        ExplainController = false;
                    }
                    else
                    {
                        OnOperationExplanationButton();
                        ExplainController = true;
                    }
                    Continue = true;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && Continue == false)
                {
                    if (ExplainController)
                    {
                        OnGamePadExplanationButton();
                        ExplainController = false;
                    }
                    else
                    {
                        OnOperationExplanationButton();
                        ExplainController = true;
                        if (Input.GetAxisRaw("Horizontal") < 0 && Continue == false)
                        {
                            Main = true;
                        }
                    }
                    Continue = true;
                }
            }
            else if (MainSelectPosition == 2)
            {
                if (Input.GetKeyDown("joystick button 0"))
                {
                    BackTitleButton();
                }
            }
        }

        if (Input.GetKeyDown("joystick button 1"))//B
        {
            BackGround.GetComponent<Image>().enabled = false;

            SettingMenu.GetComponent<Image>().enabled = false;

            SettingTitle.GetComponent<Image>().enabled = false;

            SettingBack.GetComponent<Image>().enabled = false;

            SettingButton.GetComponent<Image>().enabled = false;

            Select.GetComponent<Image>().enabled = false;

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
            TutorialManager.ON = false;
            //SettingBack.SetActive(false);
            Time.timeScale = 1;
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

        MenuCursor.GetComponent<Image>().enabled = true;

        OperationExplanationButton.GetComponent<Image>().enabled = true;

        TitleButton.GetComponent<Image>().enabled = true;

        MicSlider.gameObject.SetActive(true);

        BGMSlider.gameObject.SetActive(true);

        SESlider.gameObject.SetActive(true);

        MouseSlider.gameObject.SetActive(true);

        //SettingBack.SetActive(true);

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

        MenuCursor.GetComponent<Image>().enabled = true;

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

        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);

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
}
