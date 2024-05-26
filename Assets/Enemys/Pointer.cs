using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    float OffTime;
    float DestroyTime;
    private bool DestroyONOFF;
    public bool Nextpoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (DestroyONOFF==true)
        {
            DestroyTime += Time.deltaTime;
            if (DestroyTime >= 3f)
            {
                Nextpoint = true;
                Destroy(gameObject);
                DestroyTime = 0;
                DestroyONOFF = false;
            }
        }

        if(Nextpoint==true) 
        {
            if(OffTime>0.5f)
            Nextpoint = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DestroyONOFF=true;
        }
    }
}
