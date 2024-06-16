using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//壁　通り抜けの設定

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    private float seentime = 0.0f; //経過時間記録用

    PlayerSeen PS;
    GameObject wobj;

  //  EnemySeen ES;

    void Start()
    {
        //プレイヤーが見えていない時
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //通り抜け可能
    }

    void Update()
    {
        wobj = GameObject.Find("Player");
        PS = wobj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        //プレイヤーが見えている時
        if (PS.onoff == 1)
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
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
            /*
            GameObject Chase = GameObject.FindWithTag("Chase");
            EnemyChase EC = Chase.GetComponent<EnemyChase>();

            EC.Wall = true;
            */

            if (EC.ONoff == 0 )//||EFW.ONoff==0 )
            {
                bc.enabled = false;
            }
            else if (EC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
            }
        }
      
        if (other.gameObject.CompareTag("EnemyG"))
        {
           
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemysG EG = eobjG.GetComponent<EnemysG>(); //Enemyに付いているスクリプトを取得
      

            if (EG.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (EG.ONoff == 1)
            {
                bc.enabled = true;
                EG.targetPosition = GetRandomPosition();
            }
        }
       
    }
    private Vector3 GetRandomPosition()
    {
        // ランダムなx, y, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
}


