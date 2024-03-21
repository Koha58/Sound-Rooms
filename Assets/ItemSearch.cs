using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

//�A�C�e���擾
public class ItemSearch : MonoBehaviour
{
    public GameObject closetObject;
    public List<GameObject> ItemSearchArea = new List<GameObject>();
    public List<string> myItemList = new List<string>();
    public TextMeshProUGUI keyCountText;
    public int count;
    //public OverHeadMsgCreater overHeadMsgCreater;
    
    private void Start()
    {
        count = 0;
        SetCountText();
        //overHeadMsgCreater = GetComponent<OverHeadMsgCreater>();
        //overHeadMsgCreater.OnDisable();
    }
    private void Update()
    {
        CaluculateClosetObject();
    }

    void CaluculateClosetObject()
    {
        //��ԋ߂��A�C�e�����擾����
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
            //��苗�����ꂽ��ItemSearchArea����I�u�W�F�N�g����菜���B
            if (distance > 6f)
            {
                if (closetObject == ItemSearchArea[i].gameObject)
                {
                    closetObject = null;
                }
                ItemSearchArea.Remove(ItemSearchArea[i]);
            }
        }

        //�ł��߂��A�C�e�������̋������ɂ���ꍇ�A�A�C�e���̐���UI��\���BE�L�[�������ƏE����B
        if (closetObject == null) return;
        if (closetDistance < 1.5f)
        {
            //�A�C�e�����UI�\��
            //overHeadMsgCreater.OnEnable();
            PickUp();
        }
    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            myItemList.Add(closetObject.name);
            //ItemSearchArea����A�C�e������菜���B
            ItemSearchArea.Remove(closetObject);
            Destroy(closetObject, 0.5f);
            closetObject = null;
            //�A�C�e�����UI��\��
            //overHeadMsgCreater.OnDisable();
            count += 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }
}
