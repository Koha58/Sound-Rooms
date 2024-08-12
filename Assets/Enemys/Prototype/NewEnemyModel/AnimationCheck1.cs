using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck1 : MonoBehaviour
{
    [SerializeField] Animator anim;
    private Rigidbody rb;
    public float upForce = 200f;

    AudioSource audioSourse;

    public AudioClip TrickEnemyLaugh;
    public AudioClip TrickEnemyRun;
    public AudioClip TrickEnemyIdle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey("up")) //上キーが押されたとき前へ走る
        {
            anim.SetBool("StandUp", true);
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            transform.position += transform.forward * 0.015f;

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
            anim.SetBool("Run", false);
            anim.SetBool("StandUp", false);
        }

    }

    void Laugh()
    {
        audioSourse.PlayOneShot(TrickEnemyLaugh);
    }

    void Idle()
    {
        audioSourse.PlayOneShot(TrickEnemyIdle);
    }

    void Run()
    {
        audioSourse.PlayOneShot(TrickEnemyRun);
    }
}
