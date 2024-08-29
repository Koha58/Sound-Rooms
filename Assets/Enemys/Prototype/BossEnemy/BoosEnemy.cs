using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BoosEnemy : MonoBehaviour
{
    /*�{�X�G�ɂ���
    �{�X�G�̐ݒ聫
    �E�����o���ĉ�������͈͂��ʏ���L��(�G���A�S���1/4���炢)�A���̓G�̉��ɓ�����ƃ��C�t��1����B
    //�E�{�X�G�̉��ɓ�����Ȃ��悤�ɂ��邽�߂ɂ͏�Q���𗘗p���ĉB��鎖���K�v(�G��|��������10�b�قǃ{�X�G�������o���܂łɎ��Ԃ�����)�B*/

    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    public float MoveSpeed = 15.0f;                    // �������x
    private int CurrentPointIndex = 0;                 // ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float ONOFF = 0;                            //(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;                         //�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //�T�E���h
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //�O�㔻��
    public Transform TargetPlayer;

    //Player��ǐ�
    public float ChaseSpeed = 2.0f;                           //Player��ǂ�������X�s�[�h
    [SerializeField] bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;                           //(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;
    private bool UpON = false;
    private float NextTime;
    private bool Front;

    private int x=0;
    private int y;
    private int z;

    [SerializeField] Quaternion rotation;

    public GameObject VisualizationBoss;

    public SphereCollider SphereCollider;

    private void Chase()//�v���C���[��ǂ�������
    {
        GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (TouchWall == false)
        {
            if (ChasePlayer <=30f)//�v���C���[�����m�͈͂ɓ�������
            {
                if (PS.onoff == 1)//�v���C���[���������Ă�����
                {
                    // Run();
                    ONOFF = 1;//�������g������
                    animator.SetBool("Idle",false);
                    animator.SetBool("Move", true);
                    ChaseONOFF = true;//�ǐՒ�
                    transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
                    transform.position += transform.forward * ChaseSpeed; //�v���C���[�̕����Ɍ�����

                    Transform myTransform = this.transform;
                    Vector3 localAngle = myTransform.localEulerAngles;

                    localAngle.x = 0f;
                    localAngle.z = 0f;
                    localAngle.y =0f;
                    myTransform.localEulerAngles = localAngle;
                }
                else if (ONOFF == 0)
                {
                    ChaseONOFF = false;//�ǐՒ�����Ȃ�
                  
                }
            }
            else 
            { 
                ChaseONOFF = false;
              
            }//�ǐՒ�����Ȃ�

        }
    }

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            CurrentPointIndex = 0;
    }

    private void Visualization()//���g�̉�����ON OFF
    {
        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            if (Front == false)
            {
                //3D���f����Renderer�������Ȃ����
                PrototypeBodySkinnedMeshRenderer.enabled = false;

                ONOFF = 1;//������

                 VisualizationBoss.SetActive(false);

            }
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            if (Front == true)
            {
                //3D���f����Renderer����������
                PrototypeBodySkinnedMeshRenderer.enabled = true;

                ONOFF = 0;//�����Ȃ�
              
                VisualizationBoss.SetActive(true);
          
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        Ray ray;
        RaycastHit hit;
        Vector3 direction;   // Ray���΂�����
        float distance = 30.0f;    // Ray���΂�����

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
                PS.onoff = 1;  //�����Ă��邩��1
                PS.Visualization = true;
                ONOFF = 1;
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                Chase();
            }

        }
        else
        {
            OFFTime += Time.deltaTime;
            if (OFFTime >= 5.0f)
            {
                ONOFF = 0;
                OFFTime = 0;
                PS.Visualization = false;
                PS.onoff = 0;  //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
            }
        }
    }

    private void Chase2()
    {
        GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (ChasePlayer <= 30)
        {
            transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
            GameObject soundobj = GameObject.Find("SoundVolume");
            LevelMeter levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾
            if (levelMeter.nowdB > 0.0f)
            {
                transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
                transform.position += transform.forward * (MoveSpeed * 0.09f); //�v���C���[�̕����Ɍ�����
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//�����Ȃ����
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3D���f����Renderer�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        ChaseONOFF = false;//�ǐՒ�����Ȃ�

        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
    }

    // Update is called once per frame
    private void Update()
    {
        if(PlayerRun.CrouchOn==false) 
        {
            VisualizationBoss.SetActive(true);
        }
        else
        {
           VisualizationBoss.SetActive(false);
        }

        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (Player <= 2f)
        {
            //Idle();
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            PS.Visualization = true;
            PS.onoff = 1;  //�����Ă��邩��1
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
            UpON = true;
        }
        else if (Player >= 2.5f)
        {
            UpON = false;
        }

        if (UpON == false)
        {
            Chase2();

            if (ChaseONOFF == false)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
                // Walk();
            }

            Visualization();

            if (ChaseONOFF == false || TouchWall == true)
            {
                if (Front == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                    transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������

                    if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
                    {
                        animator.SetBool("Idle", true);
                        animator.SetBool("Move", false);
                        Front = true;
                    }
                }
                else
                {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Move", false);
                    NextTime += Time.deltaTime;
                    if (NextTime >= 5.0f)
                    {
                        NextPatrolPoint();
                        NextTime = 0;
                        Front = false;
                    }
                }
            }


            Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

            if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
            {
                if (ONOFF == 0) { ChaseONOFF = false; }
                DestroyONOFF = false;
                if (Front == true) 
                {
                    if (PlayerRun.CrouchOn == false)
                    {
                        Ray();
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
    }
    void Idle()
    {
        audioSourse.PlayOneShot(BossIdle);
    }

    void Move()
    {
        audioSourse.PlayOneShot(BossMove);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
           �@//3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;

        if (other.gameObject.tag == "LeftWall")
        {
            if (ChaseONOFF == false)
            {
                //Debug.Log("1");
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
                //Debug.Log("2");
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
                //Debug.Log("3");
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
                //Debug.Log("4");
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
                //Debug.Log("5");
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
                //Debug.Log("6");
                localAngle.x = 0f;
                localAngle.z = -90f;
                localAngle.y = -90f;
                myTransform.localEulerAngles = localAngle;
                // Physics.gravity = new Vector3(0, -10f, 0);
            }
        }
    }
}
