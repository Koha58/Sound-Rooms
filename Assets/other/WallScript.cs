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
            Enemy E = eobj.GetComponent<Enemy>(); //Enemyに付いているスクリプトを取得
            /*
            GameObject Chase = GameObject.FindWithTag("Chase");
            EnemyChase EC = Chase.GetComponent<EnemyChase>();

            EC.Wall = true;
            */

            if (E.ONoff == 0 )//||EFW.ONoff==0 )
            {
                bc.enabled = false;
            }
            else if (E.ONoff == 1)//|| EFW.ONoff == 1)
            {
                /*
                bc.enabled = true;
                if (EF.CurrentPointIndex <= 2) {
                    EF.PatrolPoints[EF.CurrentPointIndex] = EF.PatrolPoints[EF.CurrentPointIndex-1];
                }
                else if(EF.CurrentPointIndex == 0)
                {
                    EF.PatrolPoints[EF.CurrentPointIndex] = EF.PatrolPoints[EF.CurrentPointIndex++];
                }
                */
            }
        }
        
        
        /*
        if (other.gameObject.CompareTag("EnemyFailurework"))
        {

            GameObject eobj = GameObject.FindWithTag("EnemyFailurework");
            EnemyFailurework EFW = eobj.GetComponent<EnemyFailurework>();

            if (EFW.ONoff == 0)//||EFW.ONoff==0 )
            {
                bc.enabled = false;

            }
            else if (EFW.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
                // E.targetPosition = GetRandomPosition();

            }

        }
        */
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
        /*
        if (other.gameObject.CompareTag("EnemyG1"))
        {
           
            GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
            EnemyG1 EG1 = eobjG1.GetComponent<EnemyG1>(); //Enemyに付いているスクリプトを取得
            ES = eobjG1.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EG1.targetPosition = GetRandomPosition();
            }
        }

        if (other.gameObject.CompareTag("EnemyG2"))
        {
           
            GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
            EnemyG2 EG2 = eobjG2.GetComponent<EnemyG2>(); //Enemyに付いているスクリプトを取得
            ES = eobjG2.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EG2.targetPosition = GetRandomPosition();
            }
        }
        if (other.gameObject.CompareTag("EnemyG3"))
        {
           
            GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
            EnemyG3 EG3 = eobjG3.GetComponent<EnemyG3>(); //Enemyに付いているスクリプトを取得
            ES = eobjG3.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EG3.targetPosition = GetRandomPosition();
            }
        }
        if (other.gameObject.CompareTag("EnemyG4"))
        {
           
            GameObject eobjG4 = GameObject.FindWithTag("EnemyG4");
            EnemyG4 EG4 = eobjG4.GetComponent<EnemyG4>(); //Enemyに付いているスクリプトを取得
            ES = eobjG4.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EG4.targetPosition = GetRandomPosition();
            }
        }
        */

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


