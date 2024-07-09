using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Sound1 : MonoBehaviour
{
    // public AudioClip Sound;     // 足音のオーディオクリップ
    public AudioClip Sound1;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume = 40f;          // 音量

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound1;
        GameObject eobj4 = GameObject.FindWithTag("Enemy4");
        EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>();
        if (EC4.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC4.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
