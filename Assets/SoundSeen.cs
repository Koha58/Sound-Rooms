using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoundSeen : MonoBehaviour
{
    MeshRenderer spmr;


    void Start()
    {
        //�ŏ��͌����Ȃ����
        spmr = GetComponent<MeshRenderer>();
        spmr.enabled = false; //�����Ȃ��i�����j
    }
}
