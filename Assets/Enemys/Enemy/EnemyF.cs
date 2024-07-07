using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyF : MonoBehaviour
{
    public  BoxCollider EnemysF;
    static public bool FOnoff;
    float on;
    float off;
    // Start is called before the first frame update
    void Start()
    {
        EnemysF.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (FOnoff==false)
        {
            EnemysF.enabled = false;
           /* on += Time.deltaTime;
            if(on>=1f)
            {
                EnemysF.enabled = true;
                on = 0f;
            }*/
        }
        else
        {
            EnemysF.enabled = true;
           /* off += Time.deltaTime;
            if (off >= 1f)
            {
                EnemysF.enabled = false;
                off = 0f;
            }*/
        }
    }
}
