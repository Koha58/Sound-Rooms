using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// �Q�[�����ɐݒ�iBGM��SE�A��������Ȃǁj�𐧌䂷��X�N���v�g
/// </summary>
public class UIController : MonoBehaviour
{
    // �萔�̒�`
    private const int CursorBGMIndex = 0; // BGM�̃J�[�\���C���f�b�N�X
    private const int CursorSEIndex = 1; // SE�̃J�[�\���C���f�b�N�X
    private const int CursorMicIndex = 2; // �}�C�N�̃J�[�\���C���f�b�N�X
    private const int CursorMouseIndex = 3; // �}�E�X�̃J�[�\���C���f�b�N�X

    private const float MouseMaxSpeedDivisor = 50f; // Y���̃X�s�[�h�ݒ�̊���Z�Ɏg���萔
    private const float MouseMaxSpeedMultiplier = 50f; // X���̃X�s�[�h�ݒ�̊|���Z�Ɏg���萔

    private const float TimeScalePaused = 0f;  // �Q�[���i�s���ꎞ��~���邽�߂̒萔
    private const float TimeScaleRunning = 1f; // �Q�[���i�s���ĊJ���邽�߂̒萔

    // �t�B�[���h�̒�`�iUI�v�f�j
    [SerializeField] GameObject originSettingButton, menu, settingPanel, explanationPanel, goTitlePanel;
    // originSettingButton: �ݒ��ʂɈړ�����{�^��
    // menu: ���j���[�p�l���i�ݒ�⑀������Ȃǂ̉�ʂ��܂ށj
    // settingPanel: �ݒ��ʁi���ʒ����Ȃǂ��s����p�l���j
    // explanationPanel: ���������ʁi�Q�[���̑�����@��q���g��\���j
    // goTitlePanel: �^�C�g���ɖ߂�p�l���i�^�C�g����ʂɖ߂邽�߂̊m�F�p�l���j

    [SerializeField] GameObject settingButton, qperationExplanationButton, backTitleButton;
    // settingButton: �ݒ��ʂɈړ����邽�߂̃{�^��
    // qperationExplanationButton: ���������ʂɈړ����邽�߂̃{�^��
    // backTitleButton: �^�C�g����ʂɖ߂邽�߂̃{�^��

    [SerializeField] GameObject keyBoardMoveSettingSelect, gamePadMoveSettingSelect, keyBoardButton, gamePadButton, keyBoard, gamePad;
    // keyBoardMoveSettingSelect: �L�[�{�[�h�ړ��ݒ��I�����邽�߂�UI�v�f
    // gamePadMoveSettingSelect: �Q�[���p�b�h�ړ��ݒ��I�����邽�߂�UI�v�f
    // keyBoardButton: �L�[�{�[�h�ݒ��I�����邽�߂̃{�^��
    // gamePadButton: �Q�[���p�b�h�ݒ��I�����邽�߂̃{�^��
    // keyBoard: �L�[�{�[�h�ݒ�p�̃p�l��
    // gamePad: �Q�[���p�b�h�ݒ�p�̃p�l��

    [SerializeField] GameObject MicSliderGameObject, BGMSliderGameObject, SESliderGameObject, MouseSliderGameObject, closeKey, decisionA;
    // MicSliderGameObject: �}�C�N���ʂ𒲐����邽�߂̃X���C�_�[
    // BGMSliderGameObject: BGM���ʂ𒲐����邽�߂̃X���C�_�[
    // SESliderGameObject: SE���ʂ𒲐����邽�߂̃X���C�_�[
    // MouseSliderGameObject: �}�E�X���x�𒲐����邽�߂̃X���C�_�[
    // closeKey: �ݒ����邽�߂́u����v�{�^��
    // decisionA;�R���g���[���[�̎��ɕ\��������{�^��

    [SerializeField] GameObject[] Cursor;
    // Cursor: �����̃J�[�\���i�I�𒆂̍��ڂɕ\�������j��ێ�����z��

    // �e��X���C�_�[
    [SerializeField] Slider MicSlider, BGMSlider, SESlider, MouseSlider;

    // ���̑��̕K�v�ȃI�u�W�F�N�g
    [SerializeField] AudioMixer audioMixer;  // �I�[�f�B�I�~�L�T�[
    [SerializeField] GameObject micObject;    // �}�C�N�I�u�W�F�N�g
    public CinemachineFreeLook VCamera;       // Cinemachine�J����

