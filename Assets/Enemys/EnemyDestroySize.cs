using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroySize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
      
        for (float i = 1; i < 2; i += 0.1f)
        {
            this.transform.localScale = new Vector3(i, i, i);
           
        }
    }
}
