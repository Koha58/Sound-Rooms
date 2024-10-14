using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class StageSelectButton : MonoBehaviour
{
    public GameObject[] StageButtons;

    public GameObject RightButton;
    public GameObject LeftButton;

    public GameObject Cursor;
    public GameObject Cursor1;
    public GameObject Cursor2;

    public GameObject[] StageVideos;
    public GameObject[] StageTitles;

    bool Continue;

    int stage;

    bool deviceCheck;

    bool SetGameStart;

    private float originPositionX = -583;
    private float originPositionY = 245;
    private float originPositionZ = 0;

    private float changePositionY;

    private float mostUnderPositionY;

    private float originSizeX = 1.0f;
    private float originSizeY = 0.5f;
    private float originSizeZ = 1.0f;

    private float StartPositionX = 448;
    private float StartPositionY = -447;
    private float StartPositionZ = 0;

    private float StartSizeX = 1.65f;
    private float StartSizeY = 0.7f;
    private float StartSizeZ = 1.0f;


    float Timer;
    bool TimeON;

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

        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        if (deviceCheck)
        {
            //Transform cursorTransform = Cursor.transform;
           // cursorTransform.transform.localPosition = new Vector3(originPositionX, originPositionY, originPositionZ);
           // changePositionY = originPositionY;
            mostUnderPositionY = 160;

           // cursorTransform.transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);

            Cursor.GetComponent<Image>().enabled = true;
        }
        else
        {
            Cursor.GetComponent<Image>().enabled = false;
        }

        Continue = false;
        Cursor.SetActive(false);
        Cursor1.SetActive(false);
        Cursor2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Transform cursorTransform = Cursor.transform;
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
            Cursor.GetComponent<Image>().enabled = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
            Cursor.GetComponent<Image>().enabled = false;
            Cursor.SetActive(false);
            Cursor1.SetActive(false);
            Cursor2.SetActive(false);
        }

        if(deviceCheck)
        {
            Timer = Timer + 0.01f;
            Debug.Log(Timer);
            if (TimeON == true)
            {
                if (/*Input.GetAxis("Vertical") == 0 */ Timer >= 1.0f && Continue == true)
                {
                    Continue = false;
                    Timer = 0;
                    TimeON = false;
                }

                if (/*Input.GetAxis("Horizontal") == 0*/ Timer >= 1.0f && Continue == true )
                {
                    Continue = false;
                    Timer = 0;
                    TimeON = false;
                }
            }

            if (Input.GetAxis("Vertical") == 0&& Input.GetAxisRaw("Horizontal") ==0)
            {
                Timer = 0.01f;
                Continue = false;
            }

            /*
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                Continue = false;
            }*/


            if (Input.GetAxisRaw("Vertical") < 0 && Continue == false)
            {
                TimeON = true;
                //cursorTransform.transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
                if (stage != 1)
                {
                    stage++;
                    for (int i = 0; i < StageButtons.Length; i++)
                    {
                        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
                    }

                    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Cursor.SetActive(false);
                    Cursor1.SetActive(false);
                    Cursor2.SetActive(true);
                    //changePositionY -= 80;
                    //cursorTransform.transform.localPosition = new Vector3(originPositionX, changePositionY, originPositionZ);
                }
                else
                {
                    stage = 0;
                    for (int i = 0; i < StageButtons.Length; i++)
                    {
                        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
                    }

                    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Cursor.SetActive(false);
                    Cursor1.SetActive(true);
                    Cursor2.SetActive(false);
                    //cursorTransform.transform.localPosition = new Vector3(originPositionX, originPositionY, originPositionZ);
                    // changePositionY = originPositionY;
                }
                Continue = true;
            }
            else if(Input.GetAxisRaw("Vertical") > 0 && Continue == false)
            {
                TimeON = true;
                //cursorTransform.transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
                if (stage != 0)
                {
                    stage--;
                    for (int i = 0; i < StageButtons.Length; i++)
                    {
                        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
                    }

                    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Cursor.SetActive(false);
                    Cursor1.SetActive(true);
                    Cursor2.SetActive(false);

                    //changePositionY += 80;
                    // cursorTransform.transform.localPosition = new Vector3(originPositionX, changePositionY, originPositionZ);
                }
                else
                {
                    stage = 1;
                    for (int i = 0; i < StageButtons.Length; i++)
                    {
                        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
                    }

                    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    Cursor.SetActive(false);
                    Cursor1.SetActive(false);
                    Cursor2.SetActive(true);
                    //cursorTransform.transform.localPosition = new Vector3(originPositionX, mostUnderPositionY, originPositionZ);
                    //changePositionY = mostUnderPositionY;
                }

                Continue = true;
            }

            if (Input.GetAxisRaw("Horizontal") > 0 && Continue == false )
            {
                TimeON = true;
                //cursorTransform.transform.localPosition = new Vector3(StartPositionX, StartPositionY, StartPositionZ);
                //cursorTransform.transform.localScale = new Vector3(StartSizeX, StartSizeY, StartSizeZ);
                Cursor.SetActive(true);
                Cursor1.SetActive(false);
                Cursor2.SetActive(false);

                Continue = true;
                SetGameStart = true;

            }
            else if(Input.GetAxisRaw("Horizontal") < 0 && Continue == false)
            {
                TimeON = true;
                SetGameStart =false;
                //cursorTransform.transform.localPosition = new Vector3(originPositionX, originPositionY, originPositionZ);
                //cursorTransform.transform.localScale = new Vector3(originSizeX, originSizeY, originSizeZ);
                Cursor.SetActive(false);
                Cursor1.SetActive(true);
                Cursor2.SetActive(false);

                StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
                Continue = true;
                stage = 0;
            }
        }

        StageSelect();

        if (SetGameStart == true) 
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                OnStart();
            }
        }

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
        if (stage == 0)
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if(stage == 1)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
