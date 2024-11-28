using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private Transform player;

    //�A�j���[�V����
    [SerializeField] Animator animator;�@//�A�j���[�^�[�擾

    //�T�E���h
    [SerializeField] AudioSource audioSourse; //�I�[�f�B�I�\�[�X�擾
    //[SerializeField] AudioClip searchClip;    //�T����
    //[SerializeField] AudioClip runClip;       //���鉹
    //[SerializeField] AudioClip walkClip;      //������

    //�X�e�[�g�x�[�XAI
    #region
    enum enemyState
    {
        walk,    //����
        chase,   //�ǂ�������
        search,  //�T��
        doNothing//�������Ȃ�
    }

    float walking = 0;//�����Ă���@0�`�P;
    float walkingTime;//�����Ă��鎞��

    float chasing = 0;//�ǂ������Ă���@0�`�P;
    float chaseTime; //�ǂ������Ă��鎞�ԁi�X�e�[�g�̐؂�ւ������ɂ��鎞�ԁj

    float searching = 0;//�T���Ă���@0�`�P;
    float searchTime;//�T���Ă��鎞��

    enemyState curretState = enemyState.doNothing;//���݂̃X�e�[�g�͉������Ă��Ȃ�
    bool stateEnter = true;                    �@ //�X�e�[�g�̕ω����Ɉ�񂾂�����ȏ��������������Ƃ��Ɏg�p

    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;�@//�v���C���[�̈ʒu���擾
    }

    private void Update()
    {
        if (curretState != enemyState.search)//���݂̃X�e�[�g��search����Ȃ�������
        {
            searchTime += Time.deltaTime;
        }

        if (curretState != enemyState.walk)//���݂̃X�e�[�g��walk����Ȃ�������
        {
            walkingTime += Time.deltaTime/3;
        }

        if (curretState != enemyState.chase)//���݂̃X�e�[�g��chase����Ȃ�������
        {
            chaseTime += Time.deltaTime/5;
        }

        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�������Ȃ�");
                }

                if (searchTime <= 1)
                {
                    ChangeState(enemyState.search);
                    return;
                }

                #endregion
                break;
            case enemyState.search:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ��ɂ��邩�ȁH");
                }

                if (walkingTime >= 3)
                {
                    ChangeState(enemyState.walk);
                    return;
                }

                #endregion
                break;
            case enemyState.walk:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�����Ă���");
                }

                if (chaseTime >= 5)
                {
                    ChangeState(enemyState.chase);
                    return;
                }

                #endregion
                break;
            case enemyState.chase:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ����������");
                }



                #endregion
                break;


        }
    }
}
