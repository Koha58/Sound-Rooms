using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool GChase = false;
    private float Chaseonoff;
    public bool GWall = false;
    private float Wallonoff;

    // [SerializeField] public GameObject EnemyArea;

    // Start is called before the first frame update
    private void Start()
    {
        //EnemyArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GChase == true)
        {
            Chaseonoff += Time.deltaTime;
            if (Chaseonoff >= 0.5f)
                GChase = false;
        }

        if (GWall == true)
        {
            Wallonoff += Time.deltaTime;
            if (Wallonoff >= 5f)
            {
                GWall = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameObject eobj = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobj.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得

            float detectionPlayer = Vector3.Distance(transform.position, Player.position);//プレイヤーと敵の位置の計算
            if (detectionPlayer <= 5 && EGC.ONoff == 1)//Enemyが可視化状態かつプレイヤーが検知範囲に入ったら
            {
                GChase = true;
            }

            // Debug.Log("Play");
            //   Chase = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            GWall = true;
            // Debug.Log("Wall");
        }
    }
}
