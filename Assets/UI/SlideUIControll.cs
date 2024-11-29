using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIControll : MonoBehaviour
{
    // UIの状態
    public int state = 0;
    public bool loop = false;

    // UIの位置座標
    [Header("Image")]
    public Vector3 outPos01;
    public Vector3 inPos;
    public Vector3 outPos02;

    // スライドの速度
    [Header("Speed")]
    public float slideSpeedIn = 10.0f;  // スライドインの速度
    public float slideSpeedOut = 10.0f; // スライドアウトの速度

    void Update()
    {
        // 初期位置
        if (state == 0)
        {
            if (transform.localPosition != outPos01) transform.localPosition = outPos01;
        }
        // スライドIN
        else if (state == 1)
        {
            // 目標位置と現在の位置が十分近い場合、位置を設定
            if (Vector3.Distance(transform.localPosition, inPos) < 0.1f)
            {
                transform.localPosition = inPos;
            }
            else
            {
                // 目標位置に向けてスライド
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, slideSpeedIn * Time.unscaledDeltaTime);
            }
        }
        // スライドOUT
        else if (state == 2)
        {
            if (Vector3.Distance(transform.localPosition, outPos02) < 0.1f)
            {
                transform.localPosition = outPos02;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, outPos02, slideSpeedOut * Time.unscaledDeltaTime);
            }

            // スライドアウト後に初期状態に戻す
            if (Vector3.Distance(transform.localPosition, outPos02) < 0.1f)
            {
                if (loop)
                {
                    state = 0;
                }
            }
        }
    }
}
