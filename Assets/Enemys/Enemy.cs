using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    float speed = 1f;
    static public Vector3 targetPosition;

    public Transform Player;//�v���C���[���Q��
    float Detection = 1f; //�v���C���[�����m����͈�
    float ChaseSpeed = 0.01f;//�ǂ�������X�s�[�h

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    PlayerSeen PS;
    EnemySeen ES;

    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        GameObject eobj = GameObject.Find("Enemy"); //Player�I�u�W�F�N�g��T��
        ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
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
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            }
            else if (detectionPlayer >= Detection|| PS.onoff == 0)//Player�����m�͈͂ɓ����Ă��Ȃ��܂���Player�������Ă��Ȃ�
            {
                // targetPosition�Ɍ������Ĉړ�����
                 transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                 transform.LookAt(targetPosition);
            }

        // targetPosition�Ɍ������Ĉړ�����
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        // transform.LookAt(targetPosition);

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
    private void OnDestroy()//OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
            //Destroy(gameObject);
            Enemyincrease.isHidden = false;
        //}
    }

    public static Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}
