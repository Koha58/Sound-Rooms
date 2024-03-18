using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    //　近くにあるアイテムのリスト
    [SerializeField] private List<GameObject> itemList;
    //　メッセージ表示用キャンバス
    public GameObject itemMessageCanvas;


    //　アイテムがサーチエリア内に入ったら（場合によってはOnTriggerStayを使った方がいいかも？）
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            //　Eキーで取得する
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetItem(col.gameObject);
                itemMessageCanvas.GetComponentInChildren<Text>().text = "Eで取得";
                itemMessageCanvas.SetActive(true);
            }
        }
    }

    //　アイテムがサーチエリア外に出たら
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Item")
        {
            //　アイテムリストに存在していたらアイテムリストから削除
            if (itemList.Contains(col.gameObject))
            {
                itemList.Remove(col.gameObject);
                //　アイテムが範囲内になくなったら主人公の状態変更
                if (itemList.Count <= 0)
                {
                    itemMessageCanvas.SetActive(false);
                }
            }
        }
    }

    //　アイテムリストにアイテムを追加する処理
    void SetItem(GameObject addItem)
    {
        //　すでに同じアイテムがあるかどうか
        if (!itemList.Contains(addItem))
        {
            itemList.Add(addItem);
        }
    }

    //　アイテムリストからアイテムを削除する処理
    public void DeleteItem(GameObject deleteItem)
    {
        if (itemList.Contains(deleteItem))
        {
            itemList.Remove(deleteItem);
        }
    }

    //　手動モードの場合一番近いアイテムを探し取得する
    public void SelectItem()
    {
            //　一つでも検知エリア内にアイテムがあれば
            if (itemList.Count >= 1)
            {
                //　アイテムリストの先頭にあるアイテムを初期値にする
                Transform near = itemList[0].transform;
                float distance = Vector3.Distance(transform.root.position, near.position);

                //　一番近いアイテムを探す
                foreach (var item in itemList)
                {
                    if (Vector3.Distance(transform.root.position, item.transform.position) < distance)
                    {
                        near = item.transform;
                        distance = Vector3.Distance(transform.root.position, item.transform.position);
                    }
                }

                //　一番近いアイテムを取得、アイテムリストから削除、アイテムゲームオブジェクトの削除
                itemMessageCanvas.GetComponentInChildren<Text>().text = "";
                itemMessageCanvas.SetActive(false);
                DeleteItem(near.gameObject);
                Destroy(near.gameObject);
            }
    }
}
