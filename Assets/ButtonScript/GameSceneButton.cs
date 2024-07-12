using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneButton : MonoBehaviour
{
    private GameObject OperationExplanation;
    private GameObject closeButton;

    private GameObject AttentionUI;
    private GameObject AttentionTextUI;
    private GameObject AttentionButton;

    public static bool needAttention = false;

    // Start is called before the first frame update
    void Start()
    {
        OperationExplanation = GameObject.Find("Operation_Explanation");
        OperationExplanation.GetComponent<Image>().enabled = false;

        closeButton = GameObject.Find("closeButton");
        closeButton.GetComponent<Image>().enabled = false;

        AttentionUI = GameObject.Find("AttentionUI");
        AttentionUI.GetComponent<Image>().enabled = false;

        AttentionTextUI = GameObject.Find("AttentionTextUI");
        AttentionTextUI.GetComponent<Image>().enabled = false;

        AttentionButton = GameObject.Find("AttentionButton");
        AttentionButton.GetComponent<Image>().enabled = false;

        needAttention = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("joystick button 7"))//メニュー ボタン 
        {
            OperationExplanation.GetComponent<Image>().enabled = true;
            closeButton.GetComponent<Image>().enabled = true;
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.B)||Input.GetKeyDown("joystick button 1"))//B
        {
            closeButton.GetComponent<Image>().enabled = false;
            OperationExplanation.GetComponent<Image>().enabled = false;
            AttentionUI.GetComponent<Image>().enabled = false;
            Time.timeScale = 1;
        }

        if (needAttention)
        {
            if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown("joystick button 3"))//Y
            {
                AttentionUI.GetComponent<Image>().enabled = true;
                closeButton.GetComponent<Image>().enabled = true;
                Time.timeScale = 0;
            }

            AttentionTextUI.GetComponent<Image>().enabled = true;
            AttentionButton.GetComponent<Image>().enabled = true;
        }

    }

    public void SettingButton()
    {
        OperationExplanation.GetComponent<Image>().enabled = true;
        closeButton.GetComponent<Image>().enabled = true;
        Time.timeScale = 0;
    }

    public void CloseButton()
    {
        closeButton.GetComponent<Image>().enabled = false;
        OperationExplanation.GetComponent<Image>().enabled = false;
        AttentionUI.GetComponent<Image>().enabled = false;
        Time.timeScale = 1;
    }

    public void AttentionUIButton()
    {
        AttentionUI.GetComponent<Image>().enabled = true;
        closeButton.GetComponent<Image>().enabled = true;
        Time.timeScale = 0;
    }
}
