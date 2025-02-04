using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

// ���̊l����{�b�N�X�̈ړ����Ǘ�����X�N���v�g
public class ImpactOnObjects : MonoBehaviour
{
    // ���̊l������\�����邽�߂�UI�e�L�X�g
    public TextMeshProUGUI keyCountText;
    // ���̊l����
    public int count;

    // �����E���ۂ̌��ʉ�
    [SerializeField] AudioSource PickupSound;

    // �R���g���[���[�܂��̓L�[�{�[�h��UI
    [SerializeField] GameObject ControllerKeyUI;
    [SerializeField] GameObject KeyboardKeyUI;

    // ���I�u�W�F�N�g�ƌ��`�F�b�N�p�̃I�u�W�F�N�g
    GameObject Key;
    GameObject KeyCheck;

    // ���̓f�o�C�X�̎�ނ𔻒肷��t���O
    bool deviceCheck;

    void Start()
    {
        // ���̃J�E���g�����������AUI�ɔ��f
        count = 0;
        SetCountText();

        // �����E�������̌��ʉ���ݒ�
        PickupSound = GetComponent<AudioSource>();

        // ���Ƃ��̃`�F�b�N�p�̃I�u�W�F�N�g��T���Ď擾
        Key = GameObject.FindGameObjectWithTag("Key");
        KeyCheck = GameObject.FindGameObjectWithTag("KeyCheck");

        // ���̓f�o�C�X�̎�ނ��m�F���A�t���O��ݒ�
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // �R���g���[���[���g�p����Ă���
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // �L�[�{�[�h���g�p����Ă���
        }

        // ������Ԃ�UI�͔�\��
        ControllerKeyUI.GetComponent<Image>().enabled = false;
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        // ���̓f�o�C�X�̎�ނ����A���^�C���Ŋm�F���ăt���O���X�V
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // �R���g���[���[���g�p����Ă���
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // �L�[�{�[�h���g�p����Ă���
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            // Box�^�O���t����ꂽ�I�u�W�F�N�g�ɐڐG�����ꍇ�ARigidbody���擾
            var rb = other.GetComponent<Rigidbody>();

            // Box���ړ��E��]�����邽�߂ɐ��������
            rb.constraints = RigidbodyConstraints.None;

            // �I�u�W�F�N�g�ɑO���֗͂������Ĉړ�
            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
        else if (other.CompareTag("KeyCheck"))
        {
            // ���̓f�o�C�X�ɉ�����UI�̕\����؂�ւ�
            if (deviceCheck)
            {
                // �R���g���[���[�g�p��
                ControllerKeyUI.GetComponent<Image>().enabled = true;
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
            }
            else
            {
                // �L�[�{�[�h�g�p��
                KeyboardKeyUI.GetComponent<Image>().enabled = true;
                ControllerKeyUI.GetComponent<Image>().enabled = false;
            }

            // �����E������𔻒�
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("joystick button 0"))
            {
                // UI���\���ɂ���
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
                ControllerKeyUI.GetComponent<Image>().enabled = false;

                // ���I�u�W�F�N�g���\���ɂ��Č����l��
                Key.SetActive(false);
                KeyCheck.SetActive(false);

                // ���ʉ����Đ�
                PickupSound.PlayOneShot(PickupSound.clip);

                // ���̃J�E���g�𑝉�
                count++;

                // UI�̃J�E���g���X�V
                SetCountText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // UI���\���ɂ���
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
        ControllerKeyUI.GetComponent<Image>().enabled = false;
    }

    // ���̃J�E���g��UI�ɔ��f������
    public void SetCountText()
    {
        keyCountText.text = count.ToString();
    }
}
