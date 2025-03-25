using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int characterID;       �@// �L�����N�^�[��ID
    public Transform player;    �@�@//�v���C���[�̈ʒu

    NavMeshAgent navMeshAgent;      //�i���B���b�V�����擾

    public PatrolPointManager patrolPointManager;  // PatrolPointManager�̎Q��

    //�A�j���[�V����
    //[SerializeField] Animator animator;�@//�A�j���[�^�[�擾

    //�T�E���h
    [SerializeField] AudioSource audioSourse; //�I�[�f�B�I�\�[�X�擾
    [SerializeField] AudioClip searchClip;    //�T����
    [SerializeField] AudioClip runClip;       //���鉹
    [SerializeField] AudioClip walkClip;      //������

    void Idle() { audioSourse.PlayOneShot(searchClip); }
    void Run() { audioSourse.PlayOneShot(runClip); }
    void Walk() { audioSourse.PlayOneShot(walkClip); }


    //�X�e�[�g�x�[�XAI
    #region
    enum enemyState
    {
        patrol,    //����
        chase,     //�ǂ�������
        search,    //�T��
        hear,      //����
        near,      //�߂Â�
        doNothing  //�������Ȃ�
    }

    enum BehaviorType
    {
        patrol,    //����
        chase,     //�ǂ�������
        search,    //�T��
        hear,      //����
        near,      //�߂Â�
        doNothing�@//�������Ȃ�
    }

    class Behavior
    {
        public BehaviorType type { get; private set; }�@//�s���p�^�[���i���������ł��Ȃ��j
        public float value;�@�@�@�@�@�@�@�@�@�@�@�@�@�@//�s���p�^�[���ω���\���l

        public Behavior(BehaviorType _type)
        {
            //�e�ϐ��̏�����
            type = _type;
            value = 0f;
        }
    }

    class Behaviors
    {
        public List<Behavior> behaviorList { get; private set; } = new List<Behavior>();�@//�s���p�^�[���̎�ނ�\���ϐ�

        //BehaviorType�������ɁA�Y������Behavior�N���X���Q�Ƃ���
        public Behavior GetBehavior(BehaviorType type)
        {
            foreach (Behavior behaviour in behaviorList)// behaviorList������m�F
            {
                if (behaviour.type == type)
                {
                    return behaviour;
                }
            }
            return null;
        }

        public void SortDesire()
        {
            //�v�f���~���Ń\�[�g���Ă���
            behaviorList.Sort((behaviour1, behaviour2) => behaviour2.value.CompareTo(behaviour1.value));
            //�����ɂ������ꍇ�� behaviour1.value.CompareTo(behaviour2.value)
        }

        //�R���X�g���N�^
        public Behaviors()
        {
            //�񋓌^�𕶎���̔z��ɕϊ��ALength�ŗv�f�����擾
            int BehaviorNum = System.Enum.GetNames(typeof(BehaviorType)).Length;

            // Behavior�N���X�𐶐��������A���X�g�ɒǉ����Ă���
            for (int i = 0; i < BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType), i);//�񋓌^���C���f�b�N�X�Ŏ擾����
                Behavior newBehavior = new Behavior(type);�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@ //�������@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@

                behaviorList.Add(newBehavior);//�ǉ�
            }
        }
    }

    Behaviors behaviors = new Behaviors();//�N���X�̎���

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
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        ChangeState(enemyState.patrol);
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //�t���Ă���X�N���v�g���擾

        if (audioSourse.clip != null)
        {
            audioSourse.Play();
        }

        switch (curretState)
        {
            case enemyState.doNothing: //�������Ȃ�
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.doNothing).value = 0;
                    Debug.Log("�������Ȃ�");
                }

                behaviors.SortDesire();//�s���p�^�[�����\�[�g

                if (behaviors.behaviorList[0].value >= 1)//���X�g�̈�ԏ��1����������
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.patrol:�@//����
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.patrol).value = 0;
                    Debug.Log("����");


                    // �L�����N�^�[ID�Ɋ�Â��Ď��̏���|�C���g���擾
                    Transform nextPatrolPoint = patrolPointManager.GetNextPatrolPoint(characterID);
                    if (nextPatrolPoint != null)
                    {
                        navMeshAgent.SetDestination(nextPatrolPoint.position);  // ���̏����Ɍ����Ĉړ�
                    }
                    else
                    {
                        Debug.LogError("����|�C���g��������܂���");
                    }

                }

                behaviors.SortDesire();

                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.search: //�T��
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.search).value = 0;
                    Debug.Log("�ǂ��ɂ��邩�ȁH");
                }

                behaviors.SortDesire();

                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.chase:�@//�ǂ�������
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.chase).value = 0;
                    Debug.Log("�ǂ����������");
                }

                behaviors.SortDesire();

                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.hear:�@//����
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.hear).value = 0;
                    Debug.Log("����");
                }

                behaviors.SortDesire();//�s���p�^�[�����\�[�g

                if (behaviors.behaviorList[0].value >= 1)//���X�g�̈�ԏ��1����������
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.near:�@//���ɋ߂Â�
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.near).value = 0;
                    Debug.Log("�߂Â�");
                }

                behaviors.SortDesire();//�s���p�^�[�����\�[�g

                if (behaviors.behaviorList[0].value >= 1)//���X�g�̈�ԏ��1����������
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
        }
    }
}
