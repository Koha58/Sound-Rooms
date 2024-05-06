using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordCount : MonoBehaviour
{
    public GameObject[] RecCountArray = new GameObject[10];
    private int RecCount = 10;
    public float nowtime = 0;
    private bool OnButton = false;
    public GameObject RecText;
    public GameObject Rerecord;
    public GameObject texPlaySound;

    // Start is called before the first frame update
    void Start()
    {
        RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
        RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;

        Rerecord.GetComponent<Renderer>().enabled = false;
        texPlaySound.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnButton == true)
        {
            nowtime += Time.deltaTime;
            if (nowtime <= 1)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 2)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 3)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 4)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 5)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 6)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 7)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 8)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 9)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = true;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
            }
            else if (nowtime <= 10)
            {
                RecCountArray[RecCount - 1].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 2].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 3].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 4].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 5].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 6].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 7].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 8].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 9].GetComponent<Image>().enabled = false;
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = true;
            }
            else
            {
                RecCountArray[RecCount - 10].GetComponent<Image>().enabled = false;
                OnButton = false;
                nowtime = 0;
                if (Rerecord.GetComponent<Renderer>().enabled == false)
                {
                    Rerecord.GetComponent<Renderer>().enabled = true;
                    texPlaySound.GetComponent<Renderer>().enabled = true;
                    RecText.GetComponent<Renderer>().enabled = false;
                    StartButton.Startbutton.SetActive(true);
                }
            }
        }
    }

    public void OnStart()
    {
        OnButton = true;
    }
}
