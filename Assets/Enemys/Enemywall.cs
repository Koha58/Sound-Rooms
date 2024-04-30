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

            if (Enemytouch == 1)
            {
                Enemy.targetPosition = Enemy.GetRandomPosition();
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

            if (Enemytouch == 1)
            {
                Enemy1.targetPosition = Enemy1.GetRandomPosition();
                Enemytouch = 0;
            }
        }

        if (other.gameObject.CompareTag("EnemyG1"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("EnemyG1");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 1)
            {
                EnemyG1.targetPosition = EnemyG1.GetRandomPosition();
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

            if (Enemytouch == 1)
            {
                EnemyG2.targetPosition = EnemyG2.GetRandomPosition();
                Enemytouch = 0;
            }
        }

        if (other.gameObject.CompareTag("EnemyG3"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("EnemyG3");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 1)
            {
                EnemyG3.targetPosition = EnemyG3.GetRandomPosition();
                Enemytouch = 0;
            }
        }

        if (other.gameObject.CompareTag("EnemyG4"))
        {
            Enemytouch++;

            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("EnemyG4");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (Enemytouch == 1)
            {
                EnemyG4.targetPosition = EnemyG4.GetRandomPosition();
                Enemytouch = 0;
            }
        }
    }
}
