using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemycontroller : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 0.6f;                    // �������x
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
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;

    //�O�㔻��
    public Transform TargetPlayer;

    //Player��ǐ�
    float ChaseSpeed = 0.3f;                           //Player��ǂ�������X�s�[�h
    [SerializeField] bool ChaseONOFF;

    //Destroy�̔���
     public bool DestroyONOFF;                           //(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;
    private bool UpON=false;
    private float NextTime;
    private bool Front;

    public bool piano;
    int pianocnt;
    public bool zero;
    AudioSetting AS;

    bool PianoRoom;

    private void Chase()//�v���C���[��ǂ�������
    {
        GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (TouchWall == false)
        {
            if (ChasePlayer <= 10f)//�v���C���[�����m�͈͂ɓ�������
            {
                if (PS.onoff == 1)//�v���C���[���������Ă�����
                {
                    ONOFF = 1;//�������g������
                    audioSourse.enabled = true;
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    ChaseONOFF = true;//�ǐՒ�
                    transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
                    transform.position += transform.forward * ChaseSpeed; //�v���C���[�̕����Ɍ�����
                }
                else if (ONOFF == 0){ChaseONOFF = false;}//�ǐՒ�����Ȃ�
            }
            else { ChaseONOFF = false; }//�ǐՒ�����Ȃ�
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
        /*
        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            if (Front == false)
            {
                //3D���f����Renderer�������Ȃ����
                PrototypeBodySkinnedMeshRenderer.enabled = false;
                ONOFF = 1;//������
            }
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            if (Front == true)
            {
                //3D���f����Renderer����������
                PrototypeBodySkinnedMeshRenderer.enabled = true;
                ONOFF = 0;//�����Ȃ�
            }
        }*/
        if (PianoRoom == false)
        {
            if (Front == false)
            {
                //3D���f����Renderer�������Ȃ����
                PrototypeBodySkinnedMeshRenderer.enabled = false;
                ONOFF = 0;//������
            }
        }
        if (Front == true)
        {
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            ONOFF =1;//�����Ȃ�
        }

        if(PianoRoom == true)
        {
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            ONOFF = 1;//�����Ȃ�
        }
    }

    private void Ray()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (VisualizationPlayer <=10f)//�v���C���[�����m�͈͂ɓ�������
        {
            Ray ray;
            RaycastHit hit;
            Vector3 direction;   // Ray���΂�����
            float distance =10.0f;    // Ray���΂�����

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

                if (hit.collider.gameObject.CompareTag("Wall") )
                {
                    PS.Visualization = false;
                    PS.onoff = 0;  //�����Ă��邩��1
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    audioSourse.enabled = false;
                }
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
    /*
    private void Ray2()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 direction;   // Ray���΂�����
        float distance = 5.0f;    // Ray���΂�����

        // Ray���΂��������v�Z
        Vector3 temp = Player.transform.position - transform.position;
        direction = temp.normalized;

        ray = new Ray(transform.position, direction);  // Ray���΂�
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);  // Ray���V�[����ɕ`��

        // Ray���ŏ��ɓ����������̂𒲂ׂ�
        if (Physics.Raycast(ray.origin, ray.direction * distance, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                audioSourse.enabled = false;
            }
        }
    }*/


    private void Chase2()
    {
        GameObject gobj = GameObject.Find("Player");//Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if(ChasePlayer<=10)
        {
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
        TouchWall = false;
    }

    // Update is called once per frame
    private void Update()
    {
        TouchWall = false;
        //Ray2();
        float Player = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (Player<= 0.65f)
        {
            if (ONOFF == 1)
            {
                GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

                transform.LookAt(TargetPlayer.transform);//�v���C���[�̕����ɂނ�
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
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
        else if(Player >= 1.5f) {UpON = false;}

        if (UpON == false)
        {
            Chase2();

            if (ChaseONOFF == false)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
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
                        animator.SetBool("Walk", false);
                        animator.SetBool("Run", false);
                        Front = true;
                    }
                }
                else
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
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
                if (ONOFF == 1) {Ray();}
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

        //�s�A�m��������
        if (piano)
        {
            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>();
            if (AS.BGMSlider.value == -80)
            {
                zero = true;
                piano = false;
            }
            else
            {
                piano = true;
                zero = false;
                ONOFF = 1;
                //3D���f����Renderer����������
                PrototypeBodySkinnedMeshRenderer.enabled = true;
            }
        }
        else
        {
            zero = false;
            if (pianocnt % 2 != 0 && AS.BGMSlider.value != -80){piano = true;}
        }
    }

    void Idle(){audioSourse.PlayOneShot(EnemySearch);}

    void Run(){ audioSourse.PlayOneShot(EnemyRun);}

    void Walk(){audioSourse.PlayOneShot(EnemyWalk);}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
        }

        if (other.CompareTag("Wall"))
        {
            TouchWall = true;
            CurrentPointIndex--;
            if (CurrentPointIndex <= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            { CurrentPointIndex = 0; }
        }

        if (other.CompareTag("PianoRoom"))
        {
            PianoRoom = true;
            ONOFF = 1;
            pianocnt++;
            if (!zero)
            {
                piano = true;
                if (pianocnt % 2 == 0){piano = false;}
            }
        }
        else
        {
            PianoRoom = false;
        }
    }
}