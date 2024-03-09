using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private float x;
    private float z;
    public float Speed = 1.0f;
    private Vector3 latestPos;

    private Rigidbody rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey (KeyCode.W))
        {
            transform.Translate(0.0f, 0.0f, 0.01f);
            animator.SetBool("Run", true );
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0.0f, 0.0f, -0.01f);
            animator.SetBool("Run", true );
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.01f, 0.0f, 0.0f);
            animator.SetBool("Run", true );
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.01f, 0.0f, 0.0f);
            animator.SetBool("Run",true );
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Run", false);
        }
    }
}
