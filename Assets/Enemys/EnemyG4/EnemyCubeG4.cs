using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeG4 : MonoBehaviour
{
    static public bool EnemybeforG4 = false;
    float befortimeG4 = 0;
    public float EnemytouchG4 = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemybeforG4 == true)
        {
            befortimeG4 += Time.deltaTime;
            if (befortimeG4 > 2.0f)
            {
                befortimeG4 = 0;
                EnemybeforG4 = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG4");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {

            if (ES.ONoff == 1)
            {
                EnemybeforG4 = true;
                EnemytouchG4++;

                if (EnemytouchG4 == 1)
                {
                    EnemyG4.targetPosition = EnemyG4.GetRandomPosition();
                    EnemytouchG4 = 0;
                }
            }
        }
    }
}
