using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy10Sound1 : MonoBehaviour
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
        GameObject eobj10 = GameObject.FindWithTag("Enemy10");
        EnemyController10 EC10 = eobj10.GetComponent<EnemyController10>();
        if (EC10.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC10.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
