using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inwall : MonoBehaviour
{
    bool Pon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得

        if (Pon)
        {
            Pon = false;
            PS.onoff = 0;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得

            if (Input.GetKeyDown("joystick button 0"))
            {
                Pon = false;
            }
            else
            {
                Pon = true;
            }

        }
    }
}
