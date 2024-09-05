using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] Cursors;
    float timer;
    float count;
    bool Select;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;
    public float volume2;

    public CinemachineFreeLook VCamera;

    [SerializeField] Slider MicSlider;
    [SerializeField] public Slider BGMSlider;
    [SerializeField] Slider SESlider;
    [SerializeField] Slider MouseSlider;

    // Start is called before the first frame update
    void Start()
    {
        timer=0; 
        count = 0;
        Select = false;
        for (int i = 0; i < Cursors.Length; i++)
        {
            Cursors[i].SetActive(false);
        }

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

    // Update is called once per frame
    void Update()
    {
        if (Select == true)
        {
            Select = false;
        }
        if (Input.GetAxis("Vertical") > 0 && count == 0 && Select == false)
        {
            Cursors[0].SetActive(true);
            count += 1;
            Select = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && count == 1 && Select == false)
        {
            Cursors[0].SetActive(false);
            Cursors[1].SetActive(true);
            count += 1;
            Select = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && count == 2 && Select == false)
        {
            Cursors[1].SetActive(false);
            Cursors[2].SetActive(true);
            count += 1;
            Select = true;
        }
        else if (Input.GetAxis("Vertical") > 0 && count == 3 && Select == false)
        {
            Cursors[2].SetActive(false);
            Cursors[3].SetActive(true);
            Select = true;
        }


        if (Input.GetAxis("Vertical") < 0 && count == 0 && Select == false)
        {
            Cursors[0].SetActive(true);
            Cursors[1].SetActive(false);
            Select = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && count == 1 && Select == false)
        {
            Cursors[1].SetActive(true);
            Cursors[2].SetActive(false);
            count -= 1;
            Select = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && count == 2 && Select == false)
        {
            Cursors[2].SetActive(true);
            Cursors[3].SetActive(false);
            count -= 1;
            Select = true;
        }
        else if (Input.GetAxis("Vertical") < 0 && count == 3 && Select == false)
        {
            Cursors[3].SetActive(true);
            count -= 1;
            Select = true;
        }

        if (count == 0) 
        {
            if (Input.GetAxis("Horizontal") >0)
            {
                if (volume < 1)
                {
                    volume +=0.01f;
                }
                MicSlider.value = volume;
                SetMic(volume);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (volume > 0&&volume!=0)
                {
                    volume -= 0.01f;
                }
                MicSlider.value = volume;
                SetMic(volume);
            }
        }
        else if (count == 1) 
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (volume2 < 0)
                {
                    volume2 += 1f;
                }
                BGMSlider.value = volume;
                SetMic(volume);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (volume2 > -80 && volume != 0)
                {
                    volume2 -= 1f;
                }
                BGMSlider.value = volume;
                SetMic(volume);
            }
        }
        else if (count == 2)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (volume2 < 1)
                {
                    volume2 += 1f;
                }
                SESlider.value = volume;
                SetMic(volume);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (volume2 > -80 && volume != 0)
                {
                    volume2 -= 1f;
                }
                SESlider.value = volume;
                SetMic(volume);
            }
        }
        else if (count == 3)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (volume < 1)
                {
                    volume += 0.01f;
                }
                MouseSlider.value = volume;
                SetMic(volume);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (volume > 0 && volume != 0)
                {
                    volume -= 0.01f;
                }
                MouseSlider.value = volume;
                SetMic(volume);
            }
        }

    }


    public void SetBGM(float volume2)
    {
        audioMixer.SetFloat("BGM", volume2);
    }

    public void SetSE(float volume2)
    {
        audioMixer.SetFloat("SE", volume2);
    }

    public void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = MicSlider.value;
    }

    public void SetMouse(float level)
    {
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value * 50;
    }
}
