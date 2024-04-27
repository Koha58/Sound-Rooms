using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemywall : MonoBehaviour
{
    public float Enemytouch=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 3)
            {
                Enemy.targetPosition = new Vector3(5,0,5);
                Enemytouch = 0;
            }
        }

        if (other.gameObject.CompareTag("Enemy1"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("Enemy1");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 3)
            {
                Enemy1.targetPosition = new Vector3(2, 0, 2);
                Enemytouch = 0;
            }
        }

        if (other.gameObject.CompareTag("EnemyG2"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("EnemyG2");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 3)
            {
                EnemyG2.targetPosition = new Vector3(2, 0, 2);
                Enemytouch = 0;
            }
        }
    }
}
