using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController1 : MonoBehaviour
{
    public Transform player;

    NavMeshAgent navMeshAgent;

    float chaseRange = 5f;  //Player�����m����͈�
    float distanceToPlayer = Mathf.Infinity;

    private float realizeTime;
    private float realizeMaxTime=10f;

    private float searchTime;

    public Slider slider;

    public Canvas canvas;

    int i = 0;

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
        patrol,    //����
        chase,     //�ǂ�������
        search,    //�T��
        discussion,//�b������
        faind,     //������
        Stand,     //�����͂�����
        restraint, //�S��
        enjoy,     //�y����
        anticipate,//����(�\��)
        shout,     //����
        sleep,     //����
        rage,      //�\���
        doNothing//�������Ȃ�
    }

    enum BehaviorType
    {
        patrol,    //����
        chase,     //�ǂ�������
        search,    //�T��
        discussion,//�b������
        faind,     //������
        Stand,     //�����͂�����
        restraint, //�S��
        enjoy,     //�y����
        anticipate,//����(�\��)
        shout,     //����
        sleep,     //����
        rage,      //�\���
        doNothing//�������Ȃ�
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
        realizeTime = 0;
        slider.value = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        slider.value = realizeTime/realizeMaxTime;

        canvas.transform.rotation =Camera.main.transform.rotation;

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            behaviors.GetBehavior(BehaviorType.search).value = 2;
            realizeTime += 0.2f;
        }
        else
        {
            realizeTime -= 0.2f;
            ChangeState(enemyState.patrol);
        }

        if (realizeTime == realizeMaxTime||slider.value==1f)
        {
            ChangeState(enemyState.chase);
        }
        else
        {
            behaviors.GetBehavior(BehaviorType.search).value = 1f;
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
            case enemyState.search:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ��ɂ��邩�ȁH");
                    navMeshAgent.SetDestination(this.transform.position);
                    behaviors.GetBehavior(BehaviorType.search).value = 0;
                    searchTime += Time.deltaTime;
                }

                if(searchTime>=10f)
                {
                    searchTime = 0;
                    ChangeState(enemyState.patrol);
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
            case enemyState.patrol:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("����");
                    navMeshAgent.SetDestination(GameManager.instance.testPos[i].position);
                }

                if (navMeshAgent.remainingDistance <= 0.1f && !navMeshAgent.pathPending)
                {
                    i += 1;

                    if (i > 2)
                    {
                        i = 0;
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
            case enemyState.chase:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�ǂ����������");
                    navMeshAgent.speed = 4.0f;
                    Chase();
                }

                //behaviors.GetBehavior(BehaviorType.chase).value += Time.deltaTime / 5;

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
            case enemyState.discussion:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�b�������Ă���");
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
                        case BehaviorType.discussion:
                            ChangeState(enemyState.discussion);
                            return;
                        case BehaviorType.faind:
                            ChangeState(enemyState.faind);
                            return;
                        case BehaviorType.Stand:
                            ChangeState(enemyState.Stand);
                            return;
                        case BehaviorType.restraint:
                            ChangeState(enemyState.restraint);
                            return;
                        case BehaviorType.enjoy:
                            ChangeState(enemyState.enjoy);
                            return;
                        case BehaviorType.anticipate:
                            ChangeState(enemyState.anticipate);
                            return;
                        case BehaviorType.shout:
                            ChangeState(enemyState.shout);
                            return;
                        case BehaviorType.sleep:
                            ChangeState(enemyState.sleep);
                            return;
                        case BehaviorType.rage:
                            ChangeState(enemyState.rage);
                            return;
                        case BehaviorType.doNothing:
                            ChangeState(enemyState.doNothing);
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
}
