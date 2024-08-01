using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrototypeController2 : MonoBehaviour
{
    //�ۑ�
    /*1��
     2���ꂼ��ʁX�ɔj��
     3���̉���
    */

    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 2f; // �������x
    private int CurrentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float ONOFF = 0;//(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    public CapsuleCollider GameOverBoxCapsuleCollider;//�����蔻���ONOFF
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;
    public MeshRenderer Ear;
    public MeshRenderer Eey;

    //�T�E���h
    public AudioClip FootstepsSound;// �����̃I�[�f�B�I�N���b�v
    public AudioClip VisualizationSound;// �������̃I�[�f�B�I�N���b�v
    public AudioSource audioSource1;// �I�[�f�B�I�\�[�X
    public AudioSource audioSource2;// �I�[�f�B�I�\�[�X

    //�O�㔻��
    public Transform TargetPlayer;
    public bool FrontorBack;//(�O�F true/��: false)

    //Player��ǐ�
    float ChaseSpeed = 0.4f;//Player��ǂ�������X�s�[�h
    bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    public GameObject InWall;
    private bool TouchWall;

    private void Chase()
    {
        GameObject gobj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        float ChasePlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

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

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            CurrentPointIndex = 0;
    }

    private void TouchWalls()
    {
        /*
        float detectionWall = Vector3.Distance(transform.position, Wall.transform.position);//Wall�ƓG�̈ʒu�̌v�Z
        if (detectionWall <= 1.5f)
        {
            CurrentPointIndex--;
            if (CurrentPointIndex <= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
                CurrentPointIndex = 0;
        }
        */
        float detectionInWall = Vector3.Distance(transform.position, InWall.transform.position);//InWall�ƓG�̈ʒu�̌v�Z
        if (detectionInWall <= 3f)
        {
            TouchWall = true;
            CurrentPointIndex--;
            if (CurrentPointIndex <= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
                CurrentPointIndex = 0;
        }
        else
            TouchWall = false;
    }

    private void Visualization()//���g�̉�����ON OFF
    {
        if (ONOFF == 0)//�����Ȃ��Ƃ�
        {
            audioSource1.clip = FootstepsSound;//�����̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource1.enabled = true;
            GameOverBoxCapsuleCollider.enabled = false;//�����蔻��OFF
            //3D���f����Renderer�������Ȃ����
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            Ear.GetComponent<MeshRenderer>().enabled = false;
            Eey.GetComponent<MeshRenderer>().enabled = false;

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
                                                      //3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;
            Eey.GetComponent<MeshRenderer>().enabled = true;

            if (FrontorBack == true)//�O��
            {
                DestroyONOFF = false;
                GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
                PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
                var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
                if (PS.onoff == 0)
                {
                    float VisualizationPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

                    if (VisualizationPlayer <= 30f)//�v���C���[�����m�͈͂ɓ�������
                    {
                        PS.onoff = 1;  //�����Ă��邩��1
                        foreach (var playerParts in childTransforms)
                        {
                            //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                            playerParts.gameObject.GetComponent<Renderer>().enabled = true;
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

        //3D���f����Renderer�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false;
        Ear.GetComponent<MeshRenderer>().enabled = false;
        Eey.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    private void Update()
    {
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
            FrontorBack = true;
        }
        else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
        {
            FrontorBack = false;
        }
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
            Ear.GetComponent<MeshRenderer>().enabled = true;
            Eey.GetComponent<MeshRenderer>().enabled = true;

            if (FrontorBack == false)
            {
                float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

                if (detectionPlayer <= 7f)//�v���C���[�����m�͈͂ɓ�������
                {
                    DestroyONOFF = true;
                }
            }
        }
    }
}