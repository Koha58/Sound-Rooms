using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageControll : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] Messages;
    [SerializeField]
    private SlideUIControll[] MoveWays;
    float timeCnt;
    int Message;
    PlayerSeen PS;
    bool SoundCheck = false;
    bool AutoCheck = false;
    float moveTimecnt;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = 0f;
        Message = 1;
        Messages[Message-1].state = 1;
        SoundCheck = false;
        AutoCheck = false;
        moveTimecnt = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        PS = GetComponent<PlayerSeen>();

        timeCnt += Time.deltaTime;
        if (timeCnt >= 7.0f && Message < 42)
        {
            Messages[Message - 1].state = 0;
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
            GameObject.Find("Enemys").transform.Find("SoundcheckArea").gameObject.SetActive(true);
            GameObject.Find("Enemys").transform.Find("SoundcheckArea1").gameObject.SetActive(true);
            if (SoundCheck)
            {
                timeCnt = 7.0f;
                MoveWays[0].state = 0;
            }
            else
            {
                timeCnt = 0f;
                MoveWays[0].state = 1;
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
        if (Message == 22)
        {
            if (PlayerRun.CrouchOn == true)
            {
                MoveWays[1].state = 0;
            }
            else
            {
                MoveWays[1].state = 1;
            }
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
        if (Message == 36)
        {
            GameObject.Find("Enemys").transform.Find("EnemyG").gameObject.SetActive(true);
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
            MoveWays[2].state = 1;
            moveTimecnt += Time.deltaTime;
        }
    }
}
