using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���𔭐������A�͈͓��̓G�ɒʒm���s���N���X
/// </summary>
public class SoundEmitter : MonoBehaviour
{
    public float soundRange = 5f; // �����͂��͈́i���a�j

    // Start�̓Q�[���J�n���ɌĂ΂��
    void Start()
    {
        // �����������͓��ɂȂ��̂ŁA��̂܂�
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        // ���t���[���A���𔭐�������
        EmitSound();
    }

    /// <summary>
    /// ���𔭐������A�͈͓��̓G�ɒʒm���s�����\�b�h
    /// </summary>
    public void EmitSound()
    {
        // OverlapSphere�͎w�肵�����a�͈͓̔��ɂ���Collider�����o����
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, soundRange);

        // ���o���ꂽ���ׂĂ�Collider�ɑ΂��ď������s��
        foreach (Collider collider in hitColliders)
        {
            // Collider��EnemyController���A�^�b�`����Ă��邩�m�F
            EnemyController enemy = collider.GetComponent<EnemyController>();

            // EnemyController�����݂���ꍇ�A���𕷂����������Ăяo��
            if (enemy != null)
            {
                // �G�L�����N�^�[�ɉ��������������Ƃ�ʒm
                enemy.OnSoundHeard(this.transform.position);
            }
        }
    }

    /// <summary>
    /// ���͈̔͂��V�[���r���[�ŉ������邽�߂̃��\�b�h
    /// </summary>
    void OnDrawGizmosSelected()
    {
        // ���͈̔͂����F�̃��C���[�t���[���ŉ���
        Gizmos.color = Color.yellow;
        // ���͈̔͂����C���[�X�t�B�A�ŕ`��
        Gizmos.DrawWireSphere(this.transform.position, soundRange);
    }
}
