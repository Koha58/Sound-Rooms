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

    EnemySeen ES;

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
        if (other.gameObject.CompareTag("EnemyParts"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            Enemy E = eobj.GetComponent<Enemy>(); //Enemyに付いているスクリプトを取得
            EnemySeen ES = eobj.GetComponent<EnemySeen>(); //EnemySeenに付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
              
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                E.targetPosition = GetRandomPosition();
              
            }
        }

        if (other.gameObject.CompareTag("EnemyG1"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
            EnemyG1 E = eobjG1.GetComponent<EnemyG1>(); //Enemyに付いているスクリプトを取得
            ES = eobjG1.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                E.targetPosition = GetRandomPosition();
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


