using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioCaptureZone : MonoBehaviour
{
    public List<AudioSource> audioSourcesInRange = new List<AudioSource>();

    private void OnTriggerEnter(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSourcesInRange.Add(audioSource);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioSource audioSource = other.GetComponent<AudioSource>();
        if (audioSource != null && audioSourcesInRange.Contains(audioSource))
        {
            audioSourcesInRange.Remove(audioSource);
        }
    }

}
