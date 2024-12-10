using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioRecorder : MonoBehaviour
{
    public float detectionRadius = 5f; // プレイヤーが敵の音を検出する範囲
    public AudioSource recorderAudioSource; // プレイヤーの録音機（AudioSource）
    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    void Update()
    {
        // プレイヤーの周囲にいる敵を検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var collider in colliders)
        {
            // 敵がAudioSourceを持っているか確認
            AudioSource enemyAudioSource = collider.GetComponent<AudioSource>();
            if (enemyAudioSource != null && enemyAudioSource.isPlaying)
            {
                // 敵のAudioSourceの現在再生中の音を取得
                AudioClip enemyClip = enemyAudioSource.clip;
                if (enemyClip != null)
                {
                    if (clickToRecordAndVisualize.itemDrop) {
                        PlayCapturedAudio(enemyClip);
                        clickToRecordAndVisualize.itemDrop = false;
                    }
                }
                break; // 最初に見つけた敵の音のみ再生（必要に応じて変更可能）
            }
        }
    }

    private void PlayCapturedAudio(AudioClip audioClip)
    {
        // プレイヤーの録音機に敵の音をセットして再生
        if (recorderAudioSource != null && recorderAudioSource.clip != audioClip)
        {
            recorderAudioSource.clip = audioClip;
            recorderAudioSource.Play();
        }
    }

    // デバッグ用：範囲の可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
