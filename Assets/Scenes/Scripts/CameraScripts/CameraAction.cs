using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// �ڑ��f�o�C�X�ɂ���Ď��_�ړ��ɑΉ�������͂�ύX����N���X
/// </summary>
public class CameraAction : MonoBehaviour
{
    // Cinemachine�̎��R���_�J����
    [SerializeField] private CinemachineFreeLook VCamera;

    void Update()
    {
        // �R���g���[���̓��͂Ɋ�Â��Ď��_�ړ���؂�ւ���
        if (Input.GetAxis("Axis 3") != 0 || Input.GetAxis("Axis 4") != 0)
        {
            // �R���g���[����X������ (�E�X�e�B�b�N�̐����ړ�) �����o
            // Cinemachine�J������X���̓��͐ݒ�� "Axis 3" �ɕύX
            VCamera.m_XAxis.m_InputAxisName = "Axis 3";

            // �R���g���[����Y������ (�E�X�e�B�b�N�̐����ړ�) �����o
            // Cinemachine�J������Y���̓��͐ݒ�� "Axis 4" �ɕύX
            VCamera.m_YAxis.m_InputAxisName = "Axis 4";
        }
        // �}�E�X�̓��͂Ɋ�Â��Ď��_�ړ���؂�ւ���
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // �}�E�X��X���ړ� (���E�ړ�) �����o
            // Cinemachine�J������X���̓��͐ݒ�� "Mouse X" �ɕύX
            VCamera.m_XAxis.m_InputAxisName = "Mouse X";

            // �}�E�X��Y���ړ� (�㉺�ړ�) �����o
            // Cinemachine�J������Y���̓��͐ݒ�� "Mouse Y" �ɕύX
            VCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }
}