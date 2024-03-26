using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̉����E�s����

public class PlayerSeen : MonoBehaviour
{
    SkinnedMeshRenderer mr;
    GameObject Player;  //�I�u�W�F�N�g���͓K�X�ύX

    public int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p


    void Start()
    {
        //�ŏ��͌����Ȃ����
        mr = GetComponent<SkinnedMeshRenderer>();
        mr.enabled = false; //�����Ȃ��i�����j
    }

    public void Update()
    {
        //���N���b�N�Ō�����悤�ɂȂ�
        if (Input.GetMouseButtonDown(0))
        {
            mr.enabled = true;  //������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }

        //�w�肵�����Ԃ��o�߂�����v���C���[�������Ȃ�����
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                mr.enabled = false; //�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }

    }

}
