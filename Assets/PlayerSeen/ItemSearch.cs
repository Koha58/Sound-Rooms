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
    public  GameObject ItemGetText;
    ItemSeen IS;
    [SerializeField]AudioSource PickupSound;

    private void Start()
    {
        count = 0;
        SetCountText();
        PickupSound = GetComponent<AudioSource>();

        //ItemGetText = GameObject.Find("ItemGetUI");
        //ItemGetText.GetComponent<Image>().enabled = false;
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
        //ItemGetText = GameObject.Find("ItemGetUI");
        ItemSearchArea = GameObject.FindGameObjectsWithTag("Item").ToList();
        //一番近いアイテムを取得する
        float closetDistance = 1000000;
        for (int i = 0; i < ItemSearchArea.Count; i++)
        {
            if (ItemSearchArea[i] == null)
            {
                ItemSearchArea.Remove(ItemSearchArea[i]);
                return;
            }
            if (closetObject != null)
            {
                float distance = Vector3.Distance(transform.position, closetObject.transform.position);
                if (closetDistance > distance)
                {
                    closetDistance = distance;
                    //closetObject = ItemSearchArea[i].gameObject;
                }
                //一定距離離れたらItemSearchAreaからオブジェクトを取り除く。
                if (distance > 6f || IS.onoff == 0)
                {
                    if (closetObject == ItemSearchArea[i].gameObject)
                    {
                        //closetObject = null;
                    }
                    ItemSearchArea.Remove(ItemSearchArea[i]);
                }
            }
        }
        //最も近いアイテムが一定の距離内にある場合、アイテムの説明UIを表示。Eキーを押すと拾える。
        //if (closetObject == null) return;
        //if (closetDistance < 1.5f && IS.onoff == 1)
        //{
        //    ItemGetText.GetComponent<Image>().enabled = true;
        //    PickUp();
        //}
        //if(closetDistance >= 1.5f || IS.onoff == 0)
        //{
        //    ItemGetText.GetComponent<Image>().enabled = false;
        //}
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 2"))
        {
            PickupSound.PlayOneShot(PickupSound.clip);
            myItemList.Add(closetObject.name);
            //ItemSearchAreaからアイテムを取り除く。
            ItemSearchArea.Remove(closetObject);
            Destroy(closetObject, 0.5f);
            closetObject = null;
            count += 1;
            SetCountText();
            ItemSearchArea.Clear();
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }
}
