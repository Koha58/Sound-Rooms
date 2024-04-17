using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    //private float EnemydeathTime=0.0f;
   
    EnemySeen ES;
    ButtonHoldDown BD;

    private float seentime = 0.0f; //経過時間記録用
    [SerializeField] public GameObject EnemyAttackArea;

    Rigidbody rb;
    //ItemSeen IS;
    // Start is called before the first frame update
    void Start()
    {
        //最初は見えない状態
        EnemyAttackArea.GetComponent<Collider>().enabled = false;
        GameObject eobj1 = GameObject.FindWithTag("Enemy1");
        ES = eobj1.GetComponent<EnemySeen>(); //付いているスクリプトを取得
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで範囲内を可視化
        if (Input.GetMouseButtonUp(0))
        {
            EnemyAttackArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }

        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                EnemyAttackArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject hobj = GameObject.Find("GaugeManager");
        BD = hobj.GetComponent<ButtonHoldDown>(); //付いているスクリプトを取得
        if (other.CompareTag("EnemyBack") && BD.boundHeight >= 2)
        {
           // GameObject eobj = GameObject.Find("Enemy");
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 1)
            {
                Enemyincrease.isHidden = false;
            }
               
            //Destroy(eobj);
        }

        if (other.CompareTag("EnemyBack1") && BD.boundHeight >= 2)
        {
            
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            ES = eobj1.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff ==1)
            {
                // GameObject eobj = GameObject.Find("Enemy1");
                //GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                Enemyincrease1.isHidden1 = false;
                // Enemy1.Enemy01.SetActive(false);
                //Destroy(eobj1);
                //Debug.Log("1");

                /*
                if (Enemyincrease1.Clone==true)
                {
                    //Debug.Log("2");
                    //Debug.Log("3");
                    //GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                    //Enemyincrease1.isHidden1 = false;
                   // Destroy(eobj1);
                   // Enemyincrease1.Clone = false;
                }
                */
            }
        }
        //GameObject iobj = GameObject.Find("SeenArea");
        //IS = iobj.GetComponent<ItemSeen>(); //付いているスクリプトを取得
        
        if (ItemSeen.Box.activeSelf == true)
        {
            rb = ItemSeen.Box.GetComponent<Rigidbody>();
            if (other.CompareTag("Box") && BD.boundHeight >= 2)
            {
                rb.AddForce(transform.forward * 250.0f, ForceMode.Force);
            }
        }
    }
}
