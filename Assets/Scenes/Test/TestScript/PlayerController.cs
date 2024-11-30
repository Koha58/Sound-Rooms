using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //移動用の変数
    float x, z;

    //スピード調整用の変数
    float speed = 0.1f;

 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()//0.02秒ごとに呼ばれる
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;
        z = z * -1;

        transform.position += new Vector3(x, 0, z);
    }

}
