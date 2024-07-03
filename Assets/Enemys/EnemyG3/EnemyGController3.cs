using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class EnemyGController3 : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public GameObject Player;//プレイヤーを参照
    Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.07f;//Playerを追いかけるスピード
    private bool EnemyChaseOnOff;//Playerの追跡のONOFF 

    public float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public float SoundTime;//経過時間

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    public Transform _parentTransform;
    public EnemysGChase GChase;
    public GameObject EnemyWall;
    public GameObject EnemyGGetRandomPosition;
    public SkinnedMeshRenderer SkinnedMeshRendererEnemyGBody;
    public SkinnedMeshRenderer SkinnedMeshRendererEnemyGKey;
    public SkinnedMeshRenderer SkinnedMeshRendererEnemyGRing;
    public MeshRenderer Ear;
    public MeshRenderer Eey;

    float TimeWall;
    float PTime;

    // Start is called before the first frame update
    private void Start()
    {
        ONoff = 0;
        EnemyChaseOnOff = false;
        EnemyGGetRandomPosition3 EGRP3 = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition3>();
        // 初期位置をランダムに設定する
        targetPosition = EGRP3.GetRandomPositionG();
        SkinnedMeshRendererEnemyGBody.enabled = false;
        SkinnedMeshRendererEnemyGKey.enabled = false;
        SkinnedMeshRendererEnemyGRing.enabled = false;
        Ear.GetComponent<MeshRenderer>().enabled = false;//見える（有効）
        Eey.GetComponent<MeshRenderer>().enabled = false;//見える（有効）
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する    
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();

        //「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);
        //「走る」のアニメーションを再生する
        animator.SetBool("EnemyRun", false);

        Switch();

        if (EGC.GChase == true)//&& PS.onoff == 1 && ONoff == 1)
        {
            EnemyChaseOnOff = true;
        }

        if (ONoff == 0 || EGC.GChase == false)
        {
            EnemyChaseOnOff = false;
        }

        if (EnemyChaseOnOff == false && EW.Wall == true)
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 4.0f)
            {
                EnemyGGetRandomPosition3 EGRP3 = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition3>();
                targetPosition = EGRP3.GetRandomPositionG();
                TimeWall = 0.0f;
            }
        }

        if (EnemyChaseOnOff == true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            transform.LookAt(Player.transform); //プレイヤーの方向にむく
            transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
            //「歩く」のアニメーションを停止
            animator.SetBool("EnemyWalk", false);
            //「走る」のアニメーションを再生する
            animator.SetBool("EnemyRun", true);
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
                    EnemyGGetRandomPosition3 EGRP3 = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition3>();
                    targetPosition = EGRP3.GetRandomPositionG();
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
            EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();
            float randomTime = Random.Range(10f, 15f);
            SoundTime += Time.deltaTime;
            if (SoundTime >= randomTime)
            {
                SkinnedMeshRendererEnemyGBody.enabled = false;
                SkinnedMeshRendererEnemyGKey.enabled = false;
                SkinnedMeshRendererEnemyGRing.enabled = false;
                Ear.GetComponent<MeshRenderer>().enabled = false;//見える（有効）
                Eey.GetComponent<MeshRenderer>().enabled = false;//見える（有効）
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
                SkinnedMeshRendererEnemyGBody.enabled = true;
                SkinnedMeshRendererEnemyGKey.enabled = true;
                SkinnedMeshRendererEnemyGRing.enabled = true;
                Ear.GetComponent<MeshRenderer>().enabled = true;//見える（有効）
                Eey.GetComponent<MeshRenderer>().enabled = true;//見える（有効）
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

        if (other.gameObject.CompareTag("Wall"))
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 0.5f)
            {
                EnemyGGetRandomPosition3 EGRP3 = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition3>();
                targetPosition = EGRP3.GetRandomPositionG();
                TimeWall = 0.0f;
            }
        }

        if (other.gameObject.CompareTag("InWall"))
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 0.5f)
            {
                EnemyGGetRandomPosition3 EGRP3 = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition3>();
                targetPosition = EGRP3.GetRandomPositionG();
                TimeWall = 0.0f;
            }
        }
    }
}
