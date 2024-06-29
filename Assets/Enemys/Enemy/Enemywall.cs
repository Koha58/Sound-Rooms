using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemywall : MonoBehaviour
{
    public bool Wall = false;
 
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = false;
        }
    }
}
