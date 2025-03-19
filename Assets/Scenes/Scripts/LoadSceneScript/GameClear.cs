using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���N���A(�E�o����)�N���X
/// ���̃N���X�́A�v���C���[���o���ɏՓ˂����ۂɁA�Q�[���N���A�V�[���ɑJ�ڂ�����������B
/// </summary>
public class GameClear : MonoBehaviour
{
    // �Փ˂������������ɌĂ΂��
    void OnCollisionEnter(Collision other)
    {
        // "ImpactOnObjectsArea" �Ƃ������O�̃I�u�W�F�N�g��T��
        GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

        // "ImpactOnObjects" �X�N���v�g�� "impactObjectsArea" �I�u�W�F�N�g����擾
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); //�t���Ă���X�N���v�g���擾

        // �Փ˂����I�u�W�F�N�g�� "ExitDoor" �Ƃ������O�ł������ꍇ
        if (other.gameObject.name == "ExitDoor")
        {
            // "ImpactOnObjects" �X�N���v�g���̃J�E���g��1�̏ꍇ�ɃQ�[���N���A�V�[���֑J��
            if (impactObjects.count == 1)
            {
                // �Q�[���N���A�V�[���ɑJ��
                SceneManager.LoadScene("GameClear");
            }
        }
    }
}