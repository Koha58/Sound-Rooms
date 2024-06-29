using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRingSize1 : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>(); //Enemyに付いているスクリプトを取得
        EC1.ONoff = 0;
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>(); //Enemyに付いているスクリプトを取得

        if (EC1.ONoff == 0)
        {
            i = 100;
            Ring.enabled = false;
        }
        if (EC1.ONoff == 1)
        {
            Ring.enabled = true;
            //  StartCoroutine("ScaleUp");
        }
    }

    IEnumerator ScaleUp()
    {
        for (i = 150; i < 200; i += 5f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
