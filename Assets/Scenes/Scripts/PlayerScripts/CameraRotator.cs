using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//Ž‹“_ˆÚ“®
public class CameraRotator : MonoBehaviour
{
    Transform cameraTrans;
    [SerializeField] Transform playerTrans;

    [SerializeField] Vector3 cameraVec;  //Vector3(0, 1, -1)
    [SerializeField] Vector3 cameraRot;  //Vector3(45, 0, 0)

    void Awake()
    {
        cameraTrans = transform;
        cameraTrans.rotation = Quaternion.Euler(cameraRot);
    }

    private void FixedUpdate()
    {
        cameraTrans.position = playerTrans.position + cameraVec;
    }

}
