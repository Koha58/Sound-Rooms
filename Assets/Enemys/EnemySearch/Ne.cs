using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject EnemySearch = GameObject.FindWithTag("EnemySearch");
        EnemySearchcontroller ESC = EnemySearch.GetComponent<EnemySearchcontroller>();
        Debug.Log(ESC.DestroyONOFF) ;
    }
}
