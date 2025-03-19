using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �`���[�g���A���N���A(�E�o����)�N���X
/// ���̃N���X�́A�v���C���[���o���ɏՓ˂����ۂɁA�X�e�[�W1�N���A�V�[���ɑJ�ڂ�����������B
/// </summary>
public class TutorialClear : MonoBehaviour
{
    // �Փ˂����������Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision other)
    {
        // "EnemyAttackArea"�Ƃ������O�̃Q�[���I�u�W�F�N�g���V�[������擾
        GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");

        // "ImpactOnObjects"�X�N���v�g���擾�i�Q�[���I�u�W�F�N�g�̃X�N���v�g�Q�Ɓj
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>();

        // �Փ˂����I�u�W�F�N�g���uExitDoor�v�Ȃ�
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjects�X�N���v�g����count��1�̏ꍇ
            if (impactObjects.count == 1)
            {
                // �V�[�����uTutorialClear�v�ɕύX
                SceneManager.LoadScene("TutorialClear");
            }
        }
    }
}
