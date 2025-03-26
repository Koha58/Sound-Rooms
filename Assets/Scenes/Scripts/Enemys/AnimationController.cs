using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    //�A�j���[�V����
    [SerializeField] Animator animator; //�A�j���[�^�[�擾

    //�T�E���h
    [SerializeField] AudioSource audioSourse; //�I�[�f�B�I�\�[�X�擾
    [SerializeField] AudioClip searchClip;    //�T����
    [SerializeField] AudioClip runClip;       //���鉹
    [SerializeField] AudioClip walkClip;      //������

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
