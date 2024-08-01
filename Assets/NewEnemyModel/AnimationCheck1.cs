using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCheck1 : MonoBehaviour
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
        if (Input.GetKey("up")) //��L�[�������ꂽ�Ƃ��O�֑���
        {
            anim.SetBool("StandUp", true);
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            transform.position += transform.forward * 0.015f;

        }


        if (Input.GetKeyUp("left shift")) //Shift�L�[�𗣂����Ƃ�
        {
            anim.SetBool("Run", false); //����A�j���[�V��������߂�

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
}
