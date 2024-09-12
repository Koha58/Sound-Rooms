using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class GameSceneButton : MonoBehaviour
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

       deviceCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
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
            TutorialManager.ON=true;

            SettingButton.GetComponent<Image>().enabled = true;

            BackGround.GetComponent<Image>().enabled = true;

            SettingMenu.GetComponent<Image>().enabled = true;

            SettingTitle.GetComponent<Image>().enabled = true;

            SettingBack.GetComponent<Image>().enabled = true;

            Select.GetComponent<Image>().enabled = true;

            MenuCursor.GetComponent<Image>().enabled = true;

            OperationExplanationButton.GetComponent<Image>().enabled = true;

            TitleButton.GetComponent<Image>().enabled = true;

            MicSlider.gameObject.SetActive(true);

            BGMSlider.gameObject.SetActive(true);

            SESlider.gameObject.SetActive(true);

            MouseSlider.gameObject.SetActive(true);

            //Cursor.GetComponent<Image>().enabled = true;

            // ExplainFontImage = ExplainFont.GetComponent<Image>();
            //  ExplainFontImage.color = new Color32(255, 255, 255, 45);

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
            TutorialManager.ON =false;
            //SettingBack.SetActive(false);
            Time.timeScale = 1;
        }

    }

    public void SettingButtonON()
    {
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
        SceneManager.LoadScene("StartScene");
    }
}
