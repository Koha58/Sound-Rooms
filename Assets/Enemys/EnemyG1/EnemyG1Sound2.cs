using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG1Sound2 : MonoBehaviour
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
        GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
        EnemyGController1 ECG1 = eobjG1.GetComponent<EnemyGController1>();
        if (ECG1.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG1.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }
}
