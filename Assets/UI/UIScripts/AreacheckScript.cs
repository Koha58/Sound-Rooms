using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreacheckScript : MonoBehaviour
{
    [SerializeField]
    private SlideUIControll uiCount;
    [SerializeField]
    private SlideUIControll uiCount2;
    [SerializeField]
    private SlideUIControll uiCount3;
    [SerializeField]
    private SlideUIControll uiCount4;
    [SerializeField]
    private SlideUIControll uiCount5;
    [SerializeField]
    private SlideUIControll uiCount6;
    [SerializeField]
    private SlideUIControll uiCount7;
    [SerializeField]
    private SlideUIControll uiCount8;

    // Start is called before the first frame update
    void Start()
    {
        uiCount.state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AreaAcheck"))
        {
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount.state = 1;
        }
        else if(other.CompareTag("AreaBcheck"))
        {
            uiCount.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount2.state = 1;
        }
        else if (other.CompareTag("AreaCcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount3.state = 1;
        }
        else if (other.CompareTag("AreaDcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount4.state = 1;
        }
        else if (other.CompareTag("AreaEcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount5.state = 1;
        }
        else if (other.CompareTag("AreaFcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount6.state = 1;
        }
        else if (other.CompareTag("AreaGcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount8.state = 0;
            uiCount7.state = 1;
        }
        else if (other.CompareTag("AreaHcheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 1;
        }
    }

}
