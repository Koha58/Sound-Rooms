using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���g�̉����E�s����

public class SwitchDirector : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    [SerializeField] GameObject MovingSphere;
    int onoff = 0;  //����p�i���g�������Ă��Ȃ����F0/���g�������Ă��鎞�F1�j
    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    PlayerRun PR;
    GameObject mobj;

    // Update is called once per frame
    void Update()
    {
        mobj = GameObject.Find("Player");
        PR = mobj.GetComponent<PlayerRun>(); //�t���Ă���X�N���v�g���擾
        //���N���b�N�Ō�����悤�ɂȂ�
        if (Input.GetMouseButtonUp(0))
        {
            if (PR.moving == 0)
            {
                Sphere.SetActive(true);//���g��\�����\��
            }
            if (PR.moving == 1)
            {
                MovingSphere.SetActive(true);//���g��\�����\��
            }
            onoff = 1;  //�����Ă��邩��1
        }

        //�w�肵�����Ԃ��o�߂����特�g�������Ȃ�����
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
                Sphere.SetActive(false);//���g�\������\��
                MovingSphere.SetActive(false);//���g�\������\��
            }
         
            if (PR.moving == 0 && MovingSphere.activeSelf == true)
            {
                MovingSphere.SetActive(false);//���g�\������\��
                Sphere.SetActive(true);//���g��\�����\��
            }
            if (PR.moving == 1 && Sphere.activeSelf == true)
            {
                MovingSphere.SetActive(true);//���g��\�����\��
                Sphere.SetActive(false);//���g�\������\��
            }
        }
    }
}
