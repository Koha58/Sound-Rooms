using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG2Sound1 : MonoBehaviour
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
        GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
        EnemyGController2 ECG2 = eobjG2.GetComponent<EnemyGController2>();
        if (ECG2.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG2.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }
}
