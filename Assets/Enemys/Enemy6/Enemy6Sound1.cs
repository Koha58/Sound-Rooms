using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Sound1 : MonoBehaviour
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
        GameObject eobj6 = GameObject.FindWithTag("Enemy6");
        EnemyController6 EC6 = eobj6.GetComponent<EnemyController6>();
        if (EC6.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC6.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
