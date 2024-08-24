using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;

    public CinemachineFreeLook VCamera;

    [SerializeField] Slider MicSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;
    [SerializeField] Slider MouseSlider;

    private void Start()
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        MicSlider.value = Mic.volume;

        MouseSlider.value = VCamera.m_YAxis.m_MaxSpeed;
        VCamera.m_XAxis.m_MaxSpeed = 100;

        //ミキサーのvolumeにスライダーのvolumeを入れている
        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }

    public void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = MicSlider.value;
    }

    public void SetMouse(float level)
    {
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value /5;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value *100;
    }
}
