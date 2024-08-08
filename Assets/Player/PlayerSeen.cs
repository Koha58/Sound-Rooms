using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーの可視化・不可視化

public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    [SerializeField] public Transform _parentTransform;
    LevelMeter levelMeter;


    void Start()
    {
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        foreach (var playerParts in childTransforms)
        {
            //タグが"PlayerParts"である子オブジェクトを見えなくする
            playerParts.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        //音を出すことで見えるようになる
        if (levelMeter.nowdB > 0.0f)
        {
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えなくする
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            onoff = 1;  //見えているから1
        }

        /*
        GameObject gobj = GameObject.FindWithTag("Prototype"); //Enemyオブジェクトを探す
        PrototypeController2 PC2 = gobj.GetComponent<PrototypeController2>(); //付いているスクリプトを取得
        GameObject gobj1 = GameObject.FindWithTag("Enemy1"); //Enemyオブジェクトを探す
        PrototypeController2 PCI2 = gobj1.GetComponent<PrototypeController2>(); //付いているスクリプトを取得
        GameObject gobj2 = GameObject.FindWithTag("Prototype1"); //Enemyオブジェクトを探す
        PrototypeController4 PC4= gobj.GetComponent<PrototypeController4>(); //付いているスクリプトを取得
        GameObject gobj3 = GameObject.FindWithTag("Enemy2"); //Enemyオブジェクトを探す
        PrototypeController4 PCI4 = gobj3.GetComponent<PrototypeController4>(); //付いているスクリプトを取得

        if (PC2.PlayerVisualization == false|| PCI2.PlayerVisualization == false|| PC4.PlayerVisualization == false|| PCI4.PlayerVisualization == false)
        {*/
            //音を出していないとき、プレイヤーを見えなくする
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
                {
                    foreach (var playerParts in childTransforms)
                    {
                        //タグが"PlayerParts"である子オブジェクトを見えなくする
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    onoff = 0;  //見えていないから0
                }
            }
        //}
    }
}
