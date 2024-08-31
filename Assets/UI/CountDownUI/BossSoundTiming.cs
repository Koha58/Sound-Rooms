using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSoundTiming : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll[] BossCntUI;
    [SerializeField]
    private GameObject[] BossCountDown;
    float count;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < BossCntUI.Length; i++)
        {
            BossCntUI[i].state = 0;
        }

        for (int i = 0; i < BossCountDown.Length; i++)
        {
            BossCountDown[i].GetComponent<Image>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyAttack.BossTiming == true)
        {
            count += Time.deltaTime;
            for(int i = 0; i < BossCntUI.Length; i++)
            {
                BossCntUI[i].state = 1;
            }

            if (count < 1)
            {
                BossCountDown[9].GetComponent<Image>().enabled = true;
            }
            else if(count < 2)
            {
                BossCountDown[9].GetComponent<Image>().enabled = false;
                BossCountDown[8].GetComponent<Image>().enabled = true;
            }
            else if (count < 3)
            {
                BossCountDown[8].GetComponent<Image>().enabled = false;
                BossCountDown[7].GetComponent<Image>().enabled = true;
            }
            else if (count < 4)
            {
                BossCountDown[7].GetComponent<Image>().enabled = false;
                BossCountDown[6].GetComponent<Image>().enabled = true;
            }
            else if (count < 5)
            {
                BossCountDown[6].GetComponent<Image>().enabled = false;
                BossCountDown[5].GetComponent<Image>().enabled = true;
            }
            else if (count < 6)
            {
                BossCountDown[5].GetComponent<Image>().enabled = false;
                BossCountDown[4].GetComponent<Image>().enabled = true;
            }
            else if (count < 7)
            {
                BossCountDown[4].GetComponent<Image>().enabled = false;
                BossCountDown[3].GetComponent<Image>().enabled = true;
            }
            else if (count < 8)
            {
                BossCountDown[3].GetComponent<Image>().enabled = false;
                BossCountDown[2].GetComponent<Image>().enabled = true;
            }
            else if (count < 9)
            {
                BossCountDown[2].GetComponent<Image>().enabled = false;
                BossCountDown[1].GetComponent<Image>().enabled = true;
            }
            else if (count < 10)
            {
                BossCountDown[1].GetComponent<Image>().enabled = false;
                BossCountDown[0].GetComponent<Image>().enabled = true;
            }
            else
            {
                BossCountDown[0].GetComponent<Image>().enabled = false;
                count = 0;
                for (int i = 0; i < BossCntUI.Length; i++)
                {
                    BossCntUI[i].state = 0;
                }
                EnemyAttack.BossTiming = false;
            }
        }
    }
}
