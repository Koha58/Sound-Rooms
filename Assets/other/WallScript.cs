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

    float WallCount;

    void Start()
    {
        //プレイヤーが見えていない時
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //通り抜け可能
        Wall = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

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
  
        if (Wall.enabled == true)
        {
            WallCount += Time.deltaTime;
            if (WallCount >= 7.0f)
            {
                bc.enabled = false;
                Wall.enabled = false;
                WallCount = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            if (PS.onoff == 1)
            {
                Wall.enabled = true;
                bc.enabled = true;
                //Debug.Log("Wall");
            }
        }

        if (other.gameObject.CompareTag("Visualization"))
        {

            if (other.gameObject.CompareTag("EnemyWall"))
            {
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
                if (EC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>(); //Enemyに付いているスクリプトを取得
                if (EC1.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }
            }

            if (other.gameObject.CompareTag("EnemyGwall"))
            {
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
                if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //Enemyに付いているスクリプトを取得
                if (EGC1.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }


                GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
                EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //Enemyに付いているスクリプトを取得

                if (EGC2.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
                EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //Enemyに付いているスクリプトを取得

                if (EGC3.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                   // Debug.Log("1.6");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            bc.enabled = false;
            Wall.enabled = false;
        }
    }
}


