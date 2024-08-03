using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

public class StartDeviceCheck : MonoBehaviour
{
    GameObject JapaneseStartkey;
    GameObject JapaneseStartButton;
    // Start is called before the first frame update
    void Start()
    {
        JapaneseStartkey = GameObject.Find("StartButton_JapaneseKey");
        JapaneseStartButton = GameObject.Find("StartButton_Japanese");
        JapaneseStartkey.GetComponent<Image>().enabled = false;
        JapaneseStartButton.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && JapaneseStartButton != null)
        {
            JapaneseStartButton.GetComponent<Image>().enabled = true;
            JapaneseStartkey.GetComponent<Image>().enabled = false;
        }
        else if(InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && JapaneseStartkey != null)
        {
            JapaneseStartkey.GetComponent<Image>().enabled = true;
            JapaneseStartButton.GetComponent<Image>().enabled = false;
        }
    }
}
