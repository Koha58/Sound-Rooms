using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//範囲内のアイテムの可視化・不可視化
public class ItemSeen : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    private float seentime = 0.0f; //経過時間記録用

    public GameObject Key1;
    public GameObject Key2;
    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject Wall;


    void Start()
    {
        //最初は見えない状態
        SeenArea.GetComponent<Collider>().enabled = false;
        Key1.GetComponent<Renderer>().enabled = false;
        Key2.GetComponent<Renderer>().enabled = false;
        ItemCanvas.GetComponent<Canvas>().enabled = false;
        Wall.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
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
                if (Key1 != null)
                {
                    SeenArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                    Key1.GetComponent<Renderer>().enabled = false;
                    Key2.GetComponent<Renderer>().enabled = false;
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
        //接触したオブジェクトのタグが"Item"のとき
        if (other.CompareTag("Item") && Key1 != null)
        {
            Key1.GetComponent<Renderer>().enabled = true;
            Key2.GetComponent<Renderer>().enabled = true;
            ItemCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }
    }
}
