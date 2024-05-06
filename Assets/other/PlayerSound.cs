using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが出す音波

public class PlayerSound : MonoBehaviour
{
    public float angle = 45.0f;

    SphereCollider sc;
    GameObject Player;  //オブジェクト名は適宜変更

    int onoff = 0;  //判定用（無音：0/音を出す：1）

    private float seentime = 0.0f; //経過時間記録用

    void Start()
    {
        //最初は無音
        sc = GetComponent<SphereCollider>();
        sc.enabled = false; //無音
    }

    void Update()
    {
        //左クリックで音を出す
        if (Input.GetMouseButtonDown(0))
        {
            sc.enabled = true;  //音を出す
            onoff = 1;  //音を出すから1

            OnTriggerStay(sc);

        }

        //指定した時間が経過したら無音に戻す
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 0.1f)
            {
                sc.enabled = false; //無音
                onoff = 0;  //無音だから0
                seentime = 0.0f;    //経過時間をリセット

                Debug.Log("");

            }
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "obj")
        {
            Vector3 posDelta = other.transform.position - transform.position;
            float objangle = Vector3.Angle(transform.forward, posDelta);
            if (objangle < angle)
            {
                Debug.Log("!!!");
            }
        }
    }

}
