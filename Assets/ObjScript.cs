using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScript : MonoBehaviour
{
    MeshRenderer mr;
    CapsuleCollider cc;
    GameObject Obj;

    int onoff = 0;  //判定用（音波が当たってない時：0/音波が当たった時：1）

    private float seentime = 0.0f; //経過時間記録用

    void Start()
    {
        //音波が当たってない時
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false; //見えない（無効）
        cc = GetComponent<CapsuleCollider>();
        cc.enabled = false; //通り抜け可能
    }

    void Update()
    {
        //音波が当たった時
        if (Input.GetMouseButtonDown(0))
        {
            //OnTriggerStay(cc);
            mr.enabled = true;  //見える（有効）
            cc.enabled = true;  //通り抜け不可
            onoff = 1;  //見えているから1
        }

        //OnTriggerStay(cc);

        //指定した時間が経過したら見えなくなる
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 3.0f)
            {
                mr.enabled = false; //見えない（無効）
                cc.enabled = false; //通り抜け可能
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }
    }

    /*
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "player")
        {
            mr.enabled = true;  //見える（有効）
            cc.enabled = true;  //通り抜け不可
            onoff = 1;  //見えているから1
        }
        else if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 3.0f)
            {
                mr.enabled = false; //見えない（無効）
                cc.enabled = false; //通り抜け可能
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }
    }*/
}
