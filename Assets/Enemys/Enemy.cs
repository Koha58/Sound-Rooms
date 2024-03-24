using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 3f;
    static public Vector3 targetPosition;

    public Transform Player;//�v���C���[���Q��
    float Detection = 5f; //�v���C���[�����m����͈�
    float ChaseSpeed = 1f;//�ǂ�������X�s�[�h

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
        animator.SetBool("WalkEnemys", true);

        float detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= Detection)//�v���C���[�����m�͈͂ɓ�������
        {
            transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
            transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
        }

        // targetPosition�Ɍ������Ĉړ�����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);

        // targetPosition�ɓ���������V���������_���Ȉʒu��ݒ肷��
        if (transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))//�uPlayer�v�̃^�O�ɐڐG������
        {
            EnemyParturition.isHidden = false;
        }
    }

    public static Vector3 GetRandomPosition()
    {
        // �����_����x, y, z���W�𐶐�����
        float randomX = Random.Range(-10f, 10f);
        float randomY = 0.5f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
}
