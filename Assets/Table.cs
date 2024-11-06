using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    static public bool ON;
    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        ON = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ON);
        if( ON==true )
        {
            Timer += Time.deltaTime;
            if(Timer>30.0f)
            {
                ON = false;
                Timer = 0;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerRun.CrouchOn==true)
            {
                ON = true;
                Timer = 0;
            }
        }
    }
    
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerRun.CrouchOn == true)
            {
                ON = false;
                
                GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                PS.Visualization = false;
                PS.onoff = 0;                                                             //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
            else
            {
                ON = false;
            }
        }
    }*/
}
