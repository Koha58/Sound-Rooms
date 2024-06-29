using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    ItemSearch ISe;

    [SerializeField] public GameObject EnemyAttackArea;

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
            if (levelMeter.nowdB <= 0.3f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                onoff = 0;  //見えていないから0
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject sobj = GameObject.Find("Player");
        ISe = sobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        //敵の正面に当たった時
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            if (other.CompareTag("EnemyBack"))
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
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            if (other.CompareTag("EnemyBackG"))
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
        if (other.CompareTag("EnemyBack"))
        {
            stayTimeB += Time.deltaTime;
            GameObject eobj = GameObject.FindWithTag("Enemy");
            EnemyController EC = eobj.GetComponent<EnemyController>();
            Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //付いているスクリプトを取得
            //Rigidbody EnemyR = eobj.GetComponent<Rigidbody>();

            if (EC.ONoff == 1)
            {
                EI.isHidden = false;
                Debug.Log("?");
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
        if (other.CompareTag("EnemyBackG"))
        {
            stayTimeBG += Time.deltaTime;
            GameObject eobjG = GameObject.FindWithTag("EnemyG");
            EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //付いているスクリプトを取得
            if (EGC.ONoff == 1)
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
            if (other.CompareTag("EnemyGForward"))
            {
                if (stayTimeBG < 10)//正面に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeBG = 0.0f;
        }

        if (other.CompareTag("Box"))
        {
            //Rigidbodyを取得
            var rb = other.GetComponent<Rigidbody>();

            //移動、回転を可能にする
            rb.constraints = RigidbodyConstraints.None;

            rb.AddForce(transform.forward * 500.0f, ForceMode.Force);
        }
    }

}
