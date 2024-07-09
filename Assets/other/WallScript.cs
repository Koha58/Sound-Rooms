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

                GameObject eobj2 = GameObject.FindWithTag("Enemy2");
                EnemyController2 EC2 = eobj2.GetComponent<EnemyController2>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC2.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj3 = GameObject.FindWithTag("Enemy3");
                EnemyController3 EC3 = eobj3.GetComponent<EnemyController3>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC3.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj4 = GameObject.FindWithTag("Enemy4");
                EnemyController4 EC4 = eobj4.GetComponent<EnemyController4>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC4.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj5 = GameObject.FindWithTag("Enemy5");
                EnemyController5 EC5 = eobj5.GetComponent<EnemyController5>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC5.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj6 = GameObject.FindWithTag("Enemy6");
                EnemyController6 EC6= eobj6.GetComponent<EnemyController6>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC6.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj7 = GameObject.FindWithTag("Enemy7");
                EnemyController7 EC7 = eobj7.GetComponent<EnemyController7>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC7.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj8 = GameObject.FindWithTag("Enemy8");
                EnemyController8 EC8 = eobj8.GetComponent<EnemyController8>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC8.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj9 = GameObject.FindWithTag("Enemy9");
                EnemyController9 EC9 = eobj9.GetComponent<EnemyController9>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC9.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    Wall.enabled = true;
                    bc.enabled = true;
                }

                GameObject eobj10 = GameObject.FindWithTag("Enemy10");
                EnemyController10 EC10 = eobj10.GetComponent<EnemyController10>(); //Enemy�ɕt���Ă���X�N���v�g���擾
                if (EC10.ONoff == 1)//|| EFW.ONoff == 1)
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


