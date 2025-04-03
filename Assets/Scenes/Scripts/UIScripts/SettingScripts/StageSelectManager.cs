using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    private const int StageIndex0 = 0;  // チュートリアルのインデックス
    private const int StageIndex1 = 1;  // ステージ1のインデックス
    private const int StageIndex2 = 2;  // ステージ2のインデックス

    [SerializeField] private GameObject[] StageButtons;  // ステージボタンの配列
    [SerializeField] private GameObject RightButton, LeftButton, GameStartButton;
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;
    [SerializeField] private AudioSource StartSound;

    private int stage;
    private bool deviceCheck;  // 入力デバイスがXboxかどうか
    private float moveDelay = 0.5f;  // ボタン切り替えの遅延（秒）
    private float lastMoveTime = -1f;  // 最後にボタンが切り替わった時間

    void Start()
    {
        UpdateStageSelection();
        stage = StageIndex0;
        SetInitialStage();
        CheckInputDevice();
    }

    void Update()
    {
        // Xboxの場合は右左ボタンを非表示にする
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

        // Xboxコントローラーの場合、ステージ選択を処理
        if (deviceCheck)
        {
            StageSelect();
        }
    }

    void StageSelect()
    {
        // ボタン切り替えの遅延時間を管理
        if (Time.time - lastMoveTime > moveDelay)
        {
            if (Input.GetKeyDown("joystick button 0"))  // ジョイスティックのボタンA（決定ボタン）
            {
                PlayStartSound();  // スタート音を再生
                string sceneName = GetSceneNameForStage(stage);
                StartCoroutine(LoadSceneWithDelay(sceneName));
            }
            else if(Input.GetAxisRaw("Vertical") != 0)
            {
                // コントローラーで方向キーやスティックの入力を受け付け、ステージを切り替え
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    MoveStageTop();
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    MoveStageDown();
                }
            }
        }
    }

    void MoveStageTop()
    {
        if (stage != StageIndex2)
        {
            stage++;
        }
        else
        {
            stage = StageIndex0;
        }

        UpdateStageSelection();
        lastMoveTime = Time.time;
    }

    void MoveStageDown()
    {
        if (stage != StageIndex0)
        {
            stage--;
        }
        else
        {
            stage = StageIndex2;
        }

        UpdateStageSelection();
        lastMoveTime = Time.time;
    }

    void UpdateStageSelection()
    {
        // ステージのボタン色や動画、タイトルを更新
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[i].GetComponent<RawImage>().enabled = false;
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        StageVideos[stage].GetComponent<RawImage>().enabled = true;
        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    void SetInitialStage()
    {
        StageButtons[stage].GetComponent<Image>().enabled = true;
        StageVideos[stage].GetComponent<RawImage>().enabled = true;
        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    void CheckInputDevice()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }
    }

    // 音を再生するメソッド
    private void PlayStartSound()
    {
        StartSound.PlayOneShot(StartSound.clip);
    }

    private string GetSceneNameForStage(int stageIndex)
    {
        switch (stageIndex)
        {
            case StageIndex0: return "GetRecorder";
            case StageIndex1: return "Stage1";
            case StageIndex2: return "GameScene";
            default: return "GetRecorder";
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(1f); // 音が終わった後にシーン遷移
        SceneManager.LoadScene(sceneName);
    }
}