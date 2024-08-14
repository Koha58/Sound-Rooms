using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

public class StartDeviceCheck : MonoBehaviour
{
    GameObject Cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor = GameObject.Find("Cursor");
       // Cursor.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = true;
        }
        else if(InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = false;
        }
    }
}
