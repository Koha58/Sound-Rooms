using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialClearScene : MonoBehaviour
{
    private float stayTime = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        stayTime += Time.deltaTime;

        if (stayTime > 10f)
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
