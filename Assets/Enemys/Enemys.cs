using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public Transform Player;//プレイヤーを参照
    Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.025f;//Playerを追いかけるスピード
    private float Detection = 6f; //プレイヤーを検知する範囲
    private float detectionPlayer;//プレイヤーと敵の位置の計算を格納する値
    private bool EnemyChaseOnOff = false;//Playerの追跡のONOFF 

    [SerializeField]
    private GameObject ebiPrefab;      //コピーするプレハブ
    [SerializeField]
    private GameObject DestroyPrefab;  //破壊されるプレハブ
    public bool isHiddens = true;      //
    private bool Clone = false;         //Cloneを生み出すかのONOFF
    static public int enemyDeathcnt = 0;

    public Animator animator; //アニメーションの格納

    //public GameObject eobj;
   // public EnemySeens ES; // EnemySeenに付いているスクリプトを取得


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

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeens ES = eobj.GetComponent<EnemySeens>(); // EnemySeenに付いているスクリプトを取得

       
        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
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

     
        if (isHiddens == false)
        {
            isHiddens = true;
            GameObject go = Instantiate(ebiPrefab);//コピーを生成
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0以上２０以下のランダムの値を生成
            int pz = Random.Range(0, 20);//0以上２０以下のランダムの値を生成
            go.transform.position = new Vector3(px, 0, pz);
            Clone = true;

        }

        if (Clone == true)
        {
            Destroy(DestroyPrefab);
            Clone = false;
            enemyDeathcnt++;
        }

    }

    private Vector3 GetRandomPosition()
    {
        // ランダムなx, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeens ES = eobj.GetComponent<EnemySeens>(); // EnemySeenに付いているスクリプトを取得

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
