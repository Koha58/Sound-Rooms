using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

//�ǁ@�ʂ蔲���̐ݒ�

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    MeshRenderer Wall;

    float WallCount;

    void Start()
    {

    }

    private void Update()
    {
        if (Wall.enabled == true)
        {
            WallCount += Time.deltaTime;
            if (WallCount >= 7.0f)
            {
                bc.enabled = false;
                Wall.enabled = false;
                WallCount = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Visualization"))
        {

            if (other.gameObject.CompareTag("EnemyWall"))
            {
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }
            }

            if (other.gameObject.CompareTag("EnemyGwall"))
            {
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            bc.enabled = false;
            Wall.enabled = false;
        }
    }
}


