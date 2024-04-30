using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    AudioClip myclip;
    AudioSource audioSource;
    string micName = "null"; //�}�C�N�f�o�C�X�̖��O
    const int samplingFrequency = 44100; //�T���v�����O���g��
    const int maxTime_s = 10; //�ő�^������[s]

    // Start is called before the first frame update
    void Start()
    {
        //�}�C�N�f�o�C�X��T��
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            micName = null;
        }
    }

    public void StartButton()
    {
        Debug.Log("recording start!");
        //deviceName => "null" �f�t�H���g�̃}�C�N���w��
        //Microphone.Start�Ř^�����J�n�i�}�C�N�f�o�C�X�̖��O�A���[�v���邩�ǂ����A�^������[s], �T���v�����O���g���j
        //�^���f�[�^��AudioClip�ϐ��ɕۑ������
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
