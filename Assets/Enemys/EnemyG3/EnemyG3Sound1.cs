using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG3Sound1 : MonoBehaviour
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
        GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
        EnemyGController3 ECG3 = eobjG3.GetComponent<EnemyGController3>();
        if (ECG3.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG3.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }
}
