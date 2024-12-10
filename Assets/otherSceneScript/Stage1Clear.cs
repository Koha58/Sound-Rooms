using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Clear : MonoBehaviour
{
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
        GameObject cobj = GameObject.Find("EnemyAttackArea");
        EnemyAttack EAtack = cobj.GetComponent<EnemyAttack>(); //付いているスクリプトを取得

        if (other.gameObject.name == "ExitDoor")
        {
            if (EAtack.count == 1)
            {
                SceneManager.LoadScene("Stage1Clear");
            }
        }
    }
}
