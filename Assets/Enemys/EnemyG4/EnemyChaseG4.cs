using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG4 : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    static public float Detection = 7f; //プレイヤーを検知する範囲
    static public float detectionPlayerG4;

    EnemySeen ES;

    static public bool EnemyChaseG04 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG4"); //Playerオブジェクトを探す
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する


        detectionPlayerG4 = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

      
    }
}
