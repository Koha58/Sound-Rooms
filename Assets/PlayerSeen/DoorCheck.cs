using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�C�e���擾
public class DoorCheck : MonoBehaviour
{
    Animator anim;
    LevelMeter levelMeter;

    bool OnOff;

    void Start()
    {
        //�ŏ��͌����Ȃ����
        GetComponent<Collider>().enabled = false;
        OnOff = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        //�����o�����Ƃŉ���
        if (levelMeter.nowdB > 0.0f)
        {
            GetComponent<Collider>().enabled = true;//������i�L���j
            OnOff = true;
        }

        if (OnOff == true)
        {
            if (levelMeter.nowdB == 0.0f)
            {
                GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                OnOff = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutoDoor"))
        {
            anim = other.GetComponent<Animator>();
            anim.SetBool("Open", true);
        }
    }
}
