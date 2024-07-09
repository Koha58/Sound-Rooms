using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6Sound2 : MonoBehaviour
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
        GameObject eobj6 = GameObject.FindWithTag("Enemy6");
        EnemyController6 EC6 = eobj6.GetComponent<EnemyController6>();
        if (EC6.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC6.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
