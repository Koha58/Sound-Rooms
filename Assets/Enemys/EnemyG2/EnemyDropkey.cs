using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDropkey : MonoBehaviour
{

    public GameObject KeyPrefab;
    public GameObject DestroyEnemyPrefab;
    static public bool DropG2 = true;
    public bool EnemyDestroy= true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DropG2 == false)
        {
            DropG2 = true;
           
            EnemyDestroy = true;
        }

        if (EnemyDestroy == true)
        {
            Destroy(DestroyEnemyPrefab);
            EnemyDestroy = false;
            Enemyincrease.enemyDeathcnt++;
        }
    }
}
