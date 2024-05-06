using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    AudioClip myclip;
    public static AudioSource audioSource;
    string micName = "null"; //マイクデバイスの名前
    const int samplingFrequency = 44100; //サンプリング周波数
    const int maxTime_s = 10; //最大録音時間[s]

    public bool DontDestroyEnabled = true;

    public static bool playRecord = false;

    // Start is called before the first frame update
    void Start()
    {
        //マイクデバイスを探す
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micName = null;
        }

        if(DontDestroyEnabled)
        {
            DontDestroyOnLoad(this);
        }

    }

    public void StartButton()
    {
        Debug.Log("recording start!");
        //deviceName => "null" デフォルトのマイクを指定
        //Microphone.Startで録音を開始（マイクデバイスの名前、ループするかどうか、録音時間[s], サンプリング周波数）
        //録音データはAudioClip変数に保存される
        myclip = Microphone.Start(deviceName: micName, loop: false, lengthSec: maxTime_s, frequency: samplingFrequency);

        playRecord = true;
    }

    public void PlayButton()
    {
        Debug.Log("play");
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = myclip;
        audioSource.Play();
    }
}
