using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Sound1 : MonoBehaviour
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
        GameObject eobj2 = GameObject.FindWithTag("Enemy2");
        EnemyController2 EC2 = eobj2.GetComponent<EnemyController2>();
        if (EC2.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC2.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }
}
