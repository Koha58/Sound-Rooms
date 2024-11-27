using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    enum enemyState
    { 
        walk,
        chase,
        search,
        doNothing
    }

    enemyState curretState = enemyState.doNothing;//���݂̃X�e�[�g�͉������Ă��Ȃ�
    bool stateEnter = true;                    �@ //�X�e�[�g�̕ω����Ɉ�񂾂�����ȏ��������������Ƃ��Ɏg�p

    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    private void Update()
    {
        //if(curretState != enemyState.walk) 
        //{ 

        //}

        switch(curretState)
        {
            case enemyState.doNothing:
                if(stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�������Ȃ�");
                }
                break; 

        }
    }

    //�A�j���[�V����
    [SerializeField] Animator animator;�@//�A�j���[�^�[�擾

    //�T�E���h
    [SerializeField] AudioSource audioSourse; //�I�[�f�B�I�\�[�X�擾
    [SerializeField] AudioClip searchClip;    //�T����
    [SerializeField] AudioClip runClip;       //���鉹
    [SerializeField] AudioClip walkClip;      //������

}
