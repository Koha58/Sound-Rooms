using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Microphone.IsRecording(null))
            {
                Microphone.End(null);
                audioSource.Stop();

            }
            else
            {
                audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
                audioSource.loop = true;
                audioSource.Play();

            }
        }
    }
}

   //https://vsq.co.jp/plus/tos/