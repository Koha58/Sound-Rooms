using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSearch : MonoBehaviour
{
    //�@�߂��ɂ���A�C�e���̃��X�g
    [SerializeField] private List<GameObject> itemList;
    //�@���b�Z�[�W�\���p�L�����o�X
    public GameObject itemMessageCanvas;


    //�@�A�C�e�����T�[�`�G���A���ɓ�������i�ꍇ�ɂ���Ă�OnTriggerStay���g�����������������H�j
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            //�@E�L�[�Ŏ擾����
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetItem(col.gameObject);
                itemMessageCanvas.GetComponentInChildren<Text>().text = "E�Ŏ擾";
                itemMessageCanvas.SetActive(true);
            }
        }
    }

    //�@�A�C�e�����T�[�`�G���A�O�ɏo����
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Item")
        {
            //�@�A�C�e�����X�g�ɑ��݂��Ă�����A�C�e�����X�g����폜
            if (itemList.Contains(col.gameObject))
            {
                itemList.Remove(col.gameObject);
                //�@�A�C�e�����͈͓��ɂȂ��Ȃ������l���̏�ԕύX
                if (itemList.Count <= 0)
                {
                    itemMessageCanvas.SetActive(false);
                }
            }
        }
    }

    //�@�A�C�e�����X�g�ɃA�C�e����ǉ����鏈��
    void SetItem(GameObject addItem)
    {
        //�@���łɓ����A�C�e�������邩�ǂ���
        if (!itemList.Contains(addItem))
        {
            itemList.Add(addItem);
        }
    }

    //�@�A�C�e�����X�g����A�C�e�����폜���鏈��
    public void DeleteItem(GameObject deleteItem)
    {
        if (itemList.Contains(deleteItem))
        {
            itemList.Remove(deleteItem);
        }
    }

    //�@�蓮���[�h�̏ꍇ��ԋ߂��A�C�e����T���擾����
    public void SelectItem()
    {
            //�@��ł����m�G���A���ɃA�C�e���������
            if (itemList.Count >= 1)
            {
                //�@�A�C�e�����X�g�̐擪�ɂ���A�C�e���������l�ɂ���
                Transform near = itemList[0].transform;
                float distance = Vector3.Distance(transform.root.position, near.position);

                //�@��ԋ߂��A�C�e����T��
                foreach (var item in itemList)
                {
                    if (Vector3.Distance(transform.root.position, item.transform.position) < distance)
                    {
                        near = item.transform;
                        distance = Vector3.Distance(transform.root.position, item.transform.position);
                    }
                }

                //�@��ԋ߂��A�C�e�����擾�A�A�C�e�����X�g����폜�A�A�C�e���Q�[���I�u�W�F�N�g�̍폜
                itemMessageCanvas.GetComponentInChildren<Text>().text = "";
                itemMessageCanvas.SetActive(false);
                DeleteItem(near.gameObject);
                Destroy(near.gameObject);
            }
    }
}
