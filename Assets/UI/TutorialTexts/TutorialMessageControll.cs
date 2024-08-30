using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageControll : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] Messages;
    float timeCnt;
    int Message;
    PlayerSeen PS;

    // Start is called before the first frame update
    void Start()
    {
        timeCnt = 0f;
        Message = 1;
        Messages[Message-1].state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PS = GetComponent<PlayerSeen>();

        timeCnt += Time.deltaTime;
        if (timeCnt >= 7.0f && Message < 43)
        {
            Messages[Message - 1].state = 0;
            Messages[Message].state = 1;
            Message++;
            timeCnt = 0f;
        }

        MoveWait();
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
    }
}
