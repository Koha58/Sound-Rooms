using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBOXController : MonoBehaviour
{
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

    }
}
