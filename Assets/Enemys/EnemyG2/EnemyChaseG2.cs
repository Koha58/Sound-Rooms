using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG2 : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    static public float Detection = 7f; //�v���C���[�����m����͈�

    static public float detectionPlayerG2;

    EnemySeen ES;

    static public bool EnemyChaseG02 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG2"); //Player�I�u�W�F�N�g��T��
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����


        detectionPlayerG2 = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayerG2 <= Detection && ES.ONoff == 1 && (EnemyCubeG2.EnemybeforG2 == false))//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChaseG02 = true;
        }
    }
}
