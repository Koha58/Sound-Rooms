using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyVisualization1 : MonoBehaviour
{
    public GameObject Ring;
    public GameObject[] Walls;
    public GameObject[] Boxes;
    public static GameObject[] parentObject;
    private string objName;

    ItemSearch ISe;
    [SerializeField] public Transform _parentTransform;

    //bool PlayerOnoff;
    float OnoffTime;

    LevelMeter levelMeter;

    private void Start()
    {

    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        GameObject isobj = GameObject.Find("Player");
        ISe = isobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        if (PS.onoff == 1)
        {
            OnoffTime += Time.deltaTime;
            if (OnoffTime >= 5.0f)
            {
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
                OnoffTime = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 0)
            {
                PS.onoff = 1;  //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 1)
            {
                PS.onoff = 0;  //見えているから1
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えるようにする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
}