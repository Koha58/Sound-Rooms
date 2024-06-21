using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class EnemyGVisualization : MonoBehaviour
{

    //範囲内のアイテムの可視化・不可視化
        public int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

        [SerializeField] public GameObject SeenArea;
        public GameObject ItemCanvas;
        public GameObject[] Walls;
        public GameObject[] Boxes;
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

            //GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
            //BoxSeen.SetActive(true);

            Boxes = GameObject.FindGameObjectsWithTag("Box");

            foreach (GameObject Box in Boxes)
            {
                //Rigidbodyを取得
                var rb = Box.GetComponent<Rigidbody>();
                //移動も回転もしないようにする
                rb.constraints = RigidbodyConstraints.FreezeAll;
                Box.GetComponent<Renderer>().enabled = false;
            }
        }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemyに付いているスクリプトを取得

        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        parentObject = GameObject.FindGameObjectsWithTag("Item");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");
        ISe = isobj.GetComponent<ItemSearch>(); //付いているスクリプトを取得

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得



        if (EC.ONoff == 1)
        {
            SeenArea.GetComponent<Collider>().enabled = false;//見える（有効）
            onoff = 0;  //見えているから1
        }
        //音を出すと範囲内を可視化
        if (EC.ONoff == 1)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }
        /*
             //音が出ていなければ、範囲内の可視化をできなくする
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
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

                    foreach (GameObject Box in Boxes)
                    {
                        //Rigidbodyを取得
                        var rb = Box.GetComponent<Rigidbody>();
                        //移動も回転もしないようにする
                        rb.constraints = RigidbodyConstraints.FreezeAll;
                        Box.GetComponent<Renderer>().enabled = false;
                    }
                    onoff = 0;  //見えていないから0
                }
            }
            */
    }

        void OnTriggerEnter(Collider other)
        {
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
                Debug.Log("1");
                other.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else if (other.CompareTag("Box"))//接触したオブジェクトのタグが"Box"のとき
            {
                other.GetComponent<Renderer>().enabled = true;
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


            if (other.CompareTag("Enemy"))
            {

                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>(); //付いているスクリプトを取得

                if (EC.ONoff == 0)
                {
                    var childTransforms = EC._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

                    foreach (var item in childTransforms)
                    {
                        //タグが"EnemyParts"である子オブジェクトを見えるようにする
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    EC.ONoff = 1;
                    EC.SoundTime = 0.0f;
                    // EC.Sphere.SetActive(true);//音波非表示→表示
                }


                EC.ONoff = 1;
                EC.SoundTime = 0.0f;
                // EC.Sphere.SetActive(true);//音波非表示→表示
            }


            else if (other.CompareTag("EnemyG"))
            {
                EnemyGController EGC;
                EGC = other.gameObject.GetComponent<EnemyGController>(); //付いているスクリプトを取得
                if (EGC.ONoff == 0)
                {
                    var childTransforms = EGC._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                    foreach (var item in childTransforms)
                    {
                        //タグが"EnemyParts"である子オブジェクトを見えるようにする
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                    EGC.ONoff = 1;
                    EGC.SoundTime = 0.0f;
                    // EsG.Sphere.SetActive(true);//音波非表示→表示
                }
            }
        }
}






