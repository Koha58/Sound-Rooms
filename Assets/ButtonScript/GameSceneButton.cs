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

    public bool needAttention = false;
    private float AttentionCount = 0f;

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
        AttentionCount += Time.deltaTime;
        if (AttentionCount > 30)
        {
            needAttention = true;
        }

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

        if (needAttention)
        {
            AttentionTextUI.GetComponent<Image>().enabled = true;
            AttentionButton.GetComponent<Image>().enabled = true;
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
        AttentionUI.GetComponent<Image>().enabled = false;
        needAttention = false;
    }

    public void AttentionUIButton()
    {
        AttentionUI.GetComponent<Image>().enabled = true;
        closeButton.GetComponent<Image>().enabled = true;
    }
}
