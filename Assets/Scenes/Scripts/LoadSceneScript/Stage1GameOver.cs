using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Stage1Scene�ł̃��C�t�Ǘ��N���X
/// </summary>
public class Stage1GameOver : MonoBehaviour
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
        // �v���C���[�̏������C�t�A�C�R����S�ĕ\��
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // ���������C�t�̃A�C�R���͏�����ԂŔ�\��
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ���C�t���̏����l��5�ɐݒ�
        LifeCount = 5;
        Count = 0;  // �J�E���g��0�Ƀ��Z�b�g
    }

    // Update is called once per frame
    void Update()
    {
        // �J�E���g��1�̏ꍇ�A5�b�Ԃ̑ҋ@�^�C����ݒ�
        if (Count == 1)
        {
            Timer += Time.deltaTime;  // �^�C�}�[���o�ߎ��ԂōX�V
            if (Timer >= 5.0f)  // 5�b�o�߂�����
            {
                Timer = 0;  // �^�C�}�[�����Z�b�g
                Count = 0;  // �J�E���g��0�ɖ߂�
            }
        }
    }

    // �g���K�[�ɓ������Ƃ��ɌĂяo�����
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�I�u�W�F�N�g���擾
        GameObject gobj = GameObject.Find("Player");
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>();

        // �G�ɐڐG�����ꍇ�̏���
        if (other.CompareTag("Enemy"))
        {
            // �v���C���[�������Ă����ԁionoff == 1�j�̂Ƃ��������C�t������
            if (PS.onoff == 1)
            {
                if (Count == 0)  // �܂����C�t�����炵�Ă��Ȃ��ꍇ
                {
                    LifeCount--;  // ���C�t��1���炷
                    Count = 1;  // 1��ڂ̃��C�t�������J�E���g
                }
            }
        }

        // ���C�t���ɉ����ĕ\������A�C�R����ύX
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // 5�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife5.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // 4�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife4.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // 3�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife3.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // 2�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife2.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��
        }
        else if (LifeCount == 0)
        {
            // �Ō�̃��C�t���������ꍇ
            Life1.GetComponent<Image>().enabled = false;  // 1�Ԗڂ̃��C�t�A�C�R�����\��
            LostLife1.GetComponent<Image>().enabled = true;  // ����ꂽ���C�t�A�C�R����\��
            SceneManager.LoadScene("GameOver_Stage1");  // �Q�[���I�[�o�[��ʂ֑J��
        }
    }
}
