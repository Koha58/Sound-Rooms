using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PlayerAudioRecorder : MonoBehaviour
{
    public float detectionRadius = 5f; // �v���C���[������^������͈�
    public AudioSource recorderAudioSource; // �v���C���[�̘^���@�pAudioSource
    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    private void Start()
    {
        recorderAudioSource.clip =null; 
    }
    void Update()
    {
        // �G��͈͓��Ō��o���A�ł��߂��G�̉����擾
       EnemyController1 closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            AudioSource enemyAudioSource = closestEnemy.GetComponent<AudioSource>();
            if (enemyAudioSource != null && enemyAudioSource.isPlaying)
            {
                PlayCapturedAudio(enemyAudioSource.clip);
                clickToRecordAndVisualize.recordingTime = 0;
            }
        }

        if (clickToRecordAndVisualize.recordingTime==0)
        {
            recorderAudioSource = null;
        }

    }

    private EnemyController1 FindClosestEnemy()
    {
        EnemyController1[] enemies = FindObjectsOfType<EnemyController1>(); // �V�[�����̂��ׂĂ�Enemy���擾
        EnemyController1 closestEnemy = null;
        float closestDistance = detectionRadius;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    private void PlayCapturedAudio(AudioClip audioClip)
    {
        if (recorderAudioSource != null && recorderAudioSource.clip != audioClip)
        {
            recorderAudioSource.clip = audioClip;
            recorderAudioSource.Play();
        }
    }

    // �f�o�b�O�p�F�͈͂̉���
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}