using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    ItemSearch ISe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        GameObject cobj = GameObject.Find("Player");
        ISe = cobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        if (other.gameObject.tag == "Door")
        {
            if(ISe.count == 1)
            {
                SceneManager.LoadScene("GameClear");
            }
        }
    }
}
