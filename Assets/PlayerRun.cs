using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの移動

public class PlayerRun : MonoBehaviour
{
    public float Speed = 1.0f;//プレイヤーの動くスピード
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
            transform.position += transform.forward * 0.03f;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.03f;
            animator.SetBool("Run", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }

    }
}
