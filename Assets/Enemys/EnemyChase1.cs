using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase1 : MonoBehaviour
{

    public Transform Player;//�v���C���[���Q��
   static public  float Detection = 7f; //�v���C���[�����m����͈�


    EnemySeen ES;

    static public bool EnemyChase01 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj1 = GameObject.FindWithTag("Enemy1"); //Player�I�u�W�F�N�g��T��
        ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����


        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection && ES.ONoff == 1 && (EnemyCube.Enemybefor == false || EnemyCube1.Enemybefor1 == false))//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChase01 = true;
        }
    }
}
