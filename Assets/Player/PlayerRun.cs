using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの移動

public class PlayerRun : MonoBehaviour
{
    public float Speed = 1.0f;//プレイヤーの動くスピード
    public float Forward = 0.03f;
    private Rigidbody rb;
    private Animator animator;
    public int moving = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
        moving = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //歩くとき
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Forward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            transform.Rotate(0, -1, 0);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Forward;
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            transform.Rotate(0, 1, 0);
            moving = 1;
        }
        else
        {
            animator.SetBool("Walking", false);
            moving = 0;
        }

        //走るとき
        if (Input.GetKey(KeyCode.W) && Input.GetMouseButton(1))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetMouseButton(1))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            transform.Rotate(0, -1, 0);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetMouseButton(1))
        {
            transform.position -= transform.forward * Forward;
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetMouseButton(1))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
            transform.Rotate(0, 1, 0);
            moving = 1;
        }
        else
        {
            animator.SetBool("Running", false);
            moving = 0;
        }

    }
    
}
