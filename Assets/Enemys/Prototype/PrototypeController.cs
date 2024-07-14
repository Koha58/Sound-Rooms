using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AudioSource audioSource;// �I�[�f�B�I�\�[�X

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
            audioSource.clip = FootstepsSound;//�����̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
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
                onoff = 1;
                ONTime = 0;
            }
        }

        if (onoff == 1)//�����Ă���Ƃ�
        {
            audioSource.clip = VisualizationSound;// �������̃I�[�f�B�I�N���b�v���I�[�f�B�I�\�[�X�ɓ����
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
        
        if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
            NextPatrolPoint();

    }

}