    // ���͊Ǘ�
    private GameInputSystem inputActions;     // ���͊Ǘ��V�X�e��
    private Vector2 navigateInput;            // �ړ��̓��́i2D�x�N�g���j
    private bool isMenuButton;                // ���j���[�{�^����������Ă��邩
    private bool isBButton;                   // B�{�^����������Ă��邩

    // �p�l���Ǘ��p���X�g�i�p�l���̕\����\�����Ǘ�����j
    private List<GameObject> panels;

    // ���̓f�o�C�X�̎�ނ𔻒肷��t���O
    bool deviceCheck;


    private void Awake()
    {
        // Input System�̃C���X�^���X���쐬
        // GameInputSystem�́A�Q�[�����ł̓��͊Ǘ����s���N���X
        inputActions = new GameInputSystem();

        // ���j���[��ʂł̃X�e�B�b�N������Ď�
        // �X�e�B�b�N���삪�s����ƁAnavigateInput��Vector2�̒l���ݒ肳���
        inputActions.UI.Navigate.performed += ctx => navigateInput = ctx.ReadValue<Vector2>();
        inputActions.UI.Navigate.canceled += ctx => navigateInput = Vector2.zero; // �X�e�B�b�N���삪�L�����Z�����ꂽ�Ƃ��́AnavigateInput���[���Ƀ��Z�b�g

        // ���j���[�{�^���̓��͂��Ď�
        // ���j���[�{�^���������ꂽ�ꍇ�AisMenuButton��true�ɐݒ肳���
        inputActions.UI.MenuButton.performed += ctx => isMenuButton = true;
        inputActions.UI.MenuButton.canceled += ctx => isMenuButton = false; // ���j���[�{�^���������ꂽ�Ƃ��AisMenuButton��false�Ƀ��Z�b�g

        // B�{�^���̓��͂��Ď�
        // B�{�^���������ꂽ�ꍇ�AisBButton��true�ɐݒ肳���
        inputActions.UI.BButton.performed += ctx => isBButton = true;
        inputActions.UI.BButton.canceled += ctx => isBButton = false; // B�{�^���������ꂽ�Ƃ��AisBButton��false�Ƀ��Z�b�g
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
        panels = new List<GameObject> { menu, settingPanel, explanationPanel, goTitlePanel };

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
        settingPanel.SetActive(true); // �ݒ�p�l��1��\��
        explanationPanel.SetActive(false); // �ݒ�p�l��2���\��
        goTitlePanel.SetActive(false); // �ݒ�p�l��3���\��

        Time.timeScale = TimeScalePaused; // �Q�[���̐i�s���ꎞ��~
    }

    // ���j���[����鏈��
    public void CloseMenu(GameObject menuPanel)
    {
        menuPanel.SetActive(false); // ���j���[���\��
        closeKey.SetActive(false);  // ����L�[���\��

        Time.timeScale = TimeScaleRunning; // �Q�[���̐i�s���ĊJ
    }
    // �ݒ��ʂ�\��
    public void SettingPanel()
    {
        settingPanel.SetActive(true);
        explanationPanel.SetActive(false);
        goTitlePanel.SetActive(false);
    }

    // ���������ʂ�\��
    public void ExplanationPanel()
    {
        settingPanel.SetActive(false);
        explanationPanel.SetActive(true);
        goTitlePanel.SetActive(false);
    }

    // �^�C�g���ɖ߂��\�����A�V�[�������[�h
    public void GoTitlePanel()
    {
        settingPanel.SetActive(false);
        explanationPanel.SetActive(false);
        goTitlePanel.SetActive(true);
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
            settingPanel.SetActive(true);
            explanationPanel.SetActive(false);
            goTitlePanel.SetActive(false);
            Time.timeScale = TimeScalePaused; // �Q�[���̐i�s���ꎞ��~
        }
        // B�{�^���������ꂽ�ꍇ�i���j���[�����j
        else if (isBButton == true)
        {
            foreach (GameObject panel in panels)
            {
                panel.SetActive(false); // ���ׂẴp�l�����\��
            }

            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false); // �J�[�\�����\��

