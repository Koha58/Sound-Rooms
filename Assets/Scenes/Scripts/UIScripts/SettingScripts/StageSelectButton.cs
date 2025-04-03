using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// �X�e�[�W�I����ʂ𐧌䂷��N���X
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    // �萔��`
    private const int StageIndex0 = 0;  // �`���[�g���A���̃C���f�b�N�X
    private const int StageIndex1 = 1;  // �X�e�[�W1�̃C���f�b�N�X
    private const int StageIndex2 = 2;  // �X�e�[�W2�̃C���f�b�N�X

    // �X�e�[�W�{�^�����i�[����z��
    [SerializeField] private GameObject[] StageButtons;

    // �X�e�[�W�I����ʂŎg�p����{�^���̎Q��
    [SerializeField] private GameObject RightButton, LeftButton,GameStartButton, BackStartButton;

    // �X�e�[�W���Ƃ̓���ƃ^�C�g��
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;

    // ���ݑI������Ă���X�e�[�W�ԍ�
    private int stage;

    // �X�^�[�g�����i�[����AudioSource
    [SerializeField] AudioSource StartSound;

    // Start is called before the first frame update
    void Start()
    {
        // �����X�e�[�W�ԍ���ݒ� (�ŏ��̃X�e�[�W0)
        stage = StageIndex0;

        // �X�e�[�W�{�^���̍ŏ��̃{�^����\��
        StageButtons[stage].GetComponent<Image>().enabled = true;

        // �X�e�[�W�{�^���̐F�𔖂��ݒ� (�I������Ă��Ȃ��{�^���͔�����)
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        // ���ݑI������Ă���X�e�[�W�̃{�^���F�������\��
        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        // �X�e�[�W�̓�����\���ɂ��A�I�����ꂽ�X�e�[�W�̂ݕ\��
        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }
        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        // �X�e�[�W�̃^�C�g�����\���ɂ��A�I�����ꂽ�^�C�g���̂ݕ\��
        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }
        StageTitles[stage].GetComponent<Image>().enabled = true;

        // AudioSource�R���|�[�l���g���擾
        StartSound = GetComponent<AudioSource>();
    }

    // �X�e�[�W0���I�����ꂽ�Ƃ��̏���
    public void OnStage0Select()
    {
        stage = StageIndex0;
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }
    // �X�e�[�W1���I�����ꂽ�Ƃ��̏���

    public void OnStage1Select()
    {
        stage = StageIndex1;
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

          for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }


    // �X�e�[�W2���I�����ꂽ�Ƃ��̏���
    public void OnStage2Select()
    {
        stage = StageIndex2;
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    public void EnterStage0SelectButton()
    {
        if(stage != StageIndex0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage0SelectButton()
    {
        if (stage != StageIndex0)
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void EnterStage1SelectButton()
    {
        if (stage != StageIndex1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage1SelectButton()
    {
        if (stage != StageIndex1)
        {
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    public void EnterStage2SelectButton()
    {
        if (stage != StageIndex2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitStage2SelectButton()
    {
        if (stage != StageIndex2)
        {
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }
    }

    // �E�{�^���������ꂽ�Ƃ��̏���
    public void OnRightButton()
    {
        if (stage != StageIndex2)
        {
            stage++;
        }
        else
        {
            stage = StageIndex0;
        }
        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    // ���{�^���������ꂽ�Ƃ��̏���
    public void OnLeftButton()
    {
        if(stage != StageIndex0)
        {
            stage--;
        }
        else
        {
            stage = StageIndex2;
        }

        for (int i = 0; i < StageButtons.Length; i++)
        {
            StageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
        }

        StageButtons[stage].GetComponent<Image>().color = new Color32(255, 255, 255, 255);

        for (int i = 0; i < StageVideos.Length; i++)
        {
            StageVideos[i].GetComponent<RawImage>().enabled = false;
        }

        StageVideos[stage].GetComponent<RawImage>().enabled = true;

        for (int i = 0; i < StageTitles.Length; i++)
        {
            StageTitles[i].GetComponent<Image>().enabled = false;
        }

        StageTitles[stage].GetComponent<Image>().enabled = true;
    }

    // �Q�[���J�n���̏���
    public void OnStart()
    {
        // �����Đ����ăV�[���J�ڂ���R���[�`�����J�n
        StartCoroutine(PlayStartSound());
    }

    // �����Đ����郁�\�b�h
    private IEnumerator PlayStartSound()
    {
        StartSound.PlayOneShot(StartSound.clip);
        yield return new WaitForSeconds(StartSound.clip.length);

        // �����I��������ɃV�[����J��
        string sceneName = GetSceneNameForStage(stage);
        SceneManager.LoadScene(sceneName);
    }

    // �X�e�[�W�ԍ��ɑΉ�����V�[�������擾���郁�\�b�h
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
}
