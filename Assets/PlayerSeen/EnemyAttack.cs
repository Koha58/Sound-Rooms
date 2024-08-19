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

            GetComponent<ParticleSystem>().Play();
            Destroy(EnemyG);
            Enemyincrease.enemyDeathcnt++;
            PickupSound.PlayOneShot(PickupSound.clip);
            count += 1;
            SetCountText();

            GameObject Boss = GameObject.FindWithTag("Boss");
            BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

            BS.MoveSpeed = -0.1f;
            BS.ChaseSpeed = -0.1f;
            BS.VisualizationPlayer = -1f;
            BS.ONOFF = 1;
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

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
        if (other.CompareTag("Enemy2G"))
        {
            GameObject Enemy2 = GameObject.FindWithTag("Enemy2G");
            Enemy2controller E2 = Enemy2.GetComponent<Enemy2controller>();
            if (E2.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(Enemy2);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();

                GameObject Boss = GameObject.FindWithTag("Boss");
                BoosEnemy BS = Boss.GetComponent<BoosEnemy>();

                BS.MoveSpeed = -0.1f;
                BS.ChaseSpeed = -0.1f;
                BS.VisualizationPlayer = -1f;
                BS.ONOFF = 1;
            }
        }
    }

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }

}
