using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemywall : MonoBehaviour
{
    BoxCollider bc;

   public bool RingOnOff;
    //  EnemySeen ES;

    void Start()
    {
        //プレイヤーが見えていない時
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //通り抜け可能
    }

    void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        bc = GetComponent<BoxCollider>();

        if (EC.ONoff == 0)//||EFW.ONoff==0 )
        {
            bc.enabled = false;
            RingOnOff = false;
        }
        else if (EC.ONoff == 1)//|| EFW.ONoff == 1)
        {
            bc.enabled = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得

            if (RingOnOff == true)
            {
                if (EC.ONoff == 0)//||EFW.ONoff==0 )
                {
                    bc.enabled = false;
                }
                else if (EC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    bc.enabled = true;
                }
            }
        }

    }
}
