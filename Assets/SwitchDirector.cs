using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirector : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    int onoff = 0;  //判定用（音波が見えていない時：0/音波が見えている時：1）
    private float seentime = 0.0f; //経過時間記録用

    // Update is called once per frame
    void Update()
    {
        //左クリックで見えるようになる
        if (Input.GetMouseButtonDown(0))
        {
            Sphere.SetActive(true);//音波非表示→表示
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したら音波を見えなくする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
                Sphere.SetActive(false);//音波表示→非表示
            }
        }
    }
}
