using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9Sound1 : MonoBehaviour
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
        GameObject eobj9 = GameObject.FindWithTag("Enemy9");
        EnemyController9 EC9 = eobj9.GetComponent<EnemyController9>();
        if (EC9.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (EC9.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }

}
