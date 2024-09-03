using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject Cursor;
    public GameObject Cursor1;
    public GameObject Cursor2;
    public GameObject Cursor3;

    //連続入力回避用フラグ
    private bool isSel;
    //選択中番号
    private int selCnt;


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
        Cursor.SetActive(true);
        Cursor1.SetActive(false);
        Cursor2.SetActive(false);
        Cursor3.SetActive(false);

          //変数初期化
        selCnt = 0;

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
        float hori1 = Input.GetAxis("Horizontal");
        float vert1 = Input.GetAxis("Vertical");
        if ((hori1 != 0) || (vert1 != 0))//左スティック
        {
            Debug.Log("stick:" + hori1 + "," + vert1);
        }

        /*
        if (vert1 <= 0.5 && selCnt == 0 && isSel == false)
        {
            isSel = true;
            selCnt += 1;
            if (isSel == true && selCnt == 1)
            {
                Cursor.SetActive(false);
                Cursor1.SetActive(true);
                Cursor2.SetActive(false);
                Cursor3.SetActive(false);
                isSel = false;
            }
        }

        if (vert1 <= 0.5 && selCnt == 1 && isSel == false)
        {
            isSel = true;
            selCnt += 1;
            if (isSel == true && selCnt == 2)
            {
                Cursor.SetActive(false);
                Cursor1.SetActive(false);
                Cursor2.SetActive(true);
                Cursor3.SetActive(false);
                isSel = false;
            }
        }

        if (vert1 <= 0.5 && selCnt == 2 && isSel == false)
        {
            isSel = true;
            selCnt += 1;
            if (isSel == true && selCnt == 3)
            {
                Cursor.SetActive(false);
                Cursor1.SetActive(false);
                Cursor2.SetActive(false);
                Cursor3.SetActive(true);
                isSel = false;
            }
        }*/
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
