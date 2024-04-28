using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeG1 : MonoBehaviour
{
    static public bool EnemybeforG1 = false;
    float befortimeG1 = 0;
    public float EnemytouchG1 = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemybeforG1 == true)
        {
            befortimeG1 += Time.deltaTime;
            if (befortimeG1 > 2.0f)
            {
                befortimeG1 = 0;
                EnemybeforG1 = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemySeen ES;
        GameObject eobj = GameObject.FindWithTag("EnemyG1");
        ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
        if (other.gameObject.CompareTag("Wall"))
        {

            if (ES.ONoff == 1)
            {
                EnemybeforG1 = true;
                EnemytouchG1++;

                if (EnemytouchG1 == 1)
                {
                    EnemyG1.targetPosition = EnemyG1.GetRandomPosition();
                    EnemytouchG1 = 0;
                }
            }
        }
    }
}
