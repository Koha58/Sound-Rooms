using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

public class GameOverDeviceCheck : MonoBehaviour
{
    public GameObject RetryKey;
    public GameObject RetryButton;
    public GameObject JapaneseBackButton;
    public GameObject JapaneseBackKey;

    // Start is called before the first frame update
    void Start()
    {
        RetryKey.GetComponent<Image>().enabled = false;
        RetryButton.GetComponent<Image>().enabled = false;
        JapaneseBackButton.GetComponent<Image>().enabled = false;
        JapaneseBackKey.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && RetryButton != null)
        {
            RetryButton.GetComponent<Image>().enabled = true;
            RetryKey.GetComponent<Image>().enabled = false;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && RetryKey != null)
        {
            RetryKey.GetComponent<Image>().enabled = true;
            RetryButton.GetComponent<Image>().enabled = false;
        }

        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && JapaneseBackButton != null)
        {
            JapaneseBackButton.GetComponent<Image>().enabled = true;
            JapaneseBackKey.GetComponent<Image>().enabled = false;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && JapaneseBackKey != null)
        {
            JapaneseBackKey.GetComponent<Image>().enabled = true;
            JapaneseBackButton.GetComponent<Image>().enabled = false;
        }
    }
}
