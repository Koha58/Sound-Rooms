using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���J�n�{�^���̋������Ǘ�����N���X
/// </summary>
public class StartButton : MonoBehaviour
{
    public static GameObject Startbutton; // �Q�[���J�n�{�^����GameObject��ێ�����ÓI�ϐ�
    AudioSource StartSound; // �{�^���������ꂽ�Ƃ��ɖ�T�E���h��AudioSource

    // Start is called before the first frame update
    void Start()
    {
        // �V�[��������uStartButton�v�I�u�W�F�N�g��T���āAStartbutton�ϐ��Ɋi�[
        Startbutton = GameObject.Find("StartButton");

        // �Q�[���J�n�{�^����������Ԃł͔�\���ɂ���
        Startbutton.SetActive(false);

        // AudioSource�R���|�[�l���g���擾���AStartSound�Ɋi�[
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���ɍX�V���鏈���͂Ȃ��̂ŁA��̂܂�
    }

    /// <summary>
    /// �Q�[���J�n�{�^�����N���b�N���ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    /// </summary>
    public void OnStart()
    {
        // �Q�[���J�n�����Đ�
        StartSound.PlayOneShot(StartSound.clip);

        // �V�[�����uGameScene�v�ɑJ�ڂ�����
        SceneManager.LoadScene("GameScene");
    }
}
