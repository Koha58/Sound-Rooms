using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRingSize7 : MonoBehaviour
{
    float i;
    public MeshRenderer Ring;
    public GameObject Ring1;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject eobj7 = GameObject.FindWithTag("Enemy7");
        EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>(); //Enemyに付いているスクリプトを取得
        EC7.ONoff = 0;
        StartCoroutine("ScaleUp");
        Ring = GetComponent<MeshRenderer>();
        Ring1.GetComponent<Collider>().enabled = false;//見える（有効）
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj7 = GameObject.FindWithTag("Enemy7");
        EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>(); //Enemyに付いているスクリプトを取得

        if (EC7.ONoff == 0)
        {
            i = 200;
            Ring.enabled = false;
            Ring1.GetComponent<Collider>().enabled = false;//見える（有効）
        }
        if (EC7.ONoff == 1)
        {
            Ring.enabled = true;
            Ring1.GetComponent<Collider>().enabled = true;//見える（有効）
            //  StartCoroutine("ScaleUp");
        }
    }

    IEnumerator ScaleUp()
    {
        for (i = 200; i < 400; i += 5f)
        {
            this.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
