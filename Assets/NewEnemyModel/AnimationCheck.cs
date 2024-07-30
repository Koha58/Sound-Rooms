using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    [SerializeField] Animator anim;
    private Rigidbody rb;
    public float upForce = 200f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKey("up")) //上キーが押されたとき前へ歩く
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            transform.position += transform.forward * 0.001f;

        }

        if (Input.GetKey("left shift") && Input.GetKey("up")) //Shiftキーかつ上キーが押されている間
        {
            anim.SetBool("Run", true); //走るアニメーションをつける
            anim.SetBool("Walk", false);
            anim.SetBool("Idle", false);
            transform.position += transform.forward * 0.01f; //スピードアップ
        }

        if (Input.GetKeyUp("left shift")) //Shiftキーを離したとき
        {
            anim.SetBool("Run", false); //走るアニメーションをやめる

        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -1, 0);
        }

        if (Input.GetKeyUp("up") || Input.GetKeyUp("left shift") || Input.GetKeyUp("right") || Input.GetKeyUp("left"))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }

    }
}
