using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public GameObject[] StageButtons;

    public GameObject RightButton;
    public GameObject LeftButton;

    public GameObject Cursor;
    public GameObject Cursor1;

    public GameObject[] StageVideos;
    public GameObject[] StageTitles;

    bool UPDOWN;

    int stage;

    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        StageButtons[stage].GetComponent<Image>().enabled = true;

        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;

        Cursor.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Transform cursorTransform = Cursor.transform;
        //if (Input.GetAxisRaw("Vertical") < 0)
        //{
        //   if(stage == 0)
        //    {

        //    }
        //    Stage0ButtonImage.color = new Color32(255, 255, 255, 255);
        //    Stage1ButtonImage.color = new Color32(255, 255, 255, 45);
        //    Cursor.SetActive(true);
        //    Cursor1.SetActive(false);
        //    UPDOWN = true;
        //}

        //if (Input.GetAxisRaw("Vertical") > 0)
        //{
        //    Stage0ButtonImage.color = new Color32(255, 255, 255, 45);
        //    Stage1ButtonImage.color = new Color32(255, 255, 255, 255);
        //    UPDOWN = false;
        //}
        //if (UPDOWN == true)
        //{
        //    if (Input.GetKeyDown("joystick button 0"))
        //    {
        //        SceneManager.LoadScene("TutorialScene");
        //    }
        //}
        //else
        //{
        //    if (Input.GetKeyDown("joystick button 0"))
        //    {
        //        SceneManager.LoadScene("GameScene");
        //    }
        //}

        StageSelect();

    }

    public void OnStage0Select()
    {
        stage = 0;
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnStage1Select()
    {
        stage = 1;
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void EnterStage0SelectButton()
    {
        if(stage != 0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage0SelectButton()
    {
        if (stage != 0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void EnterStage1SelectButton()
    {
        if (stage != 1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage1SelectButton()
    {
        if (stage != 1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void OnRightButton()
    {
        if(stage != 1)
        {
            stage++;
        }
        else
        {
            stage = 0;
        }
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    public void OnLeftButton()
    {
        if(stage != 0)
        {
            stage--;
        }
        else
        {
            stage = 1;
        }
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    void StageSelect()
    {
        if (stage == 0)
        {
            for (int i = 0; i < StageVideos.Length; i++)
            {
                StageVideos[i].GetComponent<RawImage>().enabled = false;
            }

            StageVideos[stage].GetComponent<RawImage>().enabled = true;

            for (int i = 0; i < StageTitles.Length; i++)
            {
                StageTitles[i].GetComponent<Image>().enabled = false;
            }

            StageTitles[stage].GetComponent<Image>().enabled = true;
        }
        else if (stage == 1)
        {
            for (int i = 0; i < StageVideos.Length; i++)
            {
                StageVideos[i].GetComponent<RawImage>().enabled = false;
            }

            StageVideos[stage].GetComponent<RawImage>().enabled = true;

            for (int i = 0; i < StageTitles.Length; i++)
            {
                StageTitles[i].GetComponent<Image>().enabled = false;
            }

            StageTitles[stage].GetComponent<Image>().enabled = true;
        }
    }

    public void OnStart()
    {
        if(stage == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if(stage == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
