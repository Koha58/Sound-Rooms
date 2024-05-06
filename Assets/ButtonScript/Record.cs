using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    AudioClip myclip;
    public static AudioSource audioSource;
    string micName = "null"; //�}�C�N�f�o�C�X�̖��O
    const int samplingFrequency = 44100; //�T���v�����O���g��
    const int maxTime_s = 10; //�ő�^������[s]

    public bool DontDestroyEnabled = true;

    public static bool playRecord = false;

    // Start is called before the first frame update
    void Start()
    {
        //�}�C�N�f�o�C�X��T��
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
        //deviceName => "null" �f�t�H���g�̃}�C�N���w��
        //Microphone.Start�Ř^�����J�n�i�}�C�N�f�o�C�X�̖��O�A���[�v���邩�ǂ����A�^������[s], �T���v�����O���g���j
        //�^���f�[�^��AudioClip�ϐ��ɕۑ������
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
