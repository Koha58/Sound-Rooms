using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//�v���C���[�̉����E�s����

public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    //private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    [SerializeField] public Transform _parentTransform;
    LevelMeter levelMeter;


    void Start()
    {   
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        foreach (var playerParts in childTransforms)
        {
            //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
            playerParts.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        //���N���b�N�Ō�����悤�ɂȂ�
        if (/*Input.GetMouseButtonUp(0) ||*/ levelMeter.nowdB > 0.0f)
        {
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            onoff = 1;  //�����Ă��邩��1
        }

        //�w�肵�����Ԃ��o�߂�����v���C���[�������Ȃ�����
        if (onoff == 1)
        {
            //seentime += Time.deltaTime;
            if (/*seentime >= 10.0f*/levelMeter.nowdB <= 0.0f)
            {
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
                onoff = 0;  //�����Ă��Ȃ�����0
                //seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }

    }

}
