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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Forward;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Run", true);
            transform.Rotate(0, -1, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Forward;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Run", true);
            transform.Rotate(0, 1, 0);
        }
        else
        {
            animator.SetBool("Run", false);
        }

    }
}
