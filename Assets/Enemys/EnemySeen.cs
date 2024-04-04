using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySeen : MonoBehaviour
{
    public int ONoff = 0;//(0が見えない；１が見える状態）
    private float Seetime;  //経過時間
    public  float SoundTime;
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        //tagが"EnemyParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //タグが"EnemyParts"である子オブジェクトを見えなくする
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //tagが"EnemyParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//見えないとき
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {              
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えるようにする
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//音波非表示→表示
            }
        }
         if (ONoff == 1)//見えているとき
         {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //タグが"EnemyParts"である子オブジェクトを見えなくする
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
                Sphere.SetActive(false);//音波表示→非表示
            }
         }
    } 
}
