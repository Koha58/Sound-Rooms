using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public static GameObject Startbutton;
    AudioSource StartSound;

    // Start is called before the first frame update
    void Start()
    {
        Startbutton = GameObject.Find("StartButton");
        Startbutton.SetActive(false);
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStart()
    {
        StartSound.PlayOneShot(StartSound.clip);
        SceneManager.LoadScene("GameScene");
    }
}
