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

    private float stayTimeF = 0;
    private float stayTimeB = 0;
    public GameObject dropItemObj;//�@���Ƃ��A�C�e���Q�[���I�u�W�F�N�g
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

        if (other.CompareTag("EnemyForward") && BD.boundHeight >= 2)
        {
            stayTimeF += Time.deltaTime;
        }

        if (other.CompareTag("EnemyBack") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj = GameObject.FindWithTag("Enemy");
                ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
                {
                    Enemyincrease.isHidden = false;
                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
                //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBack1") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
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
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
        }

        if (other.CompareTag("EnemyBackG1") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                ES = eobjG1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
                {

                    EnemyincreaseG2.isHiddenG2 = false;
                    Instantiate(dropItemObj, transform.position, Quaternion.identity);

                    Destroy(eobjG1);

                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBackG2") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj2 = GameObject.FindWithTag("EnemyG2");
                ES = eobj2.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
                {

                    EnemyincreaseG2.isHiddenG2 = false;
                    Instantiate(dropItemObj, transform.position, Quaternion.identity);

                    Destroy(eobj2);

                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBackG3") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj3 = GameObject.FindWithTag("EnemyG3");
                ES = eobj3.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
                {

                    //EnemyincreaseG3.isHiddenG3= false;
                    Instantiate(dropItemObj, transform.position, Quaternion.identity);

                    Destroy(eobj3);

                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBackG4") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj4 = GameObject.FindWithTag("EnemyG4");
                ES = eobj4.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
                if (ES.ONoff == 1)
                {

                    //EnemyincreaseG2.isHiddenG2 = false;
                    Instantiate(dropItemObj, transform.position, Quaternion.identity);

                    Destroy(eobj4);

                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
            //Destroy(eobj);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyForward") && BD.boundHeight >= 2)
        {
            stayTimeF = 0.0f;
        }
    }
}
