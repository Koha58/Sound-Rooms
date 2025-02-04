using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ButtonC()
    {
        SceneManager.LoadScene("Stage1");
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
            SceneManager.LoadScene("Stage1");
        }

        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown("joystick button 1"))//B
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
