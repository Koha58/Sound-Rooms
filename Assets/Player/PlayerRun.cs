using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̈ړ�

public class PlayerRun : MonoBehaviour
{/*
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

    }*/

    [SerializeField] Animator anim;
    private Rigidbody rb;
    public float upForce = 200f;

    public int moving = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKey("up")) //��L�[�������ꂽ�Ƃ��O�֕���
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Running", false);
            transform.position += transform.forward * 0.01f;
            moving = 1;
        }
        else
        {
            anim.SetBool("Walking", false);
            moving = 0;
        }

        if (Input.GetKey("left shift") && Input.GetKey("up")) //Shift�L�[����L�[��������Ă����
        {
            anim.SetBool("Running", true); //����A�j���[�V����������
            anim.SetBool("Walking", false);
            transform.position += transform.forward * 0.03f; //�X�s�[�h�A�b�v
            moving = 1;
        }

        if (Input.GetKeyUp("left shift")) //Shift�L�[�𗣂����Ƃ�
        {
            anim.SetBool("Running", false); //����A�j���[�V��������߂�
            moving = 0;
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 1, 0);
            moving = 0;
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -1, 0);
            moving = 0;
        }

    }
}
