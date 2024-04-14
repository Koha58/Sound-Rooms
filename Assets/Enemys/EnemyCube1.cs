using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCube1 : MonoBehaviour
{
    static public bool Enemybefor1 = false;
    float befortime1 = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemybefor1 == true)
        {
            befortime1 += Time.deltaTime;
            if (befortime1 > 1.0f)
            {
                befortime1 = 0;
                Enemybefor1 = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Enemybefor1 = true;
        }
    }
}
