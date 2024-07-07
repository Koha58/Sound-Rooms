using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG2Sound2 : MonoBehaviour
{
    // public AudioClip Sound;     // �����̃I�[�f�B�I�N���b�v
    public AudioClip Sound2;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 40f;          // ����

    private void Start()
    {

    }

    private void Update()
    {
        audioSource.clip = Sound2;
        GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
        EnemyGController2 ECG2 = eobjG2.GetComponent<EnemyGController2>();
        if (ECG2.ONoff == 1)
        {
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.volume = volume;
        }

        if (ECG2.ONoff == 0)
        {
            audioSource.enabled = false;
        }
    }
}
