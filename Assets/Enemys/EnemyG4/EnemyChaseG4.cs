using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG4 : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    static public float Detection = 6f; //�v���C���[�����m����͈�


    EnemySeen ES;

    static public bool EnemyChaseG04 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG4"); //Player�I�u�W�F�N�g��T��
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����


        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection && ES.ONoff == 1 && (EnemyCubeG4.EnemybeforG4 == false))//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChaseG04 = true;
        }
    }
}
