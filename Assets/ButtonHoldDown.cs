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

    // Start is called before the first frame update
    void Start()
    {
        GaugeArray[Gauge-1].GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Gauge > 0 && holdCheck > holdTime)//短押し
        {
            holdTime = 0;
            GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
            Gauge--;
        }
        
        if(Input.GetMouseButton(0) && Gauge > 0)//leftキーを押している間の時間計測
        {
            holdTime += Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && Gauge > 0 && holdTime > 1)//leftキーを押している間
        {
            holdTime = 0;
            GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
            Gauge--;
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
