using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8Sound1 : MonoBehaviour
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
        GameObject eobj8 = GameObject.FindWithTag("Enemy8");
        EnemyController8 EC8 = eobj8.GetComponent<EnemyController8>();
        if (EC8.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC8.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
