using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGM���Ǘ�����R�[�h
/// </summary>
public class BGMController : MonoBehaviour
{
    // BGM�p��AudioSource���C���X�y�N�^�[�Őݒ�ł���悤�ɂ���
    [SerializeField] AudioSource BGM;

    // Start�͍ŏ���1�񂾂��Ă΂��
    void Start()
    {
        // AudioSource �R���|�[�l���g���擾
        BGM = GetComponent<AudioSource>();

        // PlaySound ���\�b�h��0�b��ɊJ�n���A�ȍ~10�b���ƂɌJ��Ԃ����s
        InvokeRepeating("PlaySound", 0f, 10f);
    }

    // Update�͖��t���[�����s����邪�A���݂͉����s���Ă��Ȃ�
    void Update()
    {
        // ���ɍX�V�����͕K�v�Ȃ����߁A���̃��\�b�h�͋�ł����Ȃ�
    }

    // PlaySound���\�b�h�FBGM���Đ�����
    public void PlaySound()
    {
        // BGM�������Đ�
        BGM.Play();
    }
}