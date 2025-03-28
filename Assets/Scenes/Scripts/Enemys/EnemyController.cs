using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemy�̐���i�ړ��@�A�j���[�V�����@�T�E���h�j�N���X
/// </summary>
public class EnemyController : MonoBehaviour
{
    // �L�����N�^�[��ID (�G�L�����N�^�[����ӂɎ��ʂ��邽��)
    public int characterID;

    // �i���B���b�V���G�[�W�F���g�̎Q�� (�ړ��Ɏg�p����NavMeshAgent)
    NavMeshAgent navMeshAgent;

    // PatrolPointManager�ւ̎Q�� (����|�C���g���Ǘ�)
    private PatrolPointManager patrolPointManager;

    // �A�j���[�^�[�̎Q�� (�A�j���[�V��������p)
    [SerializeField] Animator animator;

    // �T�E���h�֘A�̕ϐ�
    [SerializeField] AudioSource audioSourse; //�I�[�f�B�I�\�[�X�擾
    [SerializeField] AudioClip searchClip;    //�T����
    [SerializeField] AudioClip runClip;       //���鉹
    [SerializeField] AudioClip walkClip;      //������

    void Idle() { audioSourse.PlayOneShot(searchClip); }     //�T�������Đ�
    void Run() { audioSourse.PlayOneShot(runClip); }         //���鉹���Đ�
    void Walk() { audioSourse.PlayOneShot(walkClip); }       //���������Đ�

    //����
    private List<Transform> patrolPoints;     // ����|�C���g���X�g
    private int currentPatrolPointIndex = 0;  // ���݂̏���|�C���g�̃C���f�b�N�X
    private bool isPatrolling = false;      �@// ���񒆂��ǂ���

    //�ǐ�
    public Transform player;                  //�v���C���[�̈ʒu
    float distanceToPlayer = Mathf.Infinity;  // �v���C���[�Ƃ̋���
    float chaseRange = 7f;        �@�@�@�@�@�@//Player�����m����͈�

    //���W�I�J�Z�b�g
    public float detectionRange = 10f;   �@// ���𕷂�����͈�
    public Vector3 soundPosition;        �@//���W�I�J�Z�b�g�̒u����Ă���|�C���g
    private bool isMovingToSound = false;�@//���W�I�J�Z�b�g�ɔ������Ĉړ�����


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
        public float value;                             //�s���p�^�[���ω���\���l

        // �R���X�g���N�^
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

        // �s���p�^�[���̏d�v�x���Ƀ\�[�g
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
    bool stateEnter = true;                      //�X�e�[�g�̕ω����Ɉ�񂾂�����ȏ��������������Ƃ��Ɏg�p

    // �X�e�[�g�ύX�p���\�b�h
    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    #endregion

    // ����������
    private void Start()
    {
        // �R���|�[�l���g�̎擾
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        // PatrolPointManager�̃C���X�^���X���擾
        patrolPointManager = FindObjectOfType<PatrolPointManager>();

        // ����ID�ɑΉ����鏄��|�C���g���擾
        patrolPoints = patrolPointManager.GetPatrolPoints(characterID);

        // ����|�C���g�����݂���Ώ�����J�n
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            isPatrolling = true;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);  // �ŏ��̏���|�C���g�Ɍ�����
        }

        // �s�����X�g�̏���̏d�v�x�������ݒ�
        behaviors.GetBehavior(BehaviorType.patrol).value = 2;
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //�t���Ă���X�N���v�g���擾

        #region�@�v���C���[�̈ʒu���m�F���A�ǐՁE����𔻒f
        Vector3 Position = player.position - transform.position;      // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������

        distanceToPlayer = Vector3.Distance(player.position, transform.position); // �v���C���[�Ƃ̋������v�Z

        if (isFront && !isMovingToSound && PS.onoff == 1)
        {
            if (distanceToPlayer <= chaseRange)
            {
                PS.onoff = 1;
                PS.Visualization = true;
                behaviors.GetBehavior(BehaviorType.chase).value = 2; // �v���C���[��ǐՂ���
            }
            else if (distanceToPlayer >= chaseRange)
            {
                behaviors.GetBehavior(BehaviorType.patrol).value = 2;   // �v���C���[���͈͊O�̏ꍇ�A����ɖ߂�
                isPatrolling = true;
                PS.Visualization = false; // �v���C���[�̉������I�t
            }
        }
        else
        {
            behaviors.GetBehavior(BehaviorType.patrol).value = 2;   // �v���C���[���͈͊O�̏ꍇ�A����ɖ߂�
            isPatrolling = true;
            PS.Visualization = false; // �v���C���[�̉������I�t
        }
        #endregion

        // ���W�I�J�Z�b�g�̉��ɔ������Ĉړ�����
        if (isMovingToSound)
        {
            isPatrolling = false;
            // �ړI�n�ɋ߂Â������~
            if (Vector3.Distance(this.transform.position, soundPosition) < 1f)
            {
                behaviors.GetBehavior(BehaviorType.hear).value = 2; // ���̌��ɓ��B
                isMovingToSound = false;
            }
            else
            {
                behaviors.GetBehavior(BehaviorType.near).value = 2; // ���ɋ߂Â��Ă���
            }
        }

        // ���݂̃X�e�[�g�Ɋ�Â�������
        switch (curretState)
        {
            case enemyState.doNothing: //�������Ȃ�
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.doNothing).value = 0;
                    behaviors.GetBehavior(BehaviorType.patrol).value = 1;
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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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
                }

                Walk(); // ������

                if (isPatrolling)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    animator.SetBool("Idle", false);

                    navMeshAgent.speed = 1.0f;

                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

                    // ����|�C���g�ɓ��B�������`�F�b�N
                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.5f)
                    {
                        // ���̏���|�C���g�Ɉړ�
                        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);

                Idle();

                navMeshAgent.SetDestination(this.transform.position);

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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
                    PS.Visualization = true;
                    PS.onoff = 1;
                    Debug.Log("�ǂ����������");
                }

                Run();  // ���鉹

                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);

                transform.LookAt(player.transform);

                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.speed = 3.5f;

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                Idle();

                animator.SetBool("Walk",false);
                animator.SetBool("Run",false);
                animator.SetBool("Idle",true);

                navMeshAgent.speed = 1.0f;

                navMeshAgent.SetDestination(this.transform.position);

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", false);

                navMeshAgent.speed = 2.0f;

                navMeshAgent.SetDestination(soundPosition);

                Walk(); // ������

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
                            return;
                    }
                }

                #endregion
                break;
        }
    }

    // ���ɔ��������Ƃ��ɌĂ΂��
    public void OnSoundHeard(Vector3 position)
    {
        // �͈͓��̏ꍇ�̂݉��ɔ���
        if (Vector3.Distance(transform.position, position) <= detectionRange)
        {
            soundPosition = position;   // ���̈ʒu��ۑ�
            isMovingToSound = true;      // ���Ɉړ�����
        }
    }
}
