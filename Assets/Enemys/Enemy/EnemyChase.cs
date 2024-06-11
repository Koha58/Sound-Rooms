using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//プレイヤーを参照
    public  bool Chase=false;
    private float Chaseonoff;
    public bool Wall=false;
    private float Wallonoff;

    // Start is called before the first frame update
    private  void Start()
    {
      
    }

    // Update is called once per frame
    private void Update()
    {
        if (Chase == true)
        {
            Chaseonoff += Time.deltaTime;
            if (Chaseonoff>=0.5f)
            Chase = false;
        }

        if (Wall == true)
        {
            Wallonoff += Time.deltaTime;
            if (Wallonoff >= 5f)
                Wall= false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
           Chase = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = true;
        }
    }
}
