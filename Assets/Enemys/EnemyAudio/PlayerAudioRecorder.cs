using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioRecorder : MonoBehaviour
{
    public float detectionRadius = 5f; // �v���C���[���G�̉������o����͈�
    public AudioSource recorderAudioSource; // �v���C���[�̘^���@�iAudioSource�j
    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    void Update()
    {
        // �v���C���[�̎��͂ɂ���G�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (var collider in colliders)
        {
            // �G��AudioSource�������Ă��邩�m�F
            AudioSource enemyAudioSource = collider.GetComponent<AudioSource>();
            if (enemyAudioSource != null && enemyAudioSource.isPlaying)
            {
                // �G��AudioSource�̌��ݍĐ����̉����擾
                AudioClip enemyClip = enemyAudioSource.clip;
                if (enemyClip != null)
                {
                    if (clickToRecordAndVisualize.itemDrop) {
                        PlayCapturedAudio(enemyClip);
                        clickToRecordAndVisualize.itemDrop = false;
                    }
                }
                break; // �ŏ��Ɍ������G�̉��̂ݍĐ��i�K�v�ɉ����ĕύX�\�j
            }
        }
    }

    private void PlayCapturedAudio(AudioClip audioClip)
    {
        // �v���C���[�̘^���@�ɓG�̉����Z�b�g���čĐ�
        if (recorderAudioSource != null && recorderAudioSource.clip != audioClip)
        {
            recorderAudioSource.clip = audioClip;
            recorderAudioSource.Play();
        }
    }

    // �f�o�b�O�p�F�͈͂̉���
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
