using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBOXController : MonoBehaviour
{

    //移動用の変数
   // float x, z;

    //スピード調整用の変数
   // float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("button0");//A
        }
        if (Input.GetKeyDown("joystick button 1"))
        {
            Debug.Log("button1");//B
        }
        if (Input.GetKeyDown("joystick button 2"))
        {
            Debug.Log("button2");//X
        }
        if (Input.GetKeyDown("joystick button 3"))
        {
            Debug.Log("button3");//Y
        }
        if (Input.GetKeyDown("joystick button 4"))
        {
            Debug.Log("button4");//LB
        }
        if (Input.GetKeyDown("joystick button 5"))
        {
            Debug.Log("button5");//RB
        }
        if (Input.GetKeyDown("joystick button 6"))
        {
            Debug.Log("button6");//ビュー ボタン
        }
        if (Input.GetKeyDown("joystick button 7"))
        {
            Debug.Log("button7");//メニュー ボタン 
        }
        if (Input.GetKeyDown("joystick button 8"))
        {
            Debug.Log("button8");//
        }
        if (Input.GetKeyDown("joystick button 9"))
        {
            Debug.Log("button9");//
        }
        float hori1 = Input.GetAxis("Horizontal");
        float vert1 = Input.GetAxis("Vertical");
        if ((hori1 != 0) || (vert1 != 0))//左スティック
        {
            Debug.Log("stick:" + hori1 + "," + vert1);
        }
        /*上の変数を入れてからここをUpdateに入れれば動きます
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        transform.position += new Vector3(x, 0, z);
        */

        //ボタンの追加方法
        //https://dkrevel.com/unity-explain/input-manager/
        //https://hakonebox.hatenablog.com/entry/2018/04/15/125152#Input-Manager%E3%81%A7%E5%85%A5%E5%8A%9B%E8%A8%AD%E5%AE%9A

    }
}
