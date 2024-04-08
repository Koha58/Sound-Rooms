using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] patrolPoints; // ����|�C���g�̔z��
    public float patrolInterval = 2f; // ����̊Ԋu
    public float chaseSpeed = 5f; // Player��ǂ������鑬�x

    private int currentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X
    private Transform target; // Player�̈ʒu
    private bool isPatrolling = true; // ���񒆂��ǂ���

    private Animator animator; // �A�j���[�^�[�R���|�[�l���g

    void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextPatrolPoint();
    }

    void Update()
    {
        if (target != null)
        {
            // Player������ꍇ�͒ǂ�������
            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
        else if (isPatrolling)
        {
            // ���񒆂̏ꍇ�͏���|�C���g�Ɍ�����
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, chaseSpeed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                // ����|�C���g�ɓ��B�������莞�Ԓ�~���A���̏���|�C���g�Ɉړ�����
               // animator.SetTrigger("ShakeHead");
                isPatrolling = false;
                Invoke("MoveToNextPatrolPoint", patrolInterval);
            }
        }
    }

    void MoveToNextPatrolPoint()
    {
        // ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
        currentPointIndex++;
        if (currentPointIndex >= patrolPoints.Length)
        {
            currentPointIndex = 0;
        }

        // ���񒆂ɖ߂�
        isPatrolling = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player�����m������ǂ�������
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player���͈͊O�ɏo����ǐՂ���߂�
            target = null;
        }
    }
}

