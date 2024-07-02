using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//�A�C�e���擾
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
        IS = iobj.GetComponent<ItemSeen>(); //�t���Ă���X�N���v�g���擾
        if (IS.onoff == 1)
        {
            CaluculateClosetObject();
        }
    }

    void CaluculateClosetObject()
    {
        //ItemGetText = GameObject.Find("ItemGetUI");
        ItemSearchArea = GameObject.FindGameObjectsWithTag("Item").ToList();
        //��ԋ߂��A�C�e�����擾����
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
                //��苗�����ꂽ��ItemSearchArea����I�u�W�F�N�g����菜���B
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
        //�ł��߂��A�C�e�������̋������ɂ���ꍇ�A�A�C�e���̐���UI��\���BE�L�[�������ƏE����B
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
            //ItemSearchArea����A�C�e������菜���B
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
