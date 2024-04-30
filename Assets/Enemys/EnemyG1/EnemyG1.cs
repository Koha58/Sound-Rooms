using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyG1 : MonoBehaviour
{
    float speed = 1f;
    static public Vector3 targetPosition;

    public Transform Player;//プレイヤーを参照
                            // float Detection = 2f; //プレイヤーを検知する範囲
    float ChaseSpeed = 0.01f;//追いかけるスピード

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    PlayerSeen PS;
    EnemySeen ES;

    static public GameObject EnemyG01;

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置をランダムに設定する
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
                                               // Enemy01.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        /*
       GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
       PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
       GameObject eobj = GameObject.Find("Enemy"); //Playerオブジェクトを探す
       ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
       */

        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        GameObject eobjG1 = GameObject.FindWithTag("EnemyG1"); //Playerオブジェクトを探す
        ES = eobjG1.GetComponent<EnemySeen>(); //付いているスクリプトを取得

        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalkG2", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if ( EnemyChaseG1.EnemyChaseG01touch == true && ES.ONoff == 1 && (EnemyCubeG1.EnemybeforG1 == false))//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
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

            if (EnemyChaseG1.EnemyChaseG01 == true)
            {
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }
        }
        else if (EnemyChaseG1.EnemyChaseG01touch == true || PS.onoff == 0 || EnemyCubeG1.EnemybeforG1 == true)//Playerが検知範囲に入っていないまたはPlayerが見えていない
        {
            // targetPositionに向かって移動する
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }

        // targetPositionに向かって移動する
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // transform.LookAt(targetPosition);

        // targetPositionに到着したら新しいランダムな位置を設定する
        if (transform.position == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                animator.SetBool("EnemyWalkG2", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    targetPosition = GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
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
