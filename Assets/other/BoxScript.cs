using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    LevelMeter levelMeter;

    public bool RingOnOff;

    MeshRenderer Box;

    float BoxCount;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
        Box = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        //�v���C���[�������Ă��鎞
        if (levelMeter.nowdB > 0.0f)
        {
            bc.enabled = true;  //�ʂ蔲���s��
            onoff = 1;  //�����Ă��邩��1
        }

        //�v���C���[�������Ă��Ȃ��Ƃ�
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
                bc.enabled = false; //�ʂ蔲���\
                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }

        if (Box.enabled == true)
        {
            BoxCount += Time.deltaTime;
            if (BoxCount >= 7.0f)
            {
                bc.enabled = false;
                Box.enabled = false;
                BoxCount = 0;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            Box.enabled = true;
            bc.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            bc.enabled = false;
            Box.enabled = false;
        }
    }
}
