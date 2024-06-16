using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    //private float EnemydeathTime=0.0f;
    ItemSearch ISe;

    //private float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    [SerializeField] public GameObject EnemyAttackArea;

    Rigidbody rb;
    Rigidbody rb1;

    private float stayTimeF = 0;
    private float stayTimeFG = 0;
    private float stayTimeB = 0;
    private float stayTimeBG = 0;

    LevelMeter levelMeter;


    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͌����Ȃ����
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        //���N���b�N�Ŕ͈͓�������
        if (/*Input.GetMouseButtonUp(0) ||*/ levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
            //this.transform.localScale = new Vector3(4, 4, 4);
        }

        if (onoff == 1)
        {
            //seentime += Time.deltaTime;
            if (/*seentime >= 10.0f*/levelMeter.nowdB <= 0.5f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
                //seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //�t���Ă���X�N���v�g���擾

        //�G�̐��ʂɓ���������
        if (other.CompareTag("EnemyForward") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeF += Time.deltaTime;
            if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeF < 10)//�w��ɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0.0f;
        }

        //�GG�̐��ʂɓ���������
        if (other.CompareTag("EnemyGForward") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeFG += Time.deltaTime;
            if (other.CompareTag("EnemyBackG") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeFG < 10)//�w��ɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0.0f;
        }

        //�G�̔w��ɓ���������
        if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeB += Time.deltaTime;
            Debug.Log("?");
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //�t���Ă���X�N���v�g���擾
            if (EC.ONoff == 1)
            {
                EI.isHidden = false;
            }
            //���ʂɓ����������ɔ��肵�Ȃ��悤�ɂ���
            if (other.CompareTag("EnemyForward"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        //�GG�̔w��ɓ���������
        if (other.CompareTag("EnemyBackG") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            Debug.Log("!");
            EnemysG ESG = eobjG.GetComponent<EnemysG>(); //�t���Ă���X�N���v�g���擾
            if (ESG.ONoff == 1)
            {
                if (ItemSeen.parentObject[0] != null)
                {
                    ItemSeen.parentObject[0].transform.position = eobjG.transform.position;
                    ISe.closetObject = ItemSeen.parentObject[0];
                }
                else if (ItemSeen.parentObject[1] != null)
                {
                    ItemSeen.parentObject[1].transform.position = eobjG.transform.position;
                    ISe.closetObject = ItemSeen.parentObject[1];
                }
                else if (ItemSeen.parentObject[2] != null)
                {
                    ItemSeen.parentObject[2].transform.position = eobjG.transform.position;
                    ISe.closetObject = ItemSeen.parentObject[1];
                }
                else if (ItemSeen.parentObject[3] != null)
                {
                    ItemSeen.parentObject[3].transform.position = eobjG.transform.position;
                    ISe.closetObject = ItemSeen.parentObject[3];
                }
                Destroy(eobjG);
                Enemyincrease.enemyDeathcnt++;
            }

            //�GG�̐��ʂɓ���������
            if (other.CompareTag("EnemyGForward") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeBG < 10)//���ʂɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (ItemSeen.Box.activeSelf == true)
        {
            rb = ItemSeen.Box.GetComponent<Rigidbody>();
            rb1 = ItemSeen.Box3.GetComponent<Rigidbody>();
            if (other.CompareTag("Box"))
            {
                rb.AddForce(transform.forward * 250.0f, ForceMode.Force);
                rb1.AddForce(transform.forward * 250.0f, ForceMode.Force);
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyForward") && BD.boundHeight >= 2)
        {
            stayTimeF = 0.0f;
        }
    }*/
}
