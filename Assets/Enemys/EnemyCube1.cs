using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube1 : MonoBehaviour
{
    static public bool Enemybefor1 = false;
    float befortime1 = 0;
    public float Enemytouch1 = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemybefor1 == true)
        {
            befortime1 += Time.deltaTime;
            if (befortime1 > 1.0f)
            {
                befortime1 = 0;
                Enemybefor1 = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("Enemy1");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {
        
            if (ES.ONoff == 1)
            {
                Enemybefor1 = true;
                Enemytouch1++;

                if (Enemytouch1 == 1)
                {
                    Enemy1.targetPosition =Enemy1.GetRandomPosition();
                    Enemytouch1 = 0;
                }
            }
        }
    }
}
