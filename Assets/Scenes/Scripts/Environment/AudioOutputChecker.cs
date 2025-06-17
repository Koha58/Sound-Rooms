using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スピーカーが接続されていない場合にエラーUIを表示するクラス
/// ※ AudioListener.GetOutputData() を利用して、音声出力の有無を擬似的に検出します。
/// </summary>
public class AudioOutputChecker : MonoBehaviour
{
    [SerializeField] private GameObject SpeakerConnectionBadUI; // エラー表示用のUI（Imageを想定）

    private float[] audioSamples = new float[256]; // 出力音声のバッファ
    private float checkInterval = 3f; // チェックの間隔（秒）
    private float timer = 0f; // タイマー（経過時間を記録）

    void Start()
    {
        // 起動時にエラーUIを非表示にする
        SetSpeakerUIVisible(false);

        // 無音の AudioClip を作成し、再生しておくことで AudioListener から出力データが取得できるようにする
        var source = gameObject.AddComponent<AudioSource>();
        source.clip = AudioClip.Create("Silent", 44100, 1, 44100, false); // 1秒分の無音クリップ
        source.loop = true;  // ループ再生
        source.Play();       // 再生開始
    }

    void Update()
    {
        // 一定時間ごとに出力状態をチェック
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            CheckSpeakerOutput();
        }
    }

    /// <summary>
    /// スピーカーから音声が出ているかを擬似的に確認する
    /// </summary>
    private void CheckSpeakerOutput()
    {
        // チャンネル0（左）から256サンプル分の音声データを取得
        AudioListener.GetOutputData(audioSamples, 0);

        float level = 0f;
        // 各サンプルの絶対値を加算して、音量レベルの目安とする
        foreach (var sample in audioSamples)
        {
            level += Mathf.Abs(sample);
        }

        // 閾値未満なら出力されていないと判断（＝スピーカー未接続の可能性）
        bool noOutput = level < 0.0001f;

        // エラーUIを切り替える
        SetSpeakerUIVisible(noOutput);

        // デバッグログで状態を通知
        if (noOutput)
        {
            Debug.LogWarning("スピーカーが接続されていない可能性があります");
        }
        else
        {
            Debug.Log("スピーカーが接続されています");
        }
    }

    /// <summary>
    /// UIの表示・非表示を制御する
    /// </summary>
    /// <param name="isVisible">true で表示、false で非表示</param>
    private void SetSpeakerUIVisible(bool isVisible)
    {
        if (SpeakerConnectionBadUI != null)
        {
            var image = SpeakerConnectionBadUI.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = isVisible;
            }
        }
    }
}
