using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy2controller : MonoBehaviour
{
    /*//�E���i�͕s�������
         //�E����I�ɉ����o��(��������)
         �����̎��APlayer�Ɠ����ŉ��͈͓̔��̕���s���������Ăق����B
         //�EPlayer�����͈̔͂ɓ���A����Player�̉��͈̔͂ɓ���(�w��A���E�ȊO)��Player�����������APlayer��ǂ�������
         �EPlayer�ƐڐG������Player�̃��C�t��1����(���C�t��0�ɂȂ�ƃQ�[���I�[�o�[)
         �EPlayer�������o���Ɖ������ӂ̓G�͉����Ɍ�����
         �������h�A���J�����Ƃ��ɂ��������ė~�������ǁA��������Player�������h�A�O�ŉ������Ȃ��Ǝ����h�A��Player��F�������ɊJ���Ȃ��d�g�݂ɂ��邩��APlayer�������o�����Ƃ��A���͈͓̔��ɂ���(Player�̉��͈̔͂Ƃ͕�)�G���������Ɍ����������ł�������(�ʂɒǂ����ł͖���)�B
         ��AEnemy�������o���p�x�����Ə��Ȃ��Ă���������(Player�������o���Ӗ�������܂薳���Ȃ��Ă��܂��̂�)�B
       */

    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 0.2f; // �������x
    private int CurrentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float ONOFF = 0;//(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    public CapsuleCollider GameOverBoxCapsuleCollider;//�����蔻���ONOFF
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //�T�E���h
    AudioSource audioSourse;
    public AudioClip TrickEnemyLaugh;
    public AudioClip TrickEnemyRun;
    public AudioClip TrickEnemyIdle;

    //�O�㔻��
    public Transform TargetPlayer;

    //Player��ǐ�
    float ChaseSpeed = 0.5f;//Player��ǂ�������X�s�[�h
    bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;
    float WallONOFF = 0.0f;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;
    public GameObject[] Items;
    public GameObject ItemGameObject;

    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (TouchWall == false)
        {
            if (ChasePlayer <= 30f)//�v���C���[�����m�͈͂ɓ�������
            {
                if (PS.onoff == 1)//�v���C���[���������Ă�����
                {
                    animator.SetBool("Run", true);
                    ChaseONOFF = true;
                    transform.LookAt(TargetPlayer.transform); //�v���C���[�̕����ɂނ�
                    transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
                }
                else if (ONOFF == 0)
                {
                    ChaseONOFF = false;
                }
            }
            else
            {
                ChaseONOFF = false;
            }
        }
    }

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            CurrentPointIndex = 0;
    }

    private void TouchWalls()
    {
        if (TouchWall == true)
        {
            WallONOFF += Time.deltaTime;
            if (WallONOFF > 3f)
                TouchWall = false;//Wall����
        }

    }

    private void Visualization()//���g�̉�����ON OFF
    {
        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            GameOverBoxCapsuleCollider.enabled = false;//�����蔻��OFF
            //3D���f����Renderer�������Ȃ����
            PrototypeBodySkinnedMeshRenderer.enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//�����_���ŏo���ꂽ�l���傫�������猩����悤�ɂ���
            {
                ONOFF = 1;
                ONTime = 0;
                ItemGameObject.SetActive(true);
            }
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            GameOverBoxCapsuleCollider.enabled = true;//�����蔻��ON
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10�b�ȏ�o�����猩���Ȃ�����
            {
                ONOFF = 0;
                OFFTime = 0;
                ItemGameObject.SetActive(false);
            }
        }
    }

    private void ItemVisualization()//���g�̉�����ON OFF
    {
        GameObject gameObject = GameObject.FindWithTag("Object");
        ItemObject itemObject = gameObject.AddComponent<ItemObject>();
        foreach (var itms in Items)
        {
            float VisualizationItems = Vector3.Distance(transform.position, itms.transform.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
            if (VisualizationItems >= 3)
            {
                // itemObject.VisualizationON = true;
            }
            //else 
            //itemObject.VisualizationON = false;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//�����Ȃ����
        GameOverBoxCapsuleCollider.enabled = false;//�����蔻��OFF
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3D���f����Renderer�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

        Items = GameObject.FindGameObjectsWithTag("Object");
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetBool("Run", true);
        Visualization();
        TouchWalls();

        if (ChaseONOFF == false || TouchWall == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������

            if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
                NextPatrolPoint();
        }

        Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

        if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
        {
            ItemVisualization();
            DestroyONOFF = false;
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (VisualizationPlayer <= 30f)//�v���C���[�����m�͈͂ɓ�������
            {

                Ray ray;
                RaycastHit hit;
                Vector3 direction;   // Ray���΂�����
                float distance = 30;    // Ray���΂�����

                // Ray���΂��������v�Z
                Vector3 temp = Player.transform.position - transform.position;
                direction = temp.normalized;

                ray = new Ray(transform.position, direction);  // Ray���΂�
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Ray���V�[����ɕ`��

                // Ray���ŏ��ɓ����������̂𒲂ׂ�
                if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        PS.Visualization = true;
                        PS.onoff = 1;  //�����Ă��邩��1
                        foreach (var playerParts in childTransforms)
                        {
                            //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                            playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                        }
                        Chase();
                    }

                    if (hit.collider.gameObject.CompareTag("Wall") || (hit.collider.gameObject.CompareTag("InWall")))
                    {
                        PS.Visualization = false;
                        PS.onoff = 0;  //�����Ă��邩��1
                        foreach (var playerParts in childTransforms)
                        {
                            //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                            playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }
            }
            else
            {
                OFFTime += Time.deltaTime;
                if (OFFTime >= 10.0f)//10�b�ȏ�o�����猩���Ȃ�����
                {
                    ONOFF = 0;
                    OFFTime = 0;
                    PS.Visualization = false;
                }
            }
        }
        else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
        {
            float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (detectionPlayer <= 7f)//�v���C���[�����m�͈͂ɓ�������
            {
                DestroyONOFF = true;
            }
        }
    }

    void Laugh()
    {
        audioSourse.PlayOneShot(TrickEnemyLaugh);
    }

    void Idle()
    {
        audioSourse.PlayOneShot(TrickEnemyIdle);
    }

    void Run()
    {
        audioSourse.PlayOneShot(TrickEnemyRun);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            GameOverBoxCapsuleCollider.enabled = true;//�����蔻��ON
           �@//3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

        if (other.CompareTag("InWall") || other.CompareTag("Wall"))
        {
            TouchWall = true;
            CurrentPointIndex--;
            if (CurrentPointIndex <= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
                CurrentPointIndex = 0;
        }
    }
}
