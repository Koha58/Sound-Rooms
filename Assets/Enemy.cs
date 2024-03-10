using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float Range = 20f;//自分の行動範囲
    private Vector3 StartPosition;//初期位置
    private Vector3 targetPosition;//目標位置


    public Transform Player;//プレイヤーを参照
    public float Detection = 100f; //プレイヤーを検知する範囲
    public float ChaseSpeed = 0.01f;//追いかけるスピード

    MeshRenderer MR;
    GameObject Eneny;
    int ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間

    private float SoundTime;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {



        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection)//プレイヤーが検知範囲に入ったら
        {
            transform.LookAt(Player.transform); //プレイヤーの方向にむく
            transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう

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


        if (ONoff == 0)
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {
                MR.enabled = true;
                ONoff = 1;
                SoundTime = 0.0f;
            }

        }
        else if (ONoff == 1)
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                MR.enabled = false;
                ONoff = 0;
                Seetime = 0.0f;
            }
        }
    }
    /*
   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag =="Player" )
       {
           Destroy(other.gameObject);
       }
   }
   */
}
