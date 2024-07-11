using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController3 : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public GameObject Player;//�v���C���[���Q��
    Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.07f;//Player��ǂ�������X�s�[�h
    private bool EnemyChaseOnOff;//Player�̒ǐՂ�ONOFF 

    public float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public float SoundTime;//�o�ߎ���

    float Enemystoptime = 0;
    float Enemystoponoff;

    public Animator animator;

    public Transform _parentTransform;
    public EnemyChase Chase;
    public GameObject EnemyWall;
    public GameObject EnemyGetRandomPosition;
    public GameObject EnemysSeen3;
    public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;
    public MeshRenderer Ear;
    public MeshRenderer Eey;

    float TimeWall;
    float PTime;
    bool SeenArea;

    // Start is called before the first frame update
    private void Start()
    {
        ONoff = 0;
        EnemyChaseOnOff = false;
        EnemyGetRandomPosition3 EGRP3 = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition3>();
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = EGRP3.GetRandomPosition();
        SkinnedMeshRendererEnemyBody.enabled = false;
        Ear.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
        Eey.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����    
        SeenArea = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();

        //�u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);
        //�u����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyRun", false);

        if (SeenArea == true)
        {
            SeenArea = false;
            ONoff = 0;
            Enemystoptime = 0.0f;
            SkinnedMeshRendererEnemyBody.enabled = false;
            Ear.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
            Eey.GetComponent<MeshRenderer>().enabled = false;
        }

        Switch();

        if (EW.Wall == true)
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 4.0f)
            {
                EnemyGetRandomPosition1 ERP1 = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition1>();
                targetPosition = ERP1.GetRandomPosition();
                TimeWall = 0.0f;
            }
        }

        if (EC.Chase == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
            transform.position += transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
            ONoff = 1f;
            SkinnedMeshRendererEnemyBody.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
            Eey.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
            EnemysSeen3 EsS = EnemysSeen3.GetComponent<EnemysSeen3>();
            EsS.Enemys.enabled = true;
            //�u�����v�̃A�j���[�V�������~
            animator.SetBool("EnemyWalk", false);
            //�u����v�̃A�j���[�V�������Đ�����
            animator.SetBool("EnemyRun", true);

        }
        else if (EnemyChaseOnOff == false || PS.onoff == 0)//Player�����m�͈͂ɓ����Ă��Ȃ��܂���Player�������Ă��Ȃ�
        {
            // targetPosition�Ɍ������Ĉړ�����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.LookAt(targetPosition);
        }

        // targetPosition�ɓ���������V���������_���Ȉʒu��ݒ肷��
        if (transform.localPosition == targetPosition)
        {
            Enemystoponoff = 1;
            if (Enemystoponoff == 1)
            {
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    EnemyGetRandomPosition3 EGRP3 = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition3>();
                    targetPosition = EGRP3.GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }

    private void Switch()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemyChase EC = Chase.GetComponent<EnemyChase>();
            float randomTime = Random.Range(15f, 20f);
            SoundTime += Time.deltaTime;
            if (SoundTime >= randomTime)
            {
                SkinnedMeshRendererEnemyBody.enabled = false;
                Ear.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
                Eey.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
            }
        }

        if (ONoff == 1)//�����Ă���Ƃ�
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                SkinnedMeshRendererEnemyBody.enabled = true;
                Ear.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
                Eey.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSeen PS;
            GameObject gobj = GameObject.Find("Player");
            PS = gobj.GetComponent<PlayerSeen>();

            PTime += Time.deltaTime;
            if (PTime > 0.01999f)
            {
                PS.onoff = 1;
                PTime = 0.0f;
            }
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 0.5f)
            {
                EnemyGetRandomPosition3 EGRP3 = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition3>();
                targetPosition = EGRP3.GetRandomPosition();
                TimeWall = 0.0f;
            }
        }

        if (other.gameObject.CompareTag("InWall"))
        {
            TimeWall += Time.deltaTime;
            if (TimeWall > 0.5f)
            {
                EnemyGetRandomPosition3 EGRP3 = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition3>();
                targetPosition = EGRP3.GetRandomPosition();
                TimeWall = 0.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            SeenArea = true;
            ONoff = 1;
            SoundTime = 0.0f;
            SkinnedMeshRendererEnemyBody.enabled = true;
            Ear.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
            Eey.GetComponent<MeshRenderer>().enabled = true;//������i�L���j
            //Debug.Log("Enemy");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SeenArea"))
        {
            SeenArea = false;
            ONoff = 0;
            Enemystoptime = 0.0f;
            SkinnedMeshRendererEnemyBody.enabled = false;
            Ear.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
            Eey.GetComponent<MeshRenderer>().enabled = false;//������i�L���j
            //Debug.Log("EnemySSS");
        }
    }
}
