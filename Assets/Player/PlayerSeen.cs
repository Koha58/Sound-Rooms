using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//プレイヤーの可視化・不可視化

public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    //private float seentime = 0.0f; //経過時間記録用

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
        //左クリックで見えるようになる
        if (/*Input.GetMouseButtonUp(0) ||*/ levelMeter.nowdB > 0.0f)
        {
            foreach (var playerParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えなくする
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したらプレイヤーを見えなくする
        if (onoff == 1)
        {
            //seentime += Time.deltaTime;
            if (/*seentime >= 10.0f*/levelMeter.nowdB <= 0.0f)
            {
                foreach (var playerParts in childTransforms)
                {
                    //タグが"PlayerParts"である子オブジェクトを見えなくする
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
                onoff = 0;  //見えていないから0
                //seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

}
