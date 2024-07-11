using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool Chase=false;
    float ChaseTime;
    public bool Vi;
    float ViTime;

    // Start is called before the first frame update
    private  void Start()
    {
        Chase = false;
        Vi = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Chase==true)
        {
            ChaseTime += Time.deltaTime;
            if(ChaseTime >25.0f)
            {
                Chase = false;
                ChaseTime = 0f;
            }
        }

        if( Vi == false)
        {
            ViTime += Time.deltaTime;
            if(ViTime >0.5f) 
            {
                Vi = true;
                ViTime = 0.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Chase = true;
        }

        if (other.gameObject.CompareTag("InWall"))
        {
            Vi = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            Vi = false;
        }
    }

}
