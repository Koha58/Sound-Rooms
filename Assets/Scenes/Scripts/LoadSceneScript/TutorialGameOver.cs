using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// TutorialScene�ł̃��C�t�Ǘ��N���X
/// </summary>
public class TutorialGameOver : MonoBehaviour
{
    public int LifeCount;  // �v���C���[�̎c�胉�C�t��
    [SerializeField] GameObject Life1;  // ���C�t1��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life2;  // ���C�t2��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life3;  // ���C�t3��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life4;  // ���C�t4��UI�I�u�W�F�N�g
    [SerializeField] GameObject Life5;  // ���C�t5��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife1;  // ����ꂽ���C�t1��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife2;  // ����ꂽ���C�t2��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife3;  // ����ꂽ���C�t3��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife4;  // ����ꂽ���C�t4��UI�I�u�W�F�N�g
    [SerializeField] GameObject LostLife5;  // ����ꂽ���C�t5��UI�I�u�W�F�N�g

    private float Timer;  // �^�C�}�[ (���C�t�������Ă��玟�̏����܂ł̎��Ԃ��v��)
    private float Count;  // �J�E���g (���C�t����������̑ҋ@�t���O)

    // Start is called before the first frame update
    void Start()
    {
        // �����ݒ�Ƃ��āA���C�t��UI��S�ĕ\��
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // ����ꂽ���C�t��UI�͍ŏ��͔�\��
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ���C�t����5�ɐݒ�
        LifeCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�����C�t����������A�ҋ@���Ă�����
        if (Count == 1)
        {
            Timer += Time.deltaTime;  // �o�ߎ��Ԃ����Z
            if (Timer >= 3)  // 3�b�o�߂�����
            {
                Timer = 0;  // �^�C�}�[�����Z�b�g
                Count = 0;  // �ҋ@��Ԃ�����
            }
        }
    }

    // �g���K�[�ɓ������Ƃ��ɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // �v���C���[�I�u�W�F�N�g��T��
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeen�X�N���v�g���擾

        // �v���C���[�̃��C�t�ɉ�����UI��ύX
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // ���C�t5���\��
            LostLife5.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t5��\��
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // ���C�t4���\��
            LostLife4.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t4��\��
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // ���C�t3���\��
            LostLife3.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t3��\��
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // ���C�t2���\��
            LostLife2.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t2��\��
        }
        else if (LifeCount == 0)
        {
            Life1.GetComponent<Image>().enabled = false;  // ���C�t1���\��
            LostLife1.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t1��\��
            SceneManager.LoadScene("GameOver_Tutorial");  // �Q�[���I�[�o�[��ʂɑJ��
        }
    }
}