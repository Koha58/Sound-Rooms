using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���N���A(�E�o����)�N���X
/// ���̃N���X�́A�v���C���[���o���ɏՓ˂����ۂɁA�X�e�[�W1�N���A�V�[���ɑJ�ڂ�����������B
/// </summary>
public class Stage1Clear : MonoBehaviour
{
    // �Փ˂����������Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision other)
    {
        // ImpactOnObjectsArea�Ƃ������O��GameObject��T��
        GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

        // ImpactOnObjects�X�N���v�g���擾
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects�X�N���v�g�̃C���X�^���X���擾

        // �Փ˂����I�u�W�F�N�g�̖��O��"ExitDoor"�̏ꍇ
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjects�X�N���v�g����count��1�̂Ƃ��ɃX�e�[�W�N���A���������s
            if (impactObjects.count == 1)
            {
                // "Stage1Clear"�V�[���ɑJ��
                SceneManager.LoadScene("Stage1Clear");
            }
        }
    }
}