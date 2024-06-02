using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public Transform Player;//�v���C���[���Q��
    public  Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.025f;//Player��ǂ�������X�s�[�h
    private float Detection = 5f; //�v���C���[�����m����͈�
    private float detectionPlayer;//�v���C���[�ƓG�̈ʒu�̌v�Z���i�[����l
    private bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 

    public Animator animator; //�A�j���[�V�����̊i�[

    public  float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public  float SoundTime;//�o�ߎ���
    [SerializeField] public GameObject Sphere;
    [SerializeField] public Transform _parentTransform;

    float Enemystoptime = 0;
    float Enemystoponoff;

    private float TargetTime;

    [SerializeField]
    private AudioClip SoundAttck;     //�����o���̃I�[�f�B�I�N���b�v
    [SerializeField]
    private  AudioClip footstepSound;     // �����̃I�[�f�B�I�N���b�v
    [SerializeField]
    private  AudioSource audioSource;     // �I�[�f�B�I�\�[�X
    [SerializeField]
   // private float volume = 50f;          // ����
   
    
    public bool Soundonoff = true;

    private Vector3 GetRandomPosition()
    {
        // �����_����x, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }

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

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GetRandomPosition();// �����ʒu�������_���ɐݒ肷��
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

        //tag��"EnemyParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

        foreach (var item in childTransforms)
        {
            //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
            item.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Sound();

        AttackSiund();

        float randomTime = Random.Range(5f, 10f);

        TargetTime = randomTime;

        //tag��"EnemyParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
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
            }
        }
        if (ONoff == 1)//�����Ă���Ƃ�
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
                Sphere.SetActive(false );//���g�\������\��
            }
        }

        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms_player = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 0)
            {
                foreach (var playerParts in childTransforms_player)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
                PS.onoff = 1;  //�����Ă��邩��1
            }

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ONoff == 1)
            {
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            }

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Player�����m�͈͂ɓ����Ă��Ȃ��܂���Player�������Ă��Ȃ�
        {
            // targetPosition�Ɍ������Ĉړ�����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }

        // targetPosition�ɓ���������V���������_���Ȉʒu��ݒ肷��
        if (transform.position == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                animator.SetBool("EnemyWalk", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    targetPosition = GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }
   
    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (detectionPlayer <= Detection && ONoff == 1)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
            {
                EnemyChaseOnOff = true;
            }
        }
    }
}
