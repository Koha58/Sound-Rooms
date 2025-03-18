using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// 接続デバイスによって視点移動に対応する入力を変更するクラス
/// </summary>
public class CameraAction : MonoBehaviour
{
    // Cinemachineの自由視点カメラ
    [SerializeField] private CinemachineFreeLook VCamera;

    void Update()
    {
        // コントローラの入力に基づいて視点移動を切り替える
        if (Input.GetAxis("Axis 3") != 0 || Input.GetAxis("Axis 4") != 0)
        {
            // コントローラのX軸入力 (右スティックの水平移動) を検出
            // CinemachineカメラのX軸の入力設定を "Axis 3" に変更
            VCamera.m_XAxis.m_InputAxisName = "Axis 3";

            // コントローラのY軸入力 (右スティックの垂直移動) を検出
            // CinemachineカメラのY軸の入力設定を "Axis 4" に変更
            VCamera.m_YAxis.m_InputAxisName = "Axis 4";
        }
        // マウスの入力に基づいて視点移動を切り替える
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            // マウスのX軸移動 (左右移動) を検出
            // CinemachineカメラのX軸の入力設定を "Mouse X" に変更
            VCamera.m_XAxis.m_InputAxisName = "Mouse X";

            // マウスのY軸移動 (上下移動) を検出
            // CinemachineカメラのY軸の入力設定を "Mouse Y" に変更
            VCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        }
    }
}