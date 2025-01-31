using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RadioTextController : MonoBehaviour
{
    public Text text;  // Text UI �R���|�[�l���g�ւ̎Q��
    private int TextCounter = 0;  // �e�L�X�g�J�E���^�[
    private float textDisplayTime = 4f;  // �e�e�L�X�g��\�����鎞�ԁi�b�j

    // Start is called before the first frame update
    void Start()
    {
        // 2�b��ɍŏ��̃e�L�X�g��\��
        StartCoroutine(StartTextSequence());
    }

    // 2�b��Ƀe�L�X�g�V�[�P���X���J�n����
    IEnumerator StartTextSequence()
    {
        yield return new WaitForSeconds(2f);  // �V�[���J�n����2�b�ҋ@
        StartCoroutine(SwitchText());  // �e�L�X�g�����Ԃɐ؂�ւ���
    }

    // �e�L�X�g�����Ԃ��Ƃɐ؂�ւ���R���[�`��
    IEnumerator SwitchText()
    {
        while (TextCounter <= 12)  // �ő�̃e�L�X�g�J�E���^�[�i12�Ԗڂ܂Łj
        {
            // ���݂̃e�L�X�g��ݒ�
            SetText();

            // Text���Ƃɐ؂�ւ��^�C�~���O��ς���
            ChangeTiming();

            // �w�肵���b���҂�
            yield return new WaitForSeconds(textDisplayTime);

            // �J�E���^�[��i�߂�
            TextCounter++;
        }
    }

    // ���݂̃e�L�X�g��ݒ肷��
    void SetText()
    {
        if (TextCounter == 0)
        {
            text.text = "�������Ă��邩";
        }
        else if (TextCounter == 1)
        {
            text.text = "�m���Ɏ����͐�������";
        }
        else if (TextCounter == 2)
        {
            text.text = "�����z��͉�X�̑z���ȏ�ɐi�����߂���";
        }
        else if (TextCounter == 3)
        {
            text.text = "�z��͖ڂ����������Ɏ����悢";
        }
        else if (TextCounter == 4)
        {
            text.text = "�悢���@�z��̑O�ł͌����ĉ��𗧂ĂĂ͂Ȃ��";
        }
        else if (TextCounter == 5)
        {
            text.text = "���O����������E�o����ɂ͌����K�v����";
        }
        else if (TextCounter == 6)
        {
            text.text = "���������Ȃ��ƂɌ���ꂽ������";
        }
        else if (TextCounter == 7)
        {
            text.text = "�z�炪���ƒE�o���t�߂�����Ă���";
        }
        else if (TextCounter == 8)
        {
            text.text = "�t�������΁@�z�炪��������ꏊ�ق�";
        }
        else if (TextCounter == 9)
        {
            text.text = "���O���~�������̂��߂��ɂ���Ƃ�������";
        }
        else if (TextCounter == 10)
        {
            text.text = "�������悤���Ⴊ�@���̏o���߂��ɂ͋C��t����񂶂Ⴜ";
        }
        else if (TextCounter == 11)
        {
            text.text = "���̉����͓z�炩��g����邽�߂Ɏg���Ƃ悢";
        }
        else if (TextCounter == 12)
        {
            text.text = "�K�^���F��";
        }
    }

    void ChangeTiming()
    {
        if (TextCounter == 0)
        {
            textDisplayTime = 2.0f;
        }
        else if (TextCounter == 1)
        {
            textDisplayTime = 4.0f;
        }
        else if (TextCounter == 2)
        {
            textDisplayTime = 5.0f;
        }
        else if (TextCounter == 4)
        {
            textDisplayTime = 6.0f;
        }
        else if (TextCounter == 6)
        {
            textDisplayTime = 4.0f;
        }
        else if (TextCounter == 11)
        {
            textDisplayTime = 6.0f;
        }
    }
}