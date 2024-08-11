using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    Image StartButtonImage;
    Image TutorialButtonImage;

    public GameObject StartButton;
    public GameObject TutorialButton;

    public GameObject Cursor;
    public GameObject Cursor1;

    bool UPDOWN;

    // Start is called before the first frame update
    void Start()
    {
        StartButtonImage = StartButton.GetComponent<Image>();
        TutorialButtonImage = TutorialButton.GetComponent<Image>();

        StartButtonImage.color = new Color32(255, 255, 255, 45);
        TutorialButtonImage.color = new Color32(255, 255, 255, 45);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            StartButtonImage.color = new Color32(255, 255, 255, 255);
            TutorialButtonImage.color = new Color32(255, 255, 255, 45);
            Cursor.SetActive(true);
            Cursor1.SetActive(false);
            UPDOWN = true;
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            StartButtonImage.color = new Color32(255, 255, 255, 45);
            TutorialButtonImage.color = new Color32(255, 255, 255, 255);
            Cursor1.SetActive(true);
            Cursor.SetActive(false);
            UPDOWN = false;
        }
        if (UPDOWN == true)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                SceneManager.LoadScene("GameScene");
            }
        }

    }

    public void OnStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EnterStartButton()
    {
        StartButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitStartButton()
    {
        StartButtonImage.color = new Color32(255, 255, 255, 45);
    }

    public void EnterTutorialButton()
    {
        TutorialButtonImage.color = new Color32(255, 255, 255, 255);
    }

    public void ExitTutorialButton()
    {
        TutorialButtonImage.color = new Color32(255, 255, 255, 45);
    }
}
