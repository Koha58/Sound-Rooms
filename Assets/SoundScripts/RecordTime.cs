using UnityEngine;
using UnityEngine.UI;

public class RecordTime : MonoBehaviour
{
    public Image RecordText; // 「録音中」のImageコンポーネント
    public Image circularGauge; // ゲージ用のImageコンポーネント
    public float duration = 20f; // ゲージが満タンになるまでの時間

    private float elapsedTime = 0f;
    private bool isRecording = false;

    void OnEnable()
    {
        // イベントの購読
        ClickToRecordAndVisualize.OnRecordingStatusChanged += OnRecordingStatusChanged;
    }

    void OnDisable()
    {
        // イベントの購読解除
        ClickToRecordAndVisualize.OnRecordingStatusChanged -= OnRecordingStatusChanged;
    }

    private void Start()
    {
        circularGauge.enabled = false;
        RecordText.enabled = false;
    }

    void Update()
    {
        if (isRecording)
        {
            elapsedTime += Time.deltaTime;

            // 経過時間を正規化してゲージを更新
            float fillAmount = Mathf.Clamp01(elapsedTime / duration);
            circularGauge.fillAmount = fillAmount;
        }
    }

    private void OnRecordingStatusChanged(bool isRecording)
    {
        this.isRecording = isRecording;

        if (isRecording)
        {
            // 録音が開始されたとき
            elapsedTime = 0f;
            circularGauge.enabled = true;
            RecordText.enabled = true;
        }
        else
        {
            // 録音が終了したとき
            circularGauge.fillAmount = 0f;
            circularGauge.enabled = false;
            RecordText.enabled = false;
        }
    }
}
