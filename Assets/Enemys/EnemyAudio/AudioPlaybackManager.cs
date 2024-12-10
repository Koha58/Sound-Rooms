using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlaybackManager : MonoBehaviour
{
    public List<AudioSource> audioSourcesInRange = new List<AudioSource>();
    public AudioSource playbackSource; // ���𗬂����߂�AudioSource

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
            playbackSource.clip = source.clip; // �����̃N���b�v��ݒ�
            playbackSource.volume = source.volume; // ���ʂ𓯊�
            playbackSource.pitch = source.pitch;   // �s�b�`�𓯊�
            playbackSource.Play();                // �Đ�
        }
    }

    private void StopAudio(AudioSource source)
    {
        if (playbackSource != null && playbackSource.clip == source.clip)
        {
            playbackSource.Stop(); // �Đ���~
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
