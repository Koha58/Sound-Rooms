using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG3 : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    static public float Detection = 7f; //プレイヤーを検知する範囲
    static public float detectionPlayerG3;

    EnemySeen ES;

    static public bool EnemyChaseG03 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG3"); //Playerオブジェクトを探す
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する


        detectionPlayerG3 = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayerG3 <= Detection && ES.ONoff == 1 && (EnemyCubeG3.EnemybeforG3 == false))//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            EnemyChaseG03 = true;
        }
    }
}
