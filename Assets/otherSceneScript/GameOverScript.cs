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
    [SerializeField] GameObject Life4;
    [SerializeField] GameObject Life5;
    [SerializeField] GameObject LostLife1;
    [SerializeField] GameObject LostLife2;
    [SerializeField] GameObject LostLife3;
    [SerializeField] GameObject LostLife4;
    [SerializeField] GameObject LostLife5;

    private float Timer;
    private float Count;



    // Start is called before the first frame update
    void Start()
    {
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        LifeCount = 5;
        Count =0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Count == 1)
        {
            Timer += Time.deltaTime;
            if (Timer >=5.0f)
            {
                Timer = 0;
                Count = 0;
            }
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        GameObject gobj = GameObject.Find("Player");
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>();

        if(other.CompareTag("Enemy"))
        {
            Enemycontroller EC = other.GetComponent<Enemycontroller>();
            if (EC.ONOFF == 1&&PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }

        if (other.CompareTag("EnemyG"))
        {
            Enemycontroller EC = other.GetComponent<Enemycontroller>();
            if (EC.ONOFF == 1 && PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }

        if (other.CompareTag("Enemy2G"))
        {
            TutorialEnemyController EC = other.GetComponent<TutorialEnemyController>();
            if (EC.ONOFF == 1 && PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }

        if (other.CompareTag("Enemy1"))
        {
            TutorialEnemyController EC1 = other.GetComponent<TutorialEnemyController>();
            if (EC1.ONOFF == 1 && PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }

        if (other.CompareTag("EnemySearch"))
        {
            EnemySearchcontroller ESC = other.GetComponent<EnemySearchcontroller>();
            if (ESC.ONOFF == 1 && PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }

        if (other.CompareTag("Boss"))
        {
            BossEnemyControll BEC = other.GetComponent<BossEnemyControll>();
            if (BEC.ONOFF == 1 && PS.onoff == 1 && Table.ON == false)
            {
                if (Count == 0)
                {
                    LifeCount--;
                }
            }

        }

        if (other.CompareTag("BossV"))
        {
            if (Count == 0)
            {
                if (Table.ON == false)
                {
                    LifeCount--;
                    Count = 1;
                }
            }
        }
     
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;
            LostLife5.GetComponent<Image>().enabled = true;
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;
            LostLife4.GetComponent<Image>().enabled = true;
        }
        else if (LifeCount == 2)
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
}
