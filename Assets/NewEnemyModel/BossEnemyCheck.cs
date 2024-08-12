using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyCheck : MonoBehaviour
{
    [SerializeField] Animator anim;
    private Rigidbody rb;
    public float upForce = 200f;

    AudioSource audioSource;

    public AudioClip BossMove;
    public AudioClip BossIdle;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKey("up")) //è„ÉLÅ[Ç™âüÇ≥ÇÍÇΩÇ∆Ç´ëOÇ÷ï‡Ç≠
        {
            anim.SetBool("Move", true);
            anim.SetBool("Idle", false);
            transform.position += transform.forward * 0.03f;
        }

        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 1, 0);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -1, 0);
        }

        if (!Input.GetKey("up"))
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Move", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (collision.gameObject.tag == "LeftWall")
        {
            localAngle.z = 90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(10f, 0, 0);
        }
        else if (collision.gameObject.tag == "RightWall")
        {
            localAngle.z = -90f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(-10f, 0, 0);
        }
        else if (collision.gameObject.tag == "Ceiling")
        {
            localAngle.z = 180f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(0, 10f, 0);
        }
        else if (collision.gameObject.tag == "Floor")
        {
            localAngle.z = 0f;
            localAngle.y = 0f;
            myTransform.localEulerAngles = localAngle;
            Physics.gravity = new Vector3(0, -10f, 0);
        }
    }


    void Idle()
    {
        audioSource.PlayOneShot(BossIdle);
    }

    void Move()
    {
        audioSource.PlayOneShot(BossMove);
    }
}
