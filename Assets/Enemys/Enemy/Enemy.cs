using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public Transform Player;//�v���C���[���Q��
    public  Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.025f;//Player��ǂ�������X�s�[�h

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
       
    }

    // Update is called once per frame
    private  void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeen ES = eobj.GetComponent<EnemySeen>(); // EnemySeen�ɕt���Ă���X�N���v�g���擾
        EnemyCube EC = eobj.GetComponent<EnemyCube>();// EnemyCube�ɕt���Ă���X�N���v�g���擾
        EnemyChase EChase = eobj.GetComponent<EnemyChase>();// EnemyChase�ɕt���Ă���X�N���v�g���擾

        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        if (EnemyChase.EnemyChaseOnOff==true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 0)
            {
                for (int i = 0; i < 5; i++)//�q�I�u�W�F�N�g�̐����擾
                {
                    Transform childTransform = PS.parentObject.transform.GetChild(i);
                    PS.childObject = childTransform.gameObject;
                    PS.childObject.GetComponent<Renderer>().enabled = true;//������
                }
                PS.onoff = 1;  //�����Ă��邩��1
            }

            if (PS.onoff == 1 && EnemyChase.EnemyChaseOnOff == true )//Player�������Ă���Ƃ�
            {
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            }
           
        }
        else if (EnemyChase.EnemyChaseOnOff == false || PS.onoff == 0 )//Player�����m�͈͂ɓ����Ă��Ȃ��܂���Player�������Ă��Ȃ�
        {   
                // targetPosition�Ɍ������Ĉړ�����
                 transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                 transform.LookAt(targetPosition);
        }

        // targetPosition�ɓ���������V���������_���Ȉʒu��ݒ肷��
        if (transform.position == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                animator.SetBool("EnemyWalk", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    targetPosition = GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }

    private  Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}
