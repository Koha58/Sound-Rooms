using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioClip recordedClip;
    private int sampleRate = 44100;

    void Start()
    {
        // 録音開始
        recordedClip = Microphone.Start(null, true, 10, sampleRate);
    }

    void StopRecording()
    {
        // 録音停止
        Microphone.End(null);

        // 保存処理（例: ファイル出力）
        SaveAudioClip(recordedClip);
    }

    void SaveAudioClip(AudioClip clip)
    {
        //var path = Application.dataPath + "/RecordedAudio.wav";
        //var audioData = WavUtility.FromAudioClip(clip);
        //System.IO.File.WriteAllBytes(path, audioData);
        //Debug.Log("Audio saved at: " + path);
    }
}
