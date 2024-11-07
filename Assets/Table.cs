using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Table : MonoBehaviour
{
    static public bool ON;
    public bool ON2;
    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        ON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( ON==true )
        {
            Timer += Time.deltaTime;
            if(Timer>=35.0f)
            {
                Timer = 0;
                ON = false;
                ON2 = false;
            }
        }
        if (ON2 == true)
        {
            if (PlayerRun.CrouchOn == true)
            {
                ON = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ON2 = true;
            if (PlayerRun.CrouchOn==true)
            {
                ON = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ON2 = true;
            if (PlayerRun.CrouchOn == true)
            {
                ON = true;
              
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
                
                GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                PS.Visualization = false;
                PS.onoff = 0;                                                             //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
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
