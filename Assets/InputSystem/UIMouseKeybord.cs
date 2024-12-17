using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class UIMouseKeybord : MonoBehaviour
{
    private UIInputActions _uiInputActions; //Inputsystem�擾

    //�ݒ��ʂ�\���ABGM�ASE�A�}�E�X���x�A�}�C�N�A�R���g���[���[�����A�L�[�{�[�h�����A�^�C�g���ɖ߂��ʂ�\��
    [SerializeField] GameObject menyu, settingPanel1, settingPanel2, settingPanel3;

    //�Q�[�����ݒ��ύX�����ʂ֑J�ڂ���{�^���A���������ʂ֑J�ڂ���{�^���A�^�C�g����ʂ֑J�ڂ���{�^��
    [SerializeField] GameObject settingButton, operationExplanationButton,titleButton;
    Image imageSettingButton, imageOperationExplanationButton, imageTitleButton;

    //�Q�[���V�[���̕����A�R���g���[���[������ʁA�L�[�{�[�h�̕����A�L�[�{�[�h�������
    [SerializeField] GameObject gamePadSettingButton, controllerUI, keyBoardSettingButton, keyboardUI;
    Image imageGamePadSettingButton, imageKeyBoardSettingButton;

    //�}�C�N�X���C�_�[�ABGM�X���C�_�[�ASE�X���C�_�[�A�}�E�X�X���C�_�[
    [SerializeField] Slider micSlider, bgmSlider,seSlider, mouseSlider; �@

    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] GameObject micObject;

    public float volume, level1, volume2, volume3;
    public CinemachineFreeLook VCamera;
    public float ButtonCount;
    public bool ButtonON;

    private Gamepad gamepad;

    float isStartButton;//���j���[��ʂ̕\���@ON���P�AOFF���O�G
    float mainSelectCount;//�R���g���[���[�̃��j���[��ʈړ�

    //UI�I�����̐F
    Color whiteColor = Color.white;
    Color yellowColor = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        menyu.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);

        imageSettingButton = settingButton.GetComponent<Image>();
        imageOperationExplanationButton = operationExplanationButton.GetComponent<Image>();
        imageTitleButton = titleButton.GetComponent<Image>();

        imageGamePadSettingButton = gamePadSettingButton.GetComponent<Image>();
        imageKeyBoardSettingButton = keyBoardSettingButton.GetComponent<Image>();

        //InputSystem�̃C���X�^���X��
        _uiInputActions = new UIInputActions();
        _uiInputActions.Enable();

        // �ŏ��ɐڑ�����Ă���Q�[���p�b�h���擾
        gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("�Q�[���p�b�h���ڑ�����Ă��܂���B");
        }

    }

    // Update is called once per frame
    void Update()
    {
        OnSettingMenu();
    }

    void SetBGM(float volume2)
    {
        _audioMixer.SetFloat("BGM", volume2);
    }

    void SetSE(float volume3)
    {
        _audioMixer.SetFloat("SE", volume3);
    }

    void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = micSlider.value;
    }

    void SetMouse(float level1)
    {
        VCamera.m_YAxis.m_MaxSpeed = mouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = mouseSlider.value * 50;
    }

    //�}�E�X�E�L�[�{�[�h�ݒ�
    #region
    //���ԃ}�[�N����������
    public void OnSettingMenuButton()
    {
        menyu.SetActive(true);
        imageSettingButton.color = yellowColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(true);
        Time.timeScale = 0;
    }
    public void OnSettingButton()
    {
        imageSettingButton.color = yellowColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
    }

    public void OnOperationExplanationButton()
    {
        imageSettingButton.color = whiteColor;
        imageOperationExplanationButton.color = yellowColor;
        imageTitleButton.color = whiteColor;

        settingPanel1.SetActive(false);
        settingPanel2.SetActive(true);
        settingPanel3.SetActive(false);

        imageGamePadSettingButton.color = whiteColor;
        imageKeyBoardSettingButton.color = yellowColor;
        controllerUI.SetActive(false);
        keyboardUI.SetActive(true);
    }
    
    public void OnimageTitleButton()
    {
        imageSettingButton.color = whiteColor;
        imageOperationExplanationButton.color = whiteColor;
        imageTitleButton.color = yellowColor;

        settingPanel1.SetActive(false);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(true);
    }

    public void OnGamePadSettingButton()
    {
        imageGamePadSettingButton.color = yellowColor;
        imageKeyBoardSettingButton.color=whiteColor;

        controllerUI.SetActive(true);
        keyboardUI.SetActive(false);
    }

    public void OnKeyBoardSettingButton()
    {
        imageGamePadSettingButton.color = whiteColor;
        imageKeyBoardSettingButton.color = yellowColor;

        controllerUI.SetActive(false);
        keyboardUI.SetActive(true);

    }

    public void closeKeyBButton()
    {
        menyu.SetActive(false);
        settingPanel1.SetActive(true);
        settingPanel2.SetActive(false);
        settingPanel3.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

    //�R���g���[���[�ݒ�
    #region

    public void OnSettingMenu()
    {
        if (_uiInputActions.ControllerUI.StartButton.triggered) 
        {
            if (isStartButton == 0)
            {
                menyu.SetActive(true);
                imageSettingButton.color = yellowColor;
                imageOperationExplanationButton.color = whiteColor;
                imageTitleButton.color = whiteColor;
                settingPanel1.SetActive(true);
                Time.timeScale = 0;
                isStartButton = 1;
            }
            else if(isStartButton == 1)
            {
                menyu.SetActive(false);
                settingPanel1.SetActive(true);
                settingPanel2.SetActive(false);
                settingPanel3.SetActive(false);
                Time.timeScale = 1;
                isStartButton = 0;
            }
        }
    }

    public void OnMainSelect()
    {
       
    }



    #endregion
}

