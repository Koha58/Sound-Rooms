using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

//EnemyÇ…âπÇÇ‘Ç¬ÇØÇÈãììÆ
public class EnemyAttack : MonoBehaviour
{

    public TextMeshProUGUI keyCountText;
    public int count;
    [SerializeField] AudioSource PickupSound;
    [SerializeField] GameObject ControllerKeyUI;
    [SerializeField] GameObject KeyboardKeyUI;
    GameObject Key;

    LevelMeter levelMeter;

    bool deviceCheck;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SetCountText();
        PickupSound = GetComponent<AudioSource>();
        Key = GameObject.FindGameObjectWithTag("Key");

        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        ControllerKeyUI.GetComponent<Image>().enabled = false;
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            //RigidbodyÇéÊìæ
            var rb = other.GetComponent<Rigidbody>();

            //à⁄ìÆÅAâÒì]Çâ¬î\Ç…Ç∑ÇÈ
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
        else if (other.CompareTag("Key"))
        {
            if(deviceCheck)
            {
                ControllerKeyUI.GetComponent<Image>().enabled = true;
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
            }
            else
            {
                KeyboardKeyUI.GetComponent<Image>().enabled = true;
                ControllerKeyUI.GetComponent<Image>().enabled = false;
                Debug.Log("a");
            }

            if(Input.GetMouseButtonUp(0) || Input.GetKeyUp("joystick button 0"))
            {
                KeyboardKeyUI.GetComponent<Image>().enabled = false;
                ControllerKeyUI.GetComponent<Image>().enabled = false;
                Key.SetActive(false);
                PickupSound.PlayOneShot(PickupSound.clip);
                count++;
                SetCountText();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        KeyboardKeyUI.GetComponent<Image>().enabled = false;
        ControllerKeyUI.GetComponent<Image>().enabled = false;
    }

    public void SetCountText()
    {
        keyCountText.text = count.ToString();
    }

    
}
