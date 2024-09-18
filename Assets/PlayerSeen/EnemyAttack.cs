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

    public static bool SoundON;

    //[SerializeField]
    //private GameObject[] Prototype;

    // Start is called before the first frame update
    void Start()
    {
        //�ŏ��͌����Ȃ����
        EnemyAttackArea.GetComponent<Collider>().enabled = false;

        count = 0;
        SetCountText();
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
       
            if (DC >= 10.0f)
            {
                SoundON = true;
                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed -= 0.1f;
                BS.ChaseSpeed -= 0.01f;
                BS.ONOFF = 1;
                if (SoundON == true&& DC >= 12.0f)
                {

                    DC = 0;
                    DB = false;
                }
            }
        }
        
        if (DB2 == true)
        {
            DC += Time.deltaTime;
          
            if (DC >= 10.0f)
            {
                SoundON = true;
                GameObject Boss1 = GameObject.FindWithTag("Boss1");
                BossTutoriaru BS1 = Boss1.GetComponent<BossTutoriaru>();

                BS1.MoveSpeed -= 0.1f;
                BS1.ChaseSpeed -= 0.01f;
                BS1.ONOFF = 1;
                if (SoundON == true&& DC >= 12.0f)
                {

                    DC = 0;
                    DB2 = false;
                }
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
            BoosEnemy EC = other.GetComponent<BoosEnemy>();
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
                Debug.Log(enemyDeathcnt);
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
            EnemyController EC1 = other.GetComponent<EnemyController>();
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
            EnemyController ECG = other.GetComponent<EnemyController>();
            if (ECG.DestroyONOFF == true)
            {
                enemyDeathcnt++;
                DeathRange += 1.0f;
                GetComponent<ParticleSystem>().Play();
                Destroy(other.gameObject);
                //Enemyincrease.enemyDeathcnt++;
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
