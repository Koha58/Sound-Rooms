using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideUIControll : MonoBehaviour
{
    // UI�̏��
    public int state = 0;
    public bool loop = false;

    // UI�̈ʒu���W
    [Header("Image")]
    public Vector3 outPos01;
    public Vector3 inPos;
    public Vector3 outPos02;

    // �X���C�h�̑��x
    [Header("Speed")]
    public float slideSpeedIn = 10.0f;  // �X���C�h�C���̑��x
    public float slideSpeedOut = 10.0f; // �X���C�h�A�E�g�̑��x

    void Update()
    {
        // �����ʒu
        if (state == 0)
        {
            if (transform.localPosition != outPos01) transform.localPosition = outPos01;
        }
        // �X���C�hIN
        else if (state == 1)
        {
            // �ڕW�ʒu�ƌ��݂̈ʒu���\���߂��ꍇ�A�ʒu��ݒ�
            if (Vector3.Distance(transform.localPosition, inPos) < 0.1f)
            {
                transform.localPosition = inPos;
            }
            else
            {
                // �ڕW�ʒu�Ɍ����ăX���C�h
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, slideSpeedIn * Time.unscaledDeltaTime);
            }
        }
        // �X���C�hOUT
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

            // �X���C�h�A�E�g��ɏ�����Ԃɖ߂�
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
