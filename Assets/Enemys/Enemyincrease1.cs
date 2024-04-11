using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease1 : MonoBehaviour
{
    public GameObject ebiPrefab;
    public GameObject DestroyPrefab;
    static public bool isHidden1 = true;
    static public bool Clone = false ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden1 == false)
        {
            isHidden1 = true;
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
        }
    }
}
