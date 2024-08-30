using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public int LifeCount;
    [SerializeField] GameObject Life1;
    [SerializeField] GameObject Life2;
    [SerializeField] GameObject Life3;
    [SerializeField] GameObject LostLife1;
    [SerializeField] GameObject LostLife2;
    [SerializeField] GameObject LostLife3;

    // Start is called before the first frame update
    void Start()
    {
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;

        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;

        LifeCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");
        PS = gobj.GetComponent<PlayerSeen>();

        if(other.CompareTag("Enemy"))
        {
            if (PS.onoff == 1)
            {
                Enemycontroller EC = other.GetComponent<Enemycontroller>();
                //Debug.Log("1");
                if (EC.ONOFF == 1)
                {
                    LifeCount--;
                }
            }
        }

        if (other.CompareTag("EnemyG"))
        {
            if (PS.onoff == 1)
            {
                Enemycontroller EC = other.GetComponent<Enemycontroller>();
                //Debug.Log("1");
                if (EC.ONOFF == 1)
                {
                    LifeCount--;
                }
            }
        }

        if (other.CompareTag("EnemySearch"))
        {
            if (PS.onoff == 1)
            {
                EnemySearchcontroller ESC = other.GetComponent<EnemySearchcontroller>();
                if (ESC.ONOFF==1)
                {
                    //Debug.Log("1");
                    LifeCount--;
                }
            }
        }

        if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;
            LostLife3.GetComponent<Image>().enabled = true;
        }
        else if(LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;
            LostLife2.GetComponent<Image>().enabled = true;
        }
        else if(LifeCount == 0)
        {
            Life1.GetComponent<Image>().enabled = false;
            LostLife1.GetComponent<Image>().enabled = true;
            SceneManager.LoadScene("GameOver");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");
        PS = gobj.GetComponent<PlayerSeen>();
        if (other.CompareTag("BossV"))
        {
            if (PS.onoff == 1)
            {
                //Debug.Log("1");
                LifeCount--;
            }
        }
    }
}
