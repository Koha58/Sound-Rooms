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
    float y;
    float speed = 10f;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;

    public CinemachineFreeLook VCamera;

    [SerializeField] Slider MicSlider;
    [SerializeField] public Slider BGMSlider;
    [SerializeField] Slider SESlider;
    [SerializeField] Slider MouseSlider;

    // Start is called before the first frame update
    void Start()
    {
        Cursors[0].SetActive(true);

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
        
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            int i=+1;
            if (Cursors[i] == Cursors[0])
            {
                Cursors[0].SetActive(true);
            }
            if (Cursors[i] == Cursors[1])
            {
                Cursors[1].SetActive(true);
            }
            if (Cursors[i] == Cursors[2])
            {
                Cursors[2].SetActive(true);
            }
            if (Cursors[i] == Cursors[3])
            {
                Cursors[3].SetActive(true);
            }
        }
       
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
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value * 50;
    }
}
