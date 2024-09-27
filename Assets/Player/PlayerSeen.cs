using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//�v���C���[�̉����E�s����

public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    [SerializeField] public Transform _parentTransform;
    LevelMeter levelMeter;
    public bool piano;
    int pianocnt;
    public bool zero;
    AudioSetting AS;

    public bool Visualization;
    void Start()
    {
        onoff = 0;
        Visualization = false;

        piano = false;
        pianocnt = 0;
        zero = false;
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        //�����o�����ƂŌ�����悤�ɂȂ�
        if (levelMeter.nowdB > 0.0f && !piano)
        {
            onoff = 1;  //�����Ă��邩��1
        }

        if (Visualization == false)
        {
            //�����o���Ă��Ȃ��Ƃ��A�v���C���[�������Ȃ�����
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f && !piano)
                {
                    onoff = 0;  //�����Ă��Ȃ�����0
                }
            }
        }

        //�s�A�m��������
        if (piano)
        {
            onoff = 1;

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>();
            if (AS.BGMSlider.value == -80)
            {
                zero = true;
                piano = false;
                onoff = 0;
            }
            else
            {
                piano = true;
                zero = false;
                onoff = 1;  //�����Ă��邩��1
            }
        }
        else
        {
            zero = false;
            if(pianocnt % 2 != 0 && AS.BGMSlider.value != -80)
            {
                piano = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            pianocnt++;
            if (!zero)
            {
                piano = true;

                if (pianocnt % 2 == 0)
                {
                    piano = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RoomOut"))
        {
            onoff = 0;
        }
    }
}
