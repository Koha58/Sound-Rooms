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
        if (other.gameObject.CompareTag("EnemyParts"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            Enemy E = eobj.GetComponent<Enemy>(); //Enemy�ɕt���Ă���X�N���v�g���擾
            EnemySeen ES = eobj.GetComponent<EnemySeen>(); //EnemySeen�ɕt���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
              
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                E.targetPosition = GetRandomPosition();
              
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
            EnemyG1 E = eobjG1.GetComponent<EnemyG1>(); //Enemy�ɕt���Ă���X�N���v�g���擾
            ES = eobjG1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (ES.ONoff == 1)
            {
                bc.enabled = true;
                E.targetPosition = GetRandomPosition();
            }
        }
    }
    private Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}


