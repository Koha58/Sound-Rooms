using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeG2 : MonoBehaviour
{
    static public bool EnemybeforG2 = false;
    float befortimeG2 = 0;
    public float EnemytouchG2 = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemybeforG2 == true)
        {
            befortimeG2 += Time.deltaTime;
            if (befortimeG2 > 2.0f)
            {
                befortimeG2 = 0;
                EnemybeforG2 = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG2");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {

            if (ES.ONoff == 1)
            {
                EnemybeforG2 = true;
                EnemytouchG2++;

                if (EnemytouchG2 == 1)
                {
                    Enemy1.targetPosition = Enemy1.GetRandomPosition();
                    EnemytouchG2 = 0;
                }
            }
        }
    }
}
