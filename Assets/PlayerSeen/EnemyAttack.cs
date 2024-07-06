using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy‚É‰¹‚ğ‚Ô‚Â‚¯‚é‹““®
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //”»’è—piŒ©‚¦‚Ä‚¢‚È‚¢F0/Œ©‚¦‚Ä‚¢‚éF1j

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
        //Å‰‚ÍŒ©‚¦‚È‚¢ó‘Ô
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾

        //‰¹‚ğo‚·‚±‚Æ‚Å”ÍˆÍ“à‚ğ‰Â‹‰»
        if (levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//Œ©‚¦‚éi—LŒøj
            onoff = 1;  //Œ©‚¦‚Ä‚¢‚é‚©‚ç1
        }

        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.3f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//Œ©‚¦‚È‚¢i–³Œøj
                onoff = 0;  //Œ©‚¦‚Ä‚¢‚È‚¢‚©‚ç0
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
        ISe = sobj.GetComponent<ItemSearch>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾

        //“G‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBack"))
            {
                if (stayTimeF < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“G‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyForward1"))
        {
            stayTimeF += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBack1"))
            {
                if (stayTimeF < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBackG"))
            {
                if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyGForward1"))
        {
            stayTimeFG += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBackG1"))
            {
                if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyGForward2"))
        {
            stayTimeFG += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBackG2"))
            {
                if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyGForward3"))
        {
            stayTimeFG += Time.deltaTime;
            F = true;
            Debug.Log("!%");
            if (other.CompareTag("EnemyBackG3"))
            {
                if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
        }

        //“G‚Ì”wŒã‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyBack"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>();
            if (stayTimeB <= stayTimeF)
            {
                stayTimeB += 20f;
            }

            if (stayTimeB >= stayTimeF)
            {
                Debug.Log("09");
                if (F == false)
                {
                    EI.isHidden = false;
                    Debug.Log("?");
                }
            }
            
            //³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

        //“G‚Ì”wŒã‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyBack1"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
            Enemyincrease1 EI1 = eobj1.GetComponent<Enemyincrease1>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
            if (stayTimeB <= stayTimeF)
            {
                stayTimeB += 20f;
            }

            if (stayTimeB >= stayTimeF)
            {
                if (F == false)
                {
                    EI1.isHidden = false;
                    Debug.Log("?");
                }
            }

            //³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

        //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
        if (other.CompareTag("EnemyBackG"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
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
            }

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward"))
            {
                if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
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
            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward1"))
            {
                if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward2"))
            {
                if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward3"))
            {
                if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (other.CompareTag("Box"))
        {
            //Rigidbody‚ğæ“¾
            var rb = other.GetComponent<Rigidbody>();

            //ˆÚ“®A‰ñ“]‚ğ‰Â”\‚É‚·‚é
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
    }

    /*  private void OnTriggerEnter(Collider other)
        {
            GameObject sobj = GameObject.Find("Player");
            ISe = sobj.GetComponent<ItemSearch>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾

            //“G‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyForward"))
            {
                stayTimeF += Time.deltaTime;
                if (other.CompareTag("EnemyBack"))
                {
                    if (stayTimeF < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“G‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyForward1"))
            {
                stayTimeF += Time.deltaTime;
                if (other.CompareTag("EnemyBack1"))
                {
                    if (stayTimeF < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG"))
                {
                    if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward1"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG1"))
                {
                    if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward2"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG2"))
                {
                    if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyGForward3"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG3"))
                {
                    if (stayTimeFG < 10)//”wŒã‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“G‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBack"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>();
                Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
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

                //³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“G‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBack1"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
                Enemyincrease1 EI1 = eobj1.GetComponent<Enemyincrease1>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾

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

                //³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
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

            //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBackG"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
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

                //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
                if (other.CompareTag("EnemyGForward"))
                {
                    if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBackG1"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
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

                //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
                if (other.CompareTag("EnemyGForward1"))
                {
                    if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBackG2"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
                EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
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

                //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
                if (other.CompareTag("EnemyGForward2"))
                {
                    if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //“GG‚Ì”wŒã‚É“–‚½‚Á‚½
            if (other.CompareTag("EnemyBackG3"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
                EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //•t‚¢‚Ä‚¢‚éƒXƒNƒŠƒvƒg‚ğæ“¾
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

                //“GG‚Ì³–Ê‚É“–‚½‚Á‚½
                if (other.CompareTag("EnemyGForward3"))
                {
                    if (stayTimeBG < 10)//³–Ê‚É“–‚½‚Á‚½‚É”»’è‚µ‚È‚¢‚æ‚¤‚É‚·‚é
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            if (other.CompareTag("Box"))
            {
                //Rigidbody‚ğæ“¾
                var rb = other.GetComponent<Rigidbody>();

                //ˆÚ“®A‰ñ“]‚ğ‰Â”\‚É‚·‚é
                rb.constraints = RigidbodyConstraints.None;

                rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
            }
        }*/

}
