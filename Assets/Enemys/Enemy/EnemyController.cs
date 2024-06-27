using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public GameObject Player;//プレイヤーを参照
    public Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.035f;//Playerを追いかけるスピード
    private bool EnemyChaseOnOff = false;//Playerの追跡のONOFF 

    public float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public float SoundTime;//経過時間

    float Enemystoptime = 0;
    float Enemystoponoff;

    Animator animator;

   // public MeshRenderer Enemy;
    [SerializeField] public Transform _parentTransform;

    public GameObject Chase;
    public GameObject EnemyWall;

    public GameObject EnemyGetRandomPosition;

    private bool TouchWall = false;
    private float TouchWallCount;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
        // 初期位置をランダムに設定する
        targetPosition =EGRP. GetRandomPosition();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する    
        // Enemy = GetComponent<MeshRenderer>();
        // Enemy.enabled = true;
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();
    }

    // Update is called once per frame
    private void Update()
    {
        Switch();
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        if (ONoff == 0)
        {
            EnemyChaseOnOff = false;
        }

         //「歩く」のアニメーションを再生する
         animator.SetBool("EnemyWalk", true);

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

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ONoff == 1)
            {
                transform.LookAt(Player.transform); //プレイヤーの方向にむく
                transform.localPosition += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
                //「走る」のアニメーションを再生する
                animator.SetBool("EnemyRun", true);
            }

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Playerが検知範囲に入っていないまたはPlayerが見えていない
        {
            // targetPositionに向かって移動する
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
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

        if(TouchWall==true)
        {
            /* EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
             targetPosition = EGRP.GetRandomPosition();
             TouchWallCount += Time.deltaTime;
             if (TouchWallCount >= 3.0f)
             {
                 TouchWall = false;
             }*/
            TouchWall = false;
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();

        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得

            if (EC.Chase == true && PS.onoff == 1) //EC.Wall == false
            {
                if (EW.Wall == false)
                {
                    EnemyChaseOnOff = true;
                }
            }
            
        }

        if (EnemyChaseOnOff == true && EW.Wall == true)
        {
            EnemyChaseOnOff = false;
        }

        if (EnemyChaseOnOff == false&&EW.Wall==true)
        {
            TouchWall = true;
            EnemyGetRandomPosition ERP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
            targetPosition = ERP.GetRandomPosition();

            TouchWallCount += Time.deltaTime;
            if (TouchWallCount >= 3.0f)
            {
                TouchWall = false;
            }
        }

    }

    private void Switch()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//見えないとき
        {
            float randomTime = Random.Range(7f, 15f);
            SoundTime += Time.deltaTime;
            if (SoundTime >= randomTime)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                //Enemy.enabled = true;
            }
        }

        if (ONoff == 1)//見えているとき
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
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
}