            Time.timeScale = TimeScaleRunning; // �Q�[���̐i�s���ĊJ
        }

        // �eUI�I�u�W�F�N�g�ɉ����Đݒ�p�l����؂�ւ��鏈��
        if (selectedGameObject == settingButton)
        {
            settingPanel.SetActive(true);
            explanationPanel.SetActive(false);
            goTitlePanel.SetActive(false);
            for (int i = 0; i < Cursor.Length; i++) Cursor[i].SetActive(false);
        }
        else if (selectedGameObject == qperationExplanationButton)
        {
            settingPanel.SetActive(false);
            explanationPanel.SetActive(true);
            goTitlePanel.SetActive(false);

            keyBoard.SetActive(true); // �L�[�{�[�h�ݒ��\��
            keyBoardMoveSettingSelect.SetActive(false); // �L�[�{�[�h�ړ��ݒ���\��
            gamePadMoveSettingSelect.SetActive(false); // �Q�[���p�b�h�ړ��ݒ���\��
            gamePad.SetActive(false); // �Q�[���p�b�h�ݒ���\��
        }
        else if (selectedGameObject == backTitleButton)
        {
            settingPanel.SetActive(false);
            explanationPanel.SetActive(false);
            goTitlePanel.SetActive(true);
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
            Cursor[CursorBGMIndex].SetActive(true); // BGM�̃J�[�\����\��
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(false);
        }
        else if (selectedGameObject == SESliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(true); // SE�̃J�[�\����\��
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(false);
        }
        else if (selectedGameObject == MicSliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(true); // �}�C�N�̃J�[�\����\��
            Cursor[CursorMouseIndex].SetActive(false);
        }
        else if (selectedGameObject == MouseSliderGameObject)
        {
            Cursor[CursorBGMIndex].SetActive(false);
            Cursor[CursorSEIndex].SetActive(false);
            Cursor[CursorMicIndex].SetActive(false);
            Cursor[CursorMouseIndex].SetActive(true); // �}�E�X�̃J�[�\����\��
        }
        // �����I������Ă��Ȃ��ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
        else if (selectedGameObject == null)
        {
            // selectedGameObject��null�̏ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
            EventSystem.current.SetSelectedGameObject(settingButton);
        }
    }

    // BGM�̉��ʂ�ݒ肷�郁�\�b�h
    // �����Ƃ��ēn���ꂽbgmVolume���g�p���āA�I�[�f�B�I�~�L�T�[��BGM�̉��ʂ𒲐�����
    public void SetBGM(float bgmVolume)
    {
        // BGM�̉��ʂ�ݒ�
        audioMixer.SetFloat("BGM", bgmVolume); // �I�[�f�B�I�~�L�T�[��"BGM"�̉��ʂ�ݒ�
    }

    // SE�̉��ʂ�ݒ肷�郁�\�b�h
    // �����Ƃ��ēn���ꂽseVolume���g�p���āA�I�[�f�B�I�~�L�T�[��SE�̉��ʂ𒲐�����
    public void SetSE(float seVolume)
    {
        // SE�̉��ʂ�ݒ�
        audioMixer.SetFloat("SE", seVolume); // �I�[�f�B�I�~�L�T�[��"SE"�̉��ʂ�ݒ�
    }

    // �}�C�N�̉��ʂ�ݒ肷�郁�\�b�h
    // �����Ƃ��ēn���ꂽmicVolume���g�p���āA�}�C�N�̉��ʂ𒲐�����
    public void SetMic(float micVolume)
    {
        // �}�C�N���ʂ�ݒ�
        AudioSource micAudioSource = micObject.GetComponent<AudioSource>(); // micObject����AudioSource�R���|�[�l���g���擾
        micAudioSource.volume = micVolume; // ������micVolume�Ɋ�Â��āA�}�C�N�̉��ʂ�ݒ�
    }

    // �}�E�X���x��ݒ肷�郁�\�b�h
    // �����Ƃ��ēn���ꂽmouseSensitivity����ɁACinemachine�J������X����Y���̊��x�𒲐�����
    public void SetMouse(float mouseSensitivity)
    {
        // �}�E�X�̊��x��ݒ�iY���̈ړ����x�j
        VCamera.m_YAxis.m_MaxSpeed = mouseSensitivity / MouseMaxSpeedDivisor; // Y���̊��x��ݒ�i����Z���g�p�j

        // �}�E�X�̊��x��ݒ�iX���̈ړ����x�j
        VCamera.m_XAxis.m_MaxSpeed = mouseSensitivity * MouseMaxSpeedMultiplier; // X���̊��x��ݒ�i�|���Z���g�p�j
    }

}