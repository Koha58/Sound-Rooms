using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class EnemyVisualization : MonoBehaviour
{
    public GameObject Ring;
    private string objName;
    public EnemysGChase GChase;

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

        if (PS.onoff == 1)
        {
            OnoffTime += Time.deltaTime;
            if (OnoffTime >= 10.0f)
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
            //EnemyChase EC = Chase.GetComponent<EnemyChase>();
            EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();
            if (EGC.ViG == true)
            {
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
    }
}