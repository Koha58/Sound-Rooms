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
    public static bool TimerON;
    float count;
    bool Select;
    public static bool ON;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] GameObject micObject;
    public float volume;
    public float volume1;
    public float volume2;
    public float volume3;

    public CinemachineFreeLook VCamera;

    [SerializeField] Slider MicSlider;
    [SerializeField] public Slider BGMSlider;
    [SerializeField] Slider SESlider;
    [SerializeField] Slider MouseSlider;

    //操作説明画面
    [SerializeField] GameObject OperationExplanation;
    //説明画面表示後の下の「操作説明」文字
    [SerializeField] GameObject ExplainFont;
    //設定画面
    [SerializeField] GameObject SettingMenu;

    [SerializeField] GameObject SettingBack;

    Image ExplainFontImage;
    Image SettingFontImage;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Select = false;
        for (int i = 0; i < Cursors.Length; i++)
        {
            Cursors[i].SetActive(false);
        }

        SettingBack.SetActive(false);
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
        if(ON==false) 
        {
            for (int i = 0; i < Cursors.Length; i++)
            {
                Cursors[i].SetActive(false);
            }
            SettingBack.SetActive(false);
        }
        else if (ON == true)
        {
            SettingBack.SetActive(true);
            if (Input.GetAxis("Vertical") == 0)
            {
                Select = false;
            }

            if (Input.GetAxis("Vertical") > 0 && count == 0 && Select == false)
            {
                count += 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") > 0 && count == 1 && Select == false)
            {
                count += 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") > 0 && count == 2 && Select == false)
            {
                count += 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") > 0 && count == 3 && Select == false)
            {
                count += 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") > 0 && count == 4 && Select == false)
            {
                Select = true;
            }


            if (Input.GetAxis("Vertical") < 0 && count == 0 && Select == false)
            {
                Select = true;
            }
            else if (Input.GetAxis("Vertical") < 0 && count == 1 && Select == false)
            {
                count -= 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") < 0 && count == 2 && Select == false)
            {
                count -= 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") < 0 && count == 3 && Select == false)
            {
                count -= 1;
                Select = true;
            }
            else if (Input.GetAxis("Vertical") > 0 && count == 4 && Select == false)
            {
                count -= 1;
                Select = true;
            }

            if (count == 0)
            {
                Cursors[0].SetActive(true);
                Cursors[1].SetActive(false);
                Cursors[2].SetActive(false);
                Cursors[3].SetActive(false);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (volume < 1)
                    {
                        volume += 0.01f;
                    }
                    MicSlider.value = volume;
                    SetMic(volume);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    if (volume > 0 && volume != 0)
                    {
                        volume -= 0.01f;
                    }
                    MicSlider.value = volume;
                    SetMic(volume);
                }
            }
            else if (count == 1)
            {
                Cursors[0].SetActive(false);
                Cursors[1].SetActive(true);
                Cursors[2].SetActive(false);
                Cursors[3].SetActive(false);
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (volume2 < 0)
                    {
                        volume2 += 1f;
                    }
                    BGMSlider.value = volume2;
                    SetMic(volume2);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    if (volume2 > -80 && volume != 0)
                    {
                        volume2 -= 1f;
                    }
                    BGMSlider.value = volume2;
                    SetMic(volume2);
                }
            }
            else if (count == 2)
            {
                Cursors[0].SetActive(false);
                Cursors[1].SetActive(false);
                Cursors[2].SetActive(true);
                Cursors[3].SetActive(false);
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (volume3 < 0)
                    {
                        volume3 += 1f;
                    }
                    SESlider.value = volume3;
                    SetMic(volume3);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    if (volume3 > -80 && volume != 0)
                    {
                        volume3 -= 1f;
                    }
                    SESlider.value = volume3;
                    SetMic(volume3);
                }
            }
            else if (count == 3)
            {
                Cursors[0].SetActive(false);
                Cursors[1].SetActive(false);
                Cursors[2].SetActive(false);
                Cursors[3].SetActive(true);

                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (volume1 < 1)
                    {
                        volume1 += 0.01f;
                    }
                    MouseSlider.value = volume1;
                    SetMic(volume1);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    if (volume1 > 0 && volume1 != 0)
                    {
                        volume1 -= 0.01f;
                    }
                    MouseSlider.value = volume1;
                    SetMic(volume1);
                }
            }
            else if (count == 4)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    ExitExplainFontButton();
                    EnterSettingFontButton();
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    ExitSettingFontButton();
                    EnterExplainFontButton();
                }
            }
        }
    }


    public void SetBGM(float volume2)
    {
        audioMixer.SetFloat("BGM", volume2);
    }

    public void SetSE(float volume3)
    {
        audioMixer.SetFloat("SE", volume3);
    }

    public void SetMic(float volume)
    {
        AudioSource Mic = micObject.GetComponent<AudioSource>();
        Mic.volume = MicSlider.value;
    }

    public void SetMouse(float level1)
    {
        VCamera.m_YAxis.m_MaxSpeed = MouseSlider.value / 50;
        VCamera.m_XAxis.m_MaxSpeed = MouseSlider.value * 50;
    }

    public void EnterSettingFontButton()
    {
        if (SettingFontImage.color != new Color32(255, 255, 255, 255))
        {
            SettingFontImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void EnterExplainFontButton()
    {
        if (ExplainFontImage.color != new Color32(255, 255, 255, 255))
        {
            ExplainFontImage.color = new Color32(255, 255, 255, 255);
        }
    }

    public void ExitSettingFontButton()
    {
        if (SettingMenu.GetComponent<Image>().enabled == false)
        {
            SettingFontImage.color = new Color32(255, 255, 255, 45);
        }
    }

    public void ExitExplainFontButton()
    {
        if (OperationExplanation.GetComponent<Image>().enabled == false)
        {
            ExplainFontImage.color = new Color32(255, 255, 255, 45);
        }
    }
}
