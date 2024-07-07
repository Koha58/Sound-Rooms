using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG3Sound2 : MonoBehaviour
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
        GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
        EnemyGController3 ECG3 = eobjG3.GetComponent<EnemyGController3>();
        if (ECG3.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG3.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }
}
