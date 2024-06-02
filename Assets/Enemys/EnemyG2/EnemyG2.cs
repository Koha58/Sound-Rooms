using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyG2 : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public Transform Player;//�v���C���[���Q��
    public Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.025f;//Player��ǂ�������X�s�[�h
    private float Detection = 6f; //�v���C���[�����m����͈�
    private float detectionPlayer;
    private bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 


    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    public GameObject eobj;
    public EnemySeen ES; // EnemySeen�ɕt���Ă���X�N���v�g���擾


    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        eobj = GameObject.FindWithTag("EnemyG2");
        ES = eobj.GetComponent<EnemySeen>(); // EnemySeen�ɕt���Ă���X�N���v�g���擾


        if (ES.ONoff == 0)
        {
            EnemyChaseOnOff = false;
        }


        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalkG2", true);

        if (EnemyChaseOnOff == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 0)
            {
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                PS.onoff = 1;  //�����Ă��邩��1
            }

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ES.ONoff == 1)
            {
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            }

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Player�����m�͈͂ɓ����Ă��Ȃ��܂���Player�������Ă��Ȃ�
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
                animator.SetBool("EnemyWalkG2", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    targetPosition = GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }

    }

    private Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
            {
                EnemyChaseOnOff = true;

            }
        }
    }
}
