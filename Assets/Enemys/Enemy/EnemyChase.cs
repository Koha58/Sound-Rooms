using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    private float Detection = 6f; //プレイヤーを検知する範囲
    public  bool EnemyChaseOnOff;//Playerの追跡のONOFF 

    public  GameObject eobj;
    public  GameObject eobjEC;
    public  EnemySeen ES;
  

    // Start is called before the first frame update
    private  void Start()
    {
        eobj = GameObject.FindWithTag("Enemy");
       
        ES = eobj.GetComponent<EnemySeen>();//EnemySeenに付いているスクリプトを取得

        EnemyChaseOnOff=false ;
    }

    // Update is called once per frame
    private void Update()
    {
        ES = eobj.GetComponent<EnemySeen>();//EnemySeenに付いているスクリプトを取得

        float detectionPlayer; detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            EnemyChaseOnOff = true;
        }
    }
}
