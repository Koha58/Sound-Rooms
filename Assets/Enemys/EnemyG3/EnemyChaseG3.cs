using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseG3 : MonoBehaviour
{
    public Transform Player;//�v���C���[���Q��
    static public float Detection = 7f; //�v���C���[�����m����͈�
    static public float detectionPlayerG3;

    EnemySeen ES;

    static public bool EnemyChaseG03 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject eobj = GameObject.FindWithTag("EnemyG3"); //Player�I�u�W�F�N�g��T��
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����


        detectionPlayerG3 = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayerG3 <= Detection && ES.ONoff == 1 && (EnemyCubeG3.EnemybeforG3 == false))//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            EnemyChaseG03 = true;
        }
    }
}
