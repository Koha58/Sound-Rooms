using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioListener))]
public class ZoneAudioRecorder : MonoBehaviour
{
    public AudioMixer mixer;
    public string outputGroupName = "RecordingGroup";

    private FileStream fileStream;
    private const int HEADER_SIZE = 44;

    void Start()
    {
        string path = Application.persistentDataPath + "/ZoneAudio.wav";
        fileStream = new FileStream(path, FileMode.Create);
        WriteWavHeader(fileStream, 0, 0); // 仮のヘッダーを書き込む
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        byte[] byteArray = new byte[data.Length * sizeof(float)];
        Buffer.BlockCopy(data, 0, byteArray, 0, byteArray.Length);
        fileStream.Write(byteArray, 0, byteArray.Length);
    }

    private void OnApplicationQuit()
    {
        fileStream.Seek(0, SeekOrigin.Begin);
        WriteWavHeader(fileStream, fileStream.Length, 44100); // ヘッダーを上書き
        fileStream.Close();
    }

    private void WriteWavHeader(FileStream stream, long dataLength, int sampleRate)
    {
        // WAVフォーマットのヘッダー情報を書き込む処理を記述
    }
}
