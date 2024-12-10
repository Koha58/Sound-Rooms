using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaybackManager : MonoBehaviour
{
    public List<AudioSource> audioSourcesInRange = new List<AudioSource>();
    public AudioSource playbackSource; // 音を流すためのAudioSource

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null && !audioSourcesInRange.Contains(audioSource))
        {
            audioSourcesInRange.Add(audioSource);
            PlayAudio(audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null && audioSourcesInRange.Contains(audioSource))
        {
            audioSourcesInRange.Remove(audioSource);
            StopAudio(audioSource);
        }
    }

    private void PlayAudio(AudioSource source)
    {
        if (playbackSource != null && source.clip != null)
        {
            playbackSource.clip = source.clip; // 音源のクリップを設定
            playbackSource.volume = source.volume; // 音量を同期
            playbackSource.pitch = source.pitch;   // ピッチを同期
            playbackSource.Play();                // 再生
        }
    }

    private void StopAudio(AudioSource source)
    {
        if (playbackSource != null && playbackSource.clip == source.clip)
        {
            playbackSource.Stop(); // 再生停止
        }
    }
    private void PlayAllAudio()
    {
        foreach (AudioSource source in audioSourcesInRange)
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }
}
