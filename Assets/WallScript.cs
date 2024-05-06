using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǁ@�ʂ蔲���̐ݒ�

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    PlayerSeen PS;
    GameObject wobj;

    EnemySeen ES;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
    }

    void Update()
    {
        wobj = GameObject.Find("Player");
        PS = wobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        //�v���C���[�������Ă��鎞
        if (PS.onoff == 1)
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

        if (other.gameObject.CompareTag("Enemy1"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                Enemy1.targetPosition = Enemy1.GetRandomPosition();
                // Debug.Log("!");
            }
        }

        if (other.gameObject.CompareTag("EnemyG1"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
            ES = eobjG1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EnemyG1.targetPosition = EnemyG1.GetRandomPosition();
                // Debug.Log("!");
            }
        }

        if (other.gameObject.CompareTag("EnemyG2"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
            ES = eobjG2.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EnemyG2.targetPosition = EnemyG2.GetRandomPosition();
                // Debug.Log("!");
            }
        }

        if (other.gameObject.CompareTag("EnemyG3"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
            ES = eobjG3.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EnemyG3.targetPosition = EnemyG3.GetRandomPosition();
                // Debug.Log("!");
            }
        }

        if (other.gameObject.CompareTag("EnemyG4"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobjG4 = GameObject.FindWithTag("EnemyG4");
            ES = eobjG4.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                EnemyG4.targetPosition = EnemyG4.GetRandomPosition();
                // Debug.Log("!");
            }
        }
    }
}
