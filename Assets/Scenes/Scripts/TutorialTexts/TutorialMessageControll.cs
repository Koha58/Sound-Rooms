using System;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// �`���[�g���A���p��UI���Ǘ�����N���X
/// </summary>
public class TutorialMessageControll : MonoBehaviour
{
    // �`���[�g���A�����b�Z�[�W�̃X���C�hUI���Ǘ�����z��
    [SerializeField]
    private SlideUIControll[] Messages;

    // �L�[�{�[�h��p�̃`���[�g���A�����b�Z�[�W���Ǘ�����z��
    [SerializeField]
    private Image[] KeyboardMove;

    // �R���g���[���[��p�̃`���[�g���A�����b�Z�[�W���Ǘ�����z��
    [SerializeField]
    private Image[] ControllerMove;

    // �`���[�g���A�����b�Z�[�W���Ǘ�����z��i���݂̃f�o�C�X�Ɋ�Â��ă��b�Z�[�W��؂�ւ���j
    [SerializeField]
    private Image[] UIDeviceCheck;

    // ���ݕ\������Ă��郁�b�Z�[�W�̃C���f�b�N�X
    public int MessageIndex;

    // ���̓f�o�C�X���R���g���[���[���ǂ������`�F�b�N����t���O
    private bool isControllerInput;

    // �I�u�W�F�N�g�z�u���Ǘ�����X�N���v�g�ւ̎Q��
    [SerializeField]
    private ObjectPlacer objectPlacer;

    // ���b�Z�[�W�̃C���f�b�N�X���Ǘ�����萔
    private const int FIRST_MESSAGE_INDEX = 0;  // �ŏ��̃��b�Z�[�W�i�C���f�b�N�X0�j
    private const int SECOND_MESSAGE_INDEX = 1; // 2�Ԗڂ̃��b�Z�[�W�i�C���f�b�N�X1�j
    private const int THIRD_MESSAGE_INDEX = 2;  // 3�Ԗڂ̃��b�Z�[�W�i�C���f�b�N�X2�j

    // �`���[�g���A���̍ő僁�b�Z�[�W��
    private const int MAX_MESSAGES = 3;

    // �L�[�{�[�h�ƃR���g���[���[�̔z��̍ő吔
    private const int MAX_DEVICE_MESSAGES = 3;

    // �L�[�{�[�h�A�R���g���[���[�AUIDeviceCheck �z��̃C���f�b�N�X�̍ő�l
    private const int DEVICE_MESSAGE_0_INDEX = 0;  // 0�Ԗڂ̃f�o�C�X���b�Z�[�W
    private const int DEVICE_MESSAGE_1_INDEX = 1;  // 1�Ԗڂ̃f�o�C�X���b�Z�[�W
    private const int DEVICE_MESSAGE_2_INDEX = 2;  // 2�Ԗڂ̃f�o�C�X���b�Z�[�W

    private bool OnPut;

    private bool OnKey;

    // ���b�Z�[�W�̍ő吔�ƃC���f�b�N�X�͈͂̒萔
    private const int MAX_INDEX = 2; // ���b�Z�[�W�̃C���f�b�N�X�� 0, 1, 2 �Ȃ̂ōő�l�� 2

    void Start()
    {
        // �e���b�Z�[�W�̏�Ԃ�������
        // Messages �z��̊e�v�f�i�e���b�Z�[�W�j�� state �� 0�i��\���j�ɐݒ�
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[i].state = SlideUIControll.State.Initial; // �e���b�Z�[�W���\���ɐݒ�
        }

        // �ŏ��̃��b�Z�[�W�̃C���f�b�N�X��ݒ�
        MessageIndex = FIRST_MESSAGE_INDEX; // �ŏ��̃��b�Z�[�W��ݒ�i0�Ԗڂ̃��b�Z�[�W�j

        // �ŏ��̃��b�Z�[�W��\����ԂɕύX
        Messages[MessageIndex].state = SlideUIControll.State.SlideIn; // �ŏ��̃��b�Z�[�W��\����Ԃ�

        // ���̓f�o�C�X�i�L�[�{�[�h�A�R���g���[���[�j�̃��b�Z�[�W��������
        InitializeDeviceMessages(KeyboardMove); // �L�[�{�[�h�p�̃��b�Z�[�W���\��
        InitializeDeviceMessages(ControllerMove); // �R���g���[���[�p�̃��b�Z�[�W���\��

