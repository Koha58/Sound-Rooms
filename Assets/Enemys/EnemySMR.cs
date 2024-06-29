using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySMR : MonoBehaviour
{

    SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;
    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRendererEnemyBody = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        if (EC.ONoff == 0)//見えないとき
        {
           SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC.ONoff == 1)//見えているとき
        {
          SkinnedMeshRendererEnemyBody.enabled = true;
        }

     /*   GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        EnemyController EC1 = eobj1.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        if (EC1.ONoff == 0)//見えないとき
        {
            SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC1.ONoff == 1)//見えているとき
        {
            SkinnedMeshRendererEnemyBody.enabled = true;
        }*/
    }
}
