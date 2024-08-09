using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototypeincrease1 : MonoBehaviour
{
    public GameObject ebiPrefab1;      //コピーするプレハブ
    public GameObject ebiPrefab2;      //コピーするプレハブ
    public GameObject DestroyPrefab;  //破壊されるプレハブ
    public bool isHidden = true;      //
    private bool Clone = false;         //Cloneを生み出すかのONOFF
    static public int enemyDeathcnt = 0;  //Enemyが死んだ数
    public static float DeathRange = 0f;//Enemyが死ぬと広がる範囲

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
            GameObject go1 = Instantiate(ebiPrefab1);//コピーを生成
            GameObject go2 = Instantiate(ebiPrefab2);//コピーを生成
                                                     //Debug.Log(go);
            float px = Random.Range(-10f, 10f); ;//0以上２０以下のランダムの値を生成
            float pz = Random.Range(-10f, 10f); ;//0以上２０以下のランダムの値を生成
            go1.transform.position = new Vector3(px, 0, pz);
            go2.transform.position = new Vector3(px, 0, pz);
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
