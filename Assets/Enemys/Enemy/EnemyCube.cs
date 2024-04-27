using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    static public  bool Enemybefor=false ;
    float befortime = 0;
    public float Enemytouch = 0;

   
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemybefor == true)
        {
            befortime += Time.deltaTime;
            if( befortime >2.0f)
            {
                befortime = 0;
                Enemybefor = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("Enemy");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {
            if (ES.ONoff == 1)
            {
                Enemybefor = true;
                Enemytouch++;

                if (Enemytouch == 1)
                {
                    Enemy.targetPosition = Enemy.GetRandomPosition(); 
                    Enemytouch = 0;
                }
            }
        }
    }
}
