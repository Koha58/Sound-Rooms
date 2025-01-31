using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public AudioClip audioClip;  // 再生するオーディオクリップ

    // Start is called before the first frame update
    void Start()
    {

    }

    // オブジェクトに AudioSource を追加して音源を設定
    public void SetAudioSource(GameObject placedObject)
    {
        AudioSource placedAudioSource = placedObject.GetComponent<AudioSource>();
        if (placedAudioSource == null)
        {
            placedAudioSource = placedObject.AddComponent<AudioSource>();
        }

        // 音源設定
        placedAudioSource.clip = audioClip;
        placedAudioSource.spatialBlend = 1.0f;  // 3D 音源
        placedAudioSource.volume = 0.1f;  // 音量を0.1に設定
        placedAudioSource.loop = false;  // ループをオフ
        placedAudioSource.rolloffMode = AudioRolloffMode.Linear;  // ロールオフをLinearに設定
        placedAudioSource.minDistance = 0f;  // 最小距離を0に設定

        placedAudioSource.Play();  // 再生
    }
}