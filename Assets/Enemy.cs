using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float Range = 2f;//�����̍s���͈�
    private Vector3 StartPosition;//�����ʒu
    private Vector3 targetPosition;//�ڕW�ʒu


    public Transform Player;//�v���C���[���Q��
    public float Detection = 10f; //�v���C���[�����m����͈�

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;


    }

    // Update is called once per frame
    void Update()
    {


        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection)
        {
            transform.LookAt(Player.position);//�v���C���[�̕���������

        }



        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);//�ڕW�ʒu�Ɍ������Đi��



        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)//�ڕW�ʒu�ɓ��B������V�����ڕW�ʒu��ݒ肷��
        {
            targetPosition = GetRandomPositionlnRange();
        }


        Vector3 GetRandomPositionlnRange()//�����ʒu���烉���_���ȕ����Ɏw��͈͓��̋����������_���Ɍ��߂ĖڕW���v�Z����
        {
            Vector3 randomdetection = Random.insideUnitSphere * Range;
            randomdetection.y = StartPosition.y;//Y�̍����͏����ʒu�Ɠ����ɂ���
            return randomdetection;
        }



    }
}
