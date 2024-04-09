using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ�

public class PlayerRun : MonoBehaviour
{
    public float Speed = 1.0f;//�v���C���[�̓����X�s�[�h
    public float Forward = 0.03f;
    private Rigidbody rb;
    private Animator animator;
    public int moving = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
        moving = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Forward;
            animator.SetBool("Run", true);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Run", true);
            transform.Rotate(0, -1, 0);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Forward;
            animator.SetBool("Run", true);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Run", true);
            transform.Rotate(0, 1, 0);
            moving = 1;
        }
        else
        {
            animator.SetBool("Run", false);
            moving = 0;
        }

    }
}
