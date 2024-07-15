using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EnemyÇ…âπÇÇ‘Ç¬ÇØÇÈãììÆ
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //îªíËópÅiå©Ç¶ÇƒÇ¢Ç»Ç¢éûÅF0/å©Ç¶ÇƒÇ¢ÇÈéûÅF1Åj

    ItemSearch ISe;

    [SerializeField] public GameObject EnemyAttackArea;

    private float stayTimeF = 0;
    private float stayTimeFG = 0;
    private float stayTimeB = 0;
    private float stayTimeBG = 0;

    LevelMeter levelMeter;

    bool F;
    float Fon;

    float Foff;
    [SerializeField]
    private GameObject[] Prototype;

    // Start is called before the first frame update
    void Start()
    {
        //ç≈èâÇÕå©Ç¶Ç»Ç¢èÛë‘
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

        //âπÇèoÇ∑Ç±Ç∆Ç≈îÕàÕì‡Çâ¬éãâª
        if (levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//å©Ç¶ÇÈÅióLå¯Åj
            onoff = 1;  //å©Ç¶ÇƒÇ¢ÇÈÇ©ÇÁ1
        }

        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.3f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//å©Ç¶Ç»Ç¢Åiñ≥å¯Åj
                onoff = 0;  //å©Ç¶ÇƒÇ¢Ç»Ç¢Ç©ÇÁ0
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
        ISe = sobj.GetComponent<ItemSearch>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

        //ìGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            if(stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        //ìGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyForward1"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack1"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        //ìGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyForward2"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack2"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward3"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack3"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward4"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack4"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward5"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack5"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward6"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack6"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward7"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack7"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward8"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack8"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward9"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack9"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        if (other.CompareTag("EnemyForward10"))
        {
            stayTimeF += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack10"))
            {
                if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG"))
            {
                if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyGForward1"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG1"))
            {
                if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyGForward2"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG2"))
            {
                if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyGForward3"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG3"))
            {
                if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
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
            
            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
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

        //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBack1"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
            Enemyincrease1 EI1 = eobj1.GetComponent<Enemyincrease1>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
        
            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI1.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward1"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBack2"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj2 = GameObject.FindWithTag("Enemy2");
            Enemyincrease2 EI2 = eobj2.GetComponent<Enemyincrease2>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI2.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward2"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBack3"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj3 = GameObject.FindWithTag("Enemy3");
            Enemyincrease3 EI3 = eobj3.GetComponent<Enemyincrease3>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI3.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward3"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        if (other.CompareTag("EnemyBack4"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj4 = GameObject.FindWithTag("Enemy4");
            Enemyincrease4 EI4 = eobj4.GetComponent<Enemyincrease4>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI4.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward4"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        if (other.CompareTag("EnemyBack5"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj5 = GameObject.FindWithTag("Enemy5");
            Enemyincrease5 EI5 = eobj5.GetComponent<Enemyincrease5>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI5.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward5"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        if (other.CompareTag("EnemyBack6"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj6 = GameObject.FindWithTag("Enemy6");
            Enemyincrease6 EI6 = eobj6.GetComponent<Enemyincrease6>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI6.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward6"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }


        if (other.CompareTag("EnemyBack7"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj7 = GameObject.FindWithTag("Enemy7");
            Enemyincrease7 EI7= eobj7.GetComponent<Enemyincrease7>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI7.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward7"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }


        if (other.CompareTag("EnemyBack8"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj8 = GameObject.FindWithTag("Enemy8");
            Enemyincrease8 EI8 = eobj8.GetComponent<Enemyincrease8>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI8.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward8"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }


        if (other.CompareTag("EnemyBack9"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj9 = GameObject.FindWithTag("Enemy9");
            Enemyincrease9 EI9 = eobj9.GetComponent<Enemyincrease9>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI9.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward9"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        if (other.CompareTag("EnemyBack10"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj10 = GameObject.FindWithTag("Enemy10");
            Enemyincrease10 EI10 = eobj10.GetComponent<Enemyincrease10>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI10.isHidden = false;
                }
            }

            //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
            if (other.CompareTag("EnemyForward9"))
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
        }

        //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBackG"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
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
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward"))
            {
                if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBackG1"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
            EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>();

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG1);
                    Enemyincrease.enemyDeathcnt++;
                }
            }
            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward1"))
            {
                if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBackG2"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
            EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>();

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    Debug.Log("?");
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG2);
                    Enemyincrease.enemyDeathcnt++;
                }
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward2"))
            {
                if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyBackG3"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
            EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>();

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();
                    Debug.Log("?");
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG3);
                    Enemyincrease.enemyDeathcnt++;
                }
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward3"))
            {
                if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (other.CompareTag("Box"))
        {
            //RigidbodyÇéÊìæ
            var rb = other.GetComponent<Rigidbody>();

            //à⁄ìÆÅAâÒì]Çâ¬î\Ç…Ç∑ÇÈ
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }



        if(other.CompareTag("Prototype"))
        {
            if (Prototype[0] = GameObject.Find("Prototype"))
            {
                GameObject Prototype = GameObject.Find("Prototype");
                PrototypeController Prot = Prototype.GetComponent<PrototypeController>();
                if (Prot.DestroyONOFF == true)
                {
                    GetComponent<ParticleSystem>().Play();
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(Prototype);
                }
            }

            if (Prototype[1] = GameObject.Find("Prototype"))
            {
                GameObject Prototype = GameObject.Find("Prototype (1)");
                PrototypeController Prot = Prototype.GetComponent<PrototypeController>();
                if (Prot.DestroyONOFF == true)
                {
                    GetComponent<ParticleSystem>().Play();
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = Prototype.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(Prototype);
                }
            }
        }

        if (other.CompareTag("Prototype1"))
        {
            if (Prototype[2] = GameObject.Find("Prototype1"))
            {
                GameObject Prototype = GameObject.Find("Prototype1");
                PrototypeController1 Prot1 = Prototype.GetComponent<PrototypeController1>();
                Enemyincrease EI = Prototype.GetComponent<Enemyincrease>();
                if (Prot1.DestroyONOFF == true)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI.isHidden = false;
                }
            }

            if (Prototype[3] = GameObject.Find("Prototype1 (1)"))
            {
                GameObject Prototype1 = GameObject.Find("Prototype1 (1)");
                PrototypeController1 Prot1 = Prototype1.GetComponent<PrototypeController1>();
                Enemyincrease EI = Prototype1.GetComponent<Enemyincrease>();
                if (Prot1.DestroyONOFF == true)
                {
                    GetComponent<ParticleSystem>().Play();
                    EI.isHidden = false;
                }
            }

        }
    }

    /*  private void OnTriggerEnter(Collider other)
        {
            GameObject sobj = GameObject.Find("Player");
            ISe = sobj.GetComponent<ItemSearch>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

            //ìGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyForward"))
            {
                stayTimeF += Time.deltaTime;
                if (other.CompareTag("EnemyBack"))
                {
                    if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                //stayTimeF = 0.0f;
                if (stayTimeB <= stayTimeF)
                {
                    Des = false;
                    stayTimeF += 10.0f;
                }
                Debug.Log("!%");
            }

            //ìGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyForward1"))
            {
                stayTimeF += Time.deltaTime;
                if (other.CompareTag("EnemyBack1"))
                {
                    if (stayTimeF < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
               // stayTimeF = 0.0f;
                if (stayTimeB <= stayTimeF)
                {
                    Des = false;
                    stayTimeF += 10.0f;
                }
                Debug.Log("!%");
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG"))
                {
                    if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }

                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = false;
                    stayTimeF += 10.0f;
                }
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward1"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG1"))
                {
                    if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }

                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = false;
                    stayTimeF += 10.0f;
                }
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward2"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG2"))
                {
                    if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }

                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = false;
                    stayTimeF += 10.0f;
                }
            }

            //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyGForward3"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG3"))
                {
                    if (stayTimeFG < 10)//îwå„Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }

                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = false;
                    stayTimeF += 10.0f;
                }
            }

            //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBack"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>();
                Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
                //Rigidbody EnemyR = eobj.GetComponent<Rigidbody>();
                if (stayTimeB >= stayTimeF)
                {
                    Des = true;
                    stayTimeF = 0.0f;
                }
                if (EC.ONoff == 1 && Des == true)
                {
                        EI.isHidden = false;
                        Debug.Log("?");
                        Des = false;
                }

                //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
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

            //ìGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBack1"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
                Enemyincrease1 EI1 = eobj1.GetComponent<Enemyincrease1>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ

                if (stayTimeB >= stayTimeF)
                {
                    Des = true;
                    stayTimeF = 0.0f;
                }

                if (EC1.ONoff == 1 && Des == true)
                {
                        EI1.isHidden = false;
                        Debug.Log("?");
                        Des = false;
                }

                //ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                if (other.CompareTag("EnemyForward1"))
                {
                    if (stayTimeB < 10)
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeB = 0.0f;
            }

            //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBackG"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = true;
                    stayTimeFG = 0.0f;
                }
                if (EGC.ONoff == 1 && DesG == true)
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

                //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
                if (other.CompareTag("EnemyGForward"))
                {
                    if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBackG1"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = true;
                    stayTimeFG = 0.0f;
                }

                if (EGC1.ONoff == 1 && DesG == true)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG1);
                    Enemyincrease.enemyDeathcnt++;
                }

                //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
                if (other.CompareTag("EnemyGForward1"))
                {
                    if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBackG2"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
                EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = true;
                    stayTimeFG = 0.0f;
                }

                if (EGC2.ONoff == 1 && DesG == true)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG2);
                    Enemyincrease.enemyDeathcnt++;
                }

                //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
                if (other.CompareTag("EnemyGForward2"))
                {
                    if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //ìGGÇÃîwå„Ç…ìñÇΩÇ¡ÇΩéû
            if (other.CompareTag("EnemyBackG3"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
                EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //ïtÇ¢ÇƒÇ¢ÇÈÉXÉNÉäÉvÉgÇéÊìæ
                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = true;
                    stayTimeFG = 0.0f;
                }

                if (EGC3.ONoff == 1 && DesG == true)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobjG3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobjG3);
                    Enemyincrease.enemyDeathcnt++;
                }

                //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
                if (other.CompareTag("EnemyGForward3"))
                {
                    if (stayTimeBG < 10)//ê≥ñ Ç…ìñÇΩÇ¡ÇΩéûÇ…îªíËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            if (other.CompareTag("Box"))
            {
                //RigidbodyÇéÊìæ
                var rb = other.GetComponent<Rigidbody>();

                //à⁄ìÆÅAâÒì]Çâ¬î\Ç…Ç∑ÇÈ
                rb.constraints = RigidbodyConstraints.None;

                rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
            }
        }*/

}
