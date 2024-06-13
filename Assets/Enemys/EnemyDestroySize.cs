using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroySize : MonoBehaviour
{
    float i;
     MeshRenderer Ring;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ScaleUp");
        Ring=GetComponent<MeshRenderer>();
     
    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("Enemy");
        Enemy E = eobj.GetComponent<Enemy>(); //Enemyに付いているスクリプトを取得
        if (E.ONoff == 0)
        {
           // Ring.enabled = false;
        }
        if (E.ONoff == 1)
        {
            StartCoroutine("ScaleUp");
           // Ring.enabled = true;
        }

    }

    IEnumerator ScaleUp()
    {
        for (i = 50; i < 200; i += 1f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
