using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテム取得
public class DoorCheck : MonoBehaviour
{
    Animator anim;
    LevelMeter levelMeter;

    bool OnOff;

    void Start()
    {
        //最初は見えない状態
        GetComponent<Collider>().enabled = false;
        OnOff = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //音を出すことで可視化
        if (levelMeter.nowdB > 0.0f)
        {
            GetComponent<Collider>().enabled = true;//見える（有効）
            OnOff = true;
        }

        if (OnOff == true)
        {
            if (levelMeter.nowdB == 0.0f)
            {
                GetComponent<Collider>().enabled = false;//見えない（無効）
                OnOff = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutoDoor"))
        {
            anim = other.GetComponent<Animator>();
            anim.SetBool("Open", true);
        }
    }
}
