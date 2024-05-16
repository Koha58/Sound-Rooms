using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepsG : MonoBehaviour
{
    public AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    public AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    public float volume = 30f;          // ����
    public bool Soundonoff = true;

    EnemySeen ES;

    private void Start()
    {

    }

    void Update()
    {
      
        GameObject eobj = GameObject.FindWithTag("EnemyG");
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
