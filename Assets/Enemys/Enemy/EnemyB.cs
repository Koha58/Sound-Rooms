using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : MonoBehaviour
{
    public  BoxCollider EnemysB;
    static public bool BOnoff;
    float on;
    float off;
    // Start is called before the first frame update
    void Start()
    {
        EnemysB.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {

        if (BOnoff == false)
        {
            EnemysB.enabled = false;
          /*  on += Time.deltaTime;
            if (on >= 1f)
            {
                EnemysB.enabled = true;
                on = 0f;
            }*/
        }
        else
        {
            EnemysB.enabled = true;
           /* off += Time.deltaTime;
            if (off >= 1f)
            {
                EnemysB.enabled = false;
                off = 0f;
            }*/
        }
    }
}
