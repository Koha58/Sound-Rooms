using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy7Sound1 : MonoBehaviour
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
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
        if (EC1.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC1.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
