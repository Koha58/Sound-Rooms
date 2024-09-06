using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualizationBox : MonoBehaviour
{
    [SerializeField] Transform Enemy;
    private void Start()
    {

    }
    private void Update()
    {
        this.transform.position = Enemy.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            PS.onoff = 1;  //見えているから1
            PS.Visualization = true;
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えるようにする
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
