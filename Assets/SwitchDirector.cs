using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//音波の可視化・不可視化

public class SwitchDirector : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject MovingSphere;
    int onoff = 0;  //判定用（音波が見えていない時：0/音波が見えている時：1）
    private float seentime = 0.0f; //経過時間記録用
    PlayerRun PR;
    GameObject mobj;

    // Update is called once per frame
    void Update()
    {
        mobj = GameObject.Find("Player");
        PR = mobj.GetComponent<PlayerRun>(); //付いているスクリプトを取得
        //左クリックで見えるようになる
        if (Input.GetMouseButtonUp(0))
        {
            if (PR.moving == 0)
            {
                Sphere.SetActive(true);//音波非表示→表示
            }
            if (PR.moving == 1)
            {
                MovingSphere.SetActive(true);//音波非表示→表示
            }
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
                MovingSphere.SetActive(false);//音波表示→非表示
            }
         
            if (PR.moving == 0 && MovingSphere.activeSelf == true)
            {
                MovingSphere.SetActive(false);//音波表示→非表示
                Sphere.SetActive(true);//音波非表示→表示
            }
            if (PR.moving == 1 && Sphere.activeSelf == true)
            {
                MovingSphere.SetActive(true);//音波非表示→表示
                Sphere.SetActive(false);//音波表示→非表示
            }
        }
    }
}
