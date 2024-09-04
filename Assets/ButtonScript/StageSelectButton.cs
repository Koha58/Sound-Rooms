using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    Image Stage0ButtonImage;
    Image Stage1ButtonImage;

    public GameObject Stage0Button;
    public GameObject Stage1Button;

    public GameObject Cursor;
    public GameObject Cursor1;

    bool UPDOWN;

    // Start is called before the first frame update
    void Start()
    {
        Stage0ButtonImage = Stage0Button.GetComponent<Image>();
        Stage1ButtonImage = Stage1Button.GetComponent<Image>();

        Stage0ButtonImage.color = new Color32(255, 255, 255, 45);
        Stage1ButtonImage.color = new Color32(255, 255, 255, 45);

        Cursor.SetActive(false);
        Cursor1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            Stage0ButtonImage.color = new Color32(255, 255, 255, 255);
            Stage1ButtonImage.color = new Color32(255, 255, 255, 45);
            Cursor.SetActive(true);
            Cursor1.SetActive(false);
            UPDOWN = true;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            Stage0ButtonImage.color = new Color32(255, 255, 255, 45);
            Stage1ButtonImage.color = new Color32(255, 255, 255, 255);
            Cursor1.SetActive(true);
            Cursor.SetActive(false);
            UPDOWN = false;
        }
        if (UPDOWN == true)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("TutorialScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

    }

    public void OnStage0Select()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void OnStage1Select()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EnterStage0SelectButton()
    {
        Stage0ButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitStage0SelectButton()
    {
        Stage0ButtonImage.color = new Color32(255, 255, 255, 45);
    }

    public void EnterStage1SelectButton()
    {
        Stage1ButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitStage1SelectButton()
    {
        Stage1ButtonImage.color = new Color32(255, 255, 255, 45);
    }
}
