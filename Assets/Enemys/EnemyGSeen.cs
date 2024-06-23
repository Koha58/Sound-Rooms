using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGSeen : MonoBehaviour
{
  
    [SerializeField] public GameObject EnemyBody;
    [SerializeField] public GameObject Key;
    [SerializeField] public GameObject Ring;

    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;
    // public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody1; 
    //public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody2;

    // Start is called before the first frame update
    void Start()
    {
        //SkinnedMeshRendererEnemyBody = GetComponent<SkinnedMeshRenderer>();
        //SkinnedMeshRendererEnemyBody1 = GetComponent<SkinnedMeshRenderer>();
        //SkinnedMeshRendererEnemyBody2 = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobj.GetComponent<EnemyGController>(); //Enemyに付いているスクリプトを取得
        if (EGC.ONoff == 0)//見えないとき
        {
            EnemyBody.SetActive(false);//音波非表示→表示
            Key.SetActive(false);//音波非表示→表示
            Ring.SetActive(false);//音波非表示→表示
        }
        if (EGC.ONoff == 1)//見えているとき
        {
            EnemyBody.SetActive(true);//音波表示→非表示
            Key.SetActive(true);//音波表示→非表示
            Ring.SetActive(true);//音波表示→非表示
            //SkinnedMeshRendererEnemyBody.enabled = true;
            //SkinnedMeshRendererEnemyBody1.enabled = true;
            //SkinnedMeshRendererEnemyBody2.enabled = true;
        }
    }
}
