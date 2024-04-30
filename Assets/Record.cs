using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    AudioClip myclip;
    AudioSource audioSource;
    string micName = "null"; //マイクデバイスの名前
    const int samplingFrequency = 44100; //サンプリング周波数
    const int maxTime_s = 10; //最大録音時間[s]

    // Start is called before the first frame update
    void Start()
    {
        //マイクデバイスを探す
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micName = null;
        }
    }

    public void StartButton()
    {
        Debug.Log("recording start!");
        //deviceName => "null" デフォルトのマイクを指定
        //Microphone.Startで録音を開始（マイクデバイスの名前、ループするかどうか、録音時間[s], サンプリング周波数）
        //録音データはAudioClip変数に保存される
        myclip = Microphone.Start(deviceName: micName, loop: false, lengthSec: maxTime_s, frequency: samplingFrequency);
        /*
        if (Microphone.IsRecording(deviceName: micName) == true && maxTime_s == 10)
        {
            Debug.Log("recording stoped");
            Microphone.End(deviceName: micName);
        }*/
    }

    public void PlayButton()
    {
        Debug.Log("play");
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = myclip;
        audioSource.Play();
    }
}
