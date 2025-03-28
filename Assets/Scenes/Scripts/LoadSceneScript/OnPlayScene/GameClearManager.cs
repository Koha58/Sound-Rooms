using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���N���A���̏������܂Ƃ߂��N���X
/// </summary>
public class GameClearManager : MonoBehaviour
{
    private string currentScene;  // ���݂̃V�[����

    // �Փ˂����������Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision other)
    {
       
        // �Փ˂����I�u�W�F�N�g�̖��O��"ExitDoor"�̏ꍇ
        if (other.gameObject.name == "ExitDoor")
        {
            // ImpactOnObjectsArea�Ƃ������O��GameObject��T��
            GameObject impactObjectsArea = GameObject.Find("ImpactOnObjectsArea");

            // ImpactOnObjects�X�N���v�g���擾
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects�X�N���v�g�̃C���X�^���X���擾

            // ImpactOnObjects�X�N���v�g����count��1�̂Ƃ��ɃX�e�[�W�N���A���������s
            if (impactObjects.count == 1)
            {
                // �v���C���[�̃��C�t��0�ɂȂ����Ƃ��A���݂̃V�[������ۑ�����GameClearScene�ɑJ��
                // ���݂̃V�[������ۑ�
                PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                // �w�肳�ꂽ�V�[���ɑJ��
                SceneManager.LoadScene("GameClearScene");
            }
        }
    }
}
