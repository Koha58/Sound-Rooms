using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//アイテム取得
public class ItemSearch : MonoBehaviour
{
    public GameObject closetObject;
    public List<GameObject> ItemSearchArea = new List<GameObject>();
    public List<string> myItemList = new List<string>();
    public TextMeshProUGUI keyCountText;
    public int count;
    public GameObject ItemCanvas;
    ItemSeen IS;
    //public List<GameObject> itemArray = new List<GameObject>();
    public GameObject canvasObject;
    public List<GameObject> itemText = new List<GameObject>();
    public GameObject text;

    private void Start()
    {
        count = 0;
        SetCountText();
    }
    private void Update()
    {
        GameObject iobj = GameObject.Find("SeenArea");
        IS = iobj.GetComponent<ItemSeen>(); //付いているスクリプトを取得
        if (IS.onoff == 1)
        {
            CaluculateClosetObject();
        }
    }

    void CaluculateClosetObject()
    {
        ItemCanvas = GameObject.FindWithTag("ItemCanvas");
        ItemSearchArea = GameObject.FindGameObjectsWithTag("Item").ToList();

        // 子オブジェクトの数を取得
        int childCount = ItemCanvas.transform.childCount;
        //Instantiate(childObject, new Vector3(-6.0f, 0, -6.0f), Quaternion.identity);
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = ItemCanvas.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            //childObject.GetComponent<Renderer>().enabled = false;
        }
        itemText = GameObject.FindGameObjectsWithTag("itemText").ToList();
        //一番近いアイテムを取得する
        float closetDistance = 1000000;
        for (int i = 0; i < ItemSearchArea.Count; i++)
        {
            if (ItemSearchArea[i] == null)
            {
                ItemSearchArea.Remove(ItemSearchArea[i]);
                itemText.Remove(itemText[i]);
                return;
            }
            float distance = Vector3.Distance(transform.position, ItemSearchArea[i].transform.position);
            if (closetDistance > distance)
            {
                closetDistance = distance;
                //ItemSearchArea[i] = itemArray[i];
                closetObject = ItemSearchArea[i].gameObject;
                //itemArray[i] = closetObject;
                canvasObject = itemText[i].gameObject;
                text = canvasObject;
            }
            //一定距離離れたらItemSearchAreaからオブジェクトを取り除く。
            if (distance > 6f)
            {
                if (closetObject == ItemSearchArea[i].gameObject)
                {
                    closetObject = null;
                    canvasObject = null;
                }
                ItemSearchArea.Remove(ItemSearchArea[i]);
                itemText.Remove(itemText[i]);
            }
        }
        //PlayerSeen playerseen = GetComponent<PlayerSeen>();
        //最も近いアイテムが一定の距離内にある場合、アイテムの説明UIを表示。Eキーを押すと拾える。
        if (closetObject == null) return;
        if (closetDistance < 1.5f)
        {
            ItemCanvas.GetComponent<Canvas>().enabled = true;
            canvasObject.GetComponent<OverHeadMsg> ().enabled = false;
            text.GetComponent<OverHeadMsg>().enabled = true;
            PickUp();
        }
        if(IS.seentime >= 10.0f)
        {
            if (canvasObject.GetComponent<OverHeadMsg>().enabled == false)
            {
                canvasObject.GetComponent<OverHeadMsg>().enabled = true;
            }
        }
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myItemList.Add(closetObject.name);
            //ItemSearchAreaからアイテムを取り除く。
            ItemSearchArea.Remove(closetObject);
            //itemArray.Remove(closetObject);
            itemText.Remove(canvasObject);
            Destroy(closetObject, 0.5f);
            closetObject = null;
            canvasObject = null;
            count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }
}
