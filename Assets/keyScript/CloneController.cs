using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    //public GameObject parentObject; //�I���W�i���̃I�u�W�F�N�g
    public int objcnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int objcnt = 0; objcnt < 3; objcnt++)
        {
            GameObject parentObject = GameObject.FindWithTag("Item");

            // �q�I�u�W�F�N�g�̐����擾
            int childCount = parentObject.transform.childCount;
            //Instantiate(childObject, new Vector3(-6.0f, 0, -6.0f), Quaternion.identity);
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = false;
            }
            /* �����ʒu�����߂� */
            float x = Random.Range(-15.0f, 15.0f);
            float z = Random.Range(-10.0f, 10.0f);
            float y = 0.0f;

            /* �I�u�W�F�N�g�𐶐����� */
            Instantiate(parentObject, new Vector3(x, y, z), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
