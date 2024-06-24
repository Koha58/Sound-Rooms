using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysWall : MonoBehaviour
{

    BoxCollider bc;

    MeshRenderer Wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        if (EC.ONoff == 0|| EGC.ONoff == 0)//||EFW.ONoff==0 )
        {
           // bc.enabled = false;
            Wall.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

            if (EC.ONoff == 1|| EGC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                Wall.enabled = true;
            }
        }
    }
}
