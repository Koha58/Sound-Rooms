using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �}�C�N�̐ڑ����Ǘ�����N���X
/// </summary>
public class GetDeviceCheck : MonoBehaviour
{
    // �}�C�N�ڑ��`�F�b�N�p�t���O
    bool micCheck = false;

    // �}�C�N�ڑ��G���[���b�Z�[�W��\������UI�I�u�W�F�N�g
    [SerializeField] private GameObject MicConnectionBadUI;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ԃł̓}�C�N���ڑ�����Ă��Ȃ��Ɖ���
        micCheck = false;

        // ������Ԃł̓G���[���b�Z�[�WUI���\��
        MicConnectionBadUI.GetComponent<Image>().enabled = false;

        // �}�C�N�f�o�C�X�����o���āA�f�o�C�X�������O�ɏo��
        foreach (string device in Microphone.devices)
        {
            // �e�}�C�N�̃f�o�C�X�������O�ɕ\��
            Debug.Log("Name: " + device);

            // �}�C�N�f�o�C�X������������A�ڑ��t���O��true�ɐݒ�
            micCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���t���[���A�}�C�N���ڑ�����Ă��邩�ă`�F�b�N
        micCheck = false;

        // �}�C�N�f�o�C�X�̃��X�g���ēx�m�F
        foreach (string device in Microphone.devices)
        {
            //// �e�}�C�N�̃f�o�C�X�������O�ɕ\��
            //Debug.Log("Name: " + device);

            // �}�C�N���ڑ�����Ă���ꍇ�A�ڑ��t���O��true�ɐݒ�
            micCheck = true;

            // �}�C�N���ڑ�����Ă���΃G���[���b�Z�[�WUI���\����
            MicConnectionBadUI.GetComponent<Image>().enabled = false;
        }

        // �}�C�N���ڑ�����Ă��Ȃ��ꍇ�A�G���[���b�Z�[�WUI��\��
        if (!micCheck)
        {
            //// �}�C�N���ڑ�����Ă��Ȃ����Ƃ����O�ɕ\��
            //Debug.Log("�}�C�N���ڑ�����Ă��܂���");

            // �G���[���b�Z�[�WUI��\��
            MicConnectionBadUI.GetComponent<Image>().enabled = true;
        }
    }
}
