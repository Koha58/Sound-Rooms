using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncrease : MonoBehaviour
{
    public GameObject ebiPrefab1;      //コピーするプレハブ
    public GameObject ebiPrefab2;      //コピーするプレハブ
    public GameObject DestroyPrefab;  //破壊されるプレハブ
    public bool isHidden = true;      //
    private bool Clone = false;         //Cloneを生み出すかのONOFF
    static public int enemyDeathcnt = 0;  //Enemyが死んだ数
    public static float DeathRange = 0f;//Enemyが死ぬと広がる範囲


    public GameObject enemyPrefab; // 復活させる敵のプレハブ
    public Transform respawnPoint; // 復活させる位置
    public float respawnDistance = 20f; // 復活までの距離

    private GameObject enemyInstance; // 生成された敵のインスタンス

    // 敵が倒された時に呼ばれるメソッド
    public void EnemyDied()
    {
        // 一定距離以内で敵を復活させる
        if (Vector3.Distance(respawnPoint.position, transform.position) <= respawnDistance)
        {
            // 復活位置に敵を生成する
            enemyInstance = Instantiate(enemyPrefab, respawnPoint.position, respawnPoint.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

        if (isHidden == false)
        {
            isHidden = true;
            EnemyDied();
            /*
            GameObject go1 = Instantiate(ebiPrefab1);//コピーを生成
            GameObject go2 = Instantiate(ebiPrefab2);//コピーを生成
                                                     //Debug.Log(go);
            float px1 = Random.Range(-15f, 15f); ;//0以上２０以下のランダムの値を生成
            float pz1 = Random.Range(-15f, 15f); ;//0以上２０以下のランダムの値を生成
            float px2 = Random.Range(-15f, 15f); ;//0以上２０以下のランダムの値を生成
            float pz2 = Random.Range(-15f, 15f); ;//0以上２０以下のランダムの値を生成
            go1.transform.position = new Vector3(px1, 0, pz1);
            go2.transform.position = new Vector3(px2, 0, pz2);
            */
            Clone = true;
        }

        if (Clone == true)
        {
            Destroy(DestroyPrefab);
            Clone = false;
            enemyDeathcnt++;
            DeathRange += 1.0f;
        }
        
    }
}
