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

    enum BehaviorType
    {
        walk,    //����
        chase,   //�ǂ�������
        search,  //�T��
    }

    class Behavior
    { 
        public BehaviorType type { get; private set; }
        public float value;

        public Behavior(BehaviorType _type) 
        { 
            type = _type;
            value = 0f;
        }
    }

    class Behaviors
    { 
        public List<Behavior> behaviorList { get; private set; }=new List<Behavior>();
        //public Behavior GetBehavior(BehaviorType type)
        //{
        //    foreach (Behaviour behaviour in behaviorList)
        //    {
        //        if (behaviour.type == type)
        //        {
        //            return behaviour;
        //        }
        //    }
        //    return null;
        //}


        //�R���X�g���N�^
        public Behaviors()
        {
            int BehaviorNum = System.Enum.GetNames(typeof(BehaviorType)).Length;

            for(int i=0; i< BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType),i);
                Behavior newBehavior=new Behavior(type);

                behaviorList.Add(newBehavior);
            }
        }
    }



    float doNothingTime;//�������Ȃ�����

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

        if(curretState != enemyState.doNothing)
        {
            doNothingTime += Time.deltaTime/5;
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

                if (doNothingTime >= 10)
                {
                    ChangeState(enemyState.doNothing);
                    return;
                }

                #endregion
                break;


        }
    }
}
