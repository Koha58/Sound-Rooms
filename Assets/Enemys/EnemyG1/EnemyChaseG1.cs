using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG1 : MonoBehaviour
{
   public Transform Player;//�v���C���[���Q��
   static public float Detection = 7f; //�v���C���[�����m����͈�
   static public float detectionPlayerG1;

    EnemySeen ES;

    static public bool EnemyChaseG01 = false;
    static public bool EnemyChaseG01touch = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG1"); //Player�I�u�W�F�N�g��T��
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����

        /*
        detectionPlayerG1 = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayerG1 <= Detection && ES.ONoff == 1 && (EnemyCubeG1.EnemybeforG1 == false))//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChaseG01 = true;
        }
        
       */
    }
}
