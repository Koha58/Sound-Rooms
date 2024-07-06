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
        if(other.CompareTag("Area1check"))
        {
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount.state = 1;
        }
        else if(other.CompareTag("Area2check"))
        {
            uiCount.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount2.state = 1;
        }
        else if (other.CompareTag("Area3check"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount3.state = 1;
        }
        else if (other.CompareTag("Area4check"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount5.state = 0;
            uiCount4.state = 1;
        }
        else if (other.CompareTag("centralAreacheck"))
        {
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 1;
        }
    }

}
