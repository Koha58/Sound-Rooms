using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound1 : MonoBehaviour
{
    public AudioClip footstepSound;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume = 40f;          // 音量

    private void Start()
    {
        audioSource.clip = footstepSound;
        audioSource.loop = true;
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
