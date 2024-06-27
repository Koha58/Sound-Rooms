using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysWalls : MonoBehaviour
{
  /*  BoxCollider bc;

    float WallCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallCount += Time.deltaTime;
        if (WallCount >= 7f)
        {
            bc.enabled = false;
            Wall.enabled = false;
            WallCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得

            if (EC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                Wall.enabled = true;
            }

            if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                Wall.enabled = true;
            }
        }

        if (other.gameObject.CompareTag("EnemyWall"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
            if (EC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
            }
        }

        if (other.gameObject.CompareTag("EnemyGwall"))
        {
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
            if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
            }

        }
    }*/
}