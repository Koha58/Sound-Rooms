using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    //private float EnemydeathTime=0.0f;
   
    EnemySeen ES;
    ButtonHoldDown BD;

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    [SerializeField] public GameObject EnemyAttackArea;

    Rigidbody rb;
    //ItemSeen IS;
    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͌����Ȃ����
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N�Ŕ͈͓�������
        if (Input.GetMouseButtonUp(0))
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }

        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //�t���Ă���X�N���v�g���擾
        if (other.CompareTag("EnemyBack") && BD.boundHeight >= 2)
        {
           // GameObject eobj = GameObject.Find("Enemy");
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 1)
            {
                Enemyincrease.isHidden = false;
            }
               
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBack1") && BD.boundHeight >= 2)
        {
            
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff ==1)
            {
                // GameObject eobj = GameObject.Find("Enemy1");
                //GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                Enemyincrease1.isHidden1 = false;
                // Enemy1.Enemy01.SetActive(false);
                //Destroy(eobj1);
                //Debug.Log("1");

                /*
                if (Enemyincrease1.Clone==true)
                {
                    //Debug.Log("2");
                    //Debug.Log("3");
                    //GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                    //Enemyincrease1.isHidden1 = false;
                   // Destroy(eobj1);
                   // Enemyincrease1.Clone = false;
                }
                */
            }
        }
        //GameObject iobj = GameObject.Find("SeenArea");
        //IS = iobj.GetComponent<ItemSeen>(); //�t���Ă���X�N���v�g���擾
        
        if (ItemSeen.Box.activeSelf == true)
        {
            rb = ItemSeen.Box.GetComponent<Rigidbody>();
            if (other.CompareTag("Box") && BD.boundHeight >= 2)
            {
                rb.AddForce(transform.forward * 250.0f, ForceMode.Force);
            }
        }
    }
}
