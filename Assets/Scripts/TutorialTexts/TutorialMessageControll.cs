using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialMessageControll : MonoBehaviour
{
    // �`���[�g���A�����b�Z�[�W�̃X���C�hUI���Ǘ�����z��
    [SerializeField]
    private SlideUIControll[] Messages;

    // �R���g���[���[��p�̃`���[�g���A�����b�Z�[�W���Ǘ�����z��
    [SerializeField]
    private SlideUIControll[] ControllerMessages;

    // ���b�Z�[�W���؂�ւ��ۂ̃T�E���h
    [SerializeField] AudioSource MessageSound;

    float timeCnt; // �^�C�}�[�Ƃ��Ďg�p�����J�E���^
    public int Message; // ���݂̃��b�Z�[�W�̃C���f�b�N�X
    PlayerSeen PS; // �v���C���[�������Ă����Ԃ��Ǘ�����X�N���v�g

    bool deviceCheck; // ���̓f�o�C�X���R���g���[���[�����`�F�b�N����t���O

    // �I�u�W�F�N�g�z�u���Ǘ�����X�N���v�g�ւ̎Q��
    public ObjectPlacer OP;

    // �{�^���̃C���[�W�R���|�[�l���g
    public Image LeftButton;

    // Start�͍ŏ��̃t���[�����X�V�����O�Ɉ�x�����Ăяo�����
    void Start()
    {
        // �S�Ẵ��b�Z�[�W�̏�Ԃ�������
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[Message].state = 0;
        }
        Message = 1; // �ŏ��̃��b�Z�[�W��ݒ�
        Messages[Message - 1].state = 1;

        // �e�R���|�[�l���g�̏�����
        OP.GetComponent<ObjectPlacer>();
        LeftButton.GetComponent<Image>().enabled = false;
    }

    // Update�͖��t���[���Ăяo�����
    void Update()
    {
        // ���݂̓��̓f�o�C�X���m�F
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        // �^�C�}�[��7�b�𒴂��A�����b�Z�[�W��22�����Ȃ玟�̃��b�Z�[�W�֐i��
        if (timeCnt >= 7.0f && Message < 22)
        {
            Messages[Message - 1].state = 0; // ���݂̃��b�Z�[�W���\����
            Controller(); // ���̓f�o�C�X�ɉ���������
            Messages[Message].state = 1; // ���̃��b�Z�[�W��\��
            Message++;
            timeCnt = 0f; // �^�C�}�[�����Z�b�g
            MessageSound.PlayOneShot(MessageSound.clip); // �T�E���h���Đ�
        }

        MoveWait(); // ���b�Z�[�W�؂�ւ������̊m�F

        Messages[Message - 1].state = 1; // ���݂̃��b�Z�[�W��\��
    }

    // ���b�Z�[�W�̐؂�ւ��������m�F
    void MoveWait()
    {
        if (Message == 1)
        {
            Messages[0].state = 0;
            Message++;
        }

        if (Message == 2)
        {
            StartCoroutine(FlashButton()); // �{�^���_�ł��J�n
            if (OP.isOnSettingPoint)
            {
                Messages[1].state = 0;
                Message++;
                StopCoroutine(FlashButton()); // �_�ł��~
                SetButtonColor(Color.white); // �{�^���̐F�𔒂ɖ߂�
                LeftButton.GetComponent<Image>().enabled = false; // �{�^�����\����
            }
        }

        if (Message == 3)
        {
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // �X�N���v�g���擾
            if (impactObjects.count == 1)
            {
                Messages[2].state = 0;
                Message++;
            }
        }

        if (Message == 4)
        {
            //if (OP.Recorder.activeSelf == true)
            //{
            //    Messages[3].state = 0;
            //    Message++;
            //}
        }
    }

    // �{�^����ԂƔ��ɓ_�ł�����R���[�`��
    private IEnumerator FlashButton()
    {
        while (Message == 2)
        {
            LeftButton.GetComponent<Image>().enabled = true;
            SetButtonColor(Color.red); // �{�^����Ԃ�
            yield return new WaitForSeconds(0.5f); // 0.5�b�ҋ@
            SetButtonColor(Color.white); // �{�^���𔒂�
            yield return new WaitForSeconds(0.5f); // 0.5�b�ҋ@
        }
    }

    // �{�^���̐F��ݒ肷�郁�\�b�h
    private void SetButtonColor(Color color)
    {
        if (LeftButton != null)
        {
            LeftButton.color = color;
        }
    }

    // ���̓f�o�C�X�ɉ��������b�Z�[�W����
    void Controller()
    {
        if (deviceCheck) // �R���g���[���[�̏ꍇ
        {
            if (Message == 1) Messages[Message] = ControllerMessages[1];
            else if (Message == 2) Messages[Message] = ControllerMessages[2];
            else if (Message == 3) Messages[Message] = ControllerMessages[3];
            else if (Message == 4) timeCnt += 5.0f; // �ǉ����Ԃ����Z
        }
        else // �L�[�{�[�h�̏ꍇ
        {
            if (Message == 1) Messages[Message] = Messages[0];
            else if (Message == 2) Messages[Message] = Messages[1];
            else if (Message == 3) Messages[Message] = Messages[2];
            else if (Message == 4) Messages[Message] = Messages[3];
        }
    }
}