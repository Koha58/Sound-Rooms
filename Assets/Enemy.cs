using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float Range = 20f;//�����̍s���͈�
    private Vector3 StartPosition;//�����ʒu
    private Vector3 targetPosition;//�ڕW�ʒu


    public Transform Player;//�v���C���[���Q��
    public float Detection = 100f; //�v���C���[�����m����͈�
    public float ChaseSpeed = 0.01f;//�ǂ�������X�s�[�h

    MeshRenderer MR;
    GameObject Eneny;
    int ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���

    private float SoundTime;


    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {



        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection)//�v���C���[�����m�͈͂ɓ�������
        {
            transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
            transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����

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


        if (ONoff == 0)
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {
                MR.enabled = true;
                ONoff = 1;
                SoundTime = 0.0f;
            }

        }
        else if (ONoff == 1)
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                MR.enabled = false;
                ONoff = 0;
                Seetime = 0.0f;
            }
        }
    }
    /*
   private void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag =="Player" )
       {
           Destroy(other.gameObject);
       }
   }
   */
}
