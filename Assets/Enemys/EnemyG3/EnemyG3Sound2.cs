using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG3Sound2 : MonoBehaviour
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
