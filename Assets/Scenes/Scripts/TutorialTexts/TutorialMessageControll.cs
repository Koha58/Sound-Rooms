using System;
using System.Collections;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

/// <summary>
/// TutorialScene�̐���UI��\��������N���X
/// </summary>
public class TutorialMessageControll : MonoBehaviour
{
    // �`���[�g���A�����b�Z�[�W�̃X���C�hUI���Ǘ�����z��
    [SerializeField]
    private SlideUIControll[] Messages;

    // �R���g���[���[��p�̃`���[�g���A�����b�Z�[�W���Ǘ�����z��
    [SerializeField]
    private SlideUIControll[] ControllerMessages;

    public int Message; // ���݂̃��b�Z�[�W�̃C���f�b�N�X

    bool deviceCheck; // ���̓f�o�C�X���R���g���[���[�����`�F�b�N����t���O

    // �I�u�W�F�N�g�z�u���Ǘ�����X�N���v�g�ւ̎Q��
    [SerializeField]
    private ObjectPlacer OP;

    // --- �L�[�{�[�h�pUI ---

    [SerializeField]
    private Image TutorialMoveUI;

    [SerializeField]
    private Image TutorialSpaceUI;

    [SerializeField]
    private Image TutorialEUI;

    // --- �R���g���[���[�pUI ---

    [SerializeField]
    private Image xButtonUI;

    [SerializeField]
    private Image yButtonUI;


    // Start�͍ŏ��̃t���[�����X�V�����O�Ɉ�x�����Ăяo�����
    void Start()
    {
        // �S�Ẵ��b�Z�[�W�̏�Ԃ�������
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[i].state = 0;
        }
        Message = 1; // �ŏ��̃��b�Z�[�W��ݒ�
        Messages[Message - 1].state = 1;

        // �e�R���|�[�l���g�̏�����
        OP.GetComponent<ObjectPlacer>();

        TutorialMoveUI.GetComponent<Image>().enabled = false;
        TutorialSpaceUI.GetComponent<Image>().enabled = false;
        TutorialEUI.GetComponent<Image>().enabled = false;
        xButtonUI.GetComponent<Image>().enabled = false;
        yButtonUI.GetComponent<Image>().enabled = false;
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

        StayMessage();
    }

    // ���b�Z�[�W�̐؂�ւ��������m�F
    void StayMessage()
    {
        if(Message == 1)
        {
            if (OP.isOnSettingPoint)
            {
                Message++;
            }
        }
        else if(Message == 2)
        {
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // �X�N���v�g���擾
            if (impactObjects.count == 1)
            {
                Message++;
            }
        }
        else if (Message == 3)
        {
            if (!OP.isOnSettingPoint)
            {
                Message++;
            }
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