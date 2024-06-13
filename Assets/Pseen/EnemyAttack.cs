using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    //private float EnemydeathTime=0.0f;
    ButtonHoldDown BD;
    ItemSearch ISe;

    //private float seentime = 0.0f; //経過時間記録用
    [SerializeField] public GameObject EnemyAttackArea;

    Rigidbody rb;
    Rigidbody rb1;

    private float stayTimeF = 0;
    private float stayTimeB = 0;

    LevelMeter levelMeter;


    // Start is called before the first frame update
    void Start()
    {
        //最初は見えない状態
        EnemyAttackArea.GetComponent<Collider>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //左クリックで範囲内を可視化
        if (/*Input.GetMouseButtonUp(0) ||*/ levelMeter.nowdB > 0.0f)
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
            //this.transform.localScale = new Vector3(4, 4, 4);
        }

        if (onoff == 1)
        {
            //seentime += Time.deltaTime;
            if (/*seentime >= 10.0f*/levelMeter.nowdB <= 0.5f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                onoff = 0;  //見えていないから0
                //seentime = 0.0f;    //経過時間をリセット
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得
        GameObject hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //付いているスクリプトを取得

        if (other.CompareTag("EnemyForward") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeF += Time.deltaTime;
            if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeF < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0.0f;
        }
        
        if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeB += Time.deltaTime;
            Debug.Log("?");

            Debug.Log("!");
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyFailurework EF = eobj.GetComponent<EnemyFailurework>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //付いているスクリプトを取得
            if (EF.ONoff == 1)
            {
                EI.isHidden = false;
            }
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
        
        if (other.CompareTag("EnemyBackG") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeB += Time.deltaTime;
            Debug.Log("?");
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
                Debug.Log("!");
                EnemysG ESG = eobjG.GetComponent<EnemysG>(); //付いているスクリプトを取得
                if (ESG.ONoff == 1)
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

            if (other.CompareTag("EnemyForward") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeB < 10)
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeB = 0.0f;
            //Destroy(eobj);
        }
/*
        if (other.CompareTag("EnemyBackG1") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            // GameObject eobj = GameObject.Find("Enemy");
            if (stayTimeB > stayTimeF)
            {
                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                ES = eobjG1.GetComponent<EnemySeen>(); //付いているスクリプトを取得
                if (ES.ONoff == 1)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        //Instantiate(check1, transform.position, Quaternion.identity);
                        ItemSeen.parentObject[0].transform.position = eobjG1.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if(ItemSeen.parentObject[1] != null)
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
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBackG2") && BD.boundHeight >= 2)
        {
            stayTimeB += Time.deltaTime;
            if (stayTimeB > stayTimeF)
            {
                GameObject eobj2 = GameObject.FindWithTag("EnemyG2");
                ES = eobj2.GetComponent<EnemySeen>(); //付いているスクリプトを取得
                if (ES.ONoff == 1)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobj2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        ItemSeen.parentObject[1].transform.position = eobj2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobj2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[2];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobj2.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobj2);
                    Enemyincrease.enemyDeathcnt++;
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
                ES = eobj3.GetComponent<EnemySeen>(); //付いているスクリプトを取得
                if (ES.ONoff == 1)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobj3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        //Instantiate(check3, transform.position, Quaternion.identity);
                        ItemSeen.parentObject[1].transform.position = eobj3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobj3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[2];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobj3.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                
                    Destroy(eobj3);
                    Enemyincrease.enemyDeathcnt++;
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
                ES = eobj4.GetComponent<EnemySeen>(); //付いているスクリプトを取得
                if (ES.ONoff == 1)
                {
                    if (ItemSeen.parentObject[0] != null)
                    {
                        ItemSeen.parentObject[0].transform.position = eobj4.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[0];
                    }
                    else if (ItemSeen.parentObject[1] != null)
                    {
                        //Instantiate(check4, transform.position, Quaternion.identity);
                        ItemSeen.parentObject[1].transform.position = eobj4.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[1];
                    }
                    else if (ItemSeen.parentObject[2] != null)
                    {
                        ItemSeen.parentObject[2].transform.position = eobj4.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[2];
                    }
                    else if (ItemSeen.parentObject[3] != null)
                    {
                        ItemSeen.parentObject[3].transform.position = eobj4.transform.position;
                        ISe.closetObject = ItemSeen.parentObject[3];
                    }
                    Destroy(eobj4);
                    Enemyincrease.enemyDeathcnt++;
                }
            }
            stayTimeF = 0.0f;
            stayTimeB = 0.0f;
        }
        */
        if (ItemSeen.Box.activeSelf == true)
        {
            rb = ItemSeen.Box.GetComponent<Rigidbody>();
            rb1 = ItemSeen.Box3.GetComponent<Rigidbody>();
            if (other.CompareTag("Box") && BD.boundHeight >= 2)
            {
                rb.AddForce(transform.forward * 250.0f, ForceMode.Force);
                rb1.AddForce(transform.forward * 250.0f, ForceMode.Force);
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyForward") && BD.boundHeight >= 2)
        {
            stayTimeF = 0.0f;
        }
    }*/
}
