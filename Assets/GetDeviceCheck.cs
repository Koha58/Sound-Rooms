using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GetDeviceCheck : MonoBehaviour
{

    bool micCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        micCheck = false;
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micCheck = true;
        }
        if (!micCheck)
        {
            Debug.Log("É}ÉCÉNÇ™ê⁄ë±Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    }

}
