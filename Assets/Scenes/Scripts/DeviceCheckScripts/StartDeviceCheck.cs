using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// StartScene��UI�\�����f�o�C�X�ɂ���Đ؂�ւ���N���X
/// </summary>
public class StartDeviceCheck : MonoBehaviour
{
    GameObject Cursor; // �J�[�\���I�u�W�F�N�g���i�[���邽�߂̕ϐ�

    // Start is called before the first frame update
    void Start()
    {
        // "Cursor"�Ƃ������O��GameObject���V�[������擾���ACursor�ϐ��Ɋi�[
        Cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̓��̓f�o�C�X��Xbox�̏ꍇ�A�J�[�\����\��
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = true; // �J�[�\����Image�R���|�[�l���g��L���ɂ��ĕ\��
        }
        // ���݂̓��̓f�o�C�X���L�[�{�[�h�̏ꍇ�A�J�[�\�����\��
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = false; // �J�[�\����Image�R���|�[�l���g�𖳌��ɂ��Ĕ�\��
        }
    }
}
