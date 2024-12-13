using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EnemyController1 : MonoBehaviour
{
    public int characterID;       // �L�����N�^�[��ID
    public Transform player;
    private List<Transform> route; // ���񃋁[�g
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    public NavMeshAgent navMeshAgent;

    float chaseRange = 7f;  //Player�����m����͈�
    float distanceToPlayer = Mathf.Infinity;

    float searchTime;
    int pointCount;

    public float detectionRange = 10f; // ���𕷂�����͈�
    private Vector3 soundPosition;
    private bool isMovingToSound = false;

    public static bool ImageOn;

    //�A�j���[�V����
    [SerializeField] Animator animator;�@//�A�j���[�^�[�擾

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
        public List<Behavior> behaviorList { get; private set; }=new List<Behavior>();�@//�s���p�^�[���̎�ނ�\���ϐ�

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
            for (int i=0; i< BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType),i);//�񋓌^���C���f�b�N�X�Ŏ擾����
                Behavior newBehavior=new Behavior(type);�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@ //�������@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@

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
        // RouteManager���烋�[�g���擾

        route = GameManager.instance.GetRoute(characterID);

    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //�t���Ă���X�N���v�g���擾

        Vector3 Position = player.position - transform.position;                          // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;                      // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            if (isFront && PS.onoff == 1)
            {
                behaviors.GetBehavior(BehaviorType.chase).value = 2;
            }
        }

        if (isMovingToSound)
        {
            // ���̈ʒu�ֈړ�
            navMeshAgent.SetDestination(soundPosition);

            // �ړI�n�ɋ߂Â������~
            if (Vector3.Distance(transform.position, soundPosition) < 1f)
            {
                isMovingToSound = false;
            }
        }

        if (audioSourse.clip != null)
        {
            audioSourse.Play();
        }

        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�������Ȃ�");
                    ChangeState(enemyState.patrol);
                }

                behaviors.SortDesire();//�s���p�^�[�����\�[�g

                if (behaviors.behaviorList[0].value >=1)//���X�g�̈�ԏ��1����������
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch(behavior.type)
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
            case enemyState.patrol:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("����");
                    behaviors.GetBehavior(BehaviorType.patrol).value = 0;

                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    audioSourse.clip = walkClip;
                    navMeshAgent.speed = 2.0f;
                    transform.LookAt(PatrolPoints[pointCount].transform);
                    navMeshAgent.SetDestination(PatrolPoints[pointCount].position);
                }

                if (navMeshAgent.remainingDistance <= 0.1f && !navMeshAgent.pathPending)
                {
                    pointCount += 1;
                    if (pointCount > 2) { pointCount = 0; }
                    ChangeState(enemyState.search);
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
            case enemyState.search:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ��ɂ��邩�ȁH");
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    audioSourse.clip = runClip;
                    navMeshAgent.SetDestination(this.transform.position);
                    ImageOn = false;
                }

                searchTime += Time.deltaTime;

                if (searchTime >= 3.0f)
                {
                    searchTime = 0;
                    behaviors.GetBehavior(BehaviorType.search).value = 0;
                    behaviors.GetBehavior(BehaviorType.patrol).value = 2;
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
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
            case enemyState.chase:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ����������");
                    behaviors.GetBehavior(BehaviorType.chase).value = 0;
                    animator.SetBool("Walk",false);
                    animator.SetBool("Run", true);
                    transform.LookAt(player.transform);
                    navMeshAgent.speed = 4.0f;
                    PS.onoff = 1;
                    PS.Visualization = true;
                    Chase();

                    ImageOn = true;
                }


                if (distanceToPlayer >= chaseRange) {
                    behaviors.GetBehavior(BehaviorType.search).value = 2;
                    PS.Visualization = false;
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
            case enemyState.hear:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
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
            case enemyState.near:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
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

    void Chase()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }

    public void OnSoundHeard(Vector3 position)
    {
        // �͈͓��̏ꍇ�̂݉��ɔ���
        if (Vector3.Distance(transform.position, position) <= detectionRange)
        {
            soundPosition = position;
            isMovingToSound = true;
        }
    }
}
