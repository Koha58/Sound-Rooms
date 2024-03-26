using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの可視化・不可視化

public class PlayerSeen : MonoBehaviour
{
    SkinnedMeshRenderer mr;
    GameObject Player;  //オブジェクト名は適宜変更

    public int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    private float seentime = 0.0f; //経過時間記録用


    void Start()
    {
        //最初は見えない状態
        mr = GetComponent<SkinnedMeshRenderer>();
        mr.enabled = false; //見えない（無効）
    }

    public void Update()
    {
        //左クリックで見えるようになる
        if (Input.GetMouseButtonDown(0))
        {
            mr.enabled = true;  //見える（有効）
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したらプレイヤーを見えなくする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                mr.enabled = false; //見えない（無効）
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

}
