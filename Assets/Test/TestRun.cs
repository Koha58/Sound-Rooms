using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TestRun : MonoBehaviour
{
    //移動用の変数
    float x, z;

    //スピード調整用の変数
    float Walkspeed = 1f/100;
    //スピード調整用の変数
    float Runspeed = 2f / 100;

    //プレイヤーのRigidbody
    Rigidbody PlayerRigidbody;
    Animator Animator;

    public float Speed=10;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();   //アニメーターコントローラーからアニメーションを取得する
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalk();
        if (Input.GetKey("joystick button 5")){PlayerRun();}
        Rotation();

    }

    private void PlayerWalk()
    {
        x = Input.GetAxisRaw("Horizontal") * Walkspeed;
        z = Input.GetAxisRaw("Vertical") * Walkspeed;
        Animator.SetBool("Walking", true);
        Animator.SetBool("Running", false);

        PlayerRigidbody.transform.position += new Vector3(x * -1, 0, z);
    }

    private void PlayerRun()
    {
        x = Input.GetAxisRaw("Horizontal") * Runspeed;
        z = Input.GetAxisRaw("Vertical") * Runspeed;
        Animator.SetBool("Walking", false);
        Animator.SetBool("Running", true);

        PlayerRigidbody.transform.position += new Vector3(x * -1, 0, z);
    }

    private void Rotation()
    {
        float H1 = Input.GetAxisRaw("Horizontal");
        float V1 = Input.GetAxisRaw("Vertical");

        Vector3 rotation = new Vector3(V1, H1, 0)*Speed*Time.deltaTime;

        transform.LookAt(rotation);
    }
}
