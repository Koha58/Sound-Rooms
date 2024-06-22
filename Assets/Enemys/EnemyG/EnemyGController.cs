using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGController : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public Transform Player;//�v���C���[���Q��
    public Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.02f;//Player��ǂ�������X�s�[�h
    private bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 

    public float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public float SoundTime;//�o�ߎ���

    float Enemystoptime = 0;
    float Enemystoponoff;

    private float Chaseonoff;

    // public Animator animator;

    public MeshRenderer Enemy;
    [SerializeField] public Transform _parentTransform;

    private float TargetTime;

    public GameObject GChase;

    public GameObject EnemyGGetRandomPosition;

    // Start is called before the first frame update
    private bool TouchWall = false;

    void Start()
    {
        EnemyGGetRandomPosition EGRP = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition>();
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = EGRP.GetRandomPositionG();
        //  animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����    
        Enemy = GetComponent<MeshRenderer>();
        Enemy.enabled = true;
        EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();
    }

    // Update is called once per frame
    private void Update()
    {
        Switch();
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        //tag��"PlayerParts"�ł���q�I�u�W�F�N�g��Transform�̃R���N�V�������擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        if (ONoff == 0)
        {
            EnemyChaseOnOff = false;
        }

        // �u�����v�̃A�j���[�V�������Đ�����
        // animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 0)
            {
                foreach (var playerParts in childTransforms)
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
                // animator.SetBool("EnemyWalk", false);
                Enemystoptime += Time.deltaTime;
                if (Enemystoptime > 2.0f)
                {
                    EnemyGGetRandomPosition EGRP = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition>();
                    targetPosition = EGRP.GetRandomPositionG();
                    Enemystoponoff = 0;
                }
            }
        }

        if (TouchWall == true)
        {
          
            //EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
            //targetPosition = EGRP.GetRandomPosition();
            TouchWall = false;
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        EnemysGChase EGC = GChase.GetComponent<EnemysGChase>();
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            if (EGC.GWall == false)
            {
                if (EGC.GChase == true && PS.onoff == 1) //EC.Wall == false
                {
                    EnemyChaseOnOff = true;
                }
            }
        }

        if (EnemyChaseOnOff == false && EGC.GWall == true)
        {
           TouchWall=true;  
            //transform.Rotate(new Vector3(0, 180, 0));
           // EnemyGGetRandomPosition EGRP = EnemyGGetRandomPosition.GetComponent<EnemyGGetRandomPosition>();
           // targetPosition = EGRP.GetRandomPositionG();
        }
    }

    private void Switch()
    {
        float randomTime = Random.Range(5f, 10f);
        TargetTime = randomTime;
        // var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
            SoundTime += Time.deltaTime;
            if (SoundTime >= TargetTime)
            {
                //foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    // item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ONoff = 1;
                SoundTime = 0.0f;
                Enemy.enabled = true;
                GameObject Chase = GameObject.FindWithTag("Chase");
                EnemyChase EC = Chase.GetComponent<EnemyChase>(); //EnemyFailurework�t���Ă���X�N���v�g���擾
            }
        }
        if (ONoff == 1)//�����Ă���Ƃ�
        {
            Seetime += Time.deltaTime;
            if (Seetime >= 10.0f)
            {
                //foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                    // item.gameObject.GetComponent<Renderer>().enabled = false;
                }
                ONoff = 0;
                Seetime = 0.0f;
                Enemy.enabled = false;
            }
        }
    }
}


