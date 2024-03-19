using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//アイテム取得
public class ItemSearch : MonoBehaviour
{
    public GameObject closetObject;
    public List<GameObject> ItemSearchArea = new List<GameObject>();
    public List<string> myItemList = new List<string>();

    private void Update()
    {
        CaluculateClosetObject();
    }

    void CaluculateClosetObject()
    {
        //一番近いアイテムを取得する
        float closetDistance = 1000000;
        for (int i = 0; i < ItemSearchArea.Count; i++)
        {
            if (ItemSearchArea[i] == null)
            {
                ItemSearchArea.Remove(ItemSearchArea[i]);
                return;
            }
            float distance = Vector3.Distance(transform.position, ItemSearchArea[i].transform.position);
            if (closetDistance > distance)
            {
                closetDistance = distance;
                closetObject = ItemSearchArea[i].gameObject;
            }
            //一定距離離れたらItemSearchAreaからオブジェクトを取り除く。
            if (distance > 6f)
            {
                if (closetObject == ItemSearchArea[i].gameObject)
                {
                    closetObject = null;
                }
                ItemSearchArea.Remove(ItemSearchArea[i]);
            }
        }

        //最も近いアイテムが一定の距離内にある場合、アイテムの説明UIを表示。Eキーを押すと拾える。
        if (closetObject == null) return;
        if (closetDistance < 1.5f)
        {

            PickUp();
        }

    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myItemList.Add(closetObject.name);
            //ItemSearchAreaからアイテムを取り除く。
            ItemSearchArea.Remove(closetObject);
            Destroy(closetObject, 0.5f);
            closetObject = null;
        }
    }
}
