using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// �Q�[�����ɐݒ�iBGM��SE�A��������Ȃǁj�𐧌䂷��X�N���v�g
/// </summary>
public class UIController : MonoBehaviour
{
    //���ԃ{�^���i�ݒ��ʂɍs���{�^���j,���j���[���,�ݒ���,����������,�^�C�g���ɖ߂���
    [SerializeField] GameObject originSettingButton, menu, settingPanel1,settingPanel2,settingPanel3;

    //�Q�[�����ݒ��ύX�����ʂ֑J�ڂ���{�^��,���������ʂ֑J�ڂ���{�^��,�^�C�g����ʂ֑J�ڂ���{�^��
    [SerializeField] GameObject settingButton,qperationExplanationButton,backTitleButton;

    //
    [SerializeField] GameObject keyBoardMoveSettingSelect, gamePadMoveSettingSelect, keyBoardButton, gamePadButton, keyBoard, gamePad;

    //
    [SerializeField] GameObject MicSliderGameObject, BGMSliderGameObject, SESliderGameObject, MouseSliderGameObject, closeKey;

    [SerializeField] GameObject[] Cursor;

    /// <summary>
    /// �e��X���C�_�[���Ǘ����邽�߂�Slider�^�ϐ�
    /// </summary>
    //�}�C�N�X���C�_�[,BGM�X���C�_�[,SE�X���C�_�[,�}�E�X�X���C�_�[
    [SerializeField] Slider MicSlider, BGMSlider, SESlider, MouseSlider;

    [SerializeField] AudioMixer audioMixer; // �I�[�f�B�I�~�L�T�[
    [SerializeField] GameObject micObject;�@// �}�C�N�I�u�W�F�N�g
    public CinemachineFreeLook VCamera;     // Cinemachine�J����

    //InputSystem
    private GameInputSystem inputActions;  // ���͊Ǘ��V�X�e��
    private Vector2 navigateInput;         // �ړ��̓��́i2D�x�N�g���j

    private bool isMenuButton; // ���j���[�{�^����������Ă��邩
    private bool isBButton;    // B�{�^����������Ă��邩
    private bool isAButton;    // A�{�^����������Ă��邩


    // �p�l���Ǘ��p���X�g�i�p�l���̕\����\�����Ǘ�����j
    private List<GameObject> panels;

    private void Awake()
    {
        // Input System�̃C���X�^���X���쐬
        inputActions = new GameInputSystem();

        //���j����ʂ̃X�e�B�b�N����
        inputActions.UI.Navigate.performed += ctx => navigateInput = ctx.ReadValue<Vector2>();
        inputActions.UI.Navigate.canceled += ctx => navigateInput = Vector2.zero;

        //���j���{�^��
        inputActions.UI.MenuButton.performed += ctx => isMenuButton = true;
        inputActions.UI.MenuButton.canceled += ctx => isMenuButton = false;

        //B�{�^��
        inputActions.UI.BButton.performed += ctx => isBButton = true;
        inputActions.UI.BButton.canceled += ctx => isBButton = false;

        //A�{�^��
        inputActions.UI.AButton.performed += ctx => isAButton = true;
        inputActions.UI.AButton.canceled += ctx => isAButton = false;
    }

    // ���̓A�N�V������L���ɂ���i�Q�[���J�n���j
    private void OnEnable()
    {
        inputActions.Enable();
    }

    // ���̓A�N�V�����𖳌��ɂ���i�Q�[���I�����j
    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �p�l�����X�g���쐬
        panels = new List<GameObject> { menu, settingPanel1, settingPanel2, settingPanel3 };

