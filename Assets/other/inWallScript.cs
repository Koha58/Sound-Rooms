using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inWallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    LevelMeter levelMeter;

    MeshRenderer Wall;

    void Start()
    {
        //�v���C���[�������Ă��Ȃ���
        bc = GetComponent<BoxCollider>();
        bc.enabled = false; //�ʂ蔲���\
        Wall = GetComponent<MeshRenderer>();
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
        
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        GameObject eobjG = GameObject.FindWithTag("EnemyG");
        EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
        if (EC.ONoff == 0)//||EFW.ONoff==0 )
        {
           
            bc.enabled = false;
        }
        if (EGC.ONoff == 0)//||EFW.ONoff==0 )
        {
          
            bc.enabled = false;
        }

        if (EC.ONoff == 1)//|| EFW.ONoff == 1)
        {
            bc.enabled = true;
        }

        if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
        {
            bc.enabled = true;
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Visualization"))
        {
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

                if (EC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    bc.enabled = true;
                }

                if (EGC.ONoff == 1)//|| EFW.ONoff == 1)
                {
                    bc.enabled = true;
                }
            
        }
        
    }
}
