using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//Enemy�ɉ����Ԃ��鋓��
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    [SerializeField] public GameObject EnemyAttackArea;

    public TextMeshProUGUI keyCountText;
    public int count;
    [SerializeField] AudioSource PickupSound;

    LevelMeter levelMeter;

    static public int enemyDeathcnt = 0;

    public static float DeathRange = 0f;//Enemy�����ʂƍL����͈�

    public static bool BossTiming;

    float DC;

    bool DB = false;

    bool DB2 = false;

    float Count;
    public static bool SoundON;


    //[SerializeField]
    //private GameObject[] Prototype;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        SetCountText();
        EnemyAttackArea.GetComponent<Collider>().enabled = false;//�ŏ��͌����Ȃ����
        PickupSound = GetComponent<AudioSource>();
        BossTiming = false;
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
        
        if (DB == true)
        {
            DC += Time.deltaTime;

            if (DC >= 9.5f)
            {
                SoundON = true;
            }
            if (DC >= 10.0f)
            {
                Count=1;
                if (Count == 1)
                {
                    GameObject Boss = GameObject.FindWithTag("Boss");
                    BossEnemyControll BEC = Boss.GetComponent<BossEnemyControll>();
                    BEC.ONOFF = 1;
                    Count = 2;
                }
            }
            if (DC >= 20.0f)
            {
                DC = 0;
                DB = false;
                SoundON = false;
                Count = 0;
            }
        }
        
        if (DB2 == true)
        {
            DC += Time.deltaTime;
            if (DC >= 9.5f)
            {
                SoundON = true;
            }
            if (DC >=10.0f)
            {
                Count = 1;
                if (Count == 1)
                {
                    SoundON = true;
                    GameObject Boss1 = GameObject.FindWithTag("Boss1");
                    BossTutoriaru BS1 = Boss1.GetComponent<BossTutoriaru>();
                    BS1.ONOFF = 1;
                    Count = 2;
                }
            }
            if (DC >= 20.0f)
            {
                SoundON = false;
                DC = 0;
                DB2 = false;
                Count = 0;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            //Rigidbody���擾
            var rb = other.GetComponent<Rigidbody>();

            //�ړ��A��]���\�ɂ���
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
        if (other.CompareTag("Boss"))
        {
            BossEnemyControll BEC = other.GetComponent<BossEnemyControll>();
            if (BEC.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Debug.Log(enemyDeathcnt);
                Destroy(other.gameObject);


                DB = true;
            }
        }

        if (other.CompareTag("Boss1"))
        {
            BossTutoriaru EC = other.GetComponent<BossTutoriaru>();
            if (EC.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Debug.Log(enemyDeathcnt);
                Destroy(other.gameObject);

                DB = true;
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Enemycontroller EC = other.GetComponent<Enemycontroller>();
            if (EC.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);

                BossTiming = true;

                DB = true;
            }
        }

        if (other.CompareTag("EnemySearch"))
        {
            EnemySearchcontroller ESC = other.GetComponent<EnemySearchcontroller>();
            if (ESC.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Debug.Log(enemyDeathcnt);
                Destroy(other.gameObject);

                BossTiming = true;

                DB = true;
            }
        }

        if (other.CompareTag("EnemyG"))
        {
            Enemycontroller EC = other.GetComponent<Enemycontroller>();

            enemyDeathcnt++;
            DeathRange += 1.0f;
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
            //Enemyincrease.enemyDeathcnt++;
            PickupSound.PlayOneShot(PickupSound.clip);
            count += 1;
            SetCountText();

            BossTiming = true;


            DB = true;

        }

        if (other.CompareTag("Enemy1"))
        {
            TutorialEnemyController EC1 = other.GetComponent<TutorialEnemyController>();
            if (EC1.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);

                other.gameObject.SetActive(false);

                BossTiming = true;

                DB2 = true;
            }
        }

        if (other.CompareTag("Enemy2G"))
        {
            TutorialEnemyController ECG = other.GetComponent<TutorialEnemyController>();
            if (ECG.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();

                BossTiming = true;

                DB2 = true;
            }
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }

    
}
