using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeG3 : MonoBehaviour
{
    static public bool EnemybeforG3 = false;
    float befortimeG3 = 0;
    public float EnemytouchG3 = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemybeforG3 == true)
        {
            befortimeG3 += Time.deltaTime;
            if (befortimeG3 > 2.0f)
            {
                befortimeG3 = 0;
                EnemybeforG3= false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG3");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {

            if (ES.ONoff == 1)
            {
                EnemybeforG3 = true;
                EnemytouchG3++;

                if (EnemytouchG3 == 1)
                {
                    EnemyG3.targetPosition = EnemyG3.GetRandomPosition();
                    EnemytouchG3 = 0;
                }
            }
        }
    }
}
