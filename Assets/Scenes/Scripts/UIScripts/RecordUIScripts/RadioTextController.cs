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

    // �e�e�L�X�g�ɑΉ�����\�����ԁi�b�j
    private static readonly float[] TextDisplayTimes = {
        2.0f,  // �e�L�X�g0
        4.0f,  // �e�L�X�g1
        5.0f,  // �e�L�X�g2
        4.0f,  // �e�L�X�g3
        6.0f,  // �e�L�X�g4
        4.0f,  // �e�L�X�g5
        4.0f,  // �e�L�X�g6
        4.0f,  // �e�L�X�g7
        4.0f,  // �e�L�X�g8
        4.0f,  // �e�L�X�g9
        4.0f,  // �e�L�X�g10
        6.0f,  // �e�L�X�g11
        4.0f   // �e�L�X�g12�i�Ō�j
    };

    // �e�e�L�X�g�̓��e
    private static readonly string[] Texts = {
        "�������Ă��邩",
        "�m���Ɏ����͐�������",
        "�����z��͉�X�̑z���ȏ�ɐi�����߂���",
        "�z��͖ڂ����������Ɏ����悢",
        "�悢���@�z��̑O�ł͌����ĉ��𗧂ĂĂ͂Ȃ��",
        "���O����������E�o����ɂ͌����K�v����",
        "���������Ȃ��ƂɌ���ꂽ������",
        "�z�炪���ƒE�o���t�߂�����Ă���",
        "�t�������΁@�z�炪��������ꏊ�ق�",
        "���O���~�������̂��߂��ɂ���Ƃ�������",
        "�������悤���Ⴊ�@���̏o���߂��ɂ͋C��t����񂶂Ⴜ",
        "���̉����͓z�炩��g����邽�߂Ɏg���Ƃ悢",
        "�K�^���F��"
    };

    // �V�[���J�n����ҋ@���鎞�ԁi�b�j
    private const float InitialWaitTime = 2.0f;

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
        yield return new WaitForSeconds(InitialWaitTime);  // �V�[���J�n����2�b�ҋ@
        StartCoroutine(SwitchText());  // �e�L�X�g�����Ԃɐ؂�ւ���
    }

    // �e�L�X�g�����Ԃ��Ƃɐ؂�ւ���R���[�`��
    IEnumerator SwitchText()
    {
        while (TextCounter < Texts.Length)  // �ő�̃e�L�X�g�J�E���^�[�i�z��̒����܂Łj
        {
            // ���݂̃e�L�X�g��ݒ�
            SetText();

            // �w�肵���b���҂�
            yield return new WaitForSeconds(TextDisplayTimes[TextCounter]);

            // �J�E���^�[��i�߂�
            TextCounter++;

            // 12�Ԗڂ̃e�L�X�g���\�����ꂽ�� nextText ��\��
            if (TextCounter == Texts.Length)
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
        text.text = Texts[TextCounter];
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
