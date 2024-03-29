using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���_�ړ�
public class CameraRotator : MonoBehaviour
{
    public GameObject target; // �Ǐ]����I�u�W�F�N�g
    public Vector3 offset; // target�Ƃ̈ʒu�֌W

    [SerializeField] private float distance = 4.0f; // target�Ƃ̋���
    [SerializeField] private float polarAngle = 45.0f; // y���W�̃A���O��
    [SerializeField] private float azimuthalAngle = 45.0f; // x���W�̃A���O��
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))//right�L�[�������Ă����
        {
            updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        var lookAtPos = target.transform.position + offset;
        updatePosition(lookAtPos);
        transform.LookAt(lookAtPos);
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}
