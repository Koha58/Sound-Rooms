using UnityEngine;
using UnityEngine.UI;

public class AudioOutputChecker : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioClip;
    public bool overflowOccurred = false; // オーバーフロー発生フラグ
    public OverflowHandler overflowHandler;

    public GameObject SpeakerConnectionBadUI;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);
        audioSource.clip = audioClip;
        audioSource.mute = true;
        overflowOccurred = true; // オーバーフロー発生フラグ
        audioSource.Play();
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    void Update()
    {
        // 3秒待ってから実行する処理を開始
        StartCoroutine(WaitAndExecute());
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = false;
        // 毎フレームオーバーフローが発生していないか確認
        if (overflowOccurred)
        {
            Overflow();
        }
    }

    private System.Collections.IEnumerator WaitAndExecute()
    {
        // 3秒待機
        yield return new WaitForSeconds(3f);
    }

    private void OnAudioRead(float[] data)
    {
        // オーディオデータを生成する処理
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * 440 * (i / 44100f)); // 440Hzのサイン波
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (data.Length > 1024) // 任意のサイズを指定
        {
            overflowOccurred = false;
            Debug.Log("OK");
        }
        else
        {
            overflowOccurred = true; // オーバーフローが発生した場合はフラグを立てる
            HandleBufferOverflow((uint)data.Length);
        }
    }

    private void HandleBufferOverflow(uint overflow)
    {
        Debug.LogWarning($"バッファオーバーフローが発生しました: {overflow} samples discarded.");
        overflowOccurred = true; // オーバーフローが発生した場合フラグを立てる

        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);
        }
    }

    public void Overflow()
    {
        // オーバーフローが発生した場合に実行する処理
        Debug.LogWarning("スピーカーが接続されていません");
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    void OnDestroy()
    {
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}
