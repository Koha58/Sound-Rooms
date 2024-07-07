using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound2 : MonoBehaviour
{
    public AudioClip Sound2;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume = 1f;          // 音量

    private void Start()
    {

    }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>();

        if (EC.ONoff == 0)
        {
            audioSource.clip = Sound2;
            audioSource.Play();
        }
        if (EC.ONoff == 1)
        {
            audioSource.Stop();
        }

    }

    // 足音を再生するメソッド
    public void PlayFootstepSound()
    {
        audioSource.volume = volume;
        audioSource.Play();
    }

    // 足音の再生を停止するメソッド
    public void StopFootstepSound()
    {
        audioSource.Stop();
    }
}
