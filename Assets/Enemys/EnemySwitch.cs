using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{
    [SerializeField] GameObject Sphere;
    //private float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySeen.ONoff == 0)//�����Ȃ��Ƃ�
        {
            Sphere.SetActive(false);//���g��\�����\��
        }

        //�w�肵�����Ԃ��o�߂����特�g�������Ȃ�����
        if (EnemySeen.ONoff == 1)
        {

            Sphere.SetActive(true);//���g�\������\��

        }
    }
}
