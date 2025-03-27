using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static InputDeviceManager;

public class SkipButton : MonoBehaviour
{
    public GameObject Sikp;
    private GameInputSystem inputActions;

    bool deviceCheck;

    private bool isXButton;

    private void Awake()
    {
        // Input Systemのインスタンスを作成
        inputActions = new GameInputSystem();

        //Aボタン
        inputActions.UI.XButton.performed += ctx => isXButton = true;
        inputActions.UI.XButton.canceled += ctx => isXButton = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;

        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        if (!deviceCheck)
        {
            Sikp.SetActive(false);
        }
        else
        {
            Sikp.SetActive(true);
        }


        if (isXButton == true)
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }

    public void OnClick()
    {


        SceneManager.LoadScene("TutorialScene");
    }

}
