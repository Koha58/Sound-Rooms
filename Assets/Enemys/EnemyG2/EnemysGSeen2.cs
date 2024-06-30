using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGSeen2 : MonoBehaviour
{
    public CapsuleCollider EnemysG;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
        EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //Enemyに付いているスクリプトを取得
        if (EGC2.ONoff == 0)//見えないとき
        {
            EnemysG.enabled = false;//音波非表示→表示
                                    // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EGC2.ONoff == 1)//見えているとき
        {
            EnemysG.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
