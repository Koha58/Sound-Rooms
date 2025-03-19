using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerが設置するラジオの再生管理クラス
/// </summary>
public class RecordManager : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;  // 再生するオーディオクリップ

    // オブジェクトに AudioSource を追加して音源を設定
    public void SetAudioSource(GameObject placedObject)
    {
        // 既存の AudioSource を取得。なければ新しく AudioSource を追加
        AudioSource placedAudioSource = placedObject.GetComponent<AudioSource>();
        if (placedAudioSource == null)
        {
            placedAudioSource = placedObject.AddComponent<AudioSource>();  // AudioSource コンポーネントがない場合、追加する
        }

        // 音源設定
        placedAudioSource.clip = audioClip;  // 再生するオーディオクリップを設定
        placedAudioSource.spatialBlend = 1.0f;  // 3D音源として設定。1.0fは完全に3D音源
        placedAudioSource.volume = 0.1f;  // 音量を0.1に設定（小さめの音量）
        placedAudioSource.loop = false;  // 音声をループしないように設定
        placedAudioSource.rolloffMode = AudioRolloffMode.Linear;  // 音の減衰モードを線形減衰に設定
        placedAudioSource.minDistance = 0f;  // 最小距離を0に設定。音源との距離が0でも音量が減衰しない

        placedAudioSource.Play();  // 設定した音源を再生
    }
}