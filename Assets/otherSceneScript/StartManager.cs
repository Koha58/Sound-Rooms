using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private UIInputActions _uiInputActions;

    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    public GameObject SelectButton;
    public GameObject BackDesktopButton;


    bool UPDOWN;

    Vector3 originalSelectButtonPosition; // ���̈ʒu��ۑ����邽�߂̕ϐ�
    Vector3 originalBackDesktopButtonPosition; // ���̈ʒu��ۑ����邽�߂̕ϐ�

    [SerializeField] AudioSource SelectSound;  // AudioSource��SerializeField�Ƃ��ăC���X�y�N�^�[����ݒ�

    // Start is called before the first frame update
    void Start()
    {

        _uiInputActions = new UIInputActions();
        _uiInputActions.Enable();

        SelectButtonImage = SelectButton.GetComponent<Image>();
        BackDesktopButtonImage = BackDesktopButton.GetComponent<Image>();

        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // �{�^���̌��̈ʒu��ۑ�
        originalSelectButtonPosition = SelectButton.GetComponent<RectTransform>().localPosition;
        originalBackDesktopButtonPosition = BackDesktopButton.GetComponent<RectTransform>().localPosition;

        // Select�{�^���������}�C�i�XX�����Ɉړ�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�

        UPDOWN = true;

        // AudioSource �R���|�[�l���g���擾
        SelectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_uiInputActions.SettingUI.MainSelsectUp.triggered)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 255);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
            UPDOWN = true;

            // Select�{�^���������}�C�i�XX�����Ɉړ�
            SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
            BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�
        }
        else if (_uiInputActions.SettingUI.MainSelsectDown.triggered)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 120);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
            UPDOWN = false;

            // BackDesktop�{�^���������}�C�i�XX�����Ɉړ�
            BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-20f, 0f, 0f);
            SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButton�͈ړ����Ȃ�
        }

        // �{�^�����I�����ꂽ��ԂŃ{�^���������ꂽ�ꍇ�̏���
        if (UPDOWN == true)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                // �����Đ����ăV�[���J�ڂ���R���[�`�����J�n
                StartCoroutine(PlaySelectSoundAndLoadScene());
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // �Q�[���v���C�I��
#else
                Application.Quit(); // �Q�[���v���C�I��
#endif
            }
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
        UnityEditor.EditorApplication.isPlaying = false; // �Q�[���v���C�I��
#else
        Application.Quit(); // �Q�[���v���C�I��
#endif
    }

    public void EnterSelectButton()
    {
        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        // Select�{�^���������}�C�i�XX�����Ɉړ�
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�
    }

    public void ExitSelectButton()
    {
        SelectButtonImage.color = new Color32(0, 0, 0, 120);
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition;
    }

    public void EnterBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
        // BackDesktop�{�^���������}�C�i�XX�����Ɉړ�
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-20f, 0f, 0f);
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButton�͈ړ����Ȃ�
    }

    public void ExitBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
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
}
