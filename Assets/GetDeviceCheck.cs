using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
//using NAudio.CoreAudioApi;

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

        //CheckAudioDevices();
    }

    void CheckAudioDevices()
    {
        //var deviceEnumerator = new MMDeviceEnumerator();
        //var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

        //if (devices.Count > 0)
        //{
        //    Debug.Log("Active audio output devices detected:");
        //    foreach (var device in devices)
        //    {
        //        Debug.Log($"- {device.FriendlyName}");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("No active audio output devices found.");
        //}
    }
}
