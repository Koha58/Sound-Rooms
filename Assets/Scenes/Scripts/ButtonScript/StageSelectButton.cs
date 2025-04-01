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

/// <summary>
/// ステージ選択画面を制御するクラス
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    // 定数定義
    private const int StageIndex0 = 0;  // チュートリアルのインデックス
    private const int StageIndex1 = 1;  // ステージ1のインデックス
    private const int StageIndex2 = 2;  // ステージ2のインデックス

    // ステージボタンを格納する配列
    [SerializeField] private GameObject[] StageButtons;

    // ステージ選択画面で使用するボタンの参照
    [SerializeField] private GameObject RightButton, LeftButton,GameStartButton, BackStartButton;

    // ステージごとの動画とタイトル
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;

    // 現在選択されているステージ番号
    private int stage;

    // 入力デバイスがXboxかどうかを判定するフラグ
    private bool deviceCheck;

    // スタート音を格納するAudioSource
    [SerializeField] AudioSource StartSound;

    // Start is called before the first frame update
    void Start()
    {
        // 初期ステージ番号を設定 (最初のステージ0)
        stage = StageIndex0;

        // ステージボタンの最初のボタンを表示
        StageButtons[stage].GetComponent<Image>().enabled = true;

        // ステージボタンの色を薄く設定 (選択されていないボタンは半透明)
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        // 現在選択されているステージのボタン色を強調表示
        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        // ステージの動画を非表示にし、選択されたステージのみ表示
        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }
        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        // ステージのタイトルを非表示にし、選択されたタイトルのみ表示
        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }
        StageTitles[stage].GetComponent<Image>().enabled = true;

        // 入力デバイスがXboxかキーボードかをチェック
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;  // Xboxの場合
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;  // キーボードの場合
        }

        // AudioSourceコンポーネントを取得
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 入力デバイスの種類に応じて、右左ボタンを非表示にする処理
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

        // Xboxコントローラーの場合はステージ選択を処理
        if (deviceCheck)StageSelect();
    }

    // ステージ0が選択されたときの処理
    public void OnStage0Select()
    {
        stage = StageIndex0;
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
    // ステージ1が選択されたときの処理

    public void OnStage1Select()
    {
        stage = StageIndex2;
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


    // ステージ2が選択されたときの処理
    public void OnStage2Select()
    {
        stage = StageIndex2;
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
        if(stage != StageIndex0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage0SelectButton()
    {
        if (stage != StageIndex0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void EnterStage1SelectButton()
    {
        if (stage != StageIndex1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage1SelectButton()
    {
        if (stage != StageIndex1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void EnterStage2SelectButton()
    {
        if (stage != StageIndex2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage2SelectButton()
    {
        if (stage != StageIndex2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    // 右ボタンが押されたときの処理
    public void OnRightButton()
    {
        if(stage != StageIndex2)
        {
            stage++;
        }
        else
        {
            stage = StageIndex0;
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

    // 左ボタンが押されたときの処理
    public void OnLeftButton()
    {
        if(stage != StageIndex0)
        {
            stage--;
        }
        else
        {
            stage = StageIndex2;
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

    // ステージ選択の処理
    void StageSelect()
    {
        // 現在選択されているゲームオブジェクトを取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == StageButtons[0])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = true;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = false;
            BackStartButton.SetActive(false);

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageTitles[0].GetComponent<Image>().enabled = true;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = false;
                stage = StageIndex0;
            }
        }
        else if (selectedGameObject == StageButtons[1])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = true;
            StageVideos[2].GetComponent<RawImage>().enabled = false;
            BackStartButton.SetActive(false);

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = true;
                StageVideos[2].GetComponent<RawImage>().enabled = false;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = true;
                StageTitles[2].GetComponent<Image>().enabled = false;
                stage = StageIndex1;
            }

        }
        else if (selectedGameObject == StageButtons[2])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = true;
            BackStartButton.SetActive(false);
            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = false;
                StageVideos[2].GetComponent<RawImage>().enabled = true;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = true;
                stage = StageIndex2;
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
            // ボタンが選択されていない場合、最初のボタンにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(StageButtons[0]);
        }
    }

    // ゲーム開始時の処理
    public void OnStart()
    {
        // 音を再生してシーン遷移するコルーチンを開始
        StartCoroutine(PlayStartSound());
    }

    // 音を再生するメソッド
    private IEnumerator PlayStartSound()
    {
        StartSound.PlayOneShot(StartSound.clip);
        yield return new WaitForSeconds(StartSound.clip.length);

        // 音が終了した後にシーンを遷移
        string sceneName = GetSceneNameForStage(stage);
        SceneManager.LoadScene(sceneName);
    }

    // ステージ番号に対応するシーン名を取得するメソッド
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
}
