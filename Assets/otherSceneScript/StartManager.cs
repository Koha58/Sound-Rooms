using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    public GameObject SelectButton;
    public GameObject BackDesktopButton;

    public GameObject Cursor;
    public GameObject Cursor1;

    bool UPDOWN;

    Vector3 originalSelectButtonPosition; // ���̈ʒu��ۑ����邽�߂̕ϐ�
    Vector3 originalBackDesktopButtonPosition; // ���̈ʒu��ۑ����邽�߂̕ϐ�

    [SerializeField] AudioSource SelectSound;  // AudioSource��SerializeField�Ƃ��ăC���X�y�N�^�[����ݒ�
    [SerializeField] AudioClip selectClip;     // �{�^���I�����Ɏg�p����AudioClip

    // Start is called before the first frame update
    void Start()
    {
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

        Cursor.SetActive(false);
        Cursor1.SetActive(false);
        UPDOWN = true;

        // AudioSource �R���|�[�l���g���擾
        SelectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 255);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
            Cursor.SetActive(true);
            Cursor1.SetActive(false);
            UPDOWN = true;

            // Select�{�^���������}�C�i�XX�����Ɉړ�
            SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
            BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButton�͈ړ����Ȃ�
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 120);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
            Cursor1.SetActive(true);
            Cursor.SetActive(false);
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
                PlaySelectSound(); // �����Đ�
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                PlaySelectSound(); // �����Đ�
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
        PlaySelectSound(); // �����Đ�
        SceneManager.LoadScene("StageSelectScene");
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

    // �����Đ����郁�\�b�h
    private void PlaySelectSound()
    {
        if (SelectSound != null && selectClip != null)
        {
            SelectSound.PlayOneShot(selectClip); // ������x�����Đ�
        }
    }
}
