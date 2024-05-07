using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepsG2 : MonoBehaviour
{
    public AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 10f;          // ����
    public bool Soundonoff = true;

    private void Start()
    {

    }

    void Update()
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG2");
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
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
