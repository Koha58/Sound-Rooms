using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �I�v�V������ʕ\�����̃R���g���[���[�ł̃J�[�\�����Ǘ�����N���X
/// </summary>
public class GameSceneController : MonoBehaviour
{
    // �ݒ�I��ڈ�
    [SerializeField] GameObject Select;

    // �ݒ荀�ڂ�Y���W�i�I�����̈ʒu���Ǘ��j
    private const float MAIN_SETTING_ORIGIN_POSITION_X = -773f;  // �����ʒuX
    private const float MAIN_SETTING_ORIGIN_POSITION_Y = 317f;  // �����ʒuY
    private const float MAIN_SETTING_ORIGIN_POSITION_Z = 0f;    // �����ʒuZ

    // �e�I������Y���W�i�c�̈ʒu�j
    private const float SECOND_SETTING_POSITION_Y = 212f;   // 2�Ԗڂ̑I������Y���W
    private const float THIRD_SETTING_POSITION_Y = 113f;    // 3�Ԗڂ̑I������Y���W

    // �I���ʒu�i0: 1�Ԗ�, 1: 2�Ԗ�, 2: 3�Ԗځj
    private const int FIRST_SETTING = 0;
    private const int SECOND_SETTING = 1;
    private const int THIRD_SETTING = 2;

    // �ݒ�I���̌��݈ʒu
    private int mainSelectPosition = FIRST_SETTING;

    // �ݒ肪�I������Ă��邩�ǂ����̃t���O
    private bool mainSelectPositionSelect;


    void Update()
    {
        // �R���g���[���[�̓��͂Ɋ�Â��Đݒ�I���������Ăяo��
        Controller();
    }

    // �ݒ�̑I�����Ǘ����郁�\�b�h
    private void Controller()
    {
        // �ݒ�I���̖ڈ�ƂȂ�I�u�W�F�N�g��Transform���擾
        Transform mainSettingSelectTransform = Select.transform;

        // �����ʒu��ݒ�
        mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, MAIN_SETTING_ORIGIN_POSITION_Y, MAIN_SETTING_ORIGIN_POSITION_Z);

        // �ݒ�ύX����Y���W�������ʒu�ɐݒ�
        float mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;

        // �㉺���͂�0�̏ꍇ�A�I����Ԃ�����
        if (Input.GetAxis("Vertical") == 0)
        {
            mainSelectPositionSelect = false;
        }

        // �������̓��́iVertical�����̒l�j�őI���������Ɉړ�
        if (Input.GetAxisRaw("Vertical") < 0 && !mainSelectPositionSelect)
        {
            switch (mainSelectPosition)
            {
                case FIRST_SETTING:
                    // 1�Ԗڂ̑I��������2�ԖڂɈړ�
                    mainSelectPosition = SECOND_SETTING;
                    mainSettingChangePositionY = SECOND_SETTING_POSITION_Y;
                    break;
                case SECOND_SETTING:
                    // 2�Ԗڂ̑I��������3�ԖڂɈړ�
                    mainSelectPosition = THIRD_SETTING;
                    mainSettingChangePositionY = THIRD_SETTING_POSITION_Y;
                    break;
                case THIRD_SETTING:
                    // 3�Ԗڂ̑I��������1�Ԗڂɖ߂�
                    mainSelectPosition = FIRST_SETTING;
                    mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;
                    break;
            }

            // �ݒ�I��ڈ�̈ʒu���X�V
            mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, mainSettingChangePositionY, MAIN_SETTING_ORIGIN_POSITION_Z);
            mainSelectPositionSelect = true;
        }
        // ������̓��́iVertical�����̒l�j�őI��������Ɉړ�
        else if (Input.GetAxisRaw("Vertical") > 0 && !mainSelectPositionSelect)
        {
            switch (mainSelectPosition)
            {
                case FIRST_SETTING:
                    // 1�Ԗڂ̑I��������3�ԖڂɈړ�
                    mainSelectPosition = THIRD_SETTING;
                    mainSettingChangePositionY = THIRD_SETTING_POSITION_Y;
                    break;
                case SECOND_SETTING:
                    // 2�Ԗڂ̑I��������1�Ԗڂɖ߂�
                    mainSelectPosition = FIRST_SETTING;
                    mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;
                    break;
                case THIRD_SETTING:
                    // 3�Ԗڂ̑I��������2�ԖڂɈړ�
                    mainSelectPosition = SECOND_SETTING;
                    mainSettingChangePositionY = SECOND_SETTING_POSITION_Y;
                    break;
            }

            // �ݒ�I��ڈ�̈ʒu���X�V
            mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, mainSettingChangePositionY, MAIN_SETTING_ORIGIN_POSITION_Z);
            mainSelectPositionSelect = true;
        }
    }
}