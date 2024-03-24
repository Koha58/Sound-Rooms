using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParturition : MonoBehaviour
{
    public GameObject ebiPrefab;　//増加するオブジェクトを入れる
    static public bool isHidden = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)// falseの時
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab) as GameObject;
            int px = Random.Range(0, 20);   //Xの０～２０の範囲生成
            go.transform.position = new Vector3(px, 5, 0);
        }
    }
}
