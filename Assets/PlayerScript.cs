using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;

    //プレイヤーの可視化に使う
    MeshRenderer mr;
    GameObject Player;

    void Start()
    {
        //移動
        rb = GetComponent<Rigidbody>();
        
        //最初は見えない状態
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false; //見えない（無効）
    }

    void Update()
    {
        //Wキー（前方移動）
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }

        //Sキー（後方移動）
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = - transform.forward * speed;
        }

        //Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = - transform.right * speed;
        }

        //Dキー（右移動）
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }

        //可視化
        if(Input.GetMouseButtonDown(0))
        {
            mr.enabled = true;  //見える（有効）

            //10秒経ったら見えなくなる
        }

    }
}
