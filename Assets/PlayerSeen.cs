using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの可視化・不可視化

public class PlayerSeen : MonoBehaviour
{
    GameObject Player;  //オブジェクト名は適宜変更

    public int onoff = 0;  //判定用（プレイヤーが見えていない時：0/プレイヤーが見えている時：1）

    private float seentime = 0.0f; //経過時間記録用

    public GameObject parentObject;
    public GameObject childObject;


    void Start()
    {
        //最初は見えない状態
        parentObject = GameObject.Find("Player");

        for (int i = 0; i < 5; i++)//子オブジェクトの数を取得
        {
            Transform childTransform = parentObject.transform.GetChild(i);
            childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;//見えない（無効）
        }
    }

    public void Update()
    {
        GameObject parentObject = GameObject.Find("Player");
        //左クリックで見えるようになる
        if (Input.GetMouseButtonUp(0))
        {
            for (int i = 0; i < 5; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = true;//見える（有効）
            }
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したらプレイヤーを見えなくする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    Transform childTransform = parentObject.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = false;//見えない（無効）
                }
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

}
