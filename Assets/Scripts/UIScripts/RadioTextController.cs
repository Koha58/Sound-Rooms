using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �V�[���J�ڗp

/// <summary>
/// GetRecorder�V�[���̃e�L�X�g���Ǘ�����N���X
/// </summary>
public class RadioTextController : MonoBehaviour
{
    [SerializeField] private Text text;  // Text UI �R���|�[�l���g�ւ̎Q��
    [SerializeField] private Text nextText;  // ���̃V�[���֐i�ގw�����o���e�L�X�g
    [SerializeField] private FadeController fadeController;  // �t�F�[�h�p�̃X�N���v�g�Q��
    private int TextCounter = 0;  // �e�L�X�g�J�E���^�[
    private float textDisplayTime = 4f;  // �e�e�L�X�g��\�����鎞�ԁi�b�j

    // Start is called before the first frame update
    void Start()
    {
        // 2�b��ɍŏ��̃e�L�X�g��\��
        StartCoroutine(StartTextSequence());
        nextText.enabled = false;  // ���̃V�[���ւ̐i�s���b�Z�[�W�͏�����ԂŔ�\��
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

            // 12�Ԗڂ̃e�L�X�g���\�����ꂽ�� nextText ��\��
            if (TextCounter == 13)
            {
                text.enabled = false;  // ���݂̃e�L�X�g���\��
                nextText.text = "E�F���W�I���E��";  // ���̃V�[���ɐi�ރ��b�Z�[�W
                nextText.enabled = true;  // nextText��\��
            }
        }
    }

    // ���݂̃e�L�X�g��ݒ肷��
    void SetText()
    {
        // TextCounter�ɉ����ĕ\������e�L�X�g��ݒ�
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
        else if (TextCounter == 13)
        {
            text.text = "";  // �Ō�̃e�L�X�g��ɋ󕶎���ݒ�
        }
    }

    // Text���Ƃ̕\�����Ԃ𒲐�����
    void ChangeTiming()
    {
        // TextCounter�ɉ����ĕ\�����Ԃ𒲐�
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

    // E�{�^���������ꂽ�Ƃ��̏���
    void Update()
    {
        // E�L�[��������A���̃e�L�X�g���\������Ă���ꍇ
        if (Input.GetKeyDown(KeyCode.E) && nextText.enabled)
        {
            // �t�F�[�h�A�E�g���J�n
            StartCoroutine(FadeAndLoadScene());
        }
    }

    // �t�F�[�h�A�E�g�ƃV�[���J��
    IEnumerator FadeAndLoadScene()
    {
        // �t�F�[�h�A�E�g���J�n
        yield return StartCoroutine(fadeController.FadeOut());

        // �t�F�[�h�A�E�g������������V�[���J��
        SceneManager.LoadScene("TutorialScene");  // �V�[�����͓K�X�ύX
    }
}