using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GetDeviceCheck : MonoBehaviour
{

    bool micCheck = false;

    public GameObject MicConnectionBadUI;

    // Start is called before the first frame update
    void Start()
    {
        micCheck = false;
        MicConnectionBadUI.GetComponent<Image>().enabled = false;
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        micCheck = false;
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micCheck = true;
            MicConnectionBadUI.GetComponent<Image>().enabled = false;
        }
        if (!micCheck)
        {
            Debug.Log("É}ÉCÉNÇ™ê⁄ë±Ç≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            MicConnectionBadUI.GetComponent<Image>().enabled = true;
        }
    }

}
