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

    public bool Visualization;
    void Start()
    {
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        foreach (var playerParts in childTransforms)
        {
            //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
            playerParts.gameObject.GetComponent<Renderer>().enabled = false;
        }

        Visualization = false;
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        //�����o�����ƂŌ�����悤�ɂȂ�
        if (levelMeter.nowdB > 0.0f)
        {
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            onoff = 1;  //�����Ă��邩��1
        }

        if (Visualization == false)
        {
            //�����o���Ă��Ȃ��Ƃ��A�v���C���[�������Ȃ�����
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
                {
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    onoff = 0;  //�����Ă��Ȃ�����0
                }
            }
        }
    }
}
