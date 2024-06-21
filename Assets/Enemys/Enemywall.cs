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
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
    }

    void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
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
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

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
