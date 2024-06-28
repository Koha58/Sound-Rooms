using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysGChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public bool GChase = false;
   
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
        if (other.gameObject.CompareTag("Player"))
        {
            GChase = true;
        }
    }
}
