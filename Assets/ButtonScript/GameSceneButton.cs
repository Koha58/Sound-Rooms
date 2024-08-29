using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class GameSceneButton : MonoBehaviour
{
    //����������
    [SerializeField] GameObject OperationExplanation;
    //�R���g���[���[�p�F����uB�Fclose�v
    [SerializeField] GameObject closeButton;
    //�uclose�v
    [SerializeField] GameObject closeKey;
    //������ʕ\����̉��́u�ݒ�v����
    [SerializeField] GameObject SettingFont;
    //������ʕ\����̉��́u��������v����
    [SerializeField] GameObject ExplainFont;
    //������ʕ\����̉��́u/�v
    [SerializeField] GameObject Slash;
    //�ݒ���
    [SerializeField] GameObject SettingMenu;
    //�}�C�N�X���C�_�[
    [SerializeField] Slider MicSlider;
    //BGM�X���C�_�[
    [SerializeField] Slider BGMSlider;
    //SE�X���C�_�[
    [SerializeField] Slider SESlider;
    //�}�E�X�X���C�_�[
    [SerializeField] Slider MouseSlider;

    //�J�[�\��(�R���g���[���[�p)
    [SerializeField] GameObject Cursor;

    Image ExplainFontImage;
    Image SettingFontImage;
    bool deviceCheck;

    // Start is called before the first frame update
    void Start()
    {
        OperationExplanation.GetComponent<Image>().enabled = false;

        closeButton.GetComponent<Image>().enabled = false;

        closeKey.GetComponent<Image>().enabled = false;

        SettingFont.GetComponent<Image>().enabled = false;

        SettingMenu.GetComponent<Image>().enabled = false;

        ExplainFont.GetComponent<Image>().enabled = false;

        Slash.GetComponent<Image>().enabled = false;

        SettingMenu.GetComponent<Image>().enabled = false;

        MicSlider.gameObject.SetActive(false);

        BGMSlider.gameObject.SetActive(false);

        SESlider.gameObject.SetActive(false);

        MouseSlider.gameObject.SetActive(false);

        Cursor.GetComponent<Image>().enabled = false;

        deviceCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && closeButton != null)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && closeKey != null)
        {
            deviceCheck = false;
        }

        if (Input.GetKeyDown("joystick button 7"))//���j���[ �{�^�� 
        {
            SettingMenu.GetComponent<Image>().enabled = true;
            MicSlider.gameObject.SetActive(true);
            BGMSlider.gameObject.SetActive(true);
            SESlider.gameObject.SetActive(true);
            MouseSlider.gameObject.SetActive(true);
            SettingFont.GetComponent<Image>().enabled = true;
            ExplainFont .GetComponent<Image>().enabled = true;
            Slash .GetComponent<Image>().enabled = true;

            Cursor.GetComponent<Image>().enabled = true;

            ExplainFontImage = ExplainFont.GetComponent<Image>();
            ExplainFontImage.color = new Color32(255, 255, 255, 45);

            if (deviceCheck)
            {
                closeButton.GetComponent<Image>().enabled = true;
            }
            else
            {
                closeKey.GetComponent<Image>().enabled = true;
            }
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.B)||Input.GetKeyDown("joystick button 1"))//B
        {
            closeButton.GetComponent<Image>().enabled = false;
            closeKey.GetComponent<Image>().enabled = false;
            OperationExplanation.GetComponent<Image>().enabled = false;
            SettingMenu.GetComponent<Image>().enabled = false;
            MicSlider.gameObject.SetActive(false);
            BGMSlider.gameObject.SetActive(false);
            SESlider.gameObject.SetActive(false);
            MouseSlider.gameObject.SetActive(false);
            SettingFont.GetComponent<Image>().enabled = false;
            ExplainFont.GetComponent<Image>().enabled = false;
            Slash.GetComponent<Image>().enabled = false;
            Cursor.GetComponent<Image>().enabled = false;
            Time.timeScale = 1;
        }

    }

    public void SettingButton()
    {
        SettingMenu.GetComponent<Image>().enabled = true;
        MicSlider.gameObject.SetActive(true);
        BGMSlider.gameObject.SetActive(true);
        SESlider.gameObject.SetActive(true);
        MouseSlider.gameObject.SetActive(true);
        SettingFont.GetComponent<Image>().enabled = true;
        ExplainFont.GetComponent<Image>().enabled = true;
        Slash.GetComponent<Image>().enabled = true;

        ExplainFontImage = ExplainFont.GetComponent<Image>();
        ExplainFontImage.color = new Color32(255, 255, 255, 45);

        if (deviceCheck)
        {
            closeButton.GetComponent<Image>().enabled = true;
            Cursor.GetComponent<Image>().enabled = true;
        }
        else
        {
            closeKey.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void CloseButton()
    {
        closeButton.GetComponent<Image>().enabled = false;
        closeKey.GetComponent<Image>().enabled = false;
        OperationExplanation.GetComponent<Image>().enabled = false;
        SettingMenu.GetComponent<Image>().enabled = false;
        MicSlider.gameObject.SetActive(false);
        BGMSlider.gameObject.SetActive(false);
        SESlider.gameObject.SetActive(false);
        MouseSlider.gameObject.SetActive(false);
        SettingFont.GetComponent<Image>().enabled = false;
        ExplainFont.GetComponent<Image>().enabled = false;
        Slash.GetComponent<Image>().enabled = false;
        Time.timeScale = 1;
    }

    public void SettingFontButton()
    {
        SettingMenu.GetComponent<Image>().enabled = true;
        MicSlider.gameObject.SetActive(true);
        BGMSlider.gameObject.SetActive(true);
        SESlider.gameObject.SetActive(true);
        MouseSlider.gameObject.SetActive(true);
        SettingFont.GetComponent<Image>().enabled = true;
        ExplainFont.GetComponent<Image>().enabled = true;
        Slash.GetComponent<Image>().enabled = true;

        ExplainFontImage = ExplainFont.GetComponent<Image>();
        ExplainFontImage.color = new Color32(255, 255, 255, 45);

        if (OperationExplanation.GetComponent<Image>().enabled == true)
        {
            OperationExplanation.GetComponent<Image>().enabled = false;
            SettingFontImage = SettingFont.GetComponent<Image>();
            SettingFontImage.color = new Color32(255, 255, 255, 255);
        }

        if (deviceCheck)
        {
            closeButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            closeKey.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void ExplainFontButton()
    {
        OperationExplanation.GetComponent<Image>().enabled = true;
        SettingFont.GetComponent<Image>().enabled = true;
        ExplainFont.GetComponent<Image>().enabled = true;
        Slash.GetComponent<Image>().enabled = true;
        SettingFontImage = SettingFont.GetComponent<Image>();
        SettingFontImage.color = new Color32(255, 255, 255, 45);

        if (SettingMenu.GetComponent<Image>().enabled == true)
        {
            SettingMenu.GetComponent<Image>().enabled = false;
            MicSlider.gameObject.SetActive(false);
            BGMSlider.gameObject.SetActive(false);
            SESlider.gameObject.SetActive(false);
            MouseSlider.gameObject.SetActive(false);
            ExplainFontImage = ExplainFont.GetComponent<Image>();
            ExplainFontImage.color = new Color32(255, 255, 255, 255);
        }

        if (deviceCheck)
        {
            closeButton.GetComponent<Image>().enabled = true;
        }
        else
        {
            closeKey.GetComponent<Image>().enabled = true;
        }
        Time.timeScale = 0;
    }

    public void EnterSettingFontButton()
    {
        if(SettingFontImage.color != new Color32(255, 255, 255, 255))
        {
            SettingFontImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void EnterExplainFontButton()
    {
        if (ExplainFontImage.color != new Color32(255, 255, 255, 255))
        {
            ExplainFontImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitSettingFontButton()
    {
        if(SettingMenu.GetComponent<Image>().enabled == false)
        {
            SettingFontImage.color = new Color32(255, 255, 255, 45);
        }
    }

    public void ExitExplainFontButton()
    {
        if(OperationExplanation.GetComponent<Image>().enabled == false)
        {
            ExplainFontImage.color = new Color32(255, 255, 255, 45);
        }
    }
}
