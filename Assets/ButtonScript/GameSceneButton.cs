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
    //����������
    [SerializeField] GameObject OperationExplanation;
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

        OperationExplanation.GetComponent<Image>().enabled = false;

        SettingTitle.GetComponent<Image>().enabled = false;

        SettingBack.GetComponent<Image>().enabled = false;

        Select.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

        CloseButton.GetComponent<Image>().enabled = false;

        CloseButtonB.GetComponent<Image>().enabled = false;

        SettingButton.GetComponent<Image>().enabled = false;

        OperationExplanationButton.GetComponent<Image>().enabled = false;

        TitleButton.GetComponent<Image>().enabled = false;

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

            BackGround.GetComponent<Image>().enabled = true;

            SettingMenu.GetComponent<Image>().enabled = true;

            SettingTitle.GetComponent<Image>().enabled = true;

            SettingBack.GetComponent<Image>().enabled = true;

            Select.GetComponent<Image>().enabled = true;

            MenuCursor.GetComponent<Image>().enabled = true;

            SettingButton.GetComponent<Image>().enabled = true;

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

            OperationExplanation.GetComponent<Image>().enabled = false;

            SettingTitle.GetComponent<Image>().enabled = false;

            SettingBack.GetComponent<Image>().enabled = false;

            Select.GetComponent<Image>().enabled = false;

            MenuCursor.GetComponent<Image>().enabled = false;

            CloseButton.GetComponent<Image>().enabled = false;

            CloseButtonB.GetComponent<Image>().enabled = false;

            SettingButton.GetComponent<Image>().enabled = false;

            OperationExplanationButton.GetComponent<Image>().enabled = false;

            TitleButton.GetComponent<Image>().enabled = false;

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

        Select.GetComponent<Image>().enabled = true;

        MenuCursor.GetComponent<Image>().enabled = true;

        SettingButton.GetComponent<Image>().enabled = true;

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

        OperationExplanation.GetComponent<Image>().enabled = false;

        SettingTitle.GetComponent<Image>().enabled = false;

        SettingBack.GetComponent<Image>().enabled = false;

        Select.GetComponent<Image>().enabled = false;

        MenuCursor.GetComponent<Image>().enabled = false;

        CloseButton.GetComponent<Image>().enabled = false;

        CloseButtonB.GetComponent<Image>().enabled = false;

        SettingButton.GetComponent<Image>().enabled = false;

        OperationExplanationButton.GetComponent<Image>().enabled = false;

        TitleButton.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);
        Time.timeScale = 1;
        //SettingBack.SetActive(true);

    }

    public void OnSettingMenuButton()
    {
        //SettingMenu.GetComponent<Image>().enabled = true;
        //MicSlider.gameObject.SetActive(true);
        //BGMSlider.gameObject.SetActive(true);
        //SESlider.gameObject.SetActive(true);
        //MouseSlider.gameObject.SetActive(true);
        //SettingFont.GetComponent<Image>().enabled = true;
        //ExplainFont.GetComponent<Image>().enabled = true;
        //Slash.GetComponent<Image>().enabled = true;

        //SettingBack.SetActive(true);

        //ExplainFontImage = ExplainFont.GetComponent<Image>();
        //ExplainFontImage.color = new Color32(255, 255, 255, 45);

        //if (OperationExplanation.GetComponent<Image>().enabled == true)
        //{
        //    OperationExplanation.GetComponent<Image>().enabled = false;
        //    SettingFontImage = SettingFont.GetComponent<Image>();
        //    SettingFontImage.color = new Color32(255, 255, 255, 255);
        //}

        //if (deviceCheck)
        //{
        //    closeButton.GetComponent<Image>().enabled = true;
        //}
        //else
        //{
        //    closeKey.GetComponent<Image>().enabled = true;
        //}
        //Time.timeScale = 0;
    }

    public void ExplainFontButton()
    {
        //OperationExplanation.GetComponent<Image>().enabled = true;
        //SettingFont.GetComponent<Image>().enabled = true;
        //ExplainFont.GetComponent<Image>().enabled = true;
        //Slash.GetComponent<Image>().enabled = true;
        //SettingFontImage = SettingFont.GetComponent<Image>();
        //SettingFontImage.color = new Color32(255, 255, 255, 45);

        //if (SettingMenu.GetComponent<Image>().enabled == true)
        //{
        //    SettingMenu.GetComponent<Image>().enabled = false;
        //    MicSlider.gameObject.SetActive(false);
        //    BGMSlider.gameObject.SetActive(false);
        //    SESlider.gameObject.SetActive(false);
        //    MouseSlider.gameObject.SetActive(false);
        //    ExplainFontImage = ExplainFont.GetComponent<Image>();
        //    ExplainFontImage.color = new Color32(255, 255, 255, 255);
        //}

        //if (deviceCheck)
        //{
        //    closeButton.GetComponent<Image>().enabled = true;
        //}
        //else
        //{
        //    closeKey.GetComponent<Image>().enabled = true;
        //}
        //Time.timeScale = 0;
    }

    public void EnterSettingFontButton()
    {
        //if(SettingFontImage.color != new Color32(255, 255, 255, 255))
        //{
        //    SettingFontImage.color = new Color32(255, 255, 255, 255);
        //}
    }

    public void EnterExplainFontButton()
    {
        //if (ExplainFontImage.color != new Color32(255, 255, 255, 255))
        //{
        //    ExplainFontImage.color = new Color32(255, 255, 255, 255);
        //}
    }

    public void ExitSettingFontButton()
    {
        //if(SettingMenu.GetComponent<Image>().enabled == false)
        //{
        //    SettingFontImage.color = new Color32(255, 255, 255, 45);
        //}
    }

    public void ExitExplainFontButton()
    {
        //if(OperationExplanation.GetComponent<Image>().enabled == false)
        //{
        //    ExplainFontImage.color = new Color32(255, 255, 255, 45);
        //}
    }
}
