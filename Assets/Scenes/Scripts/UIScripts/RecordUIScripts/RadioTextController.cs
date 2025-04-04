using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �V�[���J�ڗp
using static InputDeviceManager;

/// <summary>
/// GetRecorder�V�[���̃e�L�X�g���Ǘ�����N���X
/// </summary>
public class RadioTextController : MonoBehaviour
{
    [SerializeField] private Text text;  // Text UI �R���|�[�l���g�ւ̎Q��
    [SerializeField] private Text nextText;  // ���̃V�[���֐i�ގw�����o���e�L�X�g
    [SerializeField] private FadeController fadeController;  // �t�F�[�h�p�̃X�N���v�g�Q��
    private int textCounter = 0;  // �e�L�X�g�J�E���^�[

    // �e�e�L�X�g�ɑΉ�����\�����ԁi�b�j
    private static readonly float[] TextDisplayTimes = {
        2.0f,  // �������Ă��邩
        4.0f,  // �m���Ɏ����͐�������
        5.0f,  // �����z��͉�X�̑z���ȏ�ɐi�����߂���
        5.0f,  // �z��͖ڂ����������Ɏ����悢
        6.0f,  // �悢���@�z��̑O�ł͌����ĉ��𗧂ĂĂ͂Ȃ��
        5.0f,  // ���O����������E�o����ɂ͌����K�v����
        4.0f,  // ���������Ȃ��ƂɌ���ꂽ������
        5.0f,  // �z�炪���ƒE�o���t�߂�����Ă���
        4.5f,  // �t�������΁@�z�炪��������ꏊ�ق�
        3.5f,  // ���O���~�������̂��߂��ɂ���Ƃ�������
        5.0f,  // �������悤���Ⴊ�@���̏o���߂��ɂ͋C��t����񂶂Ⴜ
        5.0f,  // ���̉����͓z�炩��g����邽�߂Ɏg���Ƃ悢
        3.5f   // �K�^���F��i�Ō�j
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

    // �f�o�C�X�iXbox/Keyboard�j�̃`�F�b�N�t���O
    private bool deviceCheck;

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
        while (textCounter < Texts.Length)  // �ő�̃e�L�X�g�J�E���^�[�i�z��̒����܂Łj
        {
            // ���݂̃e�L�X�g��ݒ�
            SetText();

            // �w�肵���b���҂�
            yield return new WaitForSeconds(TextDisplayTimes[textCounter]);

            // �J�E���^�[��i�߂�
            textCounter++;

            // 12�Ԗڂ̃e�L�X�g���\�����ꂽ�� nextText ��\��
            if (textCounter == Texts.Length)
            {
                text.enabled = false;  // ���݂̃e�L�X�g���\��
                if(deviceCheck)
                {
                    nextText.text = "X�F���W�I���E��";  // ���̃V�[���ɐi�ރ��b�Z�[�W
                }
                else
                {
                    nextText.text = "E�F���W�I���E��";  // ���̃V�[���ɐi�ރ��b�Z�[�W
                }            
                nextText.enabled = true;  // nextText��\��
            }
        }
    }

    // ���݂̃e�L�X�g��ݒ肷��
    void SetText()
    {
        // TextCounter�ɉ����ĕ\������e�L�X�g��ݒ�
        text.text = Texts[textCounter];
    }

    // E�{�^���������ꂽ�Ƃ��̏���
    void Update()
    {
        // ���ݎg�p���Ă�����̓f�o�C�X���`�F�b�N
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            // Xbox�R���g���[���[���g�p����Ă���ꍇ
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            // �L�[�{�[�h���g�p����Ă���ꍇ
            deviceCheck = false;
        }

        // Xbox�R���g���[���[���g�p����Ă���ꍇ
        if (deviceCheck)
        {
            // X�{�^����������A���̃e�L�X�g���\������Ă���ꍇ
            if (Input.GetKeyDown("joystick button 2") && nextText.enabled)
            {
                // �t�F�[�h�A�E�g���J�n
                StartCoroutine(FadeAndLoadScene());
            }
        }
        // �L�[�{�[�h���g�p����Ă���ꍇ
        else
        {
            // E�L�[��������A���̃e�L�X�g���\������Ă���ꍇ
            if (Input.GetKeyDown(KeyCode.E) && nextText.enabled)
            {
                // �t�F�[�h�A�E�g���J�n
                StartCoroutine(FadeAndLoadScene());
            }
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
