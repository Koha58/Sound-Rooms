using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySeen : MonoBehaviour
{
    public  float SoundTime;
    [SerializeField] public GameObject EnemyBody;

    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;

    // Start is called before the first frame update
    void Start()
    {
        //SkinnedMeshRendererEnemyBody = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得
        if (EC.ONoff == 0)//見えないとき
        {
                EnemyBody.SetActive(false);//音波非表示→表示
           // SkinnedMeshRendererEnemyBody.enabled = false;
        }
         if (EC.ONoff == 1)//見えているとき
         {
                EnemyBody.SetActive(true);//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;

        }
    } 
}
