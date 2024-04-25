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
    public GameObject canvasObject;
    public List<GameObject> itemText = new List<GameObject>();
    public GameObject text;

    private void Start()
    {
        count = 0;
        SetCountText();
        itemText = GameObject.FindGameObjectsWithTag("itemText").ToList();
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
                closetObject = ItemSearchArea[i].gameObject;
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

            itemText[0].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            itemText[1].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            //itemText[3].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
            text.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
            PickUp();
        }
        if (IS.seentime >= 10.0f)
        {
            for (int i = 0; i < ItemSearchArea.Count; i++)
            {
                if (itemText[i].gameObject.activeSelf == false)
                {
                    itemText[i].gameObject.SetActive(true);
                }
            }
        }
        /*
        for (int i = 0; i < ItemSearchArea.Count; i++)
        {
            if (closetDistance < 1.5f)
            {
                ItemCanvas.GetComponent<Canvas>().enabled = true;

                itemText[i].gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
                text.gameObject.GetComponent<TextMeshProUGUI>().enabled = true;
                PickUp();
            }
        }*/
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myItemList.Add(closetObject.name);
            //ItemSearchAreaからアイテムを取り除く。
            ItemSearchArea.Remove(closetObject);
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
