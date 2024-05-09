using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    private float Detection = 6f; //�v���C���[�����m����͈�
    public  bool EnemyChaseOnOff;//Player�̒ǐՂ�ONOFF 

    public  GameObject eobj;
    public  GameObject eobjEC;
    public  EnemySeen ES;
  

    // Start is called before the first frame update
    private  void Start()
    {
        eobj = GameObject.FindWithTag("Enemy");
       
        ES = eobj.GetComponent<EnemySeen>();//EnemySeen�ɕt���Ă���X�N���v�g���擾

        EnemyChaseOnOff=false ;
    }

    // Update is called once per frame
    private void Update()
    {
        ES = eobj.GetComponent<EnemySeen>();//EnemySeen�ɕt���Ă���X�N���v�g���擾

        float detectionPlayer; detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChaseOnOff = true;
        }
    }
}
