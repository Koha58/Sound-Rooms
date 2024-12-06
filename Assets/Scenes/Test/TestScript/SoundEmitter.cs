using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public float soundRange =10f; // �����͂��͈�
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {

        EmitSound();

    }

    public void EmitSound()
    {
        // �����Đ�
        audioSource.Play();

        // �͈͓��̓G�����o���Ēʒm
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, soundRange);
        foreach (Collider collider in hitColliders)
        {
            EnemyController1 enemy = collider.GetComponent<EnemyController1>();
            EnemyController2 enemy2 = collider.GetComponent<EnemyController2>();
            EnemyController3 enemy3 = collider.GetComponent<EnemyController3>();
            if (enemy != null)
            {
                enemy.OnSoundHeard(transform.position);
            }
            if (enemy2 != null)
            {
                enemy2.OnSoundHeard(transform.position);
            }
            if (enemy3 != null)
            {
                enemy3.OnSoundHeard(transform.position);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // ���͈̔͂�����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }

}
