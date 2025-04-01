using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    // �萔��`
    private const int StageIndex0 = 0;  // �`���[�g���A���̃C���f�b�N�X
    private const int StageIndex1 = 1;  // �X�e�[�W1�̃C���f�b�N�X
    private const int StageIndex2 = 2;  // �X�e�[�W2�̃C���f�b�N�X

    // �X�e�[�W�{�^�����i�[����z��
    [SerializeField] private GameObject[] StageButtons;

    // �X�e�[�W�I����ʂŎg�p����{�^���̎Q��
    [SerializeField] private GameObject RightButton, LeftButton, GameStartButton, BackStartButton;

    // �X�e�[�W���Ƃ̓���ƃ^�C�g��
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      StageSelect();
    }

    // �X�e�[�W�I���̏���
    void StageSelect()
    {
        // ���ݑI������Ă���Q�[���I�u�W�F�N�g���擾
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == StageButtons[0])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = true;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = false;

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageTitles[0].GetComponent<Image>().enabled = true;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = false;

            }
        }
        else if (selectedGameObject == StageButtons[1])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = true;
            StageVideos[2].GetComponent<RawImage>().enabled = false;

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = true;
                StageVideos[2].GetComponent<RawImage>().enabled = false;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = true;
                StageTitles[2].GetComponent<Image>().enabled = false;
            }

        }
        else if (selectedGameObject == StageButtons[2])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = true;
            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = false;
                StageVideos[2].GetComponent<RawImage>().enabled = true;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = true;
            }
        }
        else if (selectedGameObject == GameStartButton)
        {

        }
        else if (selectedGameObject == null)
        {
            // �{�^�����I������Ă��Ȃ��ꍇ�A�ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
            EventSystem.current.SetSelectedGameObject(StageButtons[0]);
        }
    }
}