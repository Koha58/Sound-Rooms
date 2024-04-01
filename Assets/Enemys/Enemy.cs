using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public GameObject characterPrefab; // ��������L�����N�^�[�̃v���n�u
    float speed = 3f;
    static public Vector3 targetPosition;

    public Transform Player;//�v���C���[���Q��
    float Detection = 5f; //�v���C���[�����m����͈�
    float ChaseSpeed = 1f;//�ǂ�������X�s�[�h

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    // [SerializeField] GameObject Sphere;

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
        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (PlayerSeen.onoff==1)//�v���C���[�������Ă��鎞
        {
            if (detectionPlayer <= Detection)//�v���C���[�����m�͈͂ɓ�������
            {
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            }
        }

        // targetPosition�Ɍ������Ĉړ�����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Destroy(gameObject);
            Enemyincrease.isHidden = false;
        }
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
