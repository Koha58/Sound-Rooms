using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneButton : MonoBehaviour
{
    private GameObject OperationExplanation;
    private GameObject closeButton;

    // Start is called before the first frame update
    void Start()
    {
        OperationExplanation = GameObject.Find("Operation_Explanation");
        OperationExplanation.GetComponent<Image>().enabled = false;

        closeButton = GameObject.Find("closeButton");
        closeButton.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 7"))
        {
            OperationExplanation.GetComponent<Image>().enabled = true;
            closeButton.GetComponent<Image>().enabled = true;
        }

        if (Input.GetKeyDown("joystick button 1"))
        {
            closeButton.GetComponent<Image>().enabled = false;
            OperationExplanation.GetComponent<Image>().enabled = false;
        }
    }

    public void SettingButton()
    {
        OperationExplanation.GetComponent<Image>().enabled = true;
        closeButton.GetComponent<Image>().enabled = true;
    }

    public void CloseButton()
    {
        closeButton.GetComponent<Image>().enabled = false;
        OperationExplanation.GetComponent<Image>().enabled = false;
    }
}
