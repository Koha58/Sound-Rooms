using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public GameObject characterPrefab; // 生成するキャラクターのプレハブ
    float speed = 3f;
    static public Vector3 targetPosition;

    public Transform Player;//プレイヤーを参照
    float Detection = 10f; //プレイヤーを検知する範囲
    float ChaseSpeed = 1f;//追いかけるスピード

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    // [SerializeField] GameObject Sphere;

    PlayerSeen PS;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置をランダムに設定する
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算
        /*
        if (PlayerSeen.onoff==1)//プレイヤーが見えている時
        {*/
            if (detectionPlayer <= Detection && EnemySeen.ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
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
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }
       // }

        // targetPositionに向かって移動する
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
            Enemyincrease.isHidden = false;
        }
    }

    public static Vector3 GetRandomPosition()
    {
        // ランダムなx, y, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
}
