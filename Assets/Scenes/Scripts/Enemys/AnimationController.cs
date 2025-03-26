using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    //アニメーション
    [SerializeField] Animator animator; //アニメーター取得

    //サウンド
    [SerializeField] AudioSource audioSourse; //オーディオソース取得
    [SerializeField] AudioClip searchClip;    //探す音
    [SerializeField] AudioClip runClip;       //走る音
    [SerializeField] AudioClip walkClip;      //歩く音

    void Idle() { audioSourse.PlayOneShot(searchClip); }
    void Run() { audioSourse.PlayOneShot(runClip); }
    void Walk() { audioSourse.PlayOneShot(walkClip); }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Idle", true);
        }
    }
}
