using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 3f;
    static public Vector3 targetPosition;

    public Transform Player;//プレイヤーを参照
    float Detection = 5f; //プレイヤーを検知する範囲
    float ChaseSpeed = 1f;//追いかけるスピード

    public Animator animator;

    // [SerializeField] GameObject Sphere;

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

        // 「歩く」のアニメーションを再生する
        animator.SetBool("WalkEnemys", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算

        if (detectionPlayer <= Detection)//プレイヤーが検知範囲に入ったら
        {
            transform.LookAt(Player.transform); //プレイヤーの方向にむく
            transform.position += transform.forward * ChaseSpeed;//プレイヤーの方向に向かう
        }

        // targetPositionに向かって移動する
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);

        // targetPositionに到着したら新しいランダムな位置を設定する
        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))//「Player」のタグに接触した時
        {
            EnemyParturition.isHidden = false;
        }
    }

    public static Vector3 GetRandomPosition()
    {
        // ランダムなx, y, z座標を生成する
        float randomX = Random.Range(-10f, 10f);
        float randomY = 0.5f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);

        // 生成した座標を返す
        return new Vector3(randomX, randomY, randomZ);
    }
}
