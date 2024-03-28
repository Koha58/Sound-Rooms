using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.XR;
using static Unity.VisualScripting.Metadata;

//範囲内のアイテムの可視化・不可視化
public class ItemSeen : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    private float seentime = 0.0f; //経過時間記録用

    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject Wall;

    void Start()
    {
        GameObject parentObject = GameObject.Find("key 1");

        // 子オブジェクトの数を取得
        int childCount = parentObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }
        //最初は見えない状態
        SeenArea.GetComponent<Collider>().enabled = false;
        ItemCanvas.GetComponent<Canvas>().enabled = false;
        Wall.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        GameObject parentObject = GameObject.Find("key 1");
        //左クリックで範囲内を可視化
        if (Input.GetMouseButtonDown(0))
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したら範囲内の可視化をできなくする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                if (parentObject != null)
                {
                    // 子オブジェクトの数を取得
                    int childCount = parentObject.transform.childCount;
                    for (int i = 0; i < childCount; i++)
                    {
                        Transform childTransform = parentObject.transform.GetChild(i);
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<Renderer>().enabled = false;
                    }
                    SeenArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                    ItemCanvas.GetComponent<Canvas>().enabled = false;
                }
                Wall.GetComponent<Renderer>().enabled = false;
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject parentObject = GameObject.Find("key 1");
        //接触したオブジェクトのタグが"Item"のとき
        if (other.CompareTag("Item") && parentObject != null)
        {
            // 子オブジェクトの数を取得
            int childCount = parentObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = true;
            }
            ItemCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }
    }
}
