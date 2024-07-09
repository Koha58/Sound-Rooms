using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysSeen7 : MonoBehaviour
{
    public CapsuleCollider Enemys;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj7 = GameObject.FindWithTag("Enemy7");
        EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>(); //Enemyに付いているスクリプトを取得
        if (EC7.ONoff == 0)//見えないとき
        {
            Enemys.enabled = false;//音波非表示→表示
                                   // SkinnedMeshRendererEnemyBody.enabled = false;
        }
        if (EC7.ONoff == 1)//見えているとき
        {
            Enemys.enabled = true;//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    }
}
