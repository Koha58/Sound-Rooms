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
        
    }
}
