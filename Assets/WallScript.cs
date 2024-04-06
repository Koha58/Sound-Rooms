using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//壁　通り抜けの設定

public class WallScript : MonoBehaviour
{
    BoxCollider bc;
    GameObject Wall;

    int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    private float seentime = 0.0f; //経過時間記録用

    EnemySeen ES;

    void Start()
    {
        //プレイヤーが見えていない時
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //通り抜け可能
    }

    void Update()
    {
        //プレイヤーが見えている時
        if (Input.GetMouseButtonUp(0))
        {
            bc.enabled = true;  //通り抜け不可
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したら通り抜け可能にする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                bc.enabled = false; //通り抜け可能
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
               // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                Enemy.targetPosition = Enemy.GetRandomPosition();
               // Debug.Log("!");
            }
        }
    }
}
