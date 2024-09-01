using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundcheckArea : MonoBehaviour
{
   public Transform Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Enemy.transform.position;
    }
}
