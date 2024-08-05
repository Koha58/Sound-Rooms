using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAction : MonoBehaviour
{
    public CinemachineFreeLook VCamera;
    void Update()
    {
        //視点移動をコントローラ操作に切り替え
        if (Input.GetAxis("Axis 3") != 0 || Input.GetAxis("Axis 4") != 0)
        {
            // X AxisのInput Axis Nameを変更
            VCamera.m_XAxis.m_InputAxisName = "Axis 3";
            // Y AxisのInput Axis Nameを変更
            VCamera.m_YAxis.m_InputAxisName = "Axis 4";

            VCamera.m_YAxis.m_MaxSpeed = 1.0f;
        }
        //視点移動をマウス操作に切り替え
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // X AxisのInput Axis Nameを変更
            VCamera.m_XAxis.m_InputAxisName = "Mouse X";
            // Y AxisのInput Axis Nameを変更
            VCamera.m_YAxis.m_InputAxisName = "Mouse Y";

            VCamera.m_YAxis.m_MaxSpeed = 1.0f;
        }
    }
}
