using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの移動

public class PlayerRun : MonoBehaviour
{/*
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
        if (Input.GetKey("up")) //上キーが押されたとき前へ歩く
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

        if (Input.GetKey("left shift") && Input.GetKey("up")) //Shiftキーかつ上キーが押されている間
        {
            anim.SetBool("Running", true); //走るアニメーションをつける
            anim.SetBool("Walking", false);
            transform.position += transform.forward * 0.03f; //スピードアップ
            moving = 1;
        }

        if (Input.GetKeyUp("left shift")) //Shiftキーを離したとき
        {
            anim.SetBool("Running", false); //走るアニメーションをやめる
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