        // �S�Ẵp�l�����\���ɂ���
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        // �J�[�\�����\���ɂ���
        for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Controller();// �R���g���[���[�̓��͏������Ǘ�
    }

    // ���j���[��\�����鏈��
    public void Menu(GameObject menuPanel)
    {
        menuPanel.SetActive(true); // ���j���[�p�l����\��
        closeKey.SetActive(true);  // ����L�[��\��
        settingPanel1.SetActive(true); // �ݒ�p�l��1��\��
        settingPanel2.SetActive(false); // �ݒ�p�l��2���\��
        settingPanel3.SetActive(false); // �ݒ�p�l��3���\��

        Time.timeScale = 0; // �Q�[���̐i�s���ꎞ��~
    }

    // ���j���[����鏈��
    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false); // ���j���[���\��
        closeKey.SetActive(false);  // ����L�[���\��

        Time.timeScale = 1; // �Q�[���̐i�s���ĊJ
    }
    // �ݒ�p�l��1��\��
    public void SettingPanel()
    {
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
    }

    // �ݒ�p�l��2��\��
    public void SettingPanel1()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(true);
        settingPanel3.SetActive(false);
    }

    // �ݒ�p�l��3��\�����A�V�[�������[�h
    public void SettingPanel2()
    {
        settingPanel1.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(true);
        SceneManager.LoadScene("StartScene"); // �V�����V�[����ǂݍ���
    }

    // �L�[�{�[�h�ݒ�p�l����\��
    public void KeyBoardPanel()
    {
        keyBoard.SetActive(true);
        gamePad.SetActive(false); // �Q�[���p�b�h�ݒ���\��
    }

    // �Q�[���p�b�h�ݒ�p�l����\��
    public void GamePadPanel()
    {
        keyBoard.SetActive(false); // �L�[�{�[�h�ݒ���\��
        gamePad.SetActive(true); // �Q�[���p�b�h�ݒ��\��
    }

    // �R���g���[���[�̓��͂Ɋ�Â���UI�𑀍삷��
    public void Controller()
    {
        // ���ݑI������Ă���UI�I�u�W�F�N�g���擾
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        // ���j���[�{�^���������ꂽ�ꍇ
        if (isMenuButton == true)
        {
            menu.SetActive(true);
            settingPanel1.SetActive(true);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(false);
            Time.timeScale = 0; // �Q�[�����ꎞ��~
        }
        // B�{�^���������ꂽ�ꍇ�i���j���[�����j
        else if (isBButton == true)
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false); // ���ׂẴp�l�����\��
            }

            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false); // �J�[�\�����\��

            Time.timeScale = 1; // �Q�[�����ĊJ
        }

        // �eUI�I�u�W�F�N�g�ɉ����Đݒ�p�l����؂�ւ��鏈��
        if (selectedGameObject == settingButton)
        {
            settingPanel1.SetActive(true);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(false);
            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
        }
        else if (selectedGameObject == qperationExplanationButton)
        {
            settingPanel1.SetActive(false);
            settingPanel2.SetActive(true);
            settingPanel3.SetActive(false);

            keyBoard.SetActive(true); // �L�[�{�[�h�ݒ��\��
            keyBoardMoveSettingSelect.SetActive(false); // �L�[�{�[�h�ړ��ݒ���\��
            gamePadMoveSettingSelect.SetActive(false); // �Q�[���p�b�h�ړ��ݒ���\��
            gamePad.SetActive(false); // �Q�[���p�b�h�ݒ���\��
        }
        else if (selectedGameObject == backTitleButton)
        {
            settingPanel1.SetActive(false);
            settingPanel2.SetActive(false);
            settingPanel3.SetActive(true);
        }
        // ���̑���UI�I�u�W�F�N�g�ɉ����Đݒ��؂�ւ��鏈���i�X���C�_�[��{�^���̑I���j
        else if (selectedGameObject == keyBoardButton)
        {
            keyBoardMoveSettingSelect.SetActive(true);
            gamePadMoveSettingSelect.SetActive(false);
            keyBoard.SetActive(true);
            gamePad.SetActive(false);
        }
        else if (selectedGameObject == gamePadButton)
        {
            keyBoardMoveSettingSelect.SetActive(false);
            gamePadMoveSettingSelect.SetActive(true);
            keyBoard.SetActive(false);
            gamePad.SetActive(true);
        }
        else if (selectedGameObject == BGMSliderGameObject)
        {
            Cursor[0].SetActive(true);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == SESliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(true);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == MicSliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(true);
            Cursor[3].SetActive(false);
        }
        else if (selectedGameObject == MouseSliderGameObject)
        {
            Cursor[0].SetActive(false);
            Cursor[1].SetActive(false);
            Cursor[2].SetActive(false);
            Cursor[3].SetActive(true);
        }
        // �����I������Ă��Ȃ��ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
        else if (selectedGameObject == null)
        {
            // selectedGameObject��null�̏ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
            EventSystem.current.SetSelectedGameObject(settingButton);
        }
    }

    public void SetBGM(float volume2)
    {
        audioMixer.SetFloat("BGM", volume2);
    }

    public void SetSE(float volume3)
    {
        audioMixer.SetFloat("SE", volume3);
    }

    public void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = MicSlider.value;
    }

    public void SetMouse(float level1)
    {
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value * 50;
    }
}