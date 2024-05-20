using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease : MonoBehaviour
{
    [SerializeField]
    private  GameObject ebiPrefab;      //コピーするプレハブ
    [SerializeField]
    private  GameObject DestroyPrefab;  //破壊されるプレハブ
    static  public  bool isHidden = true;      //
    private bool Clone = false;         //Cloneを生み出すかのONOFF
    static  public int enemyDeathcnt = 0;  
    public static float DeathRange = 1.0f;

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
            DeathRange += 1.0f;
        }
    }
}
