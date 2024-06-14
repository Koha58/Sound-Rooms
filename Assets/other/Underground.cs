using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
          transform.position = new Vector3 (2f, 2f,2f);
        }

        if (other.gameObject.CompareTag("Enemy1"))
        {
            transform.position = new Vector3(2f, 2f, 2f);
        }

        if (other.gameObject.CompareTag("EnemyG1"))
        {
            transform.position = new Vector3(2f, 2f, 2f);
        }

        if (other.gameObject.CompareTag("EnemyG2"))
        {
            transform.position = new Vector3(2f, 2f, 2f);
        }

        if (other.gameObject.CompareTag("EnemyG3"))
        {
            transform.position = new Vector3(2f, 2f, 2f);
        }

        if (other.gameObject.CompareTag("EnemyG4"))
        {
            transform.position = new Vector3(2f, 2f, 2f);
        }
    }
}
