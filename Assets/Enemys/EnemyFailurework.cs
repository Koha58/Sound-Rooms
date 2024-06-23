using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] PatrolPoints; // ����|�C���g�̔z��
    private float patrolInterval = 2f; // ����̊Ԋu
    private float chaseSpeed = 2f; // Player��ǂ������鑬�x
    private float MoveSpeed = 2f; // �������x

    public int CurrentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X
    private Transform target; // Player�̈ʒu
    private bool isPatrolling = true; // ���񒆂��ǂ���

    public float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public float SoundTime;//�o�ߎ���
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;
    public Transform Player;//�v���C���[���Q��

    private float TargetTime;

    public Animator animator; //�A�j���[�V�����̊i�[
   
    public bool Soundonoff ;


    float time;
   
    private void Start()
    {
        //tag��"EnemyParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }

        //animator = GetComponent<Animator>();
        NextPatrolPoint();

       GameObject Chase = GameObject.FindWithTag("Chase");
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
    }

    private  void Update()
    {
        Switch();

        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        


        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        if (target != null)
        {
            if (PS.onoff == 1 && ONoff == 1)
            {
                animator.SetBool("EnemyWalk", false);
                animator.SetBool("EnemyRun", true);
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
            }
        }
        else if (isPatrolling )
        {
      
                // ���񒆂̏ꍇ�͏���|�C���g�Ɍ�����
            transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPointIndex].position, MoveSpeed * Time.deltaTime);
            if (transform.position == PatrolPoints[CurrentPointIndex].position)
            {
                // ����|�C���g�ɓ��B�������莞�Ԓ�~���A���̏���|�C���g�Ɉړ�����
                // animator.SetTrigger("ShakeHead");
                time += Time.deltaTime;
                if (time >= 2f)
                {
                    isPatrolling = false;
                    Invoke("NextPatrolPoint", patrolInterval);
                    transform.LookAt(PatrolPoints[CurrentPointIndex].transform);
                    animator.SetBool("EnemyRun", false);
                    time = 0;
                }
            }
        }
    }

    void Switch()
    {
        float randomTime = Random.Range(5f, 10f);
        TargetTime = randomTime;
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
            Soundonoff = true;
            SoundTime += Time.deltaTime;
            if (SoundTime >= TargetTime)
            {
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//���g��\�����\��
                GameObject Chase = GameObject.FindWithTag("Chase");
                EnemyChase EC = Chase.GetComponent<EnemyChase>(); //EnemyFailurework�t���Ă���X�N���v�g���擾

                if (EC.Chase == false)
                {
                    target = null;  
                }
            }
        }
        if (ONoff == 1)//�����Ă���Ƃ�
        {
            Soundonoff = false;
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
                Sphere.SetActive(false);//���g�\������\��
            }
        }
    }

    void NextPatrolPoint()
    {
        // ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
        CurrentPointIndex++;
        if (CurrentPointIndex >= PatrolPoints.Length)
        {
            CurrentPointIndex = 0;
        }
        // ���񒆂ɖ߂�
        isPatrolling = true;
        animator.SetBool("EnemyRun", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           GameObject Chase = GameObject.FindWithTag("Chase");
           EnemyChase EC = Chase.GetComponent<EnemyChase>(); 

            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            /*
            if (EC.Chase == true &&EC.Wall==false&& PS.onoff == 1)
            {
                target = other.transform;  // Player�����m������ǂ�������
            }
            */
        }
    }
}

