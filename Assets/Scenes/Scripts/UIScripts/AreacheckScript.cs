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
    private SlideUIControll uiAreaA;  // �G���AA�pUI
    [SerializeField]
    private SlideUIControll uiAreaB;    // �G���AB�pUI
    [SerializeField]
    private SlideUIControll uiAreaC;    // �G���AC�pUI
    [SerializeField]
    private SlideUIControll uiAreaD;    // �G���AD�pUI
    [SerializeField]
    private SlideUIControll uiAreaE;    // �G���AE�pUI
    [SerializeField]
    private SlideUIControll uiAreaF;    // �G���AF�pUI
    [SerializeField]
    private SlideUIControll uiAreaG;    // �G���AG�pUI
    [SerializeField]
    private SlideUIControll uiAreaH;    // �G���AH�pUI

    // Start is called before the first frame update
    void Start()
    {
        // ������ԂƂ��āA�G���AA�pUI��\��
        ShowUI(uiAreaA);
    }

    // �g���K�[�ɓ������Ƃ��ɌĂ΂�郁�\�b�h
    void OnTriggerEnter(Collider other)
    {
        // �������G���A�ɑΉ�����UI��\��
        if (other.CompareTag("AreaAcheck"))
        {
            ShowUI(uiAreaA);  // �G���AA�pUI��\��
        }
        else if (other.CompareTag("AreaBcheck"))
        {
            ShowUI(uiAreaB);  // �G���AB�pUI��\��
        }
        else if (other.CompareTag("AreaCcheck"))
        {
            ShowUI(uiAreaC);  // �G���AC�pUI��\��
        }
        else if (other.CompareTag("AreaDcheck"))
        {
            ShowUI(uiAreaD);  // �G���AD�pUI��\��
        }
        else if (other.CompareTag("AreaEcheck"))
        {
            ShowUI(uiAreaE);  // �G���AE�pUI��\��
        }
        else if (other.CompareTag("AreaFcheck"))
        {
            ShowUI(uiAreaF);  // �G���AF�pUI��\��
        }
        else if (other.CompareTag("AreaGcheck"))
        {
            ShowUI(uiAreaG);  // �G���AG�pUI��\��
        }
        else if (other.CompareTag("AreaHcheck"))
        {
            ShowUI(uiAreaH);  // �G���AH�pUI��\��
        }
    }

    // �w�肳�ꂽUI��\�����A����UI�͔�\���ɂ��郁�\�b�h
    private void ShowUI(SlideUIControll activeUI)
    {
        // �S�Ă�UI���\���ɂ���
        uiAreaA.state = SlideUIControll.State.Initial;
        uiAreaB.state = SlideUIControll.State.Initial;
        uiAreaC.state = SlideUIControll.State.Initial;
        uiAreaD.state = SlideUIControll.State.Initial;
        uiAreaE.state = SlideUIControll.State.Initial;
        uiAreaF.state = SlideUIControll.State.Initial;
        uiAreaG.state = SlideUIControll.State.Initial;
        uiAreaH.state = SlideUIControll.State.Initial;

        // �w�肳�ꂽUI��\��
        activeUI.state = SlideUIControll.State.SlideIn;
    }
}
