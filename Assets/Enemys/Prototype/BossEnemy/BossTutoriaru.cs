using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossTutoriaru : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    public float MoveSpeed =1.0f; // �������x
    private int CurrentPointIndex = 0;// ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float ONOFF = 0;//(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //�T�E���h
    AudioSource audioSourse;
    public AudioClip BossIdle;
    public AudioClip BossMove;

    //�O�㔻��
    public Transform TargetPlayer;

    //Player��ǐ�
    public float ChaseSpeed = 0.07f;//Player��ǂ�������X�s�[�h
    [SerializeField] bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;
    private float NextTime;
    private bool Front;

    [SerializeField] Quaternion rotation;

    public GameObject VisualizationBoss;

    public SphereCollider SphereCollider;

    private void Chase()//�v���C���[��ǂ�������
    {
        GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (TouchWall == false)
        {
            if (ChaseONOFF == true)//�v���C���[�����m�͈͂ɓ�������
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Move", true);
                ChaseONOFF = true;//�ǐՒ�
                transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
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
        if (CurrentPointIndex >= PatrolPoints.Length) { CurrentPointIndex = 0; }
    }

    private void Visualization()//���g�̉�����ON OFF
    {

        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            ONOFF = 1;
            VisualizationBoss.SetActive(false);
            //3D���f����Renderer�������Ȃ����
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            audioSourse.maxDistance = 5;
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
            GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
            PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            //3D���f����Renderer����������
            VisualizationBoss.SetActive(true);
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            audioSourse.maxDistance = 300;
            ONTime += Time.deltaTime;
            if (ONTime >= 10.0f)
            {
                ONTime = 0;
                VisualizationBoss.SetActive(false);
                //3D���f����Renderer����������
                PrototypeBodySkinnedMeshRenderer.enabled = false;
                ONOFF = 0;//�����Ȃ�
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

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        Chase();
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
            /*
            if (hit.collider.CompareTag("Player"))
            {
                if (ONOFF == 1)
                {
                    PS.onoff = 1;  //�����Ă��邩��1
                    PS.Visualization = true;
                    ONOFF = 1;
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }*/
        }
        else
        {
            OFFTime += Time.deltaTime;
            if (OFFTime >= 7.0f)
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
                EnemyAttack.SoundON = false;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//�����Ȃ����
        VisualizationBoss.SetActive(false);
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
        if (EnemyAttack.SoundON == true)
        {
            Front = true;
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
             VisualizationBoss.SetActive(true);
        }


        Visualization();

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

        Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

        if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
        {
            if (ONOFF == 0) { ChaseONOFF = false; }
            float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
            if (ONOFF == 1 && ChasePlayer <= 30) { ChaseONOFF = true; }
            DestroyONOFF = false;
        }
        else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
        {
            float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            //�v���C���[�����m�͈͂ɓ�������
            if (detectionPlayer <= 7f) { DestroyONOFF = true; }
        }
    }
    void Idle() { audioSourse.PlayOneShot(BossIdle); }

    void Move() { audioSourse.PlayOneShot(BossMove); }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

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
