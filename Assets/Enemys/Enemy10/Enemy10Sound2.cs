using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy10Sound2 : MonoBehaviour
{
    // public AudioClip Sound;     // 足音のオーディオクリップ
    public AudioClip Sound2;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume = 40f;          // 音量

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound2;
        GameObject eobj10 = GameObject.FindWithTag("Enemy10");
        EnemyController10 EC10 = eobj10.GetComponent<EnemyController10>();
        if (EC10.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC10.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
