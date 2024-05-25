using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public  bool Chase=false;

    // Start is called before the first frame update
    private  void Start()
    {
      
    }

    // Update is called once per frame
    private void Update()
    {
 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           Chase = true;
        }
    }
}
