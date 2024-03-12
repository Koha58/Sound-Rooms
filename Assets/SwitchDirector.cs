using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirector : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    int onoff = 0;  //����p�i���g�������Ă��Ȃ����F0/���g�������Ă��鎞�F1�j
    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    // Update is called once per frame
    void Update()
    {
        //���N���b�N�Ō�����悤�ɂȂ�
        if (Input.GetMouseButtonDown(0))
        {
            Sphere.SetActive(true);//���g��\�����\��
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
            }
        }
    }
}
