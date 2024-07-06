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

    float Foff;
 
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
            F = true;
            Debug.Log("!%");
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
            F = true;
            Debug.Log("!%");
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

        //ìGGÇÃê≥ñ Ç…ìñÇΩÇ¡ÇΩéû
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            F = true;
            Debug.Log("!%");
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
            F = true;
            Debug.Log("!%");
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
            F = true;
            Debug.Log("!%");
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
            F = true;
            Debug.Log("!%");
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
                eobj.GetComponent<ParticleSystem>().Play();
                if (F == false)
                {
                    EC.Des = true;
                    EI.isHidden = false;
                    Debug.Log("?");
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
                    EI1.isHidden = false;
                    Debug.Log("?");
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
                    Debug.Log("?");
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
                    Debug.Log("?");
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
