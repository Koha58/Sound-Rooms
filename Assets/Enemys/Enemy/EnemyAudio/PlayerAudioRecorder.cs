using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class PlayerAudioRecorder : MonoBehaviour
{
    public float detectionRadius = 5f; // プレイヤーが音を録音する範囲
    public AudioSource recorderAudioSource; // プレイヤーの録音機用AudioSource
    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    private void Start()
    {
        recorderAudioSource.clip =null; 
    }
    void Update()
    {
        // 敵を範囲内で検出し、最も近い敵の音を取得
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
        EnemyController1[] enemies = FindObjectsOfType<EnemyController1>(); // シーン内のすべてのEnemyを取得
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

    // デバッグ用：範囲の可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}