using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGSound2 : MonoBehaviour
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
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController ECG = eobjG.GetComponent<EnemyGController>();
        if (ECG.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }
}
