using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//マウス長押し挙動(範囲指定)
public class ButtonHoldDown : MonoBehaviour
{
    public GameObject[] GaugeArray = new GameObject[13];
    private int Gauge = 13;
    //長押しと判定するフレーム数を管理
    private const int holdCheck = 60;
    //キーを押しているフレーム数を記録
    private float holdTime = 0;
    private float recoveryTime = 0;
    public GameObject MaxSound;
    public GameObject[] SoundArray = new GameObject[3];
    private int SoundSize = 3;
    PlayerSeen PS;
    GameObject bobj;
    private int count;
    public int isOn =0;
    public int boundHeight =0;
    private float originSizemX = 5.5f;
    private float originSizemZ = 5.5f;
    private float originSizeX = 3.5f;
    private float originSizeZ = 3.5f;
    private float originSize2X = 4.5f;
    private float originSize2Z = 4.5f;
    private float originSize3X = 5.5f;
    private float originSize3Z = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        GaugeArray[Gauge-1].GetComponent<Image>().enabled = true;
        SoundArray[SoundSize-1].SetActive(false);
        SoundArray[SoundSize-2].SetActive(false);
        SoundArray[SoundSize-3].SetActive(false);
        MaxSound.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bobj = GameObject.Find("Player");
        PS = bobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        count += 1;
        if (PS.onoff == 0)
        {
            if (Input.GetMouseButtonDown(0) && Gauge > 0 && holdCheck > holdTime)//短押し
            {
                holdTime = 0;
                GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
                Gauge--;
                boundHeight = 0;
            }

            if (Input.GetMouseButton(0) && Gauge > 0)//leftキーを押している間の時間計測
            {
                holdTime += Time.deltaTime;
            }

            if (Input.GetMouseButton(0) && Gauge > 0 && holdTime > 1)//leftキーを押している間
            {
                holdTime = 0;
                GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
                Gauge--;

                //for (int deathPointer = 0; deathPointer <= Enemyincrease.enemyDeathcnt; deathPointer++)
                //{
                    if (Enemyincrease.DeathRange == 0)
                    {
                        MaxSound.transform.localScale = new Vector3(originSizemX, 1.0f, originSizemZ);
                        MaxSound.SetActive(true);
                        SoundArray[SoundSize - 1].transform.localScale = new Vector3(originSizeX, 1.0f, originSizeZ);
                        SoundArray[SoundSize - 2].transform.localScale = new Vector3(originSize2X, 1.0f, originSize2Z);
                        SoundArray[SoundSize - 3].transform.localScale = new Vector3(originSize3X, 1.0f, originSize3Z);
                    }
                    else
                    {
                        MaxSound.transform.localScale = new Vector3(originSizemX+ Enemyincrease.DeathRange, 1.0f, originSizemZ+ Enemyincrease.DeathRange);
                        MaxSound.SetActive(true);
                        SoundArray[SoundSize - 1].transform.localScale = new Vector3(originSizeX+Enemyincrease.DeathRange,1.0f,originSizeZ+Enemyincrease.DeathRange);
                        SoundArray[SoundSize - 2].transform.localScale = new Vector3(originSize2X + Enemyincrease.DeathRange, 1.0f, originSize2Z + Enemyincrease.DeathRange);
                        SoundArray[SoundSize - 3].transform.localScale = new Vector3(originSize3X + Enemyincrease.DeathRange, 1.0f, originSize3Z + Enemyincrease.DeathRange);
                    }
                //}

                if (count % 1 == 0)
                {
                    if (isOn == 0)
                    {
                        if (boundHeight == 3)
                        {
                            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 1].GetComponent<Image>().enabled = true;
                            GaugeArray[Gauge + 2].GetComponent<Image>().enabled = true;
                            Gauge += 3;
                        }

                        SoundArray[SoundSize - 1].SetActive(true);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(false);
                        isOn = 1;
                        boundHeight = 1;
                    }
                    else if (isOn == 1)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(true);
                        SoundArray[SoundSize - 3].SetActive(false);
                        isOn = 2;
                        boundHeight = 2;
                    }
                    else if (isOn == 2)
                    {
                        SoundArray[SoundSize - 1].SetActive(false);
                        SoundArray[SoundSize - 2].SetActive(false);
                        SoundArray[SoundSize - 3].SetActive(true);
                        isOn = 0;
                        boundHeight = 3;
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
            isOn = 0;
        }

        recoveryTime += Time.deltaTime;
        
        if(Gauge < 13 && recoveryTime > 60)//60秒ごとに「１」回復
        {
            recoveryTime = 0;
            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
            Gauge++;
        }
    }
}
