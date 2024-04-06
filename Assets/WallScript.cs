using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǁ@�ʂ蔲���̐ݒ�

public class WallScript : MonoBehaviour
{
    BoxCollider bc;
    GameObject Wall;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    EnemySeen ES;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
    }

    void Update()
    {
        //�v���C���[�������Ă��鎞
        if (Input.GetMouseButtonUp(0))
        {
            bc.enabled = true;  //�ʂ蔲���s��
            onoff = 1;  //�����Ă��邩��1
        }

        //�w�肵�����Ԃ��o�߂�����ʂ蔲���\�ɂ���
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                bc.enabled = false; //�ʂ蔲���\
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
               // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                Enemy.targetPosition = Enemy.GetRandomPosition();
               // Debug.Log("!");
            }
        }
    }
}
