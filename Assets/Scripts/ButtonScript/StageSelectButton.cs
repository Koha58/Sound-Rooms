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

    [SerializeField] AudioSource StartSound;  // AudioSourceをSerializeFieldとしてインスペクターから設定

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
        }

        Continue = false;

        // AudioSource コンポーネントを取得
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Transform cursorTransform = Cursor.transform;
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
            RightButton.SetActive(false);
            LeftButton.SetActive(false);
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        if (deviceCheck)
        {
            StageSelect();

            //if (_uiInputActions.SettingUI.MainSelsectUp.triggered)
            //{
            //    stage--;
            //    for (int i = 0; i < StageButtons.Length; i++)
            //    {
            //        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            //    }

            //    if (stage < 0)
            //    {
            //        stage = 0;
            //    }

            //    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //}
            //else if (_uiInputActions.SettingUI.MainSelsectDown.triggered)
            //{
            //    stage++;
            //    for (int i = 0; i < StageButtons.Length; i++)
            //    {
            //        StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            //    }

            //    if (stage > 1)
            //    {
            //        stage = 1;
            //    }

            //    StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            //}


            if (Input.GetKeyDown("joystick button 0"))
            {
                PlayStartSound(); // 音を再生
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

    public void OnStage1Select()
    {
        stage = 1;
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

    public void OnStage2Select()
    {
        stage = 2;
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

    public void EnterStage2SelectButton()
    {
        if (stage != 2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage2SelectButton()
    {
        if (stage != 2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void OnRightButton()
    {
        if(stage != 2)
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
            stage = 2;
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
        else if (stage == 2)
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
        // 音を再生してシーン遷移するコルーチンを開始
        StartCoroutine(PlayStartSound());
    }

    // 音を再生するメソッド
    private IEnumerator PlayStartSound()
    {
        // 音を再生
        StartSound.PlayOneShot(StartSound.clip);

        // 音が再生されるのを待機 (音の長さ分だけ待機)
        yield return new WaitForSeconds(StartSound.clip.length);

        // 音が終了した後にシーンを遷移
        if (stage == 0)
        {
            SceneManager.LoadScene("GetRecorder");
        }
        else if (stage == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (stage == 2)
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
