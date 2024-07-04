using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class inWallScript : MonoBehaviour
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
                Debug.Log("inWall");
            }
        }
    }
}
