using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// StartScene�̃{�^���Ǘ��N���X
/// </summary>
public class StartManager : MonoBehaviour
{
    // Image�R���|�[�l���g���Q��
    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    // �{�^����GameObject�Q��
    [SerializeField] private GameObject SelectButton;
    [SerializeField] private GameObject BackDesktopButton;

    // �{�^���̈ړ������Ǘ��p�t���O
    bool UPDOWN;

    // ���̈ʒu��ۑ����邽�߂̕ϐ�
    Vector3 originalSelectButtonPosition;
    Vector3 originalBackDesktopButtonPosition;

    // Select�{�^���������ꂽ�Ƃ��̉����Đ�����AudioSource
    [SerializeField] AudioSource SelectSound;  // AudioSource��SerializeField�Ƃ��ăC���X�y�N�^�[����ݒ�

    // ���̓f�o�C�X�̎�ނ𔻒肷��t���O
    bool deviceCheck;

    // Start is called before the first frame update
    void Start()
    {
        // �{�^����Image�R���|�[�l���g���擾
        SelectButtonImage = SelectButton.GetComponent<Image>();
        BackDesktopButtonImage = BackDesktopButton.GetComponent<Image>();

        // Select�{�^���͐F�����ɁABackDesktop�{�^���͐F�𔖂��ݒ�
        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // �{�^���̌��̈ʒu��ۑ�
        originalSelectButtonPosition = SelectButton.GetComponent<RectTransform>().localPosition;
        originalBackDesktopButtonPosition = BackDesktopButton.GetComponent<RectTransform>().localPosition;

        // Select�{�^�����������Ɉړ�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�

        UPDOWN = true;

        // AudioSource �R���|�[�l���g���擾
        SelectSound = GetComponent<AudioSource>();

        deviceCheck = false; // �L�[�{�[�h���g�p����Ă���
    }

    void Update()
    {
        // ���̓f�o�C�X�̎�ނ��m�F���A�t���O��ݒ�
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // �R���g���[���[���g�p����Ă���
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // �L�[�{�[�h���g�p����Ă���
        }

        if (deviceCheck)
        {
            GamePadUIController();
        }
    }

    // Select�{�^�����I�����ꂽ�Ƃ��ɃV�[���J��
    public void OnSelect()
    {
        // �����Đ����ăV�[���J�ڂ���R���[�`�����J�n
        StartCoroutine(PlaySelectSoundAndLoadScene());
    }

    // BackDesktop�{�^�����I�����ꂽ�Ƃ��ɃQ�[�����I��
    public void OnBackDesktop()
    {
#if UNITY_EDITOR
        // Unity�G�f�B�^�[�̏ꍇ�A�Q�[���v���C���I��
        UnityEditor.EditorApplication.isPlaying = false; // �Q�[���v���C�I��
#else
        // �r���h���ꂽ�Q�[���̏ꍇ�A�A�v���P�[�V�������I��
        Application.Quit(); // �Q�[���v���C�I��
#endif
    }

    // Select�{�^���ɃJ�[�\�����������Ƃ��̏���
    public void EnterSelectButton()
    {
        // Select�{�^�����������Ɉړ�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-40f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�

        // Select�{�^���̐F�����ɕύX
        SelectButtonImage.color = new Color32(0, 0, 0, 255);

        // selectedGameObject��null�̏ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Select�{�^������J�[�\�����o���Ƃ��̏���
    public void ExitSelectButton()
    {
        // Select�{�^���̐F�𔖂��ݒ�
        SelectButtonImage.color = new Color32(0, 0, 0, 120);

        // Select�{�^�������̈ʒu�ɖ߂�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition;
    }

    // BackDesktop�{�^���ɃJ�[�\�����������Ƃ��̏���
    public void EnterBackDesktopButton()
    {
        // BackDesktop�{�^�����������Ɉړ�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButton�͈ړ����Ȃ�
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-40f, 0f, 0f);

        // BackDesktop�{�^���̐F�����ɕύX
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);

        // selectedGameObject��null�̏ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
        EventSystem.current.SetSelectedGameObject(null);
    }

    // BackDesktop�{�^������J�[�\�����o���Ƃ��̏���
    public void ExitBackDesktopButton()
    {
        // BackDesktop�{�^���̐F�𔖂��ݒ�
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // BackDesktop�{�^�������̈ʒu�ɖ߂�
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition;
    }

    // �����Đ����ăV�[���J�ڂ���R���[�`��
    private IEnumerator PlaySelectSoundAndLoadScene()
    {
        // �����Đ�
        SelectSound.PlayOneShot(SelectSound.clip);

        // �����Đ������̂�ҋ@ (���̒����������ҋ@)
        yield return new WaitForSeconds(SelectSound.clip.length);

        // �����I��������ɃV�[����J��
        SceneManager.LoadScene("StageSelectScene");
    }

    public void GamePadUIController()
    {
        // �I�𒆂�UI�擾
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == SelectButton)
        {
            // Select�{�^���̐F�����ɕύX
            SelectButtonImage.color = new Color32(0, 0, 0, 255);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
        }
        else if (selectedGameObject == BackDesktopButton)
        {
            // Select�{�^���̐F�𔖂��ݒ�
            SelectButtonImage.color = new Color32(0, 0, 0, 120);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
        }
        else if(selectedGameObject == null)
        {
            // selectedGameObject��null�̏ꍇ�AsettingButton�Ƀt�H�[�J�X�𓖂Ă�
            EventSystem.current.SetSelectedGameObject(SelectButton);
        }
    }
}