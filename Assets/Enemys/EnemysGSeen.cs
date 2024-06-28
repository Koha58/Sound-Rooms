using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGSeen : MonoBehaviour
{
    public CapsuleCollider EnemysG;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
        if (EGC.ONoff == 0)//見えないとき
        {
            EnemysG.enabled = false;//音波非表示→表示
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC.ONoff == 1)//見えているとき
        {
            EnemysG.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }

        GameObject eobjG1 = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC1 = eobjG.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
        if (EGC1.ONoff == 0)//見えないとき
        {
            EnemysG.enabled = false;//音波非表示→表示
                                    // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC1.ONoff == 1)//見えているとき
        {
            EnemysG.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
