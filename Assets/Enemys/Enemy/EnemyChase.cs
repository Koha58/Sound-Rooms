using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool Chase=false;
    float ChaseTime;
  
    // Start is called before the first frame update
    private  void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        if(Chase==true)
        {
            ChaseTime += Time.deltaTime;
            if(ChaseTime >10.0f)
            {
                Chase = false;
                ChaseTime = 0;
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Chase = true;
        }
    }

  /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Chase = false;
        }
    }*/

}
