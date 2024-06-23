using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public  bool Chase=false;
    private float Chaseonoff;
    public float detectionPlayer;


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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得

                Chase = true;
            
            // Debug.Log("Play");
            //Chase = true;
        }
    }

}
