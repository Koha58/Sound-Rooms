using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG1 : MonoBehaviour
{
   public Transform Player;//プレイヤーを参照
   static public float Detection = 7f; //プレイヤーを検知する範囲
   static public float detectionPlayerG1;

    EnemySeen ES;

    static public bool EnemyChaseG01 = false;
    static public bool EnemyChaseG01touch = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG1"); //Playerオブジェクトを探す
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する

        /*
        detectionPlayerG1 = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayerG1 <= Detection && ES.ONoff == 1 && (EnemyCubeG1.EnemybeforG1 == false))//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            EnemyChaseG01 = true;
        }
        
       */
    }
}
