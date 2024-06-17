using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip[] audioClips; // 再生する音声の配列
    private AudioSource audioSource;

    private int currentClipIndex = 0; // 現在再生中の音声のインデックス

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioClips());
    }

    private IEnumerator PlayAudioClips()
    {
        while (true)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length); // 現在の音声の再生が終了するまで待つ

            // インデックスを次の音声に更新
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        }
    }
}