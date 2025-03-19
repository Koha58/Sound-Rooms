using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene�̃G���A�\���p�A�j���[�V�����Ǘ��N���X
/// </summary>
public class SlideUIControll : MonoBehaviour
{
    // UI�̏�Ԃ�\���ϐ��B0 = ������ԁA1 = �X���C�h�C���A2 = �X���C�h�A�E�g
    public int state = 0;

    // UI���X���C�h�A�E�g������ɖ߂邩�ǂ��������肷��t���O
    [SerializeField] private bool loop = false;

    // �X���C�h�C������уX���C�h�A�E�g�̈ʒu���W��ݒ�
    [Header("Image")]
    [SerializeField] private Vector3 outPos01; // �X���C�h�C���O�̏����ʒu
    [SerializeField] private Vector3 inPos;    // �X���C�h�C����̕\���ʒu
    [SerializeField] private Vector3 outPos02; // �X���C�h�A�E�g��̍ŏI�ʒu

    // �X���C�h�̑��x��ݒ�
    [Header("Speed")]
    [SerializeField] private float slideSpeedIn = 10.0f;  // �X���C�h�C�����̑��x
    [SerializeField] private float slideSpeedOut = 10.0f; // �X���C�h�A�E�g���̑��x

    void Update()
    {
        // ������Ԃ�UI�ʒu�i�X���C�h�A�E�g����O�̈ʒu�j
        if (state == 0)
        {
            // ���݂̈ʒu�� outPos01 �ƈقȂ��Ă�����A�ʒu�� outPos01 �ɐݒ�
            if (transform.localPosition != outPos01)
                transform.localPosition = outPos01;
        }
        // �X���C�hIN�iUI����ʂɃX���C�h�C�����Ă���j
        else if (state == 1)
        {
            // ���݂̈ʒu���ړI�n�iinPos�j�ɏ\���߂���΁A���̈ʒu�ɐݒ�
            if (Vector3.Distance(transform.localPosition, inPos) < 0.1f)
            {
                transform.localPosition = inPos;
            }
            else
            {
                // ���݂̈ʒu����ړI�n�iinPos�j�Ɍ����ăX���C�h
                transform.localPosition = Vector3.Lerp(transform.localPosition, inPos, slideSpeedIn * Time.unscaledDeltaTime);
            }
        }
        // �X���C�hOUT�iUI����ʂ���X���C�h�A�E�g����j
        else if (state == 2)
        {
            // ���݂̈ʒu���ړI�n�ioutPos02�j�ɏ\���߂���΁A���̈ʒu�ɐݒ�
            if (Vector3.Distance(transform.localPosition, outPos02) < 0.1f)
            {
                transform.localPosition = outPos02;
            }
            else
            {
                // ���݂̈ʒu����ړI�n�ioutPos02�j�Ɍ����ăX���C�h
                transform.localPosition = Vector3.Lerp(transform.localPosition, outPos02, slideSpeedOut * Time.unscaledDeltaTime);
            }

            // �X���C�h�A�E�g��ɏ�����ԁioutPos02�j�ɓ��B�����ꍇ
            if (Vector3.Distance(transform.localPosition, outPos02) < 0.1f)
            {
                // 'loop'��true�Ȃ�AUI�������ʒu�ɖ߂�
                if (loop)
                {
                    state = 0;
                }
            }
        }
    }
}