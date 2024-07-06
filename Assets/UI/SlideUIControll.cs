using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIControll : MonoBehaviour
{
    //UIの状態
    public int state = 0;
    public bool loop = false;

    //UIの位置座標
    [Header("Image")]
    public Vector3 outPos01;
    public Vector3 inPos;
    public Vector3 outPos02;

    void Update()
    {
        //初期位置
        if (state == 0)
        {
            if (transform.localPosition != outPos01) transform.localPosition = outPos01;
        }
        //スライドIN
        else if (state == 1)
        {
            if (transform.localPosition.x > inPos.x - 1.0f
                && transform.localPosition.y > inPos.y - 1.0f
                && transform.localPosition.z > inPos.z - 1.0f) transform.localPosition = inPos;
            else transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, 4.0f * Time.unscaledDeltaTime);
        }
        else if (state == 2)
        {
            //スライドOUT
            if (transform.localPosition != outPos02)
            {
                if (transform.localPosition.x > outPos02.x - 1.0f
                    && transform.localPosition.y > outPos02.y - 1.0f
                    && transform.localPosition.z > outPos02.z - 1.0f) transform.localPosition = outPos02;
                else transform.localPosition = Vector3.Lerp(transform.localPosition, outPos02, 2.0f * Time.unscaledDeltaTime);
            }
            else
            {
                //初期状態に戻す
                if(loop)
                {
                    state = 0;
                }
            }
        }
    }
}
