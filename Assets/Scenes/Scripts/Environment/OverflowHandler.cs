using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowHandler : MonoBehaviour
{
    public void OnBufferOverflow(uint overflow)
    {
        // �I�[�o�[�t���[���Ɏ��s���鏈���������ɋL�q
        Debug.Log("�X�s�[�J�[���ڑ�����Ă��܂��� ");

        // �ǉ��̏����iUI�̍X�V�A�ď������Ȃǁj
        // Example: UI���X�V����A�x������炷�A��Ԃ����Z�b�g����Ȃ�
    }
}