        // UIDeviceCheck �z��ɃL�[�{�[�h���b�Z�[�W��ݒ�
        for (int i = 0; i < MAX_DEVICE_MESSAGES; i++)
        {
            UIDeviceCheck[i] = KeyboardMove[i]; // UIDeviceCheck �z����L�[�{�[�h�̃��b�Z�[�W�ŏ�����
        }

        // �I�u�W�F�N�g�z�u�̃R���|�[�l���g��������
        objectPlacer.GetComponent<ObjectPlacer>(); // ObjectPlacer �R���|�[�l���g���擾�i��Ŏg���j
    }

    void Update()
    {
        // ���݂̓��̓f�o�C�X�^�C�v���m�F���āA�R���g���[���[���L�[�{�[�h���𔻒f
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            isControllerInput = true; // �R���g���[���[���g�p����Ă���ꍇ�̓t���O�� true ��
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            isControllerInput = false; // �L�[�{�[�h���g�p����Ă���ꍇ�̓t���O�� false ��
        }

        // ���݂̃��b�Z�[�W��Ԃ��X�V
        StayMessage();

        // ���̓f�o�C�X�Ɋ�Â��ĕ\�����郁�b�Z�[�W��؂�ւ�
        ControllerCheck();

        // ���݂̃��b�Z�[�W��\����Ԃɐݒ�
        Messages[MessageIndex].state = SlideUIControll.State.SlideIn; // ���ݕ\������Ă��郁�b�Z�[�W��\����Ԃ�
    }

    // ���b�Z�[�W�̐؂�ւ��������m�F���郁�\�b�h
    void StayMessage()
    {
        // �ŏ��̃��b�Z�[�W�i�C���f�b�N�X 0�j�̏ꍇ
        if (MessageIndex == FIRST_MESSAGE_INDEX)
        {
            // objectPlacer ���ݒ�n�_�ɂ���ꍇ�A���̃��b�Z�[�W�ɐ؂�ւ�
            if (objectPlacer.isOnSettingPoint) // �I�u�W�F�N�g���ݒ�n�_�ɂ���ꍇ
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // ���݂̃��b�Z�[�W���\��
                MessageIndex++; // ���̃��b�Z�[�W�փC���f�b�N�X��ύX
            }
        }
        // 2�Ԗڂ̃��b�Z�[�W�i�C���f�b�N�X 1�j�̏ꍇ
        else if (MessageIndex == SECOND_MESSAGE_INDEX)
        {
            // "EnemyAttackArea" ��T���āA���̏�ԂɊ�Â��ă��b�Z�[�W��؂�ւ�
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea"); // "EnemyAttackArea" �I�u�W�F�N�g��T��
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects �X�N���v�g���擾
            if (impactObjects.count == 1) // impactObjects �̃J�E���g�� 1 �̏ꍇ
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // ���݂̃��b�Z�[�W���\��
                MessageIndex++; // ���̃��b�Z�[�W�Ɉړ�
            }
        }
        // 3�Ԗڂ̃��b�Z�[�W�i�C���f�b�N�X 2�j�̏ꍇ
        else if (MessageIndex == THIRD_MESSAGE_INDEX)
        {
            // objectPlacer ���ݒ�n�_�ɂȂ��ꍇ�A���̃��b�Z�[�W�ɐ؂�ւ�
            if (!objectPlacer.isOnSettingPoint) // �I�u�W�F�N�g���ݒ�n�_�ɂȂ��ꍇ
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // ���݂̃��b�Z�[�W���\��
                MessageIndex++; // ���̃��b�Z�[�W�Ɉړ�
            }
        }
    }

    // ���̓f�o�C�X�ɉ��������b�Z�[�W�������s�����\�b�h
    void ControllerCheck()
    {

        // "EnemyAttackArea" ��T���āA���̏�ԂɊ�Â��ă��b�Z�[�W��؂�ւ�
        GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea"); // "EnemyAttackArea" �I�u�W�F�N�g��T��
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects �X�N���v�g���擾

        if (isControllerInput) // �R���g���[���[���g�p����Ă���ꍇ
        {
            // �L�[�{�[�h���b�Z�[�W���\���ɂ��A�R���g���[���[�̃��b�Z�[�W��\��
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX] = KeyboardMove[DEVICE_MESSAGE_0_INDEX]; // �L�[�{�[�h���b�Z�[�W���\��
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX].enabled = false; // 0�Ԗڂ̃f�o�C�X���b�Z�[�W���\��

            // ���b�Z�[�W�C���f�b�N�X���ŏ��̏ꍇ�A�R���g���[���[�p�̃��b�Z�[�W��\��
            if (impactObjects.count != 1 && OnPut)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX] = ControllerMove[DEVICE_MESSAGE_1_INDEX]; // �R���g���[���[��1�Ԗڂ̃��b�Z�[�W��\��
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = true; // �R���g���[���[��1�Ԗڂ̃��b�Z�[�W��\��
            }
            // ���b�Z�[�W�C���f�b�N�X��2�Ԗڂ̏ꍇ�A�R���g���[���[��1�Ԗڂ̃��b�Z�[�W���\��
            else if (MessageIndex == SECOND_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1�Ԗڂ̃��b�Z�[�W���\��
            }
            // ���b�Z�[�W�C���f�b�N�X��3�Ԗڂ̏ꍇ�A�R���g���[���[��2�Ԗڂ̃��b�Z�[�W��\��
            else if (MessageIndex == THIRD_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1�Ԗڂ̃��b�Z�[�W���\��
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX] = ControllerMove[DEVICE_MESSAGE_2_INDEX]; // �R���g���[���[��2�Ԗڂ̃��b�Z�[�W��\��
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = true; // �R���g���[���[��2�Ԗڂ̃��b�Z�[�W��\��
            }
            else
            {
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = false; // ����ȊO�̓��b�Z�[�W���\��
            }
        }
        else // �L�[�{�[�h���g�p����Ă���ꍇ
        {
            // �L�[�{�[�h���b�Z�[�W��\��
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX] = KeyboardMove[DEVICE_MESSAGE_0_INDEX]; // �L�[�{�[�h��0�Ԗڂ̃��b�Z�[�W��\��
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX].enabled = true; // 0�Ԗڂ̃��b�Z�[�W��\��

            // ���b�Z�[�W�C���f�b�N�X���ŏ��̏ꍇ�A�L�[�{�[�h��1�Ԗڂ̃��b�Z�[�W��\��
            if (impactObjects.count != 1 && OnPut)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX] = KeyboardMove[DEVICE_MESSAGE_1_INDEX]; // �L�[�{�[�h��1�Ԗڂ̃��b�Z�[�W��\��
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = true; // �L�[�{�[�h��1�Ԗڂ̃��b�Z�[�W��\��
            }
            // ���b�Z�[�W�C���f�b�N�X��2�Ԗڂ̏ꍇ�A�L�[�{�[�h��1�Ԗڂ̃��b�Z�[�W���\��
            else if (MessageIndex == SECOND_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1�Ԗڂ̃��b�Z�[�W���\��
            }
            // ���b�Z�[�W�C���f�b�N�X��3�Ԗڂ̏ꍇ�A�L�[�{�[�h��2�Ԗڂ̃��b�Z�[�W��\��
            else if (MessageIndex == THIRD_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1�Ԗڂ̃��b�Z�[�W���\��
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX] = KeyboardMove[DEVICE_MESSAGE_2_INDEX]; // �L�[�{�[�h��2�Ԗڂ̃��b�Z�[�W��\��
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = true; // �L�[�{�[�h��2�Ԗڂ̃��b�Z�[�W��\��
            }
            else
            {
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = false; // ����ȊO�̓��b�Z�[�W���\��
            }
        }
    }

    // �f�o�C�X���b�Z�[�W������������w���p�[���\�b�h�i�e���b�Z�[�W���\���ɂ���j
    private void InitializeDeviceMessages(Image[] deviceMessages)
    {
        // �z��̊e���b�Z�[�W���\���ɂ���
        for (int i = 0; i < deviceMessages.Length; i++)
        {
            deviceMessages[i].enabled = false; // ���ׂẴ��b�Z�[�W���\��
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SettingPoint"))
        {
            OnPut = true;
        }
        else
        {
            OnPut = false;
        }

        if (other.CompareTag("KeyCheck"))
        {
            OnKey = true;
        }
        else
        {
            OnKey = false;
        }
    }

}