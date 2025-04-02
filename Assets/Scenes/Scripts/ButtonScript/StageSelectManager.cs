using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    private const int StageIndex0 = 0;
    private const int StageIndex1 = 1;
    private const int StageIndex2 = 2;

    [SerializeField] private GameObject[] StageButtons;
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;

    private int stage;
    private bool deviceCheck;

    [SerializeField] private AudioSource StartSound;

    private float inputDelay = 0.2f; // スティック入力のディレイ時間
    private float lastInputTime = 0f;

    private Dictionary<int, int> stagePriority = new Dictionary<int, int>
    {
        { StageIndex0, 0 },  // チュートリアルが最優先
        { StageIndex1, 1 },
        { StageIndex2, 2 }
    };

    void Start()
    {
        stage = StageIndex0;
        UpdateStageDisplay();

        deviceCheck = InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox;
        StartSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (deviceCheck) HandleStageSelection();
    }

    void HandleStageSelection()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Time.time - lastInputTime < inputDelay) return; // ディレイ処理

        if (input.x > 0.5f) // 右に倒したとき
        {
            SelectNextStage(1);
            lastInputTime = Time.time;
        }
        else if (input.x < -0.5f) // 左に倒したとき
        {
            SelectNextStage(-1);
            lastInputTime = Time.time;
        }
    }

    void SelectNextStage(int direction)
    {
        List<int> orderedStages = new List<int>(stagePriority.Keys);
        orderedStages.Sort((a, b) => stagePriority[a].CompareTo(stagePriority[b])); // 優先度順にソート

        int currentIndex = orderedStages.IndexOf(stage);
        int nextIndex = (currentIndex + direction + orderedStages.Count) % orderedStages.Count;
        stage = orderedStages[nextIndex];

        UpdateStageDisplay();
    }

    void UpdateStageDisplay()
    {
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = (i == stage) ? new Color32(255, 255, 255, 255) : new Color32(255, 255, 255, 45);
            StageVideos[i].GetComponent<RawImage>().enabled = (i == stage);
            StageTitles[i].GetComponent<Image>().enabled = (i == stage);
        }
    }
}