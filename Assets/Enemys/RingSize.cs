using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSize : MonoBehaviour
{

    float i;
    public MeshRenderer Ring;

    // Start is called before the first frame update
    private void Start()
    {
        Ring = GetComponent<MeshRenderer>();
        StartCoroutine("ScaleUp1");
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        /*
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobj.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
        */
        if (EC.ONoff == 0)
        {
            i = 50;
            Ring.enabled = false;
        }
        if (EC.ONoff == 1)
        {
            Ring.enabled = true;
            //  StartCoroutine("ScaleUp");
        }
        /*
        if (EGC.ONoff == 0)
        {
            i = 50;
            Ring.enabled = false;
        }
        if (EGC.ONoff == 1)
        {
            Ring.enabled = true;
            //  StartCoroutine("ScaleUp");
        }
        */
    }

    IEnumerator ScaleUp1()
    {
        for (i = 50; i < 100; i += 4f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
