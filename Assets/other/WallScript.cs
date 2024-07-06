using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

//�ǁ@�ʂ蔲���̐ݒ�

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    LevelMeter levelMeter;

    MeshRenderer Wall;

    float WallCount;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
        Wall = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        //�v���C���[�������Ă��鎞
        if (levelMeter.nowdB > 0.0f)
        {
            bc.enabled = true;  //�ʂ蔲���s��
            onoff = 1;  //�����Ă��邩��1
        }

        //�v���C���[�������Ă��Ȃ��Ƃ�
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
                bc.enabled = false; //�ʂ蔲���\
                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }
  
        if (Wall.enabled == true)
        {
            WallCount += Time.deltaTime;
            if (WallCount >= 7.0f)
            {
                bc.enabled = false;
                Wall.enabled = false;
                WallCount = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            if (PS.onoff == 1)
            {
                Wall.enabled = true;
                bc.enabled = true;
                //Debug.Log("Wall");
            }
        }

        if (other.gameObject.CompareTag("Visualization"))
        {

            if (other.gameObject.CompareTag("EnemyWall"))
            {
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC1.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }
            }

            if (other.gameObject.CompareTag("EnemyGwall"))
            {
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EGC1.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }


                GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
                EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //Enemy�ɕt���Ă���X�N���v�g���擾

                if (EGC2.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
                EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //Enemy�ɕt���Ă���X�N���v�g���擾

                if (EGC3.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                   // Debug.Log("1.6");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Visualization"))
        {
            bc.enabled = false;
            Wall.enabled = false;
        }
    }
}


