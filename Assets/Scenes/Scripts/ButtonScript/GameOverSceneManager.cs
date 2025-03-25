using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// �Q�[���I�[�o�[�V�[���Ń��g���C��^�C�g���ɖ߂鏈�����܂Ƃ߂��N���X
/// </summary>
public class GameOverSceneManager : MonoBehaviour
{
    private string previousScene;  // �O�̃V�[�������i�[����ϐ�

    // Start���\�b�h�ŁA�O�̃V�[�����擾���Ă���
    void Start()
    {
        // �v���C���[�v���t�@�����X����O�̃V�[�������擾
        previousScene = PlayerPrefs.GetString("PreviousScene", "StartScene"); // �f�t�H���g�l��"StartScene"�ɐݒ�
    }

    // ���g���C�{�^���������ꂽ�Ƃ��̏���
    public void RetryButton()
    {
        // ���g���C��̃V�[���ɑJ��
        SceneManager.LoadScene(previousScene);
    }

    // �^�C�g����ʂɖ߂鏈��
    public void TitleButton()
    {
        // �^�C�g���V�[���ɑJ��
        SceneManager.LoadScene("StartScene");
    }

    // Update���\�b�h�ŃL�[���͂��󂯎��
    void Update()
    {
        // A�L�[�܂��̓W���C�X�e�B�b�N�̃{�^��0�i�ʏ��A�{�^���j�������ꂽ��
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))
        {
            // ���g���C�{�^�����������Ƃ��̏���
            RetryButton();
        }

        // B�L�[�܂��̓W���C�X�e�B�b�N�̃{�^��1�i�ʏ��B�{�^���j�������ꂽ��
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))
        {
            // �^�C�g����ʂɖ߂�
            TitleButton();
        }
    }
}
