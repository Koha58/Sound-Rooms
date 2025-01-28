using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public AudioClip audioClip;  // 再生するオーディオクリップ

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource を初期化
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;  // 音声をループさせる
    }

    // オブジェクト設置位置で音源を設定
    public void SetAudioSource(Vector3 position)
    {
        if (audioSource != null)
        {
            // AudioSource の位置を変更
            audioSource.transform.position = position;

            // 音を再生
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(audioClip);  // 音声を1回だけ再生
            }
        }
    }
}
