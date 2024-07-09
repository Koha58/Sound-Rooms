using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen2 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj2 = GameObject.FindWithTag("Enemy2");
        EnemyController2 EC2 = eobj2.GetComponent<EnemyController2>(); //Enemyに付いているスクリプトを取得
        if (EC2.ONoff == 0)//見えないとき
        {
            Enemys.enabled = false;//音波非表示→表示
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC2.ONoff == 1)//見えているとき
        {
            Enemys.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
