using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    float speed = 1f;//�ړ��X�s�[�h
    public Transform Player;//�v���C���[���Q��
    Vector3 targetPosition;//Enemy�̖ړI�n
    float ChaseSpeed = 0.025f;//Player��ǂ�������X�s�[�h
    private float Detection = 6f; //�v���C���[�����m����͈�
    private float detectionPlayer;//�v���C���[�ƓG�̈ʒu�̌v�Z���i�[����l
    private bool EnemyChaseOnOff = false;//Player�̒ǐՂ�ONOFF 

    [SerializeField]
    private GameObject ebiPrefab;      //�R�s�[����v���n�u
    [SerializeField]
    private GameObject DestroyPrefab;  //�j�󂳂��v���n�u
    public bool isHiddens = true;      //
    private bool Clone = false;         //Clone�𐶂ݏo������ONOFF
    static public int enemyDeathcnt = 0;

    public Animator animator; //�A�j���[�V�����̊i�[

    //public GameObject eobj;
   // public EnemySeens ES; // EnemySeen�ɕt���Ă���X�N���v�g���擾


    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu�������_���ɐݒ肷��
        targetPosition = GetRandomPosition();
        animator = GetComponent<Animator>();   //�A�j���[�^�[�R���g���[���[����A�j���[�V�������擾����

    }

    // Update is called once per frame
    private void Update()
    {
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾

        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeens ES = eobj.GetComponent<EnemySeens>(); // EnemySeen�ɕt���Ă���X�N���v�g���擾

       
        // �u�����v�̃A�j���[�V�������Đ�����
        animator.SetBool("EnemyWalk", true);

        if (EnemyChaseOnOff == true)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
        {
            if (PS.onoff == 0)
            {
                for (int i = 0; i < 5; i++)//�q�I�u�W�F�N�g�̐����擾
                {
                    Transform childTransform = PS.parentObject.transform.GetChild(i);
                    PS.childObject = childTransform.gameObject;
                    PS.childObject.GetComponent<Renderer>().enabled = true;//������
                }
                PS.onoff = 1;  //�����Ă��邩��1
            }

            if (PS.onoff == 1 && EnemyChaseOnOff == true && ES.ONoff == 1)
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

     
        if (isHiddens == false)
        {
            isHiddens = true;
            GameObject go = Instantiate(ebiPrefab);//�R�s�[�𐶐�
            //Debug.Log(go);
            int px = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            int pz = Random.Range(0, 20);//0�ȏ�Q�O�ȉ��̃����_���̒l�𐶐�
            go.transform.position = new Vector3(px, 0, pz);
            Clone = true;

        }

        if (Clone == true)
        {
            Destroy(DestroyPrefab);
            Clone = false;
            enemyDeathcnt++;
        }

    }

    private Vector3 GetRandomPosition()
    {
        // �����_����x, z���W�𐶐�����
        float randomX = Random.Range(-46f, 46f);
        float randomY = 0f;// Random.Range(-10f, 10f);
        float randomZ = Random.Range(-46f, 46f);

        // �����������W��Ԃ�
        return new Vector3(randomX, randomY, randomZ);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemySeens ES = eobj.GetComponent<EnemySeens>(); // EnemySeen�ɕt���Ă���X�N���v�g���擾

        if (other.gameObject.CompareTag("Player"))
        {
            detectionPlayer = Vector3.Distance(transform.position, Player.position);//�v���C���[�ƓG�̈ʒu�̌v�Z

            if (detectionPlayer <= Detection && ES.ONoff == 1)//Enemy��������Ԃ��v���C���[�����m�͈͂ɓ�������
            {
                EnemyChaseOnOff = true;

            }
        }
    }
}
