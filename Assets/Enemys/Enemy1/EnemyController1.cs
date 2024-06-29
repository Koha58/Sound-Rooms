using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{

    float speed = 1f;//移動スピード
    public GameObject Player;//プレイヤーを参照
    Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.05f;//Playerを追いかけるスピード
    private bool EnemyChaseOnOff;//Playerの追跡のONOFF 

    public float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public float SoundTime;//経過時間

    float Enemystoptime = 0;
    float Enemystoponoff;

    //Animator animator;

    public Transform _parentTransform;
    public EnemyChase Chase;
    public GameObject EnemyWall;
    public GameObject EnemyGetRandomPosition;
    public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;

    float TimeWall;
    float PTime;

    // Start is called before the first frame update
    private void Start()
    {
        ONoff = 0;
        EnemyChaseOnOff = false;
        EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
        // 初期位置をランダムに設定する
        targetPosition = EGRP.GetRandomPosition();
        SkinnedMeshRendererEnemyBody.enabled = false;
        //animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する    
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();

        //「歩く」のアニメーションを再生する
        //animator.SetBool("EnemyWalk", true);

        Switch();

        if (EC.Chase == true)//&& PS.onoff == 1 && ONoff == 1)
        {
            EnemyChaseOnOff = true;
        }

        if (ONoff == 0 || EC.Chase == false)
        {
            EnemyChaseOnOff = false;
        }

        if (EnemyChaseOnOff == false && EW.Wall == true)
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 4.0f)
            {
                EnemyGetRandomPosition ERP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
                targetPosition = ERP.GetRandomPosition();
                TimeWall = 0.0f;
            }
        }

        if (EnemyChaseOnOff == true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            transform.LookAt(Player.transform); //プレイヤーの方向にむく
            transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
                                                                 //「走る」のアニメーションを再生する
                                                                 //animator.SetBool("EnemyRun", true);

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Playerが検知範囲に入っていないまたはPlayerが見えていない
        {
            // targetPositionに向かって移動する
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }

        // targetPositionに到着したら新しいランダムな位置を設定する
        if (transform.localPosition == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
                    targetPosition = EGRP.GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }

    private void Switch()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//見えないとき
        {
            EnemyChase EC = Chase.GetComponent<EnemyChase>();
            float randomTime = Random.Range(7f, 15f);
            SoundTime += Time.deltaTime;
            if (SoundTime >= randomTime)
            {
                SkinnedMeshRendererEnemyBody.enabled = false;
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
            }
        }

        if (ONoff == 1)//見えているとき
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                SkinnedMeshRendererEnemyBody.enabled = true;
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えなくする
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSeen PS;
            GameObject gobj = GameObject.Find("Player");
            PS = gobj.GetComponent<PlayerSeen>();

            PTime += Time.deltaTime;
            if (PTime > 0.01999f)
            {
                PS.onoff = 1;
                PTime = 0.0f;
            }
        }

        if (other.gameObject.CompareTag("InWall"))
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 0.5f)
            {
                EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
                targetPosition = EGRP.GetRandomPosition();
                TimeWall = 0.0f;
            }
        }
    }
}