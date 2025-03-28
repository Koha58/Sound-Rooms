using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static InputDeviceManager;

/// <summary>
/// �V�[���J�ڂ��Ǘ�����X�L�b�v�{�^���̃X�N���v�g
/// </summary>
public class SkipButton : MonoBehaviour
{
    // �X�L�b�v�{�^����UI�I�u�W�F�N�g�i�{�^���̕\��/��\���𐧌�j
    public GameObject Sikp;

    // �V����Input System�ł̓��͊Ǘ��p�̃C���X�^���X
    private GameInputSystem inputActions;

    // �f�o�C�X�iXbox/Keyboard�j�̃`�F�b�N�t���O
    bool deviceCheck;

    // X�{�^���������ꂽ���ǂ������Ǘ�����t���O
    private bool isXButton;

    /// <summary>
    /// ����������
    /// </summary>
    private void Awake()
    {
        // Input System�̃C���X�^���X�𐶐�
        inputActions = new GameInputSystem();

        // X�{�^���������ꂽ�Ƃ��Ƀt���O�𗧂Ă�
        inputActions.UI.XButton.performed += ctx => isXButton = true;
        inputActions.UI.XButton.canceled += ctx => isXButton = false;
    }

    /// <summary>
    /// Start���\�b�h
    /// </summary>
    // Start�̓Q�[���J�n����1�x�����Ă΂��
    void Start()
    {
        // ���ɏ����͏�����Ă��Ȃ����A�K�v�ł���΂����ɏ������������������Ƃ��ł���
    }

    /// <summary>
    /// ���͂�L��������
    /// </summary>
    private void OnEnable()
    {
        // �V�������̓V�X�e����L���ɂ���
        inputActions.Enable();
    }

    /// <summary>
    /// ���͂𖳌�������
    /// </summary>
    private void OnDisable()
    {
        // �V�������̓V�X�e���𖳌��ɂ���
        inputActions.Disable();
    }

    /// <summary>
    /// �t���[�����̏���
    /// </summary>
    void Update()
    {
        // ���ݎg�p���Ă�����̓f�o�C�X���`�F�b�N
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            // Xbox�R���g���[���[���g�p����Ă���ꍇ
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            // �L�[�{�[�h���g�p����Ă���ꍇ
            deviceCheck = false;
        }

        // ���̓f�o�C�X���L�[�{�[�h�̏ꍇ�̓X�L�b�v�{�^�����\���ɂ��AXbox�̏ꍇ�͕\������
        if (!deviceCheck)
        {
            // �L�[�{�[�h�̏ꍇ�A�X�L�b�v�{�^�����\��
            Sikp.SetActive(false);
        }
        else
        {
            // Xbox�̏ꍇ�A�X�L�b�v�{�^����\��
            Sikp.SetActive(true);
        }

        // X�{�^���������ꂽ�ꍇ�A�V�[���J�ڂ��s��
        if (isXButton == true)
        {
            // �V�[�����uTutorialScene�v�ɑJ�ڂ���
            SceneManager.LoadScene("TutorialScene");
        }
    }

    /// <summary>
    /// �{�^�����N���b�N���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    /// </summary>
    public void OnClick()
    {
        // �uTutorialScene�v�V�[���ɑJ��
        SceneManager.LoadScene("TutorialScene");
    }

}