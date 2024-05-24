using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody Sphere; // バウンドさせたいオブジェクト
    ButtonHoldDown BD;
    GameObject hobj;
    Rigidbody rb;
    private float bounce = 1.5f;
    private float Sbounce = 2.3f;
    private float Mbounce = 2.8f;
    private float Lbounce = 5.0f;
    private int preBounce;//敵を倒した判定用

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        preBounce = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //付いているスクリプトを取得

        if(Enemyincrease.enemyDeathcnt > preBounce)
        {
            bounce += 0.5f;
            Sbounce += 2.0f;
            Mbounce += 2.0f;
            Lbounce += 3.0f;
            preBounce++;
        }

        if (collision.gameObject.name == "Plane")
        {
            if (BD.boundHeight == 0)
            {
                rb.AddForce(transform.up.normalized * bounce, ForceMode.VelocityChange);
            }
            else if(BD.boundHeight == 1) 
            {
                rb.AddForce(transform.up.normalized * Sbounce, ForceMode.VelocityChange);
            }
            else if (BD.boundHeight == 2)
            {
                rb.AddForce(transform.up.normalized * Mbounce, ForceMode.VelocityChange);
            }
            else if (BD.boundHeight == 3)
            {
                rb.AddForce(transform.up.normalized * Lbounce, ForceMode.VelocityChange);
            }

        }
    }
}
