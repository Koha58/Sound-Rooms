using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�[�o�[�t���[���Ƀ��O��\������N���X
/// </summary>
public class OverflowHandler : MonoBehaviour
{
    public void OnBufferOverflow(uint overflow)
    {
        // �I�[�o�[�t���[���Ɏ��s���鏈���������ɋL�q
        Debug.Log("�X�s�[�J�[���ڑ�����Ă��܂��� ");
    }
}
