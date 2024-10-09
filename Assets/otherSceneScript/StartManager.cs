using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    public GameObject SelectButton;
    public GameObject BackDesktopButton;

    public GameObject Cursor;
    public GameObject Cursor1;

    bool UPDOWN;

    // Start is called before the first frame update
    void Start()
    {
        SelectButtonImage = SelectButton.GetComponent<Image>();
        BackDesktopButtonImage = BackDesktopButton.GetComponent<Image>();

        SelectButtonImage.color = new Color32(255, 255, 255, 45);
        BackDesktopButtonImage.color = new Color32(255, 255, 255, 45);

        Cursor.SetActive(false);
        Cursor1.SetActive(false);
        UPDOWN = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            SelectButtonImage.color = new Color32(255, 255, 255, 255);
            BackDesktopButtonImage.color = new Color32(255, 255, 255, 45);
            Cursor.SetActive(true);
            Cursor1.SetActive(false);
            UPDOWN = true;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            SelectButtonImage.color = new Color32(255, 255, 255, 45);
            BackDesktopButtonImage.color = new Color32(255, 255, 255, 255);
            Cursor1.SetActive(true);
            Cursor.SetActive(false);
            UPDOWN = false;
        }
        if (UPDOWN == true)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
            }
        }

    }

    public void OnSelect()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OnBackDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    public void EnterSelectButton()
    {
        SelectButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitSelectButton()
    {
        SelectButtonImage.color = new Color32(255, 255, 255, 45);
    }

    public void EnterBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(255, 255, 255, 45);
    }
}
