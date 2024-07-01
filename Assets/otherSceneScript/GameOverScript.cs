using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");
        PS = gobj.GetComponent<PlayerSeen>();

        if (other.CompareTag("EnemyParts") )
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("Enemy"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("Enemy1"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("EnemyG"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("EnemyG1"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("EnemyG2"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (other.CompareTag("EnemyG3"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

    }
}
