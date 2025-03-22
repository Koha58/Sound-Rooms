using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

public class StageSelectButton : MonoBehaviour
{
    public GameObject[] StageButtons;

    public GameObject RightButton, LeftButton,GameStartButton, BackStartButton;

    public GameObject[] StageVideos;
    public GameObject[] StageTitles;

    int stage;

    bool deviceCheck;

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

        // AudioSource コンポーネントを取得
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (deviceCheck)StageSelect();
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
        // 選択中のUI取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == StageButtons[0])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            BackStartButton.SetActive(false);

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = true;
                StageVideos[1].GetComponent<RawImage>().enabled = false;
                StageVideos[2].GetComponent<RawImage>().enabled = false;
                StageTitles[0].GetComponent<Image>().enabled = true;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = false;
                stage = 0;
            }
        }
        else if (selectedGameObject == StageButtons[1])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            BackStartButton.SetActive(false);

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = true;
                StageVideos[2].GetComponent<RawImage>().enabled = false;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = true;
                StageTitles[2].GetComponent<Image>().enabled = false;
                stage = 1;
            }

        }
        else if (selectedGameObject == StageButtons[2])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            BackStartButton.SetActive(false);
            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = false;
                StageVideos[2].GetComponent<RawImage>().enabled = true;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = true;
                stage = 2;
            }
        }
        else if(selectedGameObject == GameStartButton)
        {
            BackStartButton.SetActive(true);
            if (Input.GetKeyDown("joystick button 0"))
            {
                PlayStartSound(); // 音を再生
                OnStart();
            }
        }
        else if (selectedGameObject == null)
        {
            // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(StageButtons[0]);
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
