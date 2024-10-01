using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XBOXController : MonoBehaviour
{

    //移動用の変数
   // float x, z;

    //スピード調整用の変数
   // float speed = 0.1f;

    // Start is called before the first frame update

    private IEnumerator Start()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("ゲームパッド未接続");
            yield break;
        }

        Debug.Log("左モーター振動");
        gamepad.SetMotorSpeeds(1.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);

        Debug.Log("右モーター振動");
        gamepad.SetMotorSpeeds(0.0f, 1.0f);
        yield return new WaitForSeconds(1.0f);

        Debug.Log("モーター停止");
        gamepad.SetMotorSpeeds(0.0f, 0.0f);
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
            Debug.Log("button8");//Lスティック押込み
        }
        if (Input.GetKeyDown("joystick button 9"))
        {
            Debug.Log("button9");//Rスティック押込み
        }

        if (Input.GetKeyDown("joystick button 10"))
        {
            Debug.Log("button10");//
        }
        if (Input.GetKeyDown("joystick button 11"))
        {
            Debug.Log("button11");//
        }

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if ((hori != 0) || (vert != 0))//左スティック
        {
            Debug.Log("stick:" + hori + "," + vert);
        }

        //追加
        float DpadHorizontal = Input.GetAxis("DpadHorizontal");
        float DpadVertical = Input.GetAxis("DpadVertical");
        if (DpadHorizontal != 0||DpadVertical !=0) //十字キー
        {
            Debug.Log("DPad:" + DpadHorizontal + "," + DpadVertical);
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
        //https://simplestar-tech.hatenablog.com/entry/2017/11/05/181019
    }
}
