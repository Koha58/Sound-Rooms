using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGSound1 : MonoBehaviour
{
    // public AudioClip Sound;     // �����̃I�[�f�B�I�N���b�v
    public AudioClip Sound1;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 40f;          // ����

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound1;
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController ECG = eobjG.GetComponent<EnemyGController>();
        if (ECG.ONoff == 0)
        {
            audioSource.mute = false;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG.ONoff == 1)
        {
            audioSource.mute = true;
        }
    }
}
