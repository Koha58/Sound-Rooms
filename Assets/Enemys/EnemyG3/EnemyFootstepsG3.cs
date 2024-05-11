using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepsG3 : MonoBehaviour
{
    public AudioClip footstepSound;     // 足音のオーディオクリップ
    public AudioSource audioSource;     // オーディオソース
    public float volume = 30f;          // 音量
    public bool Soundonoff = true;

    private void Start()
    {

    }

    void Update()
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG3");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (ES.ONoff == 1)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = footstepSound;
                audioSource.Play();
            }
        }
        if (ES.ONoff == 0)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }
}
