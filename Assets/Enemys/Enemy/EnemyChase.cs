using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    public  bool Chase=false;
    private float Chaseonoff;
    public bool Wall=false;
    private float Wallonoff;

   // [SerializeField] public GameObject EnemyArea;

    // Start is called before the first frame update
    private  void Start()
    {
        //EnemyArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    public void Update()
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
            {
                Wall = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Play");
           Chase = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Wall = true;
            Debug.Log("Wall");
        }
    }

}
