using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    private const int StageIndex0 = 0;  // �`���[�g���A���̃C���f�b�N�X
    private const int StageIndex1 = 1;  // �X�e�[�W1�̃C���f�b�N�X
    private const int StageIndex2 = 2;  // �X�e�[�W2�̃C���f�b�N�X

    [SerializeField] private GameObject[] StageButtons;  // �X�e�[�W�{�^���̔z��
    [SerializeField] private GameObject RightButton, LeftButton, GameStartButton;
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;
    [SerializeField] private AudioSource StartSound;

    private int stage;
    private bool deviceCheck;  // ���̓f�o�C�X��Xbox���ǂ���
    private float moveDelay = 0.5f;  // �{�^���؂�ւ��̒x���i�b�j
    private float lastMoveTime = -1f;  // �Ō�Ƀ{�^�����؂�ւ��������

    void Start()
    {
        UpdateStageSelection();
        stage = StageIndex0;
        SetInitialStage();
        CheckInputDevice();
    }

    void Update()
    {
        // Xbox�̏ꍇ�͉E���{�^�����\���ɂ���
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
            RightButton.SetActive(false);
            LeftButton.SetActive(false);
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        // Xbox�R���g���[���[�̏ꍇ�A�X�e�[�W�I��������
        if (deviceCheck)
        {
            StageSelect();
        }
    }

    void StageSelect()
    {
        // �{�^���؂�ւ��̒x�����Ԃ��Ǘ�
        if (Time.time - lastMoveTime > moveDelay)
        {
            if (Input.GetKeyDown("joystick button 0"))  // �W���C�X�e�B�b�N�̃{�^��A�i����{�^���j
            {
                PlayStartSound();  // �X�^�[�g�����Đ�
                string sceneName = GetSceneNameForStage(stage);
                StartCoroutine(LoadSceneWithDelay(sceneName));
            }
            else if(Input.GetAxisRaw("Vertical") != 0)
            {
                // �R���g���[���[�ŕ����L�[��X�e�B�b�N�̓��͂��󂯕t���A�X�e�[�W��؂�ւ�
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    MoveStageTop();
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    MoveStageDown();
                }
            }
        }
    }

    void MoveStageTop()
    {
        if (stage != StageIndex2)
        {
            stage++;
        }
        else
        {
            stage = StageIndex0;
        }

        UpdateStageSelection();
        lastMoveTime = Time.time;
    }

    void MoveStageDown()
    {
        if (stage != StageIndex0)
        {
            stage--;
        }
        else
        {
            stage = StageIndex2;
        }

        UpdateStageSelection();
        lastMoveTime = Time.time;
    }

    void UpdateStageSelection()
    {
        // �X�e�[�W�̃{�^���F�⓮��A�^�C�g�����X�V
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[i].GetComponent<RawImage>().enabled = false;
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        StageVideos[stage].GetComponent<RawImage>().enabled = true;
        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    void SetInitialStage()
    {
        StageButtons[stage].GetComponent<Image>().enabled = true;
        StageVideos[stage].GetComponent<RawImage>().enabled = true;
        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    void CheckInputDevice()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }
    }

    // �����Đ����郁�\�b�h
    private void PlayStartSound()
    {
        StartSound.PlayOneShot(StartSound.clip);
    }

    private string GetSceneNameForStage(int stageIndex)
    {
        switch (stageIndex)
        {
            case StageIndex0: return "GetRecorder";
            case StageIndex1: return "Stage1";
            case StageIndex2: return "GameScene";
            default: return "GetRecorder";
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(1f); // �����I�������ɃV�[���J��
        SceneManager.LoadScene(sceneName);
    }
}