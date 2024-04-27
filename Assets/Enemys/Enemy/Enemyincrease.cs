using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease : MonoBehaviour
{
    public GameObject ebiPrefab;
    public GameObject DestroyPrefab;
    static public bool isHidden = true;
    static public bool Clone = false;
    public static int enemyDeathcnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden == false)
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab);//コピーを生成
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0以上２０以下のランダムの値を生成
            int pz = Random.Range(0, 20);//0以上２０以下のランダムの値を生成
            go.transform.position = new Vector3(px, 0, pz);
            Clone = true;
        }
        if (Clone == true)
        {
            Destroy(DestroyPrefab);
            Clone = false;
            enemyDeathcnt++;
        }
    }
}
