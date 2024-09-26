using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;

public class TutorialMessageControll : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] Messages;
    [SerializeField]
    private SlideUIControll[] MoveWays;
    [SerializeField]
    private SlideUIControll[] ControllerMessages;
    [SerializeField]
    private SlideUIControll[] ControllerMoveWays;
    float timeCnt;
    int Message;
    PlayerSeen PS;
    bool SoundCheck = false;
    bool AutoCheck = false;
    float moveTimecnt;
    bool hurryUp;

    bool deviceCheck;

    TutorialEnemyController TEC;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = 0f;
        Message = 1;
        Messages[Message-1].state = 1;
        SoundCheck = false;
        AutoCheck = false;
        moveTimecnt = 0f;
        hurryUp = false;
        deviceCheck = false;
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

        timeCnt += Time.deltaTime;
        if (timeCnt >= 7.0f && Message < 42)
        {
            Messages[Message - 1].state = 0;
            if (deviceCheck)
            {
                if (Message == 1)
                {
                    Messages[Message] = ControllerMessages[0];
                }

                if (!deviceCheck && Message == 1)
                {
                    Messages[Message].state = 0;
                    Messages[Message] = Messages[1];
                }
            }
            Messages[Message].state = 1;
            Message++;
            timeCnt = 0f;
        }

        MoveWait();

        if (moveTimecnt > 5.0f)
        {
            MoveWays[2].state = 0;
        }
    }

    void MoveWait()
    {
        if(Message == 2)
        {
            if(PS.onoff == 1)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }
        if (Message == 7)
        {
            GameObject.Find("Enemys").transform.Find("Enemy (1)").gameObject.SetActive(true);
            GameObject.Find("Enemys").transform.Find("Enemy (2)").gameObject.SetActive(true);
            GameObject.Find("Enemys").transform.Find("BossEnemy").gameObject.SetActive(true);
            if (SoundCheck)
            {
                timeCnt = 7.0f;
                if(!deviceCheck)
                {
                    MoveWays[0].state = 0;
                }
                else
                {
                    ControllerMoveWays[0].state = 0;
                }
            }
            else
            {
                timeCnt = 0f;
                if (!deviceCheck)
                {
                    MoveWays[0].state = 1;
                    MoveWays[1].state = 0;
                    MoveWays[2].state = 0;
                }
                else
                {
                    ControllerMoveWays[0].state = 1;
                    MoveWays[1].state = 0;
                    MoveWays[2].state = 0;
                }
            }
        }
        if (Message == 19)
        {
            if (EnemyAttack.enemyDeathcnt == 1)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }
        if (Message == 20)
        {
            if(!hurryUp)
            {
                timeCnt += 4.0f;
                hurryUp = true;
            }
        }
        else if (Message == 21)
        {
            if (hurryUp)
            {
                timeCnt += 4.0f;
                hurryUp = false;
            }
        }
        if (Message == 22)
        {
            if (PlayerRun.CrouchOn == true)
            {
                if(!deviceCheck)
                {
                    MoveWays[1].state = 0;
                }
                else
                {
                    ControllerMoveWays[1].state = 0;
                }
            }
            else
            {
                if (!deviceCheck)
                {
                    MoveWays[1].state = 1;
                    MoveWays[0].state = 0;
                    MoveWays[2].state = 0;
                }
                else
                {
                    ControllerMoveWays[1].state = 1;
                    MoveWays[0].state = 0;
                    MoveWays[2].state = 0;
                }
            }
        }
        else
        {
            MoveWays[1].state = 0;
        }
        if (Message == 33)
        {
            if (AutoCheck)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }
        if (Message == 35)
        {
            GameObject.Find("Enemys").transform.Find("EnemyG").gameObject.SetActive(true);
            GameObject EnemyG = GameObject.Find("EnemyG");
            TEC = EnemyG.GetComponent<TutorialEnemyController>();
            if(TEC.ONOFF == 0)
            {
                timeCnt = 0f;
            }
            else
            {
                timeCnt = 7.0f;
            }
        }
        if (Message == 39)
        {
            if(ItemSeen.FindDoor == true)
            {
                timeCnt = 7.0f;
            }
            else
            {
                timeCnt = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SoundCheck"))
        {
            SoundCheck = true;
        }
        if (other.CompareTag("AutoCheck"))
        {
            AutoCheck = true;
        }
        if (other.CompareTag("Right") || other.CompareTag("Left"))
        {
            MoveWays[0].state = 0;
            MoveWays[1].state = 0;
            MoveWays[2].state = 1;
            moveTimecnt += Time.deltaTime;
        }
    }
}
