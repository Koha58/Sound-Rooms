using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Sound2 : MonoBehaviour
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
        GameObject eobj5 = GameObject.FindWithTag("Enemy5");
        EnemyController5 EC5 = eobj5.GetComponent<EnemyController5>();
        if (EC5.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC5.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }
}
