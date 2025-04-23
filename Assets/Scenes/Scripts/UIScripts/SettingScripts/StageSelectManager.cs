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

    private float previousVertical = 0f;

    void Start()
    {
        stage = StageIndex0; // 明示的に初期ステージをセット（必要なら）
        // 選択状態の更新（色とUI表示）
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[i].GetComponent<RawImage>().enabled = false;
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

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
        float currentVertical = Input.GetAxisRaw("Vertical");

        // 決定ボタンの処理
        if (Input.GetKeyDown("joystick button 0"))
        {
            PlayStartSound();
            string sceneName = GetSceneNameForStage(stage);
            StartCoroutine(LoadSceneWithDelay(sceneName));
        }

        // 上下入力の遷移を確認（前フレームと今フレームで値が変化したときのみ）
        if (Time.time - lastMoveTime > moveDelay)
        {
            if (previousVertical == 0f && currentVertical != 0f)
            {
                if (currentVertical > 0f)
                {
                    MoveStageTop();
                }
                else if (currentVertical < 0f)
                {
                    MoveStageDown();
                }

                lastMoveTime = Time.time;
            }
        }

        previousVertical = currentVertical; // 入力値を保存
    }

    void MoveStageTop()
    {
        // ステージを次に進める（最後なら最初に戻す）
        if (stage < StageIndex2)
        {
            stage++;
        }
        else
        {
            stage = StageIndex0;
        }
        UpdateStageSelection();
    }

    void MoveStageDown()
    {
        // ステージを前に戻す（最初なら最後に戻す）
        if (stage > StageIndex0)
        {
            stage--;
        }
        else
        {
            stage =  StageIndex2;
        }
        UpdateStageSelection();
    }

    void UpdateStageSelection()
    {
        // ステージ選択ログを出力
        Debug.Log($"現在選択中のステージ: index = {stage}, 名前 = {StageButtons[stage].name}");

        // 選択状態の更新（色とUI表示）
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[i].GetComponent<RawImage>().enabled = false;
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        StageVideos[stage].GetComponent<RawImage>().enabled = true;
        StageTitles[stage].GetComponent<Image>().enabled = true;

        // UIの選択状態を確実に更新
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(StageButtons[stage]);
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
            case StageIndex2: return "Stage2";
            default: return "GetRecorder";
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return null; // 1フレーム待つ
        SceneManager.LoadScene(sceneName);
    }
}