using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    ItemSearch ISe;

    [SerializeField] public GameObject EnemyAttackArea;

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
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        //�����o�����ƂŔ͈͓�������
        if (levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }

        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.3f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //�t���Ă���X�N���v�g���擾

        //�G�̐��ʂɓ���������
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            if (other.CompareTag("EnemyBack"))
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
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            if (other.CompareTag("EnemyBackG"))
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
        if (other.CompareTag("EnemyBack"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //�t���Ă���X�N���v�g���擾
            //Rigidbody EnemyR = eobj.GetComponent<Rigidbody>();

            if (EC.ONoff == 1)
            {
                EI.isHidden = false;
                Debug.Log("?");
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
        if (other.CompareTag("EnemyBackG"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //�t���Ă���X�N���v�g���擾
            if (EGC.ONoff == 1)
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
            if (other.CompareTag("EnemyGForward"))
            {
                if (stayTimeBG < 10)//���ʂɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (other.CompareTag("Box"))
        {
            //Rigidbody���擾
            var rb = other.GetComponent<Rigidbody>();

            //�ړ��A��]���\�ɂ���
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
    }

}
