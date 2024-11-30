using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

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
        player = GameObject.FindWithTag("Player").transform;�@//�v���C���[�̈ʒu���擾
    }

    private void Update()
    {
        if (curretState != enemyState.search)//���݂̃X�e�[�g��search����Ȃ�������
        {
            behaviors.GetBehavior(BehaviorType.search).value += Time.deltaTime;
        }

        if (curretState != enemyState.walk)//���݂̃X�e�[�g��walk����Ȃ�������
        {
            behaviors.GetBehavior(BehaviorType.walk).value += Time.deltaTime / 3;
        }

        if (curretState != enemyState.chase)//���݂̃X�e�[�g��chase����Ȃ�������
        {
            behaviors.GetBehavior(BehaviorType.chase).value += Time.deltaTime / 5;
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

                behaviors.SortDesire();//�s���p�^�[�����\�[�g

                if (behaviors.behaviorList[0].value >=1)
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
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
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
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
                }

                //behaviors.GetBehavior(BehaviorType.search).value += Time.deltaTime;

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
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.walk:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("�����Ă���");
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
                }

               // behaviors.GetBehavior(BehaviorType.walk).value += Time.deltaTime / 3;

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
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
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
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
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
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;


        }
    }
}
