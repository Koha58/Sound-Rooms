using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen10 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj10 = GameObject.FindWithTag("Enemy10");
        EnemyController10 EC10 = eobj10.GetComponent<EnemyController10>(); //Enemyに付いているスクリプトを取得
        if (EC10.ONoff == 0)//見えないとき
        {
            Enemys.enabled = false;//音波非表示→表示
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC10.ONoff == 1)//見えているとき
        {
            Enemys.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
