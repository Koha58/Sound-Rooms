using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    //private float EnemydeathTime=0.0f;
    ItemSearch ISe;

    //private float seentime = 0.0f; //経過時間記録用
    [SerializeField] public GameObject EnemyAttackArea;

    Rigidbody rb;
    Rigidbody rb1;

    private float stayTimeF = 0;
    private float stayTimeFG = 0;
    private float stayTimeB = 0;
    private float stayTimeBG = 0;

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

    private void OnTriggerEnter(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        //敵の正面に当たった時
        if (other.CompareTag("EnemyForward") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeF += Time.deltaTime;
            if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeF < 10)//背後に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0.0f;
        }

        //敵Gの正面に当たった時
        if (other.CompareTag("EnemyGForward") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeFG += Time.deltaTime;
            if (other.CompareTag("EnemyBackG") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeFG < 10)//背後に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0.0f;
        }

        //敵の背後に当たった時
        if (other.CompareTag("EnemyBack") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeB += Time.deltaTime;
            Debug.Log("?");
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //付いているスクリプトを取得
            if (EC.ONoff == 1)
            {
                EI.isHidden = false;
            }
            //正面に当たった時に判定しないようにする
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

        //敵Gの背後に当たった時
        if (other.CompareTag("EnemyBackG") /*&& BD.boundHeight >= 2*/)
        {
            stayTimeBG += Time.deltaTime;
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

            //敵Gの正面に当たった時
            if (other.CompareTag("EnemyGForward") /*&& BD.boundHeight >= 2*/)
            {
                if (stayTimeBG < 10)//正面に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (ItemSeen.Box.activeSelf == true)
        {
            rb = ItemSeen.Box.GetComponent<Rigidbody>();
            rb1 = ItemSeen.Box3.GetComponent<Rigidbody>();
            if (other.CompareTag("Box"))
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
