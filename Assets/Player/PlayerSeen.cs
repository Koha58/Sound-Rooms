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
    public bool piano;
    int pianocnt;
    public bool zero;
    AudioSetting AS;

    public bool Visualization;
    void Start()
    {
        onoff = 0;
        Visualization = false;

        piano = false;
        pianocnt = 0;
        zero = false;
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得
        //tagが"PlayerParts"である子オブジェクトのTransformのコレクションを取得
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        //音を出すことで見えるようになる
        if (levelMeter.nowdB > 0.0f && !piano)
        {
            onoff = 1;  //見えているから1
        }

        if (Visualization == false)
        {
            //音を出していないとき、プレイヤーを見えなくする
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f && !piano)
                {
                    onoff = 0;  //見えていないから0
                }
            }
        }

        //ピアノ部屋挙動
        if (piano)
        {
            onoff = 1;

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>();
            if (AS.BGMSlider.value == -80)
            {
                zero = true;
                piano = false;
                onoff = 0;
            }
            else
            {
                piano = true;
                zero = false;
                onoff = 1;  //見えているから1
            }
        }
        else
        {
            zero = false;
            if(pianocnt % 2 != 0 && AS.BGMSlider.value != -80)
            {
                piano = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            pianocnt++;
            if (!zero)
            {
                piano = true;

                if (pianocnt % 2 == 0)
                {
                    piano = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RoomOut"))
        {
            onoff = 0;
        }
    }
}
