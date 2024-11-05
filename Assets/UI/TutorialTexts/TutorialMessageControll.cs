using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

public class TutorialMessageControll : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] Messages;
    [SerializeField]
    private SlideUIControll[] MoveWays;
    [SerializeField]
    private SlideUIControll[] ControllerMessages;
    [SerializeField] AudioSource MessageSound;
    float timeCnt;
    public int Message;
    PlayerSeen PS;
    bool AutoCheck = false;

    bool deviceCheck;

    TutorialEnemyController TEC;

    public GameObject Conceal;
    public GameObject Conceal2;

    //ポーズUI
    [SerializeField] GameObject Pause;

    [SerializeField]
    private GameObject[] AutoDoors;

    LevelMeter levelMeter;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = 0f;
        Message = 1;
        Messages[Message-1].state = 1;
        AutoCheck = false;
        deviceCheck = false;

        Conceal.SetActive(true);
        Conceal2.SetActive(true);

        Pause.GetComponent<Image>().enabled = false;

        MessageSound = GetComponent<AudioSource>();

        for(int i = 0; i < AutoDoors.Length; i++)
        {
            AutoDoors[i].GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        PS = GetComponent<PlayerSeen>();

        timeCnt += Time.unscaledDeltaTime;
        if (timeCnt >= 7.0f && Message < 22)
        {
            Messages[Message - 1].state = 0;
            Controller();
            Messages[Message].state = 1;
            Message++;
            timeCnt = 0f;
            MessageSound.PlayOneShot(MessageSound.clip);
        }

        MoveWait();

        if(TutorialEnemyController.ChaseONOFF == true)
        {
            MoveWays[0].state = 1;
            MoveWays[1].state = 0;
        }
        else
        {
            MoveWays[0].state = 0;
        }
    }

    void MoveWait()
    {
        if (Message == 1)
        {
            if(PlayerRun.walk == true)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 2)
        {
            if (PlayerRun.run == true)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 3)
        {
            if (PlayerRun.crouch == true)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 4)
        {
            if (PS.onoff == 1)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 5)
        {
            Conceal.SetActive(false);
            AutoDoors[0].GetComponent<Collider>().enabled = true;
            AutoDoors[1].GetComponent<Collider>().enabled = true;
        }

        if(Message == 6)
        {
            if (AutoCheck)
            {
                timeCnt = 7.0f;
                GameObject.Find("Enemys").transform.Find("Enemy (1)").gameObject.SetActive(true);
                GameObject.Find("Enemys").transform.Find("BossEnemy").gameObject.SetActive(true);
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 7)
        {
            GameObject Enemy = GameObject.Find("Enemy (1)");
            TEC = Enemy.GetComponent<TutorialEnemyController>();
            if (TEC.ONOFF == 0)
            {
                timeCnt = 0f;
            }
            else
            {
                timeCnt = 7.0f;
            }
        }

        if(Message == 8)
        {
            if (EnemyAttack.enemyDeathcnt >= 1)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if(Message == 9)
        {
            Time.timeScale = 0;
            Pause.GetComponent<Image>().enabled = true;
            timeCnt += 5.0f;
            GameObject soundobj = GameObject.Find("SoundVolume");
            levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
            levelMeter.nowdB = 0;
        }

        if (Message == 10 || Message == 11)
        {
            GameObject soundobj = GameObject.Find("SoundVolume");
            levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
            levelMeter.nowdB = 0;
        }

        if (Message == 12)
        {
            Time.timeScale = 1;
            Pause.GetComponent<Image>().enabled = false;
            AutoCheck = false;
        }

        if (Message == 16)
        {
            Conceal2.SetActive(false);

            AutoDoors[2].GetComponent<Collider>().enabled = true;
            AutoDoors[3].GetComponent<Collider>().enabled = true;

            if (AutoCheck)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if (Message == 17)
        {
            GameObject.Find("Enemys").transform.Find("EnemyG").gameObject.SetActive(true);
        }

        if (Message == 18)
        {
            if (EnemyAttack.enemyDeathcnt >= 2)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }

        if(Message == 21)
        {
            if (ItemSeen.FindDoor == true)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }
    }

    void Controller()
    {
        if (deviceCheck)
        {
            if (Message == 1)
            {
                Messages[Message] = ControllerMessages[0];
            }
            else if(Message == 2)
            {
                Messages[Message] = ControllerMessages[1];
            }
            else if (Message == 3)
            {
                Messages[Message] = ControllerMessages[2];
            }
            else if((Message == 4))
            {
                Messages[Message] = ControllerMessages[3];
            }


            if (!deviceCheck && Message == 1)
            {
                Messages[Message].state = 0;
                Messages[Message] = Messages[0];
            }
            else if(!deviceCheck && Message == 1)
            {
                Messages[Message].state = 0;
                Messages[Message] = Messages[1];
            }
            else if (!deviceCheck && Message == 2)
            {
                Messages[Message].state = 0;
                Messages[Message] = Messages[2];
            }
            else if (!deviceCheck && Message == 3)
            {
                Messages[Message].state = 0;
                Messages[Message] = Messages[3];
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutoCheck"))
        {
            AutoCheck = true;
        }
        if (other.CompareTag("Right") || other.CompareTag("Left"))
        {
            MoveWays[0].state = 0;
            MoveWays[1].state = 1;
        }
        else
        {
            MoveWays[1].state = 0;
        }
    }
}
