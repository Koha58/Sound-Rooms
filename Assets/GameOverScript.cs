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
        if (other.CompareTag("Enemy") || other.CompareTag("Enemy1"))
        {
            if (PS.onoff == 1)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
