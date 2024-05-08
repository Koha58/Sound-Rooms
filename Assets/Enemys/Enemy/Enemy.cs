using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public Transform Player;//プレイヤーを参照
    public  Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.025f;//Playerを追いかけるスピード

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置をランダムに設定する
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
       
    }

    // Update is called once per frame
    private  void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeen ES = eobj.GetComponent<EnemySeen>(); // EnemySeenに付いているスクリプトを取得
        EnemyCube EC = eobj.GetComponent<EnemyCube>();// EnemyCubeに付いているスクリプトを取得
        EnemyChase EChase = eobj.GetComponent<EnemyChase>();// EnemyChaseに付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);

        if (EnemyChase.EnemyChaseOnOff==true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            if (PS.onoff == 0)
            {
                for (int i = 0; i < 5; i++)//子オブジェクトの数を取得
                {
                    Transform childTransform = PS.parentObject.transform.GetChild(i);
                    PS.childObject = childTransform.gameObject;
                    PS.childObject.GetComponent<Renderer>().enabled = true;//見える
                }
                PS.onoff = 1;  //見えているから1
            }

            if (PS.onoff == 1 && EnemyChase.EnemyChaseOnOff == true )//Playerが見えているとき
            {
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }
           
        }
        else if (EnemyChase.EnemyChaseOnOff == false || PS.onoff == 0 )//Playerが検知範囲に入っていないまたはPlayerが見えていない
        {   
                // targetPositionに向かって移動する
                 transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                 transform.LookAt(targetPosition);
        }

        // targetPositionに到着したら新しいランダムな位置を設定する
        if (transform.position == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                animator.SetBool("EnemyWalk", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    targetPosition = GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }

    private  Vector3 GetRandomPosition()
    {
        // ランダムなx, y, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
}
