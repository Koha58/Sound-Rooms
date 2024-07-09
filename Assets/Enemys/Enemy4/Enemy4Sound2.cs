using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Sound2 : MonoBehaviour
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
        GameObject eobj4 = GameObject.FindWithTag("Enemy4");
        EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>();
        if (EC4.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC4.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }

}
