using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyG2 : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public Transform Player;//プレイヤーを参照
    public Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.025f;//Playerを追いかけるスピード
    private float Detection = 6f; //プレイヤーを検知する範囲
    private float detectionPlayer;
    private bool EnemyChaseOnOff = false;//Playerの追跡のONOFF 


    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    public GameObject eobj;
    public EnemySeen ES; // EnemySeenに付いているスクリプトを取得


    // Start is called before the first frame update
    void Start()
    {
        // 初期位置をランダムに設定する
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        eobj = GameObject.FindWithTag("EnemyG2");
        ES = eobj.GetComponent<EnemySeen>(); // EnemySeenに付いているスクリプトを取得


        if (ES.ONoff == 0)
        {
            EnemyChaseOnOff = false;
        }


        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalkG2", true);

        if (EnemyChaseOnOff == true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            if (PS.onoff == 0)
            {
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                PS.onoff = 1;  //見えているから1
            }

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ES.ONoff == 1)
            {
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            }

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Playerが検知範囲に入っていないまたはPlayerが見えていない
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

    private Vector3 GetRandomPosition()
    {
        // ランダムなx, y, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

            if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
            {
                EnemyChaseOnOff = true;

            }
        }
    }
}
