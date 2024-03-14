using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//sphereオブジェクトの不可視化
public class SoundSeen : MonoBehaviour
{
    MeshRenderer spmr;


    void Start()
    {
        //最初は見えない状態
        spmr = GetComponent<MeshRenderer>();
        spmr.enabled = false; //見えない（無効）
    }
}
