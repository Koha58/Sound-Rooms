using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepsG4 : MonoBehaviour
{
    public AudioClip footstepSound;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume =0.5f;          // 音量

    private void Start()
    {

    }

    void Update()
    {
        if (EnemyChaseG4.detectionPlayerG4 <= EnemyChaseG4.Detection)
        {
            audioSource.clip = footstepSound;
            audioSource.loop = true;
        }
    }
    // 足音を再生するメソッド
    public void PlayFootstepSound()
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    // 足音の再生を停止するメソッド
    public void StopFootstepSound()
    {
        audioSource.Stop();
    }
}
