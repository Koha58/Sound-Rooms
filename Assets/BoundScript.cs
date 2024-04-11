using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Sphere; // ���o�E���h���������I�u�W�F�N�g
    ButtonHoldDown BD;
    GameObject hobj;
    Rigidbody rb;
    private float bounce = 4.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //�t���Ă���X�N���v�g���擾
        if (collision.gameObject.name == "Plane")
        {
            //if (BD.isOn == 1 && BD.MaxSound.activeSelf == false)
           // {
                rb.AddForce(transform.up.normalized * bounce, ForceMode.VelocityChange);
          //  }
        }
    }
}
