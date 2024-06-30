using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRingSize : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject Ring0;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        EC.ONoff = 0;
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        Ring0.GetComponent<Collider>().enabled = false;//見える（有効）
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
     
        if (EC.ONoff == 0)
        {
            i = 100;
            Ring.enabled = false;
            Ring.GetComponent<Collider>().enabled = false;//見える（有効）
        }
        if (EC.ONoff == 1)
        {
            Ring.enabled = true;
            Ring.GetComponent<Collider>().enabled = true;//見える（有効）
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
