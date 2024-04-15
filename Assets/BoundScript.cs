using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Sphere; // ←バウンドさせたいオブジェクト
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
        BD = hobj.GetComponent<ButtonHoldDown>(); //付いているスクリプトを取得
        if (collision.gameObject.name == "Plane")
        {
            if (BD.boundHeight == 0)
            {
                rb.AddForce(transform.up.normalized * bounce, ForceMode.VelocityChange);
            }
            else if(BD.boundHeight == 1) 
            {
                rb.AddForce(transform.up.normalized * bounce*1.3f, ForceMode.VelocityChange);
            }
            else if (BD.boundHeight == 2)
            {
                rb.AddForce(transform.up.normalized * bounce * 1.5f, ForceMode.VelocityChange);
            }
            else if (BD.boundHeight == 3)
            {
                rb.AddForce(transform.up.normalized * bounce * 2.5f, ForceMode.VelocityChange);
            }
            else if (BD.boundHeight == 4)
            {
                rb.AddForce(transform.up.normalized * bounce * 3.5f, ForceMode.VelocityChange);
            }
        }
    }
}
