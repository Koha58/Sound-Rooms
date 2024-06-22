using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public  bool Chase=false;
    private float Chaseonoff;
    public bool Wall=false;
    private float Wallonoff;

   // [SerializeField] public GameObject EnemyArea;

    // Start is called before the first frame update
    private  void Start()
    {
        //EnemyArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Chase == true)
        {
            Chaseonoff += Time.deltaTime;
            if (Chaseonoff>=0.5f)
            Chase = false;
        }

        if (Wall == true)
        {
            Wallonoff += Time.deltaTime;
            if (Wallonoff >= 5f)
            {
                Wall = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得

            float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算
            if (detectionPlayer <=5 && EC.ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
            {
                Chase = true;
            }

            // Debug.Log("Play");
           //   Chase = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = true;
           // Debug.Log("Wall");
        }
    }

}
