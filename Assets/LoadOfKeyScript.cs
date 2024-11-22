using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOfKeyScript : MonoBehaviour
{
    public float speed;
    public float maxDistanceFromParent = 5f; // �e�I�u�W�F�N�g�Ƃ̍ő勗��
    public float returnDistance = 2f; // �e�I�u�W�F�N�g�̌��ɖ߂�ۂ̋���臒l
    private GameObject[] targets;
    private bool isSwitch = false;

    private GameObject closeEnemy;
    private bool isAtTarget = false; // �ړI�n�i�G�j�ɓ��B�������ǂ���

    private void Start()
    {
        // �^�O���g���ĉ�ʏ�̑S�Ă̓G�̏����擾
        targets = GameObject.FindGameObjectsWithTag("EnemyG");

        // �u�����l�v�̐ݒ�
        float closeDist = 1000;

        foreach (GameObject t in targets)
        {
            // ���̃I�u�W�F�N�g�ƓG�܂ł̋������v��
            float tDist = Vector3.Distance(transform.position, t.transform.position);

            // �������u�����l�v�����u�v�������G�܂ł̋����v�̕����߂��Ȃ�΁A
            if (closeDist > tDist)
            {
                // �ucloseDist�v���utDist�i���̓G�܂ł̋����j�v�ɒu��������B
                closeDist = tDist;

                // ��ԋ߂��G�̏���closeEnemy�Ƃ����ϐ��Ɋi�[����i���j
                closeEnemy = t;
            }
        }

        // �C�e�����������0.5�b��ɁA��ԋ߂��G�Ɍ������Ĉړ����J�n����B
        Invoke("SwitchOn", 0.5f);
    }

    void Update()
    {
        if (isSwitch)
        {
            if (isAtTarget)
            {
                // �ړI�n�i�G�j�ɓ��B�����ꍇ
                HandleReturnToParent();
            }
            else
            {
                // �e�I�u�W�F�N�g�Ƃ̋������v�Z
                float distanceFromParent = Vector3.Distance(transform.position, transform.parent.position);

                // �e�I�u�W�F�N�g�Ƃ̋������ő勗���𒴂��Ă��Ȃ����`�F�b�N
                if (distanceFromParent > maxDistanceFromParent)
                {
                    // �ő勗���𒴂��Ă�����A�e�I�u�W�F�N�g�ɋ߂Â���
                    Vector3 directionToParent = (transform.parent.position - transform.position).normalized;
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
                }
                else
                {
                    // �e�I�u�W�F�N�g�Ƃ̋��������e�͈͓��Ȃ�A�ړI�n�i�G�j�Ɍ������Ĉړ�
                    float step = speed * Time.deltaTime;
                    Vector3 targetPosition = closeEnemy.transform.position;

                    // y���W���Œ�
                    targetPosition.y = transform.position.y;

                    // x, z���W���ړ�
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                    // �ړI�n�ɓ��B�����ꍇ
                    if (Vector3.Distance(transform.position, closeEnemy.transform.position) <= 0.1f)
                    {
                        isAtTarget = true; // �ړI�n�ɓ��B
                    }
                }
            }
        }
    }

    void SwitchOn()
    {
        isSwitch = true;
    }

    // �e�I�u�W�F�N�g�̌��ɖ߂鏈��
    void HandleReturnToParent()
    {
        float distanceFromParent = Vector3.Distance(transform.position, transform.parent.position);

        // �e�I�u�W�F�N�g�Ƃ̋�����臒l����������Ζ߂�
        if (distanceFromParent > returnDistance)
        {
            Vector3 directionToParent = (transform.parent.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, speed * Time.deltaTime);
        }
        else
        {
            // �e�I�u�W�F�N�g�ɋ߂Â�����ړI�n�ւ̈ړ����ĊJ
            isAtTarget = false;
        }
    }
}
