using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioClip;
    public OverflowHandler overflowHandler; // オーバーフロー処理用のクラスへの参照

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);

        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void OnAudioRead(float[] data)
    {
        // オーディオデータを供給する処理
        // ここにオーディオデータの生成や取得のコードを記述
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (data.Length > 1024) // 任意のサイズを指定
        {
            Debug.Log("OK"); 
            HandleBufferOverflow((uint)data.Length);
        }
    }

    private void HandleBufferOverflow(uint overflow)
    {
        Debug.LogWarning($"バッファオーバーフローが発生しました: {overflow} samples discarded.");

        // OverflowHandlerのメソッドを呼び出す
        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);
        }
    }

    void OnDestroy()
    {
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}
