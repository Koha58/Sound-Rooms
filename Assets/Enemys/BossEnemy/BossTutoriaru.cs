using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossTutoriaru : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    public float MoveSpeed =1.0f;                      // �������x
    private float NextTime;                            //���̃|�C���g�Ɍ������܂ł̎���
    private int CurrentPointIndex = 0;                 // ���݂̏���|�C���g�̃C���f�b�N�X
    private bool Front;                                //�|�C���g�ɂ��ǂ蒅�����Ƃ��ɕԂ�

    //����
    public float ONOFF = 0;    //(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;//3D���f����Renderer��ONOFF

    //�T�E���h
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //�O�㔻��
    public Transform TargetPlayer;�@//�v���C���[�̈ʒu���擾

    //Player��ǐ�
    public float ChaseSpeed = 0.07f;�@�@//Player��ǂ�������X�s�[�h
    [SerializeField] bool ChaseONOFF;�@//(ChaseON�F true/ChaseOFF: false)

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;�@//�v���C���[�I�u�W�F�N�g�擾

    public GameObject VisualizationBoss;   //�{�X�̉����̉�(����)
    public SphereCollider SphereCollider; //�������̉�������������

    public GameObject gravity;
    public GameObject Blur;

    private�@void MoveBossEnemy()
    {
        if (ChaseONOFF == false)
        {
            if (Front == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������

                if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
                {
                    if (ONOFF == 0)
                    {
                        animator.SetBool("Idle", false);
                        animator.SetBool("Move", true);
                    }
                    else
                    {
                        animator.SetBool("Idle", true);
                        animator.SetBool("Move", false);
                    }
                    Front = true;
                }
            }
            else
            {
                if (ONOFF == 0)
                {
                    animator.SetBool("Idle", false);
                    animator.SetBool("Move", true);
                }
                else
                {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Move", false);
                }
                NextTime += Time.deltaTime;
                if (NextTime >= 5.0f)
                {
                    NextPatrolPoint();
                    NextTime = 0;
                    Front = false;
                    TouchWall = false;
                }
            }
        }
    }

    private void Chase()//�v���C���[��ǂ�������
    {
        GameObject gobj = GameObject.Find("Player");        //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>();    //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (TouchWall == false)
        {
            if (ChaseONOFF == true)//�v���C���[�����m�͈͂ɓ�������
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);

                ChaseONOFF = true;                                    //�ǐՒ�
                transform.LookAt(TargetPlayer.transform);             //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed; //�v���C���[�̕����Ɍ�����

                Transform myTransform = this.transform;
                Vector3 localAngle = myTransform.localEulerAngles;

                localAngle.x = 0f;
                localAngle.z = 0f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
            }
            else { ChaseONOFF = false; }//�ǐՒ�����Ȃ�
        }
    }

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        //����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
        if (CurrentPointIndex >= PatrolPoints.Length) {CurrentPointIndex = 0;}
    }

    private void Visualization()//���g�̉�����ON OFF
    {
        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            gravity.SetActive(false);
            Blur.SetActive(false);
            PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
            audioSourse.maxDistance = 5;                      //������������͈�
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            GameObject gobj = GameObject.Find("Player");     //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

            gravity.SetActive(true);
            Blur.SetActive(true);
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            VisualizationBoss.SetActive(true);              //�����̉�(�~)����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3D���f����Renderer����������
            audioSourse.maxDistance = 300;                 //������������͈�
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;                                        //�����Ȃ����
        VisualizationBoss.SetActive(false);               //�����̉�(�~)�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
        ChaseONOFF = false;                               //�ǐՒ�����Ȃ�
        animator = GetComponent<Animator>();              //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
        audioSourse = GetComponent<AudioSource>();�@     //�I�[�f�B�I�\�[�X���擾
    }

    // Update is called once per frame
    private void Update()
    {
        Visualization();
        if (EnemyAttack.OFF == true)
        {
            ONTime += Time.deltaTime;
            if (ONTime >= 30.0f)
            {
                GameObject gobj = GameObject.Find("Player");        //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = gobj.GetComponent<PlayerSeen>();    //�t���Ă���X�N���v�g���擾
               
                PS.Visualization = false;
                PS.onoff = 0;

                ONTime = 0;
                VisualizationBoss.SetActive(false);              //�����̉�(�~)�������Ȃ����
                ONOFF = 0;                                       //�����Ȃ�
                PrototypeBodySkinnedMeshRenderer.enabled = false;//3D���f����Renderer����������
                EnemyAttack.OFF = false;
            }
        }

        if (EnemyAttack.SoundON == true)
        {
            Front = true;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            VisualizationBoss.SetActive(true);
        }
        else if (EnemyAttack.SoundON == false)
        {
            ONOFF = 0;
            PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
            MoveBossEnemy();
            VisualizationBoss.SetActive(false);
        }

        Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

        if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
        {
            Chase();
            float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
            if (ONOFF == 0) { ChaseONOFF = false;}
            if (ONOFF == 1 && ChasePlayer <= 5) { ChaseONOFF = true; }
            DestroyONOFF = false;
        }
        else if (isBack){DestroyONOFF = true;}
    }
    void Idle() { audioSourse.PlayOneShot(BossIdle); }

    void Move() { audioSourse.PlayOneShot(BossMove); }

    private void OnTriggerStay(Collider other)
    {
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (other.gameObject.tag == "LeftWall")
        {
            if (ChaseONOFF == false)
            {
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(10f, 0, 0);
            }
        }
        else if (other.gameObject.tag == "RightWall")
        {
            if (ChaseONOFF == false)
            {
                localAngle.x = 0f;
                localAngle.z = 90f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                //Physics.gravity = new Vector3(-10f, 0, 0);
            }
        }
        else if (other.gameObject.tag == "Ceiling")
        {
            if (ChaseONOFF == false)
            { 
                localAngle.x = 0f;
                localAngle.y = 0f;
                localAngle.z = 180f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, 10f, 0);
            }
        }
        else if (other.gameObject.tag == "Floor")
        {
            if (ChaseONOFF == false)
            {
                localAngle.x = 0f;
                localAngle.z = 0f;
                localAngle.y = 0f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }
        else if (other.gameObject.tag == "RW2")
        {
            if (ChaseONOFF == false)
            {
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }
        else if (other.gameObject.tag == "LW2")
        {
            if (ChaseONOFF == false)
            {
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }

        if (other.CompareTag("RoomOut"))
        {
            TouchWall = true;
            NextPatrolPoint();
        }
    }
}
