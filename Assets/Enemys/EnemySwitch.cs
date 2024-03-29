using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    //private float seentime = 0.0f; //経過時間記録用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySeen.ONoff == 0)//見えないとき
        {
            Sphere.SetActive(false);//音波非表示→表示
        }

        //指定した時間が経過したら音波を見えなくする
        if (EnemySeen.ONoff == 1)
        {

            Sphere.SetActive(true);//音波表示→非表示

        }
    }
}
