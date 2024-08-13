using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    ItemSearch ISe;

    [SerializeField] public GameObject EnemyAttackArea;

    public TextMeshProUGUI keyCountText;
    public int count;
    [SerializeField] AudioSource PickupSound;

    private float stayTimeF = 0;
    private float stayTimeFG = 0;
    private float stayTimeB = 0;
    private float stayTimeBG = 0;

    LevelMeter levelMeter;

    bool F;
    float Fon;

    float Foff;
    [SerializeField]
    //private GameObject[] Prototype;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͌����Ȃ����
        EnemyAttackArea.GetComponent<Collider>().enabled = false;

        count = 0;
        SetCountText();
        PickupSound = GetComponent<AudioSource>();
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
            if (levelMeter.nowdB <= 0.4f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }

        if(F==true)
        {
            Foff += Time.deltaTime;
            if(Foff >1.0f)
            {
                F=false;
                Foff = 0.0f;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //�t���Ă���X�N���v�g���擾

        //�G�̐��ʂɓ���������
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            if(stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack"))
            {
                if (stayTimeF < 10)//�w��ɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        //�GG�̐��ʂɓ���������
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG"))
            {
                if (stayTimeFG < 10)//�w��ɓ����������ɔ��肵�Ȃ��悤�ɂ���
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //�G�̔w��ɓ���������
        if (other.CompareTag("EnemyBack"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>();

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI.isHidden = false;
                }
                
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

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();

                    Destroy(eobjG);
                    Enemyincrease.enemyDeathcnt++;
                    PickupSound.PlayOneShot(PickupSound.clip);
                    count += 1;
                    SetCountText();
                }
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

        if (other.CompareTag("EnemyG"))
        {
            GameObject EnemyG = GameObject.FindWithTag("EnemyG");
            Enemycontroller Ec = EnemyG.GetComponent<Enemycontroller>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(EnemyG);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

        if (other.CompareTag("EnemyGAnim"))
        {
            GameObject EnemyGAnim = GameObject.FindWithTag("EnemyGAnim");
            EnemyAnimcontroller EAc = EnemyGAnim.GetComponent<EnemyAnimcontroller>();
            if (EAc.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(EnemyGAnim);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

        if (other.CompareTag("Enemy"))
        {
            GameObject Enemy = GameObject.FindWithTag("Enemy");
            Enemycontroller Ec =Enemy.GetComponent<Enemycontroller>();
            EnemyIncrease EI = Enemy.GetComponent<EnemyIncrease>();
            if (Ec.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EI.isHidden = false;
            }
        }

        if (other.CompareTag("EnemyAnim"))
        {
            GameObject EnemyAnim = GameObject.FindWithTag("EnemyAnim");
            EnemyAnimcontroller EA = EnemyAnim.GetComponent<EnemyAnimcontroller>();
            EnemyAnimIncrease EAI = EnemyAnim.GetComponent<EnemyAnimIncrease>();
            if (EA.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                EAI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy2"))
        {
            GameObject Prototype4 = GameObject.FindWithTag("Enemy2");
            PrototypeController4 Prot4 = Prototype4.GetComponent<PrototypeController4>();
            Prototypeincrease PI = Prototype4.GetComponent<Prototypeincrease>();
            if (Prot4.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                PI.isHidden = false;
            }
        }

        if (other.CompareTag("Enemy3"))
        {
            GameObject Prototype3= GameObject.FindWithTag("Enemy3");
            PrototypeController3 Prot3 = Prototype3.GetComponent<PrototypeController3>();
            Prototypeincrease PI = Prototype3.GetComponent<Prototypeincrease>();
            if (Prot3.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                PI.isHidden = false;
            }
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }

}
