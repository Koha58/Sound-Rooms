using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUIRight : MonoBehaviour
{
    float CloerTime;

    public Image mouseImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        mouseImage.color = new Color(255, 255, 255);
        if (EnemyController1.ImageOn)
        {
            CloerTime += Time.deltaTime;
            float red = 0;

            if (CloerTime > 0.5f)
            {
                red = 255;
                mouseImage.color = new Color(red, 0, 0);
                CloerTime = 0;
            }
            EnemyController1.ImageOn = false;
        }
    }
}
