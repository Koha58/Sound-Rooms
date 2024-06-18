using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ǁ@�ʂ蔲���̐ݒ�

public class WallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    LevelMeter levelMeter;

    //  EnemySeen ES;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
    }

    void Update()
    {

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

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

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
            /*
            GameObject Chase = GameObject.FindWithTag("Chase");
            EnemyChase EC = Chase.GetComponent<EnemyChase>();

            EC.Wall = true;
            */

            if (EC.ONoff == 0 )//||EFW.ONoff==0 )
            {
                bc.enabled = false;
            }
            else if (EC.ONoff == 1)//|| EFW.ONoff == 1)
            {
                bc.enabled = true;
            }
        }
      
        if (other.gameObject.CompareTag("EnemyG"))
        {
           
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemysG EG = eobjG.GetComponent<EnemysG>(); //Enemy�ɕt���Ă���X�N���v�g���擾
      

            if (EG.ONoff == 0)
            {
                bc.enabled = false;
                // Debug.Log("?");
            }
            else if (EG.ONoff == 1)
            {
                bc.enabled = true;
                EG.targetPosition = GetRandomPosition();
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


