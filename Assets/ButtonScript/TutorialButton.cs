using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    void Start()
    {

    }

    public void ButtonC()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("joystick button 0"))//A
        {
            SceneManager.LoadScene("TutorialScene");
        }

        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))//B
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
