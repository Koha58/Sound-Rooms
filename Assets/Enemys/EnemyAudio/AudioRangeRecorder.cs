using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRangeRecorder : MonoBehaviour
{
    public List<AudioSource> audioSourcesInRange = new List<AudioSource>();
    public AudioSource relayAudioSource; // 音を再生する別のAudioSource

    private void Start()
    {
        if (relayAudioSource == null)
        {
            // 自動で新しいAudioSourceを作成
            GameObject relayObject = new GameObject("RelayAudioSource");
            relayAudioSource = relayObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSourcesInRange.Add(audioSource);

            // 音を取り出して再生
            PlayAudio(audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null && audioSourcesInRange.Contains(audioSource))
        {
            audioSourcesInRange.Remove(audioSource);

            // 範囲外になったオーディオを停止（必要なら）
            StopAudio(audioSource);
        }
    }

    private void PlayAudio(AudioSource source)
    {
        if (source.clip != null)
        {
            // `relayAudioSource`で再生
            relayAudioSource.clip = source.clip;
            relayAudioSource.Play();
        }
    }

    private void StopAudio(AudioSource source)
    {
        if (relayAudioSource.isPlaying && relayAudioSource.clip == source.clip)
        {
            relayAudioSource.Stop();
        }
    }
}
