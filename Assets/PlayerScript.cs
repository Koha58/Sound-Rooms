using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;

    //�v���C���[�̉����Ɏg��
    MeshRenderer mr;
    GameObject Player;

    void Start()
    {
        //�ړ�
        rb = GetComponent<Rigidbody>();
        
        //�ŏ��͌����Ȃ����
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false; //�����Ȃ��i�����j
    }

    void Update()
    {
        //W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * speed;
        }

        //S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = - transform.forward * speed;
        }

        //A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = - transform.right * speed;
        }

        //D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }

        //����
        if(Input.GetMouseButtonDown(0))
        {
            mr.enabled = true;  //������i�L���j

            //10�b�o�����猩���Ȃ��Ȃ�
        }

    }
}
