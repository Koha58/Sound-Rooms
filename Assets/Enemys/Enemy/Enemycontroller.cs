using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 0.3f;                    // �������x
    private int CurrentPointIndex = 0;                 // ���݂̏���|�C���g�̃C���f�b�N�X
    private float NextTime;                            //���̃|�C���g�Ɍ������܂ł̎���
�@�@private bool Front;                                //�|�C���g�ɂ��ǂ蒅�����Ƃ��ɕԂ�

    //����
    public float ONOFF = 0;                               �@�@�@ //(0�������Ȃ��G�P���������ԁj
    private float OFFTime;                                �@�@�@ //�v���C���[���������Ă��玞�Ԃ�
    float VisualizationRandom;                             �@�@�@//�������Ԃ������_��
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer; //3D���f����Renderer

    //�T�E���h
   [SerializeField] AudioSource audioSourse;
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;

    //�O�㔻��
    public Transform TargetPlayer;�@�@�@�@�@�@�@�@�@�@ //�v���C���[�̈ʒu���擾

    //Player��ǐ�
    float ChaseSpeed = 0.08f;                           //Player��ǂ������鑬�x
    [SerializeField] bool ChaseONOFF;                  //(ChaseON�F true/ChaseOFF: false)


    //Destroy�̔���
    public bool DestroyONOFF;                          //(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWallONOFF;�@�@�@�@�@�@�@�@�@�@�@ //(TouchWallON�F true/ TouchWallOFF: false)

    //�A�j���[�V����
    [SerializeField] Animator animator;�@�@�@�@�@�@�@  //�A�j���[�^�[�擾

    public GameObject Player;                          //�v���C���[�I�u�W�F�N�g�擾

    //�v���C���[�����͈̔͂ɓ��������ɕԂ�
    private bool INPlayerONOFF;                        //(INPlayerON�F true/ INPlayerOFF: false)

    //�s�A�m�̕����Ɋւ��邱��
    public bool piano;
    int pianocnt;
    public bool zero;
    AudioSetting AS;
    bool PianoRoom;

    private bool IdleONOFF;
    private bool SeenAreaONOFF;
    private float VisualizationTime;
    [SerializeField] GameObject HitBox;

    private void MoveEnemy()
    {
        if (ChaseONOFF == false && TouchWallONOFF == false)
        {
            if (Front == false)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
                transform.LookAt(PatrolPoints[CurrentPointIndex].transform);              //���̃|�C���g�̕���������

                if (this.transform.position == PatrolPoints[CurrentPointIndex].position)  // ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
                {
                    animator.SetBool("Run", false);
                    animator.SetBool("Walk", false);
                    Front = true;
                }
            }
            else if (Front == true)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
                NextTime += Time.deltaTime;
                if (NextTime >= 5.0f)
                {
                    Front = false;
                    NextPatrolPoint();
                    NextTime = 0;
                }
            }
        }
    }

    private void Chase()//�v���C���[��ǂ�������
    {
        GameObject obj = GameObject.Find("Player");      //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position); //�v���C���[�ƓG�̈ʒu�̌v�Z
        if (ChasePlayer <= 5f) //�v���C���[�����m�͈͂ɓ�������
        {
            if ((PS.onoff == 1 && ONOFF == 1 && TouchWallONOFF == false)) //�v���C���[���������Ă�����
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                ONOFF = 1; //�������g������
                HitBox.SetActive(true);
                ChaseONOFF = true; //�ǐՒ�
                transform.LookAt(TargetPlayer.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed; //�v���C���[�̕����Ɍ�����
                PS.Visualization = true;
                PS.onoff = 1;  //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else if (ChasePlayer > 5 && ChaseONOFF == true)
        {
            PS.Visualization = false;
            PS.onoff = 0;
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                playerParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            SeenAreaONOFF = false;
            ChaseONOFF = true;
            IdleONOFF = true;
            ONOFF = 1;
            HitBox.SetActive(true);

            if (IdleONOFF == true)
            {
                OFFTime += Time.deltaTime;
                if (OFFTime >= 3.0f)
                {
                    ChaseONOFF = false;
                    IdleONOFF = false;
                    ONOFF = 0;
                    HitBox.SetActive(false);
                    PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
                    NextTime = 0;
                    OFFTime = 0;
                }
            }
        }
    }

    private void NextPatrolPoint()  �@�@�@�@�@�@�@�@�@�@//�|�C���g�̍X�V                 
    {
        CurrentPointIndex++;                         �@//���̃|�C���g
        //����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
        if (CurrentPointIndex >= PatrolPoints.Length){CurrentPointIndex = 0;}
    }

    private void Visualization()�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@//���g�̉�����ON OFF
    {
        if (PianoRoom == false)                                 �@//�s�A�m�̕�������Ȃ�
        {
            if (Front == false)                                 �@//�|�C���g�ɂ��܂ł͌����Ȃ����
            {
                ONOFF = 0;
                HitBox.SetActive(false);
                PrototypeBodySkinnedMeshRenderer.enabled = false;�@//3D���f����Renderer�������Ȃ����                                    
            }
            if (Front == true)                                     //�|�C���g�ɂ����̂Ō�������
            {
                PrototypeBodySkinnedMeshRenderer.enabled = true;�@//3D���f����Renderer����������
                ONOFF = 1;
            }
        }

        if (SeenAreaONOFF == true)
        {
            GameObject obj = GameObject.Find("Player");                                                                                 //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

            ONOFF = 1;
            HitBox.SetActive(true);
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3D���f����Renderer����������
            Front = true;
            if (ChaseONOFF == false)
            {
                VisualizationTime += Time.deltaTime;
                if (VisualizationTime >= 5.0f)
                {
                    PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
                    ONOFF = 0;                                         //�����Ȃ����
                    HitBox.SetActive(false);
                    VisualizationTime = 0;
                    SeenAreaONOFF = false;
                }
            }
            else
            {
                float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position); //�v���C���[�ƓG�̈ʒu�̌v�Z
                if (ChasePlayer > 5 && ChaseONOFF == true)
                {
                    PS.Visualization = false;
                    PS.onoff = 0;
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                    SeenAreaONOFF = false;
                    ChaseONOFF = true;
                    IdleONOFF = true;
                    ONOFF = 1;
                    HitBox.SetActive(true);

                    if (IdleONOFF == true)
                    {
                        ONOFF = 1;
                        HitBox.SetActive(true);
                        OFFTime += Time.deltaTime;
                        if (OFFTime >= 3.0f)
                        {
                            ChaseONOFF = false;
                            IdleONOFF = false;
                            ONOFF = 0;
                            HitBox.SetActive(false);
                            PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
                            NextTime = 0;
                            OFFTime = 0;
                        }
                    }
                }
            }
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player");                                                                                 //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);                                    //�v���C���[�ƓG�̈ʒu�̌v�Z
        if (VisualizationPlayer <=5f)                                                                                              //�v���C���[�����m�͈͂ɓ�������
        {
            Chase();
            Ray ray;
            RaycastHit hit;
            Vector3 direction;                                                                                                      // Ray���΂�����
            float distance =5.0f;                                                                                                  // Ray���΂�����

            // Ray���΂��������v�Z
            Vector3 temp = Player.transform.position - transform.position;
            direction = temp.normalized;

            ray = new Ray(transform.position, direction);                                                                           // Ray���΂�
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);                                                         // Ray���V�[����ɕ`��

            // Ray���ŏ��ɓ����������̂𒲂ׂ�
            if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Wall") )
                {
                    PS.Visualization = false;
                    PS.onoff = 0;                                                                                                   //�����Ă��邩��1
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }

                if (hit.collider.CompareTag("Player"))
                {
                    Chase();
                    PS.Visualization = true;
                    PS.onoff = 1;  //�����Ă��邩��1
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                }
            }
        }
    }
    
    
    private void Ray2()
    {
        if (PianoRoom != true)
        {
            if (TouchWallONOFF == false)
            {
                GameObject obj = GameObject.Find("Player");                                                                                 //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>();                                                                             //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);                                    //�v���C���[�ƓG�̈ʒu�̌v�Z
                if (VisualizationPlayer <= 5f)                                                                                              //�v���C���[�����m�͈͂ɓ�������
                {
                    Ray ray;
                    RaycastHit hit;
                    Vector3 direction;                                                                                                      // Ray���΂�����
                    float distance = 5f;                                                                                                  // Ray���΂�����

                    // Ray���΂��������v�Z
                    Vector3 temp = Player.transform.position - transform.position;
                    direction = temp.normalized;

                    ray = new Ray(transform.position, direction);                                                                           // Ray���΂�
                    Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);                                                         // Ray���V�[����ɕ`��

                    // Ray���ŏ��ɓ����������̂𒲂ׂ�
                    if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
                    {
                        if (hit.collider.gameObject.CompareTag("Wall"))
                        {
                            if (ChaseONOFF == false)
                            {
                                audioSourse.maxDistance = 1;
                            }
                            else
                            {
                                audioSourse.maxDistance = 3.5f;
                            }
                        }
                        if (hit.collider.CompareTag("Player"))
                        {
                            if (PS.onoff == 1)
                            {

                                transform.LookAt(TargetPlayer.transform);                                                                         //�v���C���[�̕����ɂނ�
                                transform.position += transform.forward * 0.05f;                                                             //�v���C���[�̕����Ɍ�����
                            }
                        }
                    }
                }
            }
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;                                �@�@�@�@//�����Ȃ����
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        PrototypeBodySkinnedMeshRenderer.enabled = false; //3D���f����Renderer�������Ȃ����
        ChaseONOFF = false;                       �@�@�@�@//�ǐՒ�����Ȃ�
        TouchWallONOFF = false;
        INPlayerONOFF = false;
        animator = GetComponent<Animator>();      �@�@�@�@//�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
        audioSourse = GetComponent<AudioSource>();
        animator.SetBool("Walk", true);
    }

    // Update is called once per frame
    private void Update()
    {
        TouchWallONOFF = false;
        float Player = Vector3.Distance(transform.position, TargetPlayer.position);   //�v���C���[�ƓG�̈ʒu�̌v�Z
        if (Player <= 0.8f)
        {
            if (ONOFF == 1)
            {
                Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
                bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
                bool isBack = Vector3.Dot(Position, transform.forward) < 0;   // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

                if (isFront)
                {
                    GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
                    PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾
                    var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                    PS.Visualization = true;
                    PS.onoff = 1;
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    ONOFF = 1;
                    HitBox.SetActive(true);
                    PrototypeBodySkinnedMeshRenderer.enabled = true;                         //3D���f����Renderer����������
                    ChaseONOFF = false;
                    INPlayerONOFF = true;
                }
                else if (isBack)
                {
                    GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
                    PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾
                    var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                    PS.Visualization = false;
                    PS.onoff = 0;
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }

                    ONOFF = 1;
                    HitBox.SetActive(true);
                    PrototypeBodySkinnedMeshRenderer.enabled = true;                         //3D���f����Renderer����������
                    ChaseONOFF = false;
                    INPlayerONOFF = true;
                }
            }
        }
        else if (Player >= 2f) { INPlayerONOFF = false; }

        if (INPlayerONOFF == false)
        {
           
            Visualization();
            MoveEnemy();

            Vector3 Position = TargetPlayer.position - transform.position;                          // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
            bool isFront = Vector3.Dot(Position, transform.forward) > 0;                            // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
            bool isBack = Vector3.Dot(Position, transform.forward) < 0;                             // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

            if (isFront)                                                                            //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
            {
                Chase();
                Ray2();
                if (ONOFF == 0) { ChaseONOFF = false; } else { Ray(); }
                DestroyONOFF = false;
            }
            else if (isBack)                                                                         // �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position); //�v���C���[�ƓG�̈ʒu�̌v�Z

                //�v���C���[�����m�͈͂ɓ�������
                if (detectionPlayer <= 5f) { DestroyONOFF = true; }
            }
        }

        //�s�A�m��������
        if (piano)
        {
            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>();
            if (AS.BGMSlider.value == -80)
            {
                zero = true;
                piano = false;
                PianoRoom = false;
                Visualization();
            }
            else
            {
                piano = true;
                zero = false;
                ONOFF = 1;
                PianoRoom = true;
                PrototypeBodySkinnedMeshRenderer.enabled = true;                �@�@�@�@�@�@�@�@�@�@�@//3D���f����Renderer����������
            }
        }
        else
        {
            zero = false;
            if (pianocnt % 2 != 0 && AS.BGMSlider.value != -80) { piano = true; }
        }
    }

    void Idle(){audioSourse.PlayOneShot(EnemySearch);}

    void Run(){ audioSourse.PlayOneShot(EnemyRun);}

    void Walk(){audioSourse.PlayOneShot(EnemyWalk);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))//�^�O�i"SeenArea"�j�ڐG�������Ă�����
        {
            ONOFF = 1;
            HitBox.SetActive(true);
            PrototypeBodySkinnedMeshRenderer.enabled = true;//3D���f����Renderer����������
            SeenAreaONOFF = true;
        }

        if (other.CompareTag("Wall"))
        {
            TouchWallONOFF = true;
            NextPatrolPoint();
            NextTime = 0;
            Front = false;
        }

        if (other.CompareTag("PianoRoom"))
        {
            PianoRoom = true;
            pianocnt++;
            if (!zero)
            {
                piano = true;
                if (pianocnt % 2 == 0){piano = false;}
            }
        }

        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (ONOFF == 0)
            {

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