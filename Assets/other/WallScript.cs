using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

//壁　通り抜けの設定

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    LevelMeter levelMeter;

    MeshRenderer Wall;

    void Start()
    {
        //プレイヤーが見えていない時
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //通り抜け可能
        Wall = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //プレイヤーが見えている時
        if (levelMeter.nowdB > 0.0f)
        {
            bc.enabled = true;  //通り抜け不可
            onoff = 1;  //見えているから1
        }

        //プレイヤーが見えていないとき
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
                bc.enabled = false; //通り抜け可能
                onoff = 0;  //見えていないから0
            }
        }
        
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
     
        if (EC.ONoff == 0)//||EFW.ONoff==0 )
        {
           bc.enabled = false;
            Wall.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
            /*
                bc.enabled = false;
                // RingOnOff = false;
                Wall.enabled = false;
           */

            if (EC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
            }
        }

        /*
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject soundobj = GameObject.Find("SoundVolume");
            levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

            //プレイヤーが見えている時
            if (levelMeter.nowdB > 0.0f)
            {
                bc.enabled = true;  //通り抜け不可
                onoff = 1;  //見えているから1
            }

            //プレイヤーが見えていないとき
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
                {
                    bc.enabled = false; //通り抜け可能
                    onoff = 0;  //見えていないから0
                }
            }
        }*/
    }
}


