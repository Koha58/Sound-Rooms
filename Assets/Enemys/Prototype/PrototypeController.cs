using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PrototypeController : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 2f; // �������x
    private int CurrentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X

    //����
    public float onoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float ONTime;
    private float OFFTime;
    public CapsuleCollider PrototypeCapsuleCollider;//�����蔻���ONOFF
    float VisualizationRandom;//�������Ԃ������_��

    //3D���f����Renderer��ONOFF
    public SkinnedMeshRenderer PrototypeBodySkinnedMeshRenderer;
    public SkinnedMeshRenderer PrototypeKeySkinnedMeshRenderer;
    public SkinnedMeshRenderer PrototypeRingSkinnedMeshRenderer;
    public MeshRenderer Ear;
    public MeshRenderer Eey;

    //�T�E���h
    public AudioClip FootstepsSound;// �����̃I�[�f�B�I�N���b�v
    public AudioClip VisualizationSound;// �������̃I�[�f�B�I�N���b�v
    public AudioSource audioSource1;// �I�[�f�B�I�\�[�X
    public AudioSource audioSource2;// �I�[�f�B�I�\�[�X

    //�O�㔻��orPlayer��ǂ�������
    public Transform TargetPlayer;
    float ChaseSpeed = 0.05f;//Player��ǂ�������X�s�[�h
                             // bool FrontorBack;//(�O�F true/��: false)

    //Wall�ɓ���������
    // public Transform TransformWall;

    private void Chase()
    {
        float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

        if (detectionPlayer <= 10)//�v���C���[�����m�͈͂ɓ�������
        {
            Debug.Log("�ǐ�");
            transform.LookAt(TargetPlayer.transform); //�v���C���[�̕����ɂނ�
            transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
        }
    }

    private void Destroy()
    {
        float detectionPlayer = Vector3.Distance(transform.position, TargetPlayer.position);//�v���C���[�ƓG�̈ʒu�̌v�Z
        if (detectionPlayer <= 5)//�v���C���[�����m�͈͂ɓ�������
        {
         
        }
    }

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            CurrentPointIndex = 0;
    }

    private void Visualization()//������ON OFF
    {
        if (onoff == 0)//�����Ȃ��Ƃ�
        {
            audioSource1.clip = FootstepsSound;//�����̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource1.enabled =true;
            audioSource1.PlayOneShot(FootstepsSound);
            PrototypeCapsuleCollider.enabled = false;//�����蔻��OFF
            //3D���f����Renderer�������Ȃ����
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            PrototypeKeySkinnedMeshRenderer.enabled = false;
            PrototypeRingSkinnedMeshRenderer.enabled = false;
            Ear.GetComponent<MeshRenderer>().enabled = false;
            Eey.GetComponent<MeshRenderer>().enabled = false;

            ONTime += Time.deltaTime;
            if (ONTime >= VisualizationRandom)//�����_���ŏo���ꂽ�l���傫�������猩����悤�ɂ���
            {
                audioSource1.enabled = false;
                onoff = 1;
                ONTime = 0;
            }
        }

        if (onoff == 1)//�����Ă���Ƃ�
        {
            audioSource2.clip = VisualizationSound;// �������̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
            audioSource2.enabled = true;
            audioSource2.PlayOneShot(VisualizationSound);
            PrototypeCapsuleCollider.enabled = true;//�����蔻��ON
           �@//3D���f����Renderer����������
            PrototypeBodySkinnedMeshRenderer.enabled =true;
            PrototypeKeySkinnedMeshRenderer.enabled =true;
            PrototypeRingSkinnedMeshRenderer.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;
            Eey.GetComponent<MeshRenderer>().enabled = true;

            OFFTime += Time.deltaTime;
            if (OFFTime >= 10.0f)//10�b�ȏ�o�����猩���Ȃ�����
            {
                audioSource2.enabled = false;
                onoff = 0;
                OFFTime = 0;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        onoff = 0;//�����Ȃ����
        PrototypeCapsuleCollider.enabled = false;//�����蔻��OFF
        VisualizationRandom = Random.Range(5.0f, 10.0f);

        //3D���f����Renderer�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false;
        PrototypeKeySkinnedMeshRenderer.enabled = false;
        PrototypeRingSkinnedMeshRenderer.enabled = false;
        Ear.GetComponent<MeshRenderer>().enabled = false;
        Eey.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Visualization();

        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
        transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������

        if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
            NextPatrolPoint();

        Vector3 Position = TargetPlayer.position - transform.position; // �^�[�Q�b�g�̈ʒu�Ǝ��g�̈ʒu�̍����v�Z
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // �^�[�Q�b�g�����g�̑O���ɂ��邩�ǂ�������
        bool isBack = Vector3.Dot(Position, transform.forward) < 0;  // �^�[�Q�b�g�����g�̌���ɂ��邩�ǂ�������

        if (isFront) //�^�[�Q�b�g�����g�̑O���ɂ���Ȃ�
        {
            Chase();
            //FrontorBack = true;
        }
        else if (isBack)// �^�[�Q�b�g�����g�̌���ɂ���Ȃ�
        {
            Destroy();
            //FrontorBack=false;
        }
    }
}