using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameScene�ŃQ�[���I�[�o�[���̃V�[���J�ڂ��Ǘ�����N���X
/// </summary>
public class Button : MonoBehaviour
{
    // "GameScene" �ɑJ�ڂ��邽�߂̃{�^������
    public void ButtonC()
    {
        // "GameScene" �V�[�������[�h
        SceneManager.LoadScene("GameScene");
    }

    // "StartScene" �ɑJ�ڂ��邽�߂̃{�^������
    public void TitleButton()
    {
        // "StartScene" �V�[�������[�h
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        // "A"�L�[�܂��̓W���C�X�e�B�b�N�̃{�^��0�������ꂽ�ꍇ�i�ʏ�̃{�^��A�j
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0")) // A
        {
            // "GameScene" �V�[�������[�h
            SceneManager.LoadScene("GameScene");
        }

        // "B"�L�[�܂��̓W���C�X�e�B�b�N�̃{�^��1�������ꂽ�ꍇ�i�ʏ�̃{�^��B�j
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1")) // B
        {
            // "StartScene" �V�[�������[�h
            SceneManager.LoadScene("StartScene");
        }
    }
}