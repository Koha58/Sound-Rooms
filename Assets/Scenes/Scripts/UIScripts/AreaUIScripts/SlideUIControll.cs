using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene�̃G���A�\���p�A�j���[�V�����Ǘ��N���X
/// </summary>
public class SlideUIControll : MonoBehaviour
{
    // UI�̏�Ԃ�\���񋓌^�BInitial = ������ԁASlideIn = �X���C�h�C���ASlideOut = �X���C�h�A�E�g
    public enum State
    {
        Initial = 0, // �������
        SlideIn = 1, // �X���C�h�C�����
        SlideOut = 2 // �X���C�h�A�E�g���
    }

    // ���݂̏��
    public State state = State.Initial;

    // UI���X���C�h�A�E�g������ɖ߂邩�ǂ��������肷��t���O
    [SerializeField] private bool loop = false;

    // �X���C�h�C������уX���C�h�A�E�g�̈ʒu���W��ݒ�
    [Header("Image")]
    [SerializeField] private Vector3 outPos01; // �X���C�h�C���O�̏����ʒu
    [SerializeField] private Vector3 inPos;    // �X���C�h�C����̕\���ʒu
    [SerializeField] private Vector3 outPos02; // �X���C�h�A�E�g��̍ŏI�ʒu

    // �X���C�h�̑��x��ݒ�
    [Header("Speed")]
    [SerializeField] private float slideSpeedIn = DefaultSlideSpeed;  // �X���C�h�C�����̑��x
    [SerializeField] private float slideSpeedOut = DefaultSlideSpeed; // �X���C�h�A�E�g���̑��x

    // �萔
    private const float SlideCompletionThreshold = 0.1f;  // �X���C�h�����̔���臒l
    private const float DefaultSlideSpeed = 10.0f;       // �f�t�H���g�̃X���C�h���x

    void Update()
    {
        // ������Ԃ�UI�ʒu�i�X���C�h�A�E�g����O�̈ʒu�j
        if (state == State.Initial)
        {
            // ���݂̈ʒu�� outPos01 �ƈقȂ��Ă�����A�ʒu�� outPos01 �ɐݒ�
            if (transform.localPosition != outPos01)
                transform.localPosition = outPos01;
        }
        // �X���C�hIN�iUI����ʂɃX���C�h�C�����Ă���j
        else if (state == State.SlideIn)
        {
            // ���݂̈ʒu���ړI�n�iinPos�j�ɏ\���߂���΁A���̈ʒu�ɐݒ�
            if (Vector3.Distance(transform.localPosition, inPos) < SlideCompletionThreshold)
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
        else if (state == State.SlideOut)
        {
            // ���݂̈ʒu���ړI�n�ioutPos02�j�ɏ\���߂���΁A���̈ʒu�ɐݒ�
            if (Vector3.Distance(transform.localPosition, outPos02) < SlideCompletionThreshold)
            {
                transform.localPosition = outPos02;
            }
            else
            {
                // ���݂̈ʒu����ړI�n�ioutPos02�j�Ɍ����ăX���C�h
                transform.localPosition = Vector3.Lerp(transform.localPosition, outPos02, slideSpeedOut * Time.unscaledDeltaTime);
            }

            // �X���C�h�A�E�g��ɏ�����ԁioutPos02�j�ɓ��B�����ꍇ
            if (Vector3.Distance(transform.localPosition, outPos02) < SlideCompletionThreshold)
            {
                // 'loop'��true�Ȃ�AUI�������ʒu�ɖ߂�
                if (loop)
                {
                    state = State.Initial; // ������Ԃɖ߂�
                }
            }
        }
    }
}