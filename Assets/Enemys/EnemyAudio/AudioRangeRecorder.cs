using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRangeRecorder : MonoBehaviour
{
    public List<AudioSource> audioSourcesInRange = new List<AudioSource>();
    public AudioSource relayAudioSource; // �����Đ�����ʂ�AudioSource

    private void Start()
    {
        if (relayAudioSource == null)
        {
            // �����ŐV����AudioSource���쐬
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

            // �������o���čĐ�
            PlayAudio(audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null && audioSourcesInRange.Contains(audioSource))
        {
            audioSourcesInRange.Remove(audioSource);

            // �͈͊O�ɂȂ����I�[�f�B�I���~�i�K�v�Ȃ�j
            StopAudio(audioSource);
        }
    }

    private void PlayAudio(AudioSource source)
    {
        if (source.clip != null)
        {
            // `relayAudioSource`�ōĐ�
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
