using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool GChase = false;
    private float Chaseonoff;
    public float detectionPlayer;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject eobj = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobj.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得

                GChase = true;
            
            // Debug.Log("Play");
            // GChase = true;
        }
    }
}
