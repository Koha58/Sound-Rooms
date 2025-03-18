using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// GameOver����UI��ڑ��f�o�C�X�ɂ���ĕύX����N���X
/// </summary>
public class GameOverDeviceCheck : MonoBehaviour
{
    // ���g���C�{�^���i�Q�[���p�b�h�܂��̓L�[�{�[�h�ɉ����ĕ\��/��\���j
    [SerializeField] private GameObject RetryKey;
    [SerializeField] private GameObject RetryButton;
    // ���{��̃o�b�N�{�^���i�Q�[���p�b�h�܂��̓L�[�{�[�h�ɉ����ĕ\��/��\���j
    [SerializeField] private GameObject JapaneseBackButton;
    [SerializeField] private GameObject JapaneseBackKey;

    // Start is called before the first frame update
    void Start()
    {
        // ������Ԃł́A���ׂĂ�UI�C���[�W���\���ɂ���
        RetryKey.GetComponent<Image>().enabled = false;
        RetryButton.GetComponent<Image>().enabled = false;
        JapaneseBackButton.GetComponent<Image>().enabled = false;
        JapaneseBackKey.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���̓f�o�C�X��Xbox�̏ꍇ�A���g���C�{�^����\�����A�L�[�{�[�h�̃��g���C�L�[���\���ɂ���
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && RetryButton != null)
        {
            RetryButton.GetComponent<Image>().enabled = true;  // Xbox�p�̃{�^���\��
            RetryKey.GetComponent<Image>().enabled = false;   // �L�[�{�[�h�p�̃L�[��\��
        }
        // ���̓f�o�C�X���L�[�{�[�h�̏ꍇ�A���g���C�L�[��\�����AXbox�̃��g���C�{�^�����\���ɂ���
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && RetryKey != null)
        {
            RetryKey.GetComponent<Image>().enabled = true;     // �L�[�{�[�h�p�̃L�[�\��
            RetryButton.GetComponent<Image>().enabled = false; // Xbox�̃��g���C�{�^����\��
        }

        // ���̓f�o�C�X��Xbox�̏ꍇ�A���{��̃o�b�N�{�^����\�����A�L�[�{�[�h�̃o�b�N�L�[���\���ɂ���
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && JapaneseBackButton != null)
        {
            JapaneseBackButton.GetComponent<Image>().enabled = true;  // Xbox�p�̃o�b�N�{�^���\��
            JapaneseBackKey.GetComponent<Image>().enabled = false;   // �L�[�{�[�h�p�̃o�b�N�L�[��\��
        }
        // ���̓f�o�C�X���L�[�{�[�h�̏ꍇ�A���{��̃o�b�N�L�[��\�����AXbox�̃o�b�N�{�^�����\���ɂ���
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && JapaneseBackKey != null)
        {
            JapaneseBackKey.GetComponent<Image>().enabled = true;     // �L�[�{�[�h�p�̃o�b�N�L�[�\��
            JapaneseBackButton.GetComponent<Image>().enabled = false; // Xbox�̃o�b�N�{�^����\��
        }
    }
}