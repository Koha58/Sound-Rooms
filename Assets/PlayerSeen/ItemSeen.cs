using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

//範囲内のアイテムの可視化・不可視化
public class ItemSeen : MonoBehaviour
{
    public int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    public float seentime = 0.0f; //経過時間記録用
    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject[] Walls;
    public static GameObject Box;
    public static GameObject Box1;
    public static GameObject Box2;
    public static GameObject Box3;
    public static GameObject Box4;
    public static GameObject Box5;
    public static GameObject[] parentObject;
    private string objName;
    ItemSearch ISe;
    LevelMeter levelMeter;

    void Start()
    {
        parentObject = GameObject.FindGameObjectsWithTag("Item");
        GameObject Key1 = parentObject[0];
        // 子オブジェクトの数を取得
        int childCount1 = Key1.transform.childCount;
        for (int i = 0; i < childCount1; i++)
        {
            Transform childTransform = Key1.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }
        GameObject Key2 = parentObject[1];
        // 子オブジェクトの数を取得
        int childCount2 = Key2.transform.childCount;
        for (int a = 0; a < childCount2; a++)
        {
            Transform childTransform = Key2.transform.GetChild(a);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }
        GameObject Key3 = parentObject[2];
        // 子オブジェクトの数を取得
        int childCount3 = Key3.transform.childCount;
        for (int b = 0; b < childCount3; b++)
        {
            Transform childTransform = Key3.transform.GetChild(b);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }
        GameObject Key4 = parentObject[3];
        // 子オブジェクトの数を取得
        int childCount4 = Key4.transform.childCount;
        for (int c = 0; c < childCount4; c++)
        {
            Transform childTransform = Key4.transform.GetChild(c);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }

        GameObject doorObject = GameObject.Find("Door1");

        // 子オブジェクトの数を取得
        int doorparts = doorObject.transform.childCount;
        for (int j = 0; j < doorparts; j++)
        {
            Transform childTransform = doorObject.transform.GetChild(j);
            GameObject door = childTransform.gameObject;
            door.GetComponent<Renderer>().enabled = false;
        }

        //最初は見えない状態
        SeenArea.GetComponent<Collider>().enabled = false;
        ItemCanvas.GetComponent<Canvas>().enabled = false;
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject Wall in Walls)
        {
            Wall.GetComponent<Renderer>().enabled = false;
        }
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        BoxSeen.SetActive(true);
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box.SetActive(false);
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        Box1.SetActive(false);
        Box2 = BoxSeen.transform.Find("cardboard (2)").gameObject;
        Box2.SetActive(false);
        Box3 = BoxSeen.transform.Find("cardboard (3)").gameObject;
        Box3.SetActive(false);
        Box4 = BoxSeen.transform.Find("cardboard (4)").gameObject;
        Box4.SetActive(false);
        Box5 = BoxSeen.transform.Find("cardboard (5)").gameObject;
        Box5.SetActive(false);
    }

    private void Update()
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        Box2 = BoxSeen.transform.Find("cardboard (2)").gameObject;
        Box3 = BoxSeen.transform.Find("cardboard (3)").gameObject;
        Box4 = BoxSeen.transform.Find("cardboard (4)").gameObject;
        Box5 = BoxSeen.transform.Find("cardboard (5)").gameObject;

        parentObject = GameObject.FindGameObjectsWithTag("Item");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");
        ISe = isobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //音を出すと範囲内を可視化
        if (/*Input.GetMouseButtonUp(0)*/levelMeter.nowdB > 0.0f)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }

        //音が出ていなければ、範囲内の可視化をできなくする
        if (onoff == 1)
        {
            //seentime += Time.deltaTime;
            if (/*seentime >= 10.0f*/levelMeter.nowdB <= 0.0f)
            {
                if (ISe.count != 4)
                {
                    if (parentObject[0] != null)
                    {
                        GameObject Key1 = parentObject[0];
                        // 子オブジェクトの数を取得
                        int childCount1 = Key1.transform.childCount;
                        for (int i = 0; i < childCount1; i++)
                        {
                            Transform childTransform = Key1.transform.GetChild(i);
                            GameObject childObject = childTransform.gameObject;
                            childObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                    else if (parentObject[1] != null)
                    {
                        GameObject Key2 = parentObject[1];
                        // 子オブジェクトの数を取得
                        int childCount2 = Key2.transform.childCount;
                        for (int a = 0; a < childCount2; a++)
                        {
                            Transform childTransform = Key2.transform.GetChild(a);
                            GameObject childObject = childTransform.gameObject;
                            childObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                    else if (parentObject[2] != null)
                    {
                        GameObject Key3 = parentObject[2];
                        int childCount3 = Key3.transform.childCount;
                        for (int b = 0; b < childCount3; b++)
                        {
                            Transform childTransform = Key3.transform.GetChild(b);
                            GameObject childObject = childTransform.gameObject;
                            childObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                    else if (parentObject[3] != null)
                    {
                        GameObject Key4 = parentObject[3];
                        int childCount4 = Key4.transform.childCount;
                        for (int c = 0; c < childCount4; c++)
                        {
                            Transform childTransform = Key4.transform.GetChild(c);
                            GameObject childObject = childTransform.gameObject;
                            childObject.GetComponent<Renderer>().enabled = false;
                        }
                    }
                }

                SeenArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                foreach (GameObject Wall in Walls)
                {
                    Wall.GetComponent<Renderer>().enabled = false;
                }
                // 子オブジェクトの数を取得
                int doorparts = doorObject.transform.childCount;
                for (int j = 0; j < doorparts; j++)
                {
                    Transform childTransform = doorObject.transform.GetChild(j);
                    GameObject door = childTransform.gameObject;
                    door.GetComponent<Renderer>().enabled = false;
                }
                if (Box.activeSelf == true)
                {
                    Box.SetActive(false);
                    Box1.SetActive(false);
                    Box2.SetActive(false);
                    Box3.SetActive(false);
                    Box4.SetActive(false);
                    Box5.SetActive(false);
                    BoxSeen.GetComponent<Collider>().enabled = true;
                }
                onoff = 0;  //見えていないから0
                //seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        parentObject = GameObject.FindGameObjectsWithTag("Item");
        GameObject doorObject = GameObject.Find("Door1");
        objName = other.gameObject.name;
        //接触したオブジェクトのタグが"Item"のとき
        if (objName == "key 1 (1)")
        {
            if (parentObject[0] != null)
            {
                GameObject Key1 = parentObject[0];
                // 子オブジェクトの数を取得
                int childCount1 = Key1.transform.childCount;
                for (int i = 0; i < childCount1; i++)
                {
                    Transform childTransform = Key1.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[1] != null)
            {
                GameObject Key1 = parentObject[1];
                // 子オブジェクトの数を取得
                int childCount1 = Key1.transform.childCount;
                for (int i = 0; i < childCount1; i++)
                {
                    Transform childTransform = Key1.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[2] != null)
            {
                GameObject Key1 = parentObject[2];
                // 子オブジェクトの数を取得
                int childCount1 = Key1.transform.childCount;
                for (int i = 0; i < childCount1; i++)
                {
                    Transform childTransform = Key1.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[3] != null)
            {
                GameObject Key1 = parentObject[3];
                // 子オブジェクトの数を取得
                int childCount1 = Key1.transform.childCount;
                for (int i = 0; i < childCount1; i++)
                {
                    Transform childTransform = Key1.transform.GetChild(i);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else if (objName == "key 1")
        {
            if (parentObject[0] != null)
            {
                GameObject Key2 = parentObject[0];
                // 子オブジェクトの数を取得
                int childCount2 = Key2.transform.childCount;
                for (int a = 0; a < childCount2; a++)
                {
                    Transform childTransform = Key2.transform.GetChild(a);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[1] != null)
            {
                GameObject Key2 = parentObject[1];
                // 子オブジェクトの数を取得
                int childCount2 = Key2.transform.childCount;
                for (int a = 0; a < childCount2; a++)
                {
                    Transform childTransform = Key2.transform.GetChild(a);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[2] != null)
            {
                GameObject Key2 = parentObject[2];
                // 子オブジェクトの数を取得
                int childCount2 = Key2.transform.childCount;
                for (int a = 0; a < childCount2; a++)
                {
                    Transform childTransform = Key2.transform.GetChild(a);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[3] != null)
            {
                GameObject Key2 = parentObject[3];
                // 子オブジェクトの数を取得
                int childCount2 = Key2.transform.childCount;
                for (int a = 0; a < childCount2; a++)
                {
                    Transform childTransform = Key2.transform.GetChild(a);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else if (objName == "key 1 (2)")
        {
            if (parentObject[0] != null)
            {
                GameObject Key3 = parentObject[0];
                int childCount3 = Key3.transform.childCount;
                for (int b = 0; b < childCount3; b++)
                {
                    Transform childTransform = Key3.transform.GetChild(b);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[1] != null)
            {
                GameObject Key3 = parentObject[1];
                int childCount3 = Key3.transform.childCount;
                for (int b = 0; b < childCount3; b++)
                {
                    Transform childTransform = Key3.transform.GetChild(b);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[2] != null)
            {
                GameObject Key3 = parentObject[2];
                int childCount3 = Key3.transform.childCount;
                for (int b = 0; b < childCount3; b++)
                {
                    Transform childTransform = Key3.transform.GetChild(b);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[3] != null)
            {
                GameObject Key3 = parentObject[3];
                int childCount3 = Key3.transform.childCount;
                for (int b = 0; b < childCount3; b++)
                {
                    Transform childTransform = Key3.transform.GetChild(b);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }
        else if (objName == "key 1 (3)")
        {
            if (parentObject[0] != null)
            {
                GameObject Key4 = parentObject[0];
                int childCount4 = Key4.transform.childCount;
                for (int c = 0; c < childCount4; c++)
                {
                    Transform childTransform = Key4.transform.GetChild(c);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[1] != null)
            {
                GameObject Key4 = parentObject[1];
                int childCount4 = Key4.transform.childCount;
                for (int c = 0; c < childCount4; c++)
                {
                    Transform childTransform = Key4.transform.GetChild(c);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[2] != null)
            {
                GameObject Key4 = parentObject[2];
                int childCount4 = Key4.transform.childCount;
                for (int c = 0; c < childCount4; c++)
                {
                    Transform childTransform = Key4.transform.GetChild(c);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
            else if (parentObject[3] != null)
            {
                GameObject Key4 = parentObject[3];
                int childCount4 = Key4.transform.childCount;
                for (int c = 0; c < childCount4; c++)
                {
                    Transform childTransform = Key4.transform.GetChild(c);
                    GameObject childObject = childTransform.gameObject;
                    childObject.GetComponent<Renderer>().enabled = true;
                }
            }
        }

        else if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
        else if (other.CompareTag("BoxJudge"))//接触したオブジェクトのタグが"BoxJudge"のとき
        {
            Box.SetActive(true);
            Box1.SetActive(true);
            Box2.SetActive(true);
            Box3.SetActive(true);
            Box4.SetActive(true);
            Box5.SetActive(true);
            BoxSeen.GetComponent<Collider>().enabled = false;
        }

        else if (other.CompareTag("Door"))
        {
            // 子オブジェクトの数を取得
            int doorparts = doorObject.transform.childCount;
            for (int j = 0; j < doorparts; j++)
            {
                Transform childTransform = doorObject.transform.GetChild(j);
                GameObject door = childTransform.gameObject;
                door.GetComponent<Renderer>().enabled = true;
            }
        }


       

            /*
            if (other.CompareTag("Enemy"))
            {

                GameObject eobj = GameObject.FindWithTag("Enemy");
                Enemys Es = eobj.GetComponent<Enemys>(); //付いているスクリプトを取得

                if (Es.ONoff == 0)
                {
                    var childTransforms = Es._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

                    foreach (var item in childTransforms)
                    {
                        //タグが"EnemyParts"である子オブジェクトを見えるようにする
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    ES.ONoff = 1;
                    ES.SoundTime = 0.0f;
                    ES.Sphere.SetActive(true);//音波非表示→表示
                }
            }

                    Es.ONoff = 1;
                    Es.SoundTime = 0.0f;
                    Es.Sphere.SetActive(true);//音波非表示→表示

                }

            }
            /*
            else if (other.CompareTag("EnemyFailurework"))
            {
                GameObject eobj = GameObject.FindWithTag("EnemyFailurework");
                EnemyFailurework EFW = eobj.GetComponent<EnemyFailurework>();

                if (EFW.ONoff == 0)
                {
                    var childTransforms = EFW._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                    foreach (var item in childTransforms)
                    {
                        //タグが"EnemyParts"である子オブジェクトを見えるようにする
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    EFW.ONoff = 1;
                    EFW.SoundTime = 0.0f;
                    EFW.Sphere.SetActive(true);//音波非表示→表示

                }

            }
            */

            else if (other.CompareTag("EnemyG"))
            {
                EnemysG EsG;
                //EnemySeen ES;
                //GameObject eobj2 = GameObject.FindWithTag("EnemyG");
                EsG = other.gameObject.GetComponent<EnemysG>(); //付いているスクリプトを取得
                if (EsG.ONoff == 0)
                {
                    var childTransforms = EsG._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                    foreach (var item in childTransforms)
                    {
                        //タグが"EnemyParts"である子オブジェクトを見えるようにする
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                    EsG.ONoff = 1;
                    EsG.SoundTime = 0.0f;
                    EsG.Sphere.SetActive(true);//音波非表示→表示
                }
            }
            
        
    }
}



