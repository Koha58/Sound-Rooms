using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //�ړ��p�̕ϐ�
    float x, z;

    //�X�s�[�h�����p�̕ϐ�
    float speed = 0.1f;

 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()//0.02�b���ƂɌĂ΂��
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;
        z = z * -1;

        transform.position += new Vector3(x, 0, z);
    }

}
