using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Stage1�ŃQ�[���I�[�o�[���̃V�[���J�ڂ��Ǘ�����N���X
/// </summary>
public class Stage1Button : MonoBehaviour
{
    // �{�^���������ꂽ���ɌĂяo����郁�\�b�h
    public void ButtonC()
    {
        // "Stage1" �V�[�������[�h����
        SceneManager.LoadScene("Stage1");
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
            // "Stage1" �V�[�������[�h����
            SceneManager.LoadScene("Stage1");
        }

        // B�L�[�܂��̓Q�[���p�b�h�̃{�^��1�i�ʏ��B�{�^���j�������ꂽ��
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // "StartScene" �V�[�������[�h����i�^�C�g����ʂɑJ�ځj
            SceneManager.LoadScene("StartScene");
        }
    }
}