using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameScene�ŃG���A���Ƃ�UI�̕\�����Ǘ�����N���X
/// </summary>
public class AreacheckScript : MonoBehaviour
{
    // UI�̏�Ԃ��Ǘ�����SlideUIControll�̃C���X�^���X���i�[����ϐ�
    [SerializeField]
    private SlideUIControll uiCount;    // �ŏ���UI
    [SerializeField]
    private SlideUIControll uiCount2;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount3;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount4;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount5;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount6;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount7;   // ����UI
    [SerializeField]
    private SlideUIControll uiCount8;   // ����UI

    // Start is called before the first frame update
    void Start()
    {
        // ������ԂƂ��āAuiCount�̏�Ԃ�1�ɐݒ�
        uiCount.state = 1;  // �ŏ���UI��\��
    }

    // �g���K�[�ɓ������Ƃ��ɌĂ΂�郁�\�b�h
    void OnTriggerEnter(Collider other)
    {
        // ���̃I�u�W�F�N�g���w�肵���G���A�ɓ������ꍇ�A���̃G���A�ɑΉ�����UI��ύX

        // "AreaAcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        if (other.CompareTag("AreaAcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount�݂̂�\��
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount.state = 1;  // uiCount��\��
        }
        // "AreaBcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaBcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount2�݂̂�\��
            uiCount.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount2.state = 1;  // uiCount2��\��
        }
        // "AreaCcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaCcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount3�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount3.state = 1;  // uiCount3��\��
        }
        // "AreaDcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaDcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount4�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount4.state = 1;  // uiCount4��\��
        }
        // "AreaEcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaEcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount5�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount5.state = 1;  // uiCount5��\��
        }
        // "AreaFcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaFcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount6�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount6.state = 1;  // uiCount6��\��
        }
        // "AreaGcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaGcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount7�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount8.state = 0;
            uiCount7.state = 1;  // uiCount7��\��
        }
        // "AreaHcheck"�Ƃ����^�O�����Ă���I�u�W�F�N�g���������ꍇ
        else if (other.CompareTag("AreaHcheck"))
        {
            // �S�Ă�UI���\���ɂ��AuiCount8�݂̂�\��
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 1;  // uiCount8��\��
        }
    }
}
