using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    [SerializeField] public GameObject EnemyAttackArea;

    public TextMeshProUGUI keyCountText;
    public int count;
    [SerializeField] AudioSource PickupSound;

    LevelMeter levelMeter;

    static public int enemyDeathcnt = 0;

    public static float DeathRange = 0f;//Enemyが死ぬと広がる範囲

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
        //最初は見えない状態
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
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //音を出すことで範囲内を可視化
        if (levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }

        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.4f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                onoff = 0;  //見えていないから0
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
            //Rigidbodyを取得
            var rb = other.GetComponent<Rigidbody>();

            //移動、回転を可能にする
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
