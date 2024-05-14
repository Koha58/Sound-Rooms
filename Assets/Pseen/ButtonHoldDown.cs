using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�}�E�X����������(�͈͎w��)
public class ButtonHoldDown : MonoBehaviour
{
    public GameObject[] GaugeArray = new GameObject[13];
    private int Gauge = 13;
    //�������Ɣ��肷��t���[�������Ǘ�
    private const int holdCheck = 60;
    //�L�[�������Ă���t���[�������L�^
    private float holdTime = 0;
    private float recoveryTime = 0;
    public GameObject MaxSound;
    public GameObject[] SoundArray = new GameObject[7];
    private int SoundSize = 7;
    PlayerSeen PS;
    GameObject bobj;
    private int count;
    public int isOn =0;
    public int boundHeight =0;

    // Start is called before the first frame update
    void Start()
    {
        GaugeArray[Gauge-1].GetComponent<Image>().enabled = true;
        SoundArray[SoundSize-1].SetActive(false);
        SoundArray[SoundSize-2].SetActive(false);
        SoundArray[SoundSize-3].SetActive(false);
        SoundArray[SoundSize-4].SetActive(false);
        SoundArray[SoundSize - 5].SetActive(false);
        SoundArray[SoundSize - 6].SetActive(false);
        SoundArray[SoundSize - 7].SetActive(false);
        MaxSound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bobj = GameObject.Find("Player");
        PS = bobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        count += 1;
        if (PS.onoff == 0)
        {
            if (Input.GetMouseButtonDown(0) && Gauge > 0 && holdCheck > holdTime)//�Z����
            {
                holdTime = 0;
                GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
                Gauge--;
                boundHeight = 0;
            }

            if (Input.GetMouseButton(0) && Gauge > 0)//left�L�[�������Ă���Ԃ̎��Ԍv��
            {
                holdTime += Time.deltaTime;
            }

            if (Input.GetMouseButton(0) && Gauge > 0 && holdTime > 1)//left�L�[�������Ă����
            {
                holdTime = 0;
                GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
                Gauge--;

                if (Enemyincrease.enemyDeathcnt == 0)
                {
                    MaxSound.SetActive(true);
                }
                else if(Enemyincrease.enemyDeathcnt == 1)
                {
                    MaxSound.transform.localScale = new Vector3(5.7f, 1.0f, 5.7f);
                    MaxSound.SetActive(true);
                }
                else if (Enemyincrease.enemyDeathcnt == 2)
                {
                    MaxSound.transform.localScale = new Vector3(6.5f, 1.0f, 6.5f);
                    MaxSound.SetActive(true);
                }
                else if (Enemyincrease.enemyDeathcnt == 3)
                {
                    MaxSound.transform.localScale = new Vector3(7.5f, 1.0f, 7.5f);
                    MaxSound.SetActive(true);
                }
                else if (Enemyincrease.enemyDeathcnt == 4)
                {
                    MaxSound.transform.localScale = new Vector3(8.5f, 1.0f, 8.5f);
                    MaxSound.SetActive(true);
                }

                if (count % 1 == 0)
                {
                    if (isOn == 0)
                    {
                        if(boundHeight == 3)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            Gauge += 3;
                        }
                        else if (boundHeight == 4)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 3].GetComponent<Image>().enabled = true;
                            Gauge += 4;
                        }
                        else if (boundHeight == 5)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 3].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 4].GetComponent<Image>().enabled = true;
                            Gauge += 5;
                        }
                        else if (boundHeight == 6)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 3].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 4].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 5].GetComponent<Image>().enabled = true;
                            Gauge += 6;
                        }
                        else if (boundHeight == 7)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 3].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 4].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 5].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 6].GetComponent<Image>().enabled = true;
                            Gauge += 7;
                        }
                        SoundArray[SoundSize - 1].SetActive(true);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(false);
                        isOn = 1;
                        boundHeight = 1;
                    }
                    else if (isOn == 1)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(true);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(false);
                        isOn = 2;
                        boundHeight = 2;
                    }
                    else if (isOn == 2)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(true);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(false);
                        if (Enemyincrease.enemyDeathcnt == 1)
                        {
                            isOn = 3;
                        }
                        else
                        {
                            isOn = 0;
                        }
                        boundHeight = 3;
                    }
                    else if (isOn == 3)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(true);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(false);
                        if (Enemyincrease.enemyDeathcnt >= 2)
                        {
                            isOn = 4;
                        }
                        else
                        {
                            isOn = 0;
                        }
                        boundHeight = 4;
                    }
                    else if (isOn == 4)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(true);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(false);
                        if (Enemyincrease.enemyDeathcnt >= 3)
                        {
                            isOn = 5;
                        }
                        else
                        {
                            isOn = 0;
                        }
                        boundHeight = 5;
                    }
                    else if (isOn == 5)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(true);
                        SoundArray[SoundSize - 7].SetActive(false);
                        if (Enemyincrease.enemyDeathcnt >= 4)
                        {
                            isOn = 6;
                        }
                        else
                        {
                            isOn = 0;
                        }
                        boundHeight = 6;
                    }
                    else if (isOn == 6)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        SoundArray[SoundSize - 4].SetActive(false);
                        SoundArray[SoundSize - 5].SetActive(false);
                        SoundArray[SoundSize - 6].SetActive(false);
                        SoundArray[SoundSize - 7].SetActive(true);
                        if (Enemyincrease.enemyDeathcnt >= 5)
                        {
                            isOn = 0;
                        }
                        boundHeight = 7;
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            MaxSound.SetActive(false);
            SoundArray[SoundSize - 1].SetActive(false);
            SoundArray[SoundSize - 2].SetActive(false);
            SoundArray[SoundSize - 3].SetActive(false);
            SoundArray[SoundSize - 4].SetActive(false);
            SoundArray[SoundSize - 5].SetActive(false);
            SoundArray[SoundSize - 6].SetActive(false);
            SoundArray[SoundSize - 7].SetActive(false);
            isOn = 0;
        }

        recoveryTime += Time.deltaTime;
        
        if(Gauge < 13 && recoveryTime > 60)//60�b���ƂɁu�P�v��
        {
            recoveryTime = 0;
            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
            Gauge++;
        }
    }
}
