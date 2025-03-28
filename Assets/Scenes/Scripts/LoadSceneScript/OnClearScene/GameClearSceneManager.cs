using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// �Q�[���N���A�V�[���̏������s���N���X
/// </summary>
public class GameClearSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image[] backgroundImages; // �w�i�摜�̔z��
    [SerializeField] private Text typewriterText; // �^�C�v���C�^�[���ʗp�̃e�L�X�g
    [SerializeField] private Text normalText; // ���ʂɕ\������e�L�X�g

    [Header("UI for Each Scene")]
    [SerializeField] private Sprite[][] backgroundSets; // �e�V�[�����Ƃ̔w�i�Z�b�g�i2�����z��j

    [SerializeField] private string[] typewriterTextContent; // �\������e�L�X�g���e�i�^�C�v���C�^�[���ʗp�j
    [SerializeField] private string[] normalTextContent; // �\������e�L�X�g���e�i���ʂɕ\���p�j

    private int currentIndex = 0; // ���݂̃e�L�X�g�̃C���f�b�N�X
    private bool isTyping = false; // ���͒����ǂ���

    // ���͂��I��������ɑJ�ڂ���V�[����
    private string nextScene;

    // �萔��`
    private const int TUTORIAL_SCENE_INDEX = 0; // �`���[�g���A���V�[���̃C���f�b�N�X
    private const int STAGE1_SCENE_INDEX = 1;  // Stage1�V�[���̃C���f�b�N�X
    private const int GAME_SCENE_INDEX = 2;    // GameScene�V�[���̃C���f�b�N�X

    private const string TUTORIAL_SCENE = "TutorialScene"; // �`���[�g���A���V�[���̖��O
    private const string STAGE1_SCENE = "Stage1";         // Stage1�V�[���̖��O
    private const string GAME_SCENE = "GameScene";         // GameScene�V�[���̖��O
    private const string START_SCENE = "StartScene";       // StartScene�V�[���̖��O

    private const string PREVIOUS_SCENE_KEY = "PreviousScene"; // PlayerPrefs�L�[��

    private const float TYPEWRITER_DELAY = 0.3f; // �^�C�v���C�^�[���ʂ̒x������
    private const float SCENE_TRANSITION_DELAY = 1f; // �V�[���J�ڂ܂ł̑ҋ@����

    // �w�i�摜�̃C���f�b�N�X�p�萔
    private const int BACKGROUND_TUTORIAL = TUTORIAL_SCENE_INDEX;
    private const int BACKGROUND_STAGE1 = STAGE1_SCENE_INDEX;
    private const int BACKGROUND_GAME = GAME_SCENE_INDEX;

    // �C���f�b�N�X�̒萔
    private const int NORMAL_TEXT_INDEX = 0;

    void Start()
    {
        // �O�̃V�[������PlayerPrefs����擾����
        string previousScene = PlayerPrefs.GetString(PREVIOUS_SCENE_KEY, START_SCENE);

        // �O�̃V�[�������f�o�b�O���O�ŕ\��
        Debug.Log("Previous Scene: " + previousScene);

        // �V�[���Ɋ�Â��ĕ\������e�L�X�g��ݒ�
        SetTextBasedOnScene(previousScene);

        // UI��؂�ւ���
        SwitchUI(previousScene);

        // nextScene���ݒ肳��Ă��邩�`�F�b�N
        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("���̃V�[�����ݒ肳��Ă��܂���B");
            return; // nextScene���ݒ肳��Ă��Ȃ��ꍇ�͏����𒆒f
        }

        // normalText�ɕ\������e�L�X�g�͂����ɕ\��
        normalText.text = normalTextContent[NORMAL_TEXT_INDEX]; // ���ʂɕ\������e�L�X�g

        // typewriterText�ɂ̓^�C�v���C�^�[���ʂ��g�p���ăe�L�X�g��\��
        StartCoroutine(TypeText());
    }

    // �V�[���Ɋ�Â��ăe�L�X�g��ݒ�
    void SetTextBasedOnScene(string previousScene)
    {
        // �O�̃V�[����"TutorialScene"�������ꍇ�A����̃e�L�X�g��ݒ�
        if (previousScene == TUTORIAL_SCENE)
        {
            normalTextContent = new string[] { "TUTORIAL" }; //�V�[������\��
            typewriterTextContent = new string[] { "CLEAR\nNEXT STAGE1" }; // �^�C�v���C�^�[���ʗp
            nextScene = STAGE1_SCENE; // TutorialScene����Stage1�ɑJ��
        }
        // �O�̃V�[����"Stage1"�������ꍇ�A����̃e�L�X�g��ݒ�
        else if (previousScene == STAGE1_SCENE)
        {
            normalTextContent = new string[] { "STAGE1" }; // �V�[������\��
            typewriterTextContent = new string[] { "CLEAR\nNEXT STAGE2" }; // �^�C�v���C�^�[���ʗp
            nextScene = GAME_SCENE; // Stage1����GameScene�ɑJ��
        }
        // �O�̃V�[����"GameScene"�������ꍇ�A����̃e�L�X�g��ݒ�
        else if (previousScene == GAME_SCENE)
        {
            normalTextContent = new string[] { "STAGE2" }; // �V�[������\��
            typewriterTextContent = new string[] { "CLEAR\nBACK TO TITLE" }; // �^�C�v���C�^�[���ʗp
            nextScene = START_SCENE; // GameScene����StartScene�ɑJ��
        }
        else
        {
            // �\�����Ȃ��V�[���̏ꍇ�̃f�t�H���g����
            typewriterTextContent = new string[] { "�\�����Ȃ��V�[������J�ڂ��܂����B" }; // �^�C�v���C�^�[���ʗp
            nextScene = START_SCENE; // �f�t�H���g��StartScene�ɖ߂�
        }

        // ���ɑJ�ڂ���V�[�������f�o�b�O���O�ŕ\��
        Debug.Log("Next Scene: " + nextScene);
    }

    // UI���V�[���Ɋ�Â��Đ؂�ւ���
    void SwitchUI(string previousScene)
    {
        // �܂��A���ׂĂ̔w�i�摜���\���ɂ���
        foreach (Image img in backgroundImages)
        {
            img.enabled = false; // ��\���ɂ���
        }

        // �V�[���Ɋ�Â��Ĕw�i�ƃe�L�X�g��؂�ւ�
        if (previousScene == TUTORIAL_SCENE)
        {
            // �`���[�g���A���p�̔w�i��\��
            backgroundImages[BACKGROUND_TUTORIAL].enabled = true;
        }
        else if (previousScene == STAGE1_SCENE)
        {
            // Stage1�p�̔w�i��\��
            backgroundImages[BACKGROUND_STAGE1].enabled = true;
        }
        else
        {
            // GameScene�p�̔w�i��\��
            backgroundImages[BACKGROUND_GAME].enabled = true;
        }
    }

    // �^�C�v���C�^�[���ʂ���������R���[�`��
    IEnumerator TypeText()
    {
        // 1�ڂ̃e�L�X�g�i�^�C�v���C�^�[���ʁj
        foreach (char letter in typewriterTextContent[currentIndex].ToCharArray())
        {
            typewriterText.text += letter;
            yield return new WaitForSeconds(TYPEWRITER_DELAY); // ������łԊu
        }

        // �����̓��͂��I��������A���̃V�[���֑J��
        isTyping = false;
        yield return new WaitForSeconds(SCENE_TRANSITION_DELAY); // �����҂��Ă���J��

        // nextScene���ݒ肳��Ă���ꍇ�̂ݑJ��
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene); // �J�ڐ�̃V�[���Ɉړ�
        }
        else
        {
            Debug.LogError("�����ȃV�[�����ł��B");
        }
    }
}
