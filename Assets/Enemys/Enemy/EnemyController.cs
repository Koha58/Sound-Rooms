using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public GameObject Player;//�v���C���[���Q��
    public Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.05f;//Player��ǂ�������X�s�[�h
    private bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 

    public float ONoff = 0;//(0�������Ȃ��G�P���������ԁj
    private float Seetime;  //�o�ߎ���
    public float SoundTime;//�o�ߎ���

    float Enemystoptime = 0;
    float Enemystoponoff;

    //Animator animator;

    [SerializeField] public Transform _parentTransform;

    public GameObject Chase;
    public GameObject EnemyWall;

    public GameObject EnemyGetRandomPosition;

    public SkinnedMeshRenderer SkinnedMeshRendererEnemyBody;

    // Start is called before the first frame update
    void Start()
    {
        EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
        // �����ʒu�������_���ɐݒ肷��
        targetPosition =EGRP. GetRandomPosition();
        SkinnedMeshRendererEnemyBody.enabled = false;
        //animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����    
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
        EnemyChaseOnOff = false;
        
         //�u�����v�̃A�j���[�V�������Đ�����
         //animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            Debug.Log("!?");
            if (PS.onoff == 1 && EnemyChaseOnOff == true && ONoff == 1)
            {
                transform.position = transform.forward * ChaseSpeed;//�v���C���[�̕����Ɍ�����
                transform.LookAt(Player.transform); //�v���C���[�̕����ɂނ�
                                                    //�u����v�̃A�j���[�V�������Đ�����
                                                    //animator.SetBool("EnemyRun", true);
                Debug.Log("!!");
            }
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
                    EnemyGetRandomPosition EGRP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
                    targetPosition = EGRP.GetRandomPosition();
                    Enemystoponoff = 0;
                }
            }
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        EnemyChase EC = Chase.GetComponent<EnemyChase>();
        Enemywall EW = EnemyWall.GetComponent<Enemywall>();

        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

            if (EC.Chase == true && PS.onoff == 1)
            {
                EnemyChaseOnOff = true;
                Debug.Log("!?");
            }

        }

        if (EnemyChaseOnOff == false&&EW.Wall==true)
        {
            EnemyGetRandomPosition ERP = EnemyGetRandomPosition.GetComponent<EnemyGetRandomPosition>();
            targetPosition = ERP.GetRandomPosition();
        }

    }

    private void Switch()
    {
        var childTransforms = _parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
        if (ONoff == 0)//�����Ȃ��Ƃ�
        {
            EnemyChase EC = Chase.GetComponent<EnemyChase>();
            float randomTime = Random.Range(7f, 15f);
            SoundTime += Time.deltaTime;
            if (SoundTime >= randomTime)
            {
                SkinnedMeshRendererEnemyBody.enabled = false;
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
               SkinnedMeshRendererEnemyBody.enabled =true;
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
}
