using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TitleEnemyMove : MonoBehaviour
{
    //�ړ�
    [SerializeField] private Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float MoveSpeed = 0.5f;                    // �������x
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

    //�A�j���[�V����
    [SerializeField] Animator animator;

    public GameObject VisualizationGameObject;
    private bool UpON = false;
    private float NextTime;
    private bool Front;

    private void NextPatrolPoint() //���̃|�C���g
    {
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)//����|�C���g���Ō�܂ōs������ŏ��ɖ߂�
            CurrentPointIndex = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
        ONOFF = 0;//�����Ȃ����
        VisualizationRandom = Random.Range(5.0f, 10.0f);
        audioSourse = GetComponent<AudioSource>();

        //3D���f����Renderer�������Ȃ����
        PrototypeBodySkinnedMeshRenderer.enabled = false;

        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����
    }

    // Update is called once per frame
    private void Update()
    {

        if (ONOFF == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            transform.LookAt(PatrolPoints[CurrentPointIndex].transform);//���̃|�C���g�̕���������
            animator.SetBool("Walk", true);
            PrototypeBodySkinnedMeshRenderer.enabled = false;
            VisualizationGameObject.SetActive(false);
            if (transform.position == PatrolPoints[CurrentPointIndex].position)// ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                ONOFF = 1;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            PrototypeBodySkinnedMeshRenderer.enabled = true;
            VisualizationGameObject.SetActive(true);
            NextTime += Time.deltaTime;
            if (NextTime >= 5.0f)
            {
                NextPatrolPoint();
                NextTime = 0;
                ONOFF = 0;
            }
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
}
