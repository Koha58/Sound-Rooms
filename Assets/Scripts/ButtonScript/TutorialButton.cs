using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// TutorialScene�ŃQ�[���I�[�o�[���̃V�[���J�ڂ��Ǘ�����N���X
/// </summary>
public class TutorialButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ����ł͏����������͂Ȃ�
    }

    // �`���[�g���A���V�[���ɑJ�ڂ���{�^������
    public void ButtonC()
    {
        // "TutorialScene" �V�[�������[�h����
        SceneManager.LoadScene("TutorialScene");
    }

    // �^�C�g����ʂɖ߂邽�߂̃{�^������
    public void TitleButton()
    {
        // "StartScene" �V�[�������[�h����i�^�C�g����ʂɑJ�ځj
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�{�[�h��Q�[���p�b�h�œ��͂��󂯎�鏈��

        // A�L�[�܂��̓Q�[���p�b�h�̃{�^��0�i�ʏ��A�{�^���j�������ꂽ��
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            // "TutorialScene" �V�[�������[�h����
            SceneManager.LoadScene("TutorialScene");
        }

        // B�L�[�܂��̓Q�[���p�b�h�̃{�^��1�i�ʏ��B�{�^���j�������ꂽ��
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // "StartScene" �V�[�������[�h����i�^�C�g����ʂɑJ�ځj
            SceneManager.LoadScene("StartScene");
        }
    }
}