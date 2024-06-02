using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    float speed = 1f;//移動スピード
    public Transform Player;//プレイヤーを参照
    public  Vector3 targetPosition;//Enemyの目的地
    float ChaseSpeed = 0.025f;//Playerを追いかけるスピード
    private float Detection = 5f; //プレイヤーを検知する範囲
    private float detectionPlayer;//プレイヤーと敵の位置の計算を格納する値
    private bool EnemyChaseOnOff = false;//Playerの追跡のONOFF 

    public Animator animator; //アニメーションの格納

    public  float ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public  float SoundTime;//経過時間
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    float Enemystoptime = 0;
    float Enemystoponoff;

    private float TargetTime;

    [SerializeField]
    private AudioClip SoundAttck;     //音を出すのオーディオクリップ
    [SerializeField]
    private  AudioClip footstepSound;     // 足音のオーディオクリップ
    [SerializeField]
    private  AudioSource audioSource;     // オーディオソース
    [SerializeField]
   // private float volume = 50f;          // 音量
   
    
    public bool Soundonoff = true;

    private Vector3 GetRandomPosition()
    {
        // ランダムなx, z座標を生成する
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }

    private void Sound()
    {
        if (ONoff == 0)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = footstepSound;
                audioSource.Play();
            }
        }
        if (ONoff == 1)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    private void AttackSiund()
    {
        if (ONoff == 1)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = SoundAttck;
                audioSource.Play();
            }
        }
        if (ONoff == 0)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GetRandomPosition();// 初期位置をランダムに設定する
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

        //tagが"EnemyParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //タグが"EnemyParts"である子オブジェクトを見えなくする
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Sound();

        AttackSiund();

        float randomTime = Random.Range(5f, 10f);

        TargetTime = randomTime;

        //tagが"EnemyParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//見えないとき
        {
            SoundTime += Time.deltaTime;
            if (SoundTime >= TargetTime)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//音波非表示→表示
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
                Sphere.SetActive(false );//音波表示→非表示
            }
        }

        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms_player = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        // 「歩く」のアニメーションを再生する
        animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
        {
            if (PS.onoff == 0)
            {
                foreach (var playerParts in childTransforms_player)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                PS.onoff = 1;  //見えているから1
            }

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ONoff == 1)
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
            detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

            if (detectionPlayer <= Detection && ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
            {
                EnemyChaseOnOff = true;
            }
        }
    }
}
