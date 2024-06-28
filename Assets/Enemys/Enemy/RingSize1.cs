using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSize1 : MonoBehaviour
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
 
        if (EC.ONoff == 0)
        {
            i = 5;
            Ring.enabled = false;
        }
        if (EC.ONoff == 1)
        {
            Ring.enabled = true;
            //  StartCoroutine("ScaleUp");
        }

        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController EC1 = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得

        if (EC1.ONoff == 0)
        {
            i = 5;
            Ring.enabled = false;
        }
        if (EC1.ONoff == 1)
        {
            Ring.enabled = true;
            //  StartCoroutine("ScaleUp");
        }

    }

    IEnumerator ScaleUp1()
    {
        for (i = 5; i < 30; i += 3f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
