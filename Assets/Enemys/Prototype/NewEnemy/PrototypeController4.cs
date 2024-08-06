using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrototypeController4 : MonoBehaviour
{

    //�ۑ�
    /*1��
     2���̉���
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
    public GameObject VisualizationBox;//���̉����̓����蔻��
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;

    //�T�E���h
    AudioSource audioSourse;
    public AudioClip FootstepsSound;// �����̃I�[�f�B�I�N���b�v
    public AudioClip VisualizationSound;// �������̃I�[�f�B�I�N���b�v
    public AudioClip EnemySearch;
    public AudioClip EnemyRun;
    public AudioClip EnemyWalk;
    public AudioSource audioSource1;// �I�[�f�B�I�\�[�X
    public AudioSource audioSource2;// �I�[�f�B�I�\�[�X

    //�O�㔻��
    public Transform TargetPlayer;
    public bool FrontorBack;//(�O�F true/��: false)

    //Player��ǐ�
    float ChaseSpeed = 0.05f;//Player��ǂ�������X�s�[�h
    bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    private bool TouchWall;
    float WallONOFF = 0.0f;

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject Player;
    public GameObject Prototype;

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
            audioSource1.clip = FootstepsSound;//�����̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource1.enabled = true;
            GameOverBoxCapsuleCollider.enabled = false;//�����蔻��OFF
            VisualizationBox.SetActive(false);//���̉�������OFF
            //3D���f����Renderer�������Ȃ����
            PrototypeBodySkinnedMeshRenderer.enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//�����_���ŏo���ꂽ�l���傫�������猩����悤�ɂ���
            {
                audioSource1.enabled = false;
                ONOFF = 1;
                ONTime = 0;
            }
        }
        else if (ONOFF == 1)//�����Ă���Ƃ�
        {
            audioSource2.clip = VisualizationSound;// �������̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource2.enabled = true;
            GameOverBoxCapsuleCollider.enabled = true;//�����蔻��ON
            VisualizationBox.SetActive(true);//���̉�������ON
            //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            if (FrontorBack == true)//�O��
            {
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
                    float distance = 50;    // Ray���΂�����

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
                            Debug.Log("�v���C���[����");
                            PS.onoff = 1;  //�����Ă��邩��1
                            foreach (var playerParts in childTransforms)
                            {
                                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                            }
                        }

                        if (hit.collider.gameObject.CompareTag("Wall") || (hit.collider.gameObject.CompareTag("InWall")))
                        {
                            Debug.Log("�v���C���[�Ƃ̊Ԃɕǂ�����");
                        }

                    }
                }

            }

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10�b�ȏ�o�����猩���Ȃ�����
            {
                audioSource2.enabled = false;
                ONOFF = 0;
                OFFTime = 0;
            }
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (ChaseONOFF == false)
        {
            animator.SetBool("Run", true);
        }

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
            Chase();
            /*float Wall= Vector3.Distance(transform.position, TargetPlayer.position);
              if (Wall <= 30f)//�ǂ����m�͈͂ɓ�������
              {

              }*/
            FrontorBack = true;
        }
        else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
        {
            FrontorBack = false;
        }
    }

    void Idle()
    {
        audioSourse.PlayOneShot(EnemySearch);
    }

    void Run()
    {
        audioSourse.PlayOneShot(EnemyRun);
    }

    void Walk()
    {
        audioSourse.PlayOneShot(EnemyWalk);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            ONOFF = 1;
            ONTime = 0;
            audioSource1.enabled = false;
            audioSource2.clip = VisualizationSound;// �������̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource2.enabled = true;
            GameOverBoxCapsuleCollider.enabled = true;//�����蔻��ON
           �@//3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;

            if (FrontorBack == false)
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

                if (detectionPlayer <= 7f)//�v���C���[�����m�͈͂ɓ�������
                {
                    DestroyONOFF = true;
                }
            }
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
