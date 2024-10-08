using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class GetDeviceCheck : MonoBehaviour
{

    bool micCheck = false;

    bool speakerCheck = false;

    public AudioSource audioSource; // InspectorでAudioSourceを指定

    // Start is called before the first frame update
    void Start()
    {
        micCheck = false;
        speakerCheck = false;
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micCheck = true;
        }

        //CheckAudioDevices();
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
            Debug.Log("マイクが接続されていません");
        }

      
    }

}
