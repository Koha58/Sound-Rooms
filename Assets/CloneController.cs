using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    //public GameObject parentObject; //�I���W�i���̃I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        GameObject parentObject = GameObject.Find("key 1");

        // �q�I�u�W�F�N�g�̐����擾
        int childCount = parentObject.transform.childCount;
        //Instantiate(childObject, new Vector3(-6.0f, 0, -6.0f), Quaternion.identity);
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;

            Instantiate(childObject, new Vector3(-6.0f, 0, -6.0f), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
