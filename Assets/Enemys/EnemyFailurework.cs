using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFailurework : MonoBehaviour
{
    public Transform[] patrolPoints; // ����|�C���g�̔z��
    public float patrolInterval = 2f; // ����̊Ԋu
    public float chaseSpeed = 5f; // Player��ǂ������鑬�x

    private int currentPointIndex = 0; // ���݂̏���|�C���g�̃C���f�b�N�X
    private Transform target; // Player�̈ʒu
    private bool isPatrolling = true; // ���񒆂��ǂ���

    public float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public float SoundTime;//�o�ߎ���
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    public Animator animator; //�A�j���[�V�����̊i�[

    [SerializeField]
    private AudioClip SoundAttck;     //�����o���̃I�[�f�B�I�N���b�v
    [SerializeField]
    private AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    [SerializeField]
    private AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    [SerializeField]

    public bool Soundonoff = true;

    private void Sound()
    {
        if (ONoff == 0)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = footstepSound;
                audioSource.Play();
            }
        }
        if (ONoff == 1)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    private void AttackSiund()
    {
        if (ONoff == 1)//EnemyChaseG1.detectionPlayerG1 <= EnemyChaseG1.Detection)
        {
            if (Soundonoff == true)
            {
                audioSource.clip = SoundAttck;
                audioSource.Play();
            }
        }
        if (ONoff == 0)
        {
            if (Soundonoff == false)
            {
                audioSource.Stop();
            }
        }
    }

    private  void Start()
    {
        animator = GetComponent<Animator>();
        MoveToNextPatrolPoint();
    }

    private  void Update()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 1)//�����Ȃ��Ƃ�
        {
            SoundTime += Time.deltaTime;
            if (SoundTime > 10.0f)
            {
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Sphere.SetActive(true);//���g��\�����\��
            }
        }
        if (ONoff == 0)//�����Ă���Ƃ�
        {
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

        if (target != null)
        {
            // Player������ꍇ�͒ǂ�������
            transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
        else if (isPatrolling)
        {
            // ���񒆂̏ꍇ�͏���|�C���g�Ɍ�����
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, chaseSpeed * Time.deltaTime);
            if (transform.position == patrolPoints[currentPointIndex].position)
            {
                // ����|�C���g�ɓ��B�������莞�Ԓ�~���A���̏���|�C���g�Ɉړ�����
               // animator.SetTrigger("ShakeHead");
                isPatrolling = false;
                Invoke("MoveToNextPatrolPoint", patrolInterval);
            }
        }

     
    }


    void MoveToNextPatrolPoint()
    {
        // ���̏���|�C���g�ւ̃C���f�b�N�X���X�V
        currentPointIndex++;
        if (currentPointIndex >= patrolPoints.Length)
        {
            currentPointIndex = 0;
        }

        // ���񒆂ɖ߂�
        isPatrolling = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player�����m������ǂ�������
            target = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player���͈͊O�ɏo����ǐՂ���߂�
            target = null;
        }
    }
}

