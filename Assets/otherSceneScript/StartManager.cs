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
        if(Input.GetKeyDown("joystick button 0"))
        {
            SceneManager.LoadScene("GameScene");
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
