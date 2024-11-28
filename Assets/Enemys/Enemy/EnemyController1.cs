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
    [SerializeField] AudioClip searchClip;    //�T����
    [SerializeField] AudioClip runClip;       //���鉹
    [SerializeField] AudioClip walkClip;      //������

    //�X�e�[�g�x�[�XAI
    #region
    enum enemyState
    {
        walk,
        chase,
        search,
        doNothing
    }

    float walking = 0;//�����Ă���@0�`�P;

    float chasing = 0;//�ǂ������Ă���@0�`�P;

    float searching = 0;//�T���Ă���@0�`�P;

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
        //if(curretState != enemyState.walk) 
        //{ 

        //}

        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�������Ȃ�");
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



                #endregion
                break;
            case enemyState.walk:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�����Ă���");
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
