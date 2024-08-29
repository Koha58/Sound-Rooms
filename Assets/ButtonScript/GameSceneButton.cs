using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class GameSceneButton : MonoBehaviour
{
    //操作説明画面
    [SerializeField] GameObject OperationExplanation;
    //コントローラー用：閉じる「B：close」
    [SerializeField] GameObject closeButton;
    //「close」
    [SerializeField] GameObject closeKey;
    //説明画面表示後の下の「設定」文字
    [SerializeField] GameObject SettingFont;
    //説明画面表示後の下の「操作説明」文字
    [SerializeField] GameObject ExplainFont;
    //説明画面表示後の下の「/」
    [SerializeField] GameObject Slash;
    //設定画面
    [SerializeField] GameObject SettingMenu;
    //マイクスライダー
    [SerializeField] Slider MicSlider;
    //BGMスライダー
    [SerializeField] Slider BGMSlider;
    //SEスライダー
    [SerializeField] Slider SESlider;
    //マウススライダー
    [SerializeField] Slider MouseSlider;

    //カーソル(コントローラー用)
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

        if (Input.GetKeyDown("joystick button 7"))//メニュー ボタン 
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
