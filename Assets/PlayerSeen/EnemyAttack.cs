using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Enemyに音をぶつける挙動
public class EnemyAttack : MonoBehaviour
{
    int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    ItemSearch ISe;

    [SerializeField] public GameObject EnemyAttackArea;

    public TextMeshProUGUI keyCountText;
    public int count;
    [SerializeField] AudioSource PickupSound;

    private float stayTimeF = 0;
    private float stayTimeFG = 0;
    private float stayTimeB = 0;
    private float stayTimeBG = 0;

    LevelMeter levelMeter;

    bool F;
    float Fon;

    float Foff;
    [SerializeField]
    //private GameObject[] Prototype;

    // Start is called before the first frame update
    void Start()
    {
        //最初は見えない状態
        EnemyAttackArea.GetComponent<Collider>().enabled = false;

        count = 0;
        SetCountText();
        PickupSound = GetComponent<AudioSource>();
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
        ISe = sobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        //敵の正面に当たった時
        if (other.CompareTag("EnemyForward"))
        {
            stayTimeF += Time.deltaTime;
            if(stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBack"))
            {
                if (stayTimeF < 10)//背後に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeF = 0;
        }

        //敵Gの正面に当たった時
        if (other.CompareTag("EnemyGForward"))
        {
            stayTimeFG += Time.deltaTime;
            if (stayTimeF > 0.1f)
            {
                F = true;
            }
            if (other.CompareTag("EnemyBackG"))
            {
                if (stayTimeFG < 10)//背後に当たった時に判定しないようにする
                {
                    other.GetComponent<Collider>().enabled = false;
                }
                other.GetComponent<Collider>().enabled = true;
            }
            stayTimeFG = 0;
        }

        //敵の背後に当たった時
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

            if (stayTimeBG <= stayTimeFG)
            {
                stayTimeBG += 20f;
            }

            if (stayTimeBG >= stayTimeFG)
            {
                if (F == false)
                {
                    GetComponent<ParticleSystem>().Play();

                    Destroy(eobjG);
                    Enemyincrease.enemyDeathcnt++;
                    PickupSound.PlayOneShot(PickupSound.clip);
                    count += 1;
                    SetCountText();
                }
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



        if(other.CompareTag("Prototype"))
        {
            GameObject Prototype = GameObject.FindWithTag("Prototype");
            PrototypeController Prot = Prototype.GetComponent<PrototypeController>();
            if (Prot.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(Prototype);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

        if (other.CompareTag("Prototype1"))
        {
            GameObject Prototype = GameObject.FindWithTag("Prototype1");
            PrototypeController Prot1 = Prototype.GetComponent<PrototypeController>();
            if (Prot1.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(Prototype);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

        if (other.CompareTag("Prototype2"))
        {
            GameObject Prototype = GameObject.FindWithTag("Prototype2");
            PrototypeController Prot2 = Prototype.GetComponent<PrototypeController>();
            if (Prot2.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(Prototype);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

        if (other.CompareTag("Prototype3"))
        {
            GameObject Prototype = GameObject.FindWithTag("Prototype3");
            PrototypeController Prot3 = Prototype.GetComponent<PrototypeController>();
            if (Prot3.DestroyONOFF == true)
            {
                GetComponent<ParticleSystem>().Play();
                Destroy(Prototype);
                Enemyincrease.enemyDeathcnt++;
                PickupSound.PlayOneShot(PickupSound.clip);
                count += 1;
                SetCountText();
            }
        }

    }

    /*  private void OnTriggerEnter(Collider other)
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
                //stayTimeF = 0.0f;
                if (stayTimeB <= stayTimeF)
                {
                    Des = false;
                    stayTimeF += 10.0f;
                }
                Debug.Log("!%");
            }

            //敵の正面に当たった時
            if (other.CompareTag("EnemyForward1"))
            {
                stayTimeF += Time.deltaTime;
                if (other.CompareTag("EnemyBack1"))
                {
                    if (stayTimeF < 10)//背後に当たった時に判定しないようにする
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

                if (stayTimeBG <= stayTimeFG)
                {
                    DesG = false;
                    stayTimeF += 10.0f;
                }
            }

            //敵Gの正面に当たった時
            if (other.CompareTag("EnemyGForward1"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG1"))
                {
                    if (stayTimeFG < 10)//背後に当たった時に判定しないようにする
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

            //敵Gの正面に当たった時
            if (other.CompareTag("EnemyGForward2"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG2"))
                {
                    if (stayTimeFG < 10)//背後に当たった時に判定しないようにする
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

            //敵Gの正面に当たった時
            if (other.CompareTag("EnemyGForward3"))
            {
                stayTimeFG += Time.deltaTime;
                if (other.CompareTag("EnemyBackG3"))
                {
                    if (stayTimeFG < 10)//背後に当たった時に判定しないようにする
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

            //敵の背後に当たった時
            if (other.CompareTag("EnemyBack"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>();
                Enemyincrease EI = eobj.GetComponent<Enemyincrease>(); //付いているスクリプトを取得
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

            //敵の背後に当たった時
            if (other.CompareTag("EnemyBack1"))
            {
                stayTimeB += Time.deltaTime;
                GameObject eobj1 = GameObject.FindWithTag("Enemy1");
                EnemyController1 EC1 = eobj1.GetComponent<EnemyController1>();
                Enemyincrease1 EI1 = eobj1.GetComponent<Enemyincrease1>(); //付いているスクリプトを取得

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

                //正面に当たった時に判定しないようにする
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

            //敵Gの背後に当たった時
            if (other.CompareTag("EnemyBackG"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG = GameObject.FindWithTag("EnemyG");
                EnemyGController EGC = eobjG.GetComponent<EnemyGController>(); //付いているスクリプトを取得
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

            //敵Gの背後に当たった時
            if (other.CompareTag("EnemyBackG1"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG1 = GameObject.FindWithTag("EnemyG1");
                EnemyGController1 EGC1 = eobjG1.GetComponent<EnemyGController1>(); //付いているスクリプトを取得
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

                //敵Gの正面に当たった時
                if (other.CompareTag("EnemyGForward1"))
                {
                    if (stayTimeBG < 10)//正面に当たった時に判定しないようにする
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //敵Gの背後に当たった時
            if (other.CompareTag("EnemyBackG2"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG2 = GameObject.FindWithTag("EnemyG2");
                EnemyGController2 EGC2 = eobjG2.GetComponent<EnemyGController2>(); //付いているスクリプトを取得
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

                //敵Gの正面に当たった時
                if (other.CompareTag("EnemyGForward2"))
                {
                    if (stayTimeBG < 10)//正面に当たった時に判定しないようにする
                    {
                        other.GetComponent<Collider>().enabled = false;
                    }
                    other.GetComponent<Collider>().enabled = true;
                }
                stayTimeBG = 0.0f;
            }

            //敵Gの背後に当たった時
            if (other.CompareTag("EnemyBackG3"))
            {
                stayTimeBG += Time.deltaTime;
                GameObject eobjG3 = GameObject.FindWithTag("EnemyG3");
                EnemyGController3 EGC3 = eobjG3.GetComponent<EnemyGController3>(); //付いているスクリプトを取得
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

                //敵Gの正面に当たった時
                if (other.CompareTag("EnemyGForward3"))
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
        }*/

    void SetCountText()
    {
        keyCountText.text = count.ToString();
    }

}
