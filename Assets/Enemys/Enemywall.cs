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

            if (Enemytouch == 10)
            {
                Enemy.targetPosition = new Vector3(1,0,1);
                Enemytouch = 0;
            }
        }
    }
}
