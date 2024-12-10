using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialMessageControll : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] Messages;
    //[SerializeField]
    //private SlideUIControll[] MoveWays;
    //[SerializeField]
    //private SlideUIControll[] ControllerMessages;
    //[SerializeField] AudioSource MessageSound;
    //float timeCnt;
    public int Message;
    //PlayerSeen PS;
    //bool AutoCheck = false;

    //bool deviceCheck;

    //TutorialEnemyController TEC;

    //public GameObject Conceal;
    //public GameObject Conceal2;

    ////�|�[�YUI
    //[SerializeField] GameObject Pause;

    //[SerializeField]
    //private GameObject[] AutoDoors;

    //LevelMeter levelMeter;

    public ClickToRecordAndVisualize clickToRecordAndVisualize;

    public ObjectPlacer OP;

    public Image LeftButton;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[Message].state = 0;
        }
        Message = 1;
        Messages[Message-1].state = 1;
        clickToRecordAndVisualize.GetComponent<ClickToRecordAndVisualize>();
        OP.GetComponent<ObjectPlacer>();
        LeftButton.GetComponent<Image>().enabled = false;
        //AutoCheck = false;
        //deviceCheck = false;

        //Conceal.SetActive(true);
        //Conceal2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        //{
        //    deviceCheck = true;
        //}
        //else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        //{
        //    deviceCheck = false;
        //}

        //PS = GetComponent<PlayerSeen>();

        //if (timeCnt >= 7.0f && Message < 22)
        //{
        //    Messages[Message - 1].state = 0;
        //    Controller();
        //    Messages[Message].state = 1;
        //    Message++;
        //    timeCnt = 0f;
        //    MessageSound.PlayOneShot(MessageSound.clip);
        //}

        MoveWait();

        Messages[Message - 1].state = 1;

        //if(TutorialEnemyController.ChaseONOFF == true)
        //{
        //    MoveWays[0].state = 1;
        //    MoveWays[1].state = 0;
        //}
        //else
        //{
        //    MoveWays[0].state = 0;
        //}
    }

    void MoveWait()
    {

        if (Message == 1)
        {
            if (clickToRecordAndVisualize.isRecording)
            {
                Messages[0].state = 0;
                Message++;
            }
        }

        if (Message == 2)
        {
            StartCoroutine(FlashButton());
            if (OP.isOnSettingPoint)
            {
                Messages[1].state = 0;
                Message++;
                StopCoroutine(FlashButton());  // �_�ł��~
                SetButtonColor(Color.white);  // �{�^���𔒂ɖ߂�
                LeftButton.GetComponent<Image>().enabled = false;
            }
        }

        if (Message == 3)
        {
            GameObject cobj = GameObject.Find("EnemyAttackArea");
            EnemyAttack EAtack = cobj.GetComponent<EnemyAttack>(); //�t���Ă���X�N���v�g���擾
            if (EAtack.count == 1)
            {
                Messages[2].state = 0;
                Message++;
            }
        }

        if (Message == 4)
        {
            if(OP.Recorder.activeSelf == true)
            {
                Messages[3].state = 0;
                Message++;
            }
        }
    }

    // �{�^����ԂƔ��ɓ_�ł�����R���[�`��
    private IEnumerator FlashButton()
    {
        while (Message == 2)
        {
            LeftButton.GetComponent<Image>().enabled = true;
            SetButtonColor(Color.red);  // �{�^����Ԃ�
            yield return new WaitForSeconds(0.5f);  // 0.5�b�ҋ@
            SetButtonColor(Color.white);  // �{�^���𔒂�
            yield return new WaitForSeconds(0.5f);  // 0.5�b�ҋ@
        }
    }

    // �{�^���̐F��ݒ肷�郁�\�b�h
    private void SetButtonColor(Color color)
    {
        if (LeftButton != null)
        {
            LeftButton.color = color;
        }
    }


    //void Controller()
    //{
    //    if (deviceCheck)
    //    {
    //        if (Message == 1)
    //        {
    //            Messages[Message] = ControllerMessages[1];
    //        }
    //        else if(Message == 2)
    //        {
    //            Messages[Message] = ControllerMessages[2];
    //        }
    //        else if (Message == 3)
    //        {
    //            Messages[Message] = ControllerMessages[3];
    //        }
    //        else if((Message == 4))
    //        {
    //            timeCnt += 5.0f;
    //        }


    //        if (!deviceCheck && Message == 1)
    //        {
    //            Messages[Message].state = 0;
    //            Messages[Message] = Messages[0];
    //        }
    //        else if(!deviceCheck && Message == 1)
    //        {
    //            Messages[Message].state = 0;
    //            Messages[Message] = Messages[1];
    //        }
    //        else if (!deviceCheck && Message == 2)
    //        {
    //            Messages[Message].state = 0;
    //            Messages[Message] = Messages[2];
    //        }
    //        else if (!deviceCheck && Message == 3)
    //        {
    //            Messages[Message].state = 0;
    //            Messages[Message] = Messages[3];
    //        }
    //    }
    //}
}
