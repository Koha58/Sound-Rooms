using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public CinemachineFreeLook VCamera;
    void Update()
    {
        //���_�ړ����R���g���[������ɐ؂�ւ�
        if (Input.GetAxis("Axis 3") != 0 || Input.GetAxis("Axis 4") != 0)
        {
            // X Axis��Input Axis Name��ύX
            VCamera.m_XAxis.m_InputAxisName = "Axis 3";
            // Y Axis��Input Axis Name��ύX
            VCamera.m_YAxis.m_InputAxisName = "Axis 4";

            VCamera.m_YAxis.m_MaxSpeed = 1.0f;
        }
        //���_�ړ����}�E�X����ɐ؂�ւ�
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // X Axis��Input Axis Name��ύX
            VCamera.m_XAxis.m_InputAxisName = "Mouse X";
            // Y Axis��Input Axis Name��ύX
            VCamera.m_YAxis.m_InputAxisName = "Mouse Y";

            VCamera.m_YAxis.m_MaxSpeed = 1.0f;
        }
    }
}
