using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySearchcontroller : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 0.5f; // �������x
    private int CurrentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float ONOFF = 0;//(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //�T�E���h
    [SerializeField] AudioSource audioSourse;
    public AudioClip TrickEnemyLaugh;
    public AudioClip TrickEnemyRun;
    public AudioClip TrickEnemyIdle;

    //�O�㔻��
    public Transform TargetPlayer;

    //Player��ǐ�
    float ChaseSpeed = 0.4f;//Player��ǂ�������X�s�[�h
    bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;

    [SerializeField]
    private Transform Pos;

    [SerializeField] Quaternion rotation;
    private bool UpON = false;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;

    int Count = 0;
    float CountTime;

    [SerializeField]
    private Vector3 initialTransform;
    float initialCount;
    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (ChasePlayer <= 5f)//�v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 1&&ONOFF==1)//�v���C���[���������Ă�����
            {
                ONOFF = 1;
                audioSourse.enabled = true;
                animator.SetBool("StandUp", true);
                animator.SetBool("Run", true);
                ChaseONOFF = true;
                transform.LookAt(TargetPlayer.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
                PS.Visualization = true;
                PS.onoff = 1;//�����Ă��邩��1
                audioSourse.enabled = true;
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }

            }
            else
            {
                ChaseONOFF = false;
                animator.SetBool("Run", true);
            }
        }
        else
        {
            ChaseONOFF = false;
            animator.SetBool("Run", true);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", true);
            PS.Visualization = false;
            PS.onoff = 0;//�����Ă��邩��1
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                playerParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
        
    }

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        //����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
        if (CurrentPointIndex >= PatrolPoints.Length){CurrentPointIndex = 0;}
    }

    private void Visualization()//���g�̉�����ON OFF
    {
        if (ChaseONOFF == false)
        {
            if (ONOFF == 0)//�����Ȃ��Ƃ�
            {
                //3D���f����Renderer�������Ȃ����
                PrototypeBodySkinnedMeshRenderer.enabled = false;
                ONTime += Time.deltaTime;
                if (Count == 0)
                {
                    if (ONTime >= VisualizationRandom)//�����_���ŏo���ꂽ�l���傫�������猩����悤�ɂ���
                    {
                        if (VisualizationRandom <= 5.0f) { Count = 0; }
                        else { Count = 1; }
                        ONOFF = 1;
                        ONTime = 0;
                    }
                }
            }
            else if (ONOFF == 1)//�����Ă���Ƃ�
            {
                PrototypeBodySkinnedMeshRenderer.enabled = true;

                OFFTime += Time.deltaTime;
                if (OFFTime >= 5.0f)//10�b�ȏ�o�����猩���Ȃ�����
                {
                    ONOFF = 0;
                    OFFTime = 0;
                }
            }

            if (Count == 1)
            {
                CountTime += Time.deltaTime;
                if (CountTime >= 20.0f)
                {
                    CountTime = 0;
                    Count = 0;
                }
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (VisualizationPlayer <= 5f)//�v���C���[�����m�͈͂ɓ�������
        {
            Chase();
            Ray ray;
            RaycastHit hit;
            Vector3 direction;   // Ray���΂�����
            float distance =5;    // Ray���΂�����

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

                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    PS.Visualization = false;
                    PS.onoff = 0;//�����Ă��邩��1
                    audioSourse.enabled = false;
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }
    }
   
    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//�����Ȃ����
        VisualizationRandom = Random.Range(4.0f, 6.0f);
        audioSourse = GetComponent<AudioSource>();
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
        ChaseONOFF = false;
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
        initialTransform = transform.position; // �����ʒu�̎擾�i�̂���j
    }

    // Update is called once per frame
    private void Update()
    {
        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (Player <= 2f)
        {
            if (ONOFF == 1)
            {
                GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                animator.SetBool("Run", true);
                PS.Visualization = true;
                PS.onoff = 1;  //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                UpON = true;
            }
        }
        else if (Player >= 2.5f) { UpON = false; }

        if (UpON == false)
        {
            if (this.transform.position == Pos.transform.position)
            {
                animator.SetBool("StandUp", false);
                animator.SetBool("Run", false);
                this.transform.localRotation = rotation;
                TouchWall = false;
            }

            if (ONOFF==0)
            {
                transform.position = initialTransform;
            }

            Visualization();

            if (ChaseONOFF == false || TouchWall == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������
                // ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
                if (transform.position == PatrolPoints[CurrentPointIndex].position){NextPatrolPoint();}
            }

            Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

            if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
            {
                if (ONOFF == 0) { ChaseONOFF = false; }else{ Ray(); }
                DestroyONOFF = false;
            }
            else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
                //�v���C���[�����m�͈͂ɓ�������
                if (detectionPlayer <= 5f){DestroyONOFF = true;}
            }
        }
    }

    void Laugh(){audioSourse.PlayOneShot(TrickEnemyLaugh);}

    void Idle(){audioSourse.PlayOneShot(TrickEnemyIdle);}

    void Run(){audioSourse.PlayOneShot(TrickEnemyRun);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;//�����蔻��ON
           �@//3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

        if ( other.CompareTag("Wall"))
        {
            TouchWall = true;
            CurrentPointIndex--;
            //����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            if (CurrentPointIndex <= PatrolPoints.Length){ CurrentPointIndex = 0; }
        }

        if (other.CompareTag("RoomOut"))
        {
            TouchWall = true;
            animator.SetBool("Run", true);
            CurrentPointIndex--;
            //����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            if (CurrentPointIndex <= PatrolPoints.Length) { CurrentPointIndex = 0; }
        }

        if (other.CompareTag("Player"))
        {
            if (ONOFF == 0)
            {
                GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                PS.Visualization = false;
                PS.onoff = 0;                                                             //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }
}