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
    }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>();
        if (EC.ONoff == 0)
        {
            PlayNextClip();
        }
        if (EC.ONoff == 1)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        // 次の音声のインデックスを更新する
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;

        // 次の音声をオーディオソースに設定して再生する
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }
}