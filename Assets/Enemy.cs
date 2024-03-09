using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float Range = 2f;//自分の行動範囲
    private Vector3 StartPosition;//初期位置
    private Vector3 targetPosition;//目標位置


    public Transform Player;//プレイヤーを参照
    public float Detection = 10f; //プレイヤーを検知する範囲

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {


        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection)
        {
            transform.LookAt(Player.position);//プレイヤーの方向を見る

        }



        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);//目標位置に向かって進む



        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)//目標位置に到達したら新しい目標位置を設定する
        {
            targetPosition = GetRandomPositionlnRange();
        }


        Vector3 GetRandomPositionlnRange()//初期位置からランダムな方向に指定範囲内の距離をランダムに決めて目標を計算する
        {
            Vector3 randomdetection = Random.insideUnitSphere * Range;
            randomdetection.y = StartPosition.y;//Yの高さは初期位置と同じにする
            return randomdetection;
        }



    }
}
