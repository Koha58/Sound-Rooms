using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ�

public class PlayerRun : MonoBehaviour
{
    Rigidbody rb;
    float speed = 3.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.velocity = -transform.forward * speed;
        }

        //A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * speed;
        }

        //D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * speed;
        }
    }

}
