using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// GameScene�ł̃��C�t�Ǘ��N���X
/// </summary>
public class GameOverScript : MonoBehaviour
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

    private bool istLife;  // �v���C���[�����C�t�����������ǂ����̃t���O

    // Start is called before the first frame update
    void Start()
    {
        // �����ݒ�Ƃ��āA�S�Ẵ��C�tUI��\��
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // ����ꂽ���C�tUI�͔�\��
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // �v���C���[�̃��C�t����5�ɐݒ�
        LifeCount = 5;
        Count = 0;  // �J�E���g��������
    }

    // Update is called once per frame
    void Update()
    {
        // �v���C���[�����C�t�������Ă���3�b�ԑҋ@���Ă���ꍇ
        if (Count == 1)
        {
            Timer += Time.deltaTime;  // �o�ߎ��Ԃ����Z
            if (Timer >= 5.0f)  // 5�b�o�߂�����
            {
                Timer = 0;  // �^�C�}�[�����Z�b�g
                Count = 0;  // �J�E���g�����Z�b�g
                istLife = false;  // ���C�t���������t���O�����Z�b�g
            }
        }

        // �v���C���[�����C�t�������Ă��Ȃ���ԂŁA���C�t��1���炷
        if (istLife == true && Count == 0)
        {
            LifeCount--;  // ���C�t��1���炷
            Count = 1;  // �J�E���g��1�ɐݒ肵�āA�^�C�}�[�J�n
        }

        // ���C�t���ɉ�����UI���X�V
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
            SceneManager.LoadScene("GameOver");  // �Q�[���I�[�o�[�V�[���ɑJ��
        }
    }

    // �g���K�[�ɓ������Ƃ��ɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[���uEnemy�v�ƃ^�O�t�����ꂽ�I�u�W�F�N�g�ƏՓ˂����ꍇ
        if (other.gameObject.tag == "Enemy")
        {
            istLife = true;  // �v���C���[�����C�t����������Ԃɐݒ�
        }
    }
}