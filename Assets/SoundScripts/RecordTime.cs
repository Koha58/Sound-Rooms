using UnityEngine;
using UnityEngine.UI;

public class RecordTime : MonoBehaviour
{
    public Image recordText;
    public Image circularGauge;
    public float duration = 20f;

    private float elapsedTime = 0f;
    private bool isRecording = false;

    void OnEnable()
    {
        // 録音状態変更イベントを購読
        ClickToRecordAndVisualize.OnRecordingStatusChanged += OnRecordingStatusChanged;
    }

    void OnDisable()
    {
        // 録音状態変更イベントの購読解除
        ClickToRecordAndVisualize.OnRecordingStatusChanged -= OnRecordingStatusChanged;
    }

    private void Start()
    {
        circularGauge.enabled = false;
        recordText.enabled = false;
    }

    void Update()
    {
        if (isRecording)
        {
            elapsedTime += Time.deltaTime;

            // 録音中のゲージの進行
            float fillAmount = Mathf.Clamp01(elapsedTime / duration);
            circularGauge.fillAmount = fillAmount;
        }
    }

    // 録音状態変更時の処理
    private void OnRecordingStatusChanged(bool isRecording)
    {
        this.isRecording = isRecording;

        if (isRecording)
        {
            elapsedTime = 0f;
            circularGauge.enabled = true;
            recordText.enabled = true;
        }
        else
        {
            circularGauge.fillAmount = 0f;
            circularGauge.enabled = false;
            recordText.enabled = false;

            // 録音停止後にUIを非表示にする処理
            StartCoroutine(HideUIAfterDelay(10f));
        }
    }

    // 録音停止後にUIを10秒後に非表示にする処理
    private System.Collections.IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        circularGauge.enabled = false;
        recordText.enabled = false;
    }
}
