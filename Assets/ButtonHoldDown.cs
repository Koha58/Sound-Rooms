using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�}�E�X����������(�͈͎w��)
public class ButtonHoldDown : MonoBehaviour
{
    public GameObject[] GaugeArray = new GameObject[13];
    private int Gauge = 13;
    //�������Ɣ��肷��t���[�������Ǘ�
    private const int holdCheck = 60;
    //�L�[�������Ă���t���[�������L�^
    private float holdTime = 0;
    private float recoveryTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GaugeArray[Gauge-1].GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Gauge > 0 && holdCheck > holdTime)//�Z����
        {
            holdTime = 0;
            GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
            Gauge--;
        }
        
        if(Input.GetMouseButton(0) && Gauge > 0)//left�L�[�������Ă���Ԃ̎��Ԍv��
        {
            holdTime += Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && Gauge > 0 && holdTime > 1)//left�L�[�������Ă����
        {
            holdTime = 0;
            GaugeArray[Gauge - 1].GetComponent<Image>().enabled = false;
            Gauge--;
        }

        recoveryTime += Time.deltaTime;
        
        if(Gauge < 13 && recoveryTime > 60)//60�b���ƂɁu�P�v��
        {
            recoveryTime = 0;
            GaugeArray[Gauge].GetComponent<Image>().enabled = true;
            Gauge++;
        }
    }
}
