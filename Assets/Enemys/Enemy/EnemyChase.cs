using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
   static public  float Detection = 5f; //プレイヤーを検知する範囲
    static public float detectionPlayer;


    EnemySeen ES;
  

    static public bool EnemyChase00 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject eobj1 = GameObject.FindWithTag("Enemy1"); //Playerオブジェクトを探す
        ES = eobj1.GetComponent<EnemySeen>(); //付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する
      

           detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection && ES.ONoff == 1 && (EnemyCube.Enemybefor == false ))//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
             EnemyChase00 = true ;
        }
    }
}
