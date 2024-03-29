using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyincrease : MonoBehaviour
{
    public GameObject ebiPrefab;
    static public bool isHidden = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)
        {
            isHidden = true;
            GameObject go = Instantiate(ebiPrefab) as GameObject;
            Debug.Log(go);
            int px = Random.Range(0, 20);
            go.transform.position = new Vector3(px, 5, 0);
        }
    }
}
