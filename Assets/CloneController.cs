using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    public GameObject originObject; //オリジナルのオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(originObject, new Vector3(-6.0f, 0, -6.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
