using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySwitch : MonoBehaviour
{
    public float onoff = 0;  //判定用（見えていない時：0/見えている時：1）Visualization
    //public GameObject ItemCanvas;
    public GameObject[] Walls;
    public GameObject[] Boxes;
    public static GameObject[] parentObject;
    private string objName;

    //LevelMeter levelMeter;
    [SerializeField] public Transform _parentTransform;

    bool PlayerOnoff;
    float OnoffTime;

    LevelMeter levelMeter;


    public SkinnedMeshRenderer Player;
    public SkinnedMeshRenderer Player1;
    public SkinnedMeshRenderer Player2;
    public SkinnedMeshRenderer Player3;
    public SkinnedMeshRenderer Player4;
    public SkinnedMeshRenderer Player5;
    public SkinnedMeshRenderer Player6;
    public SkinnedMeshRenderer Player7; 
    public SkinnedMeshRenderer Player8;
    public SkinnedMeshRenderer Player9;
    public SkinnedMeshRenderer Player10;
    public SkinnedMeshRenderer Player11;
    public SkinnedMeshRenderer Player12;
    public SkinnedMeshRenderer Player13;
    public SkinnedMeshRenderer Player14;
    public SkinnedMeshRenderer Player15;


    public string Player0;
    public string Player01;
    public string Player02;
    public string Player03;
    public string Player04;
    public string Player05;
    public string Player06;
    public string Player07;
    public string Player08;
    public string Player09;
    public string Player010;
    public string Player011;
    public string Player012;
    public string Player013;
    public string Player014;
    public string Player015;


    private void Start()
    {

        Player =GameObject.Find(Player0).GetComponent<SkinnedMeshRenderer>();
        Player1 = GameObject.Find(Player01).GetComponent<SkinnedMeshRenderer>();
        Player2 = GameObject.Find(Player02).GetComponent<SkinnedMeshRenderer>();
        Player3 = GameObject.Find(Player03).GetComponent<SkinnedMeshRenderer>();
        Player4 = GameObject.Find(Player04).GetComponent<SkinnedMeshRenderer>();
        Player5 = GameObject.Find(Player05).GetComponent<SkinnedMeshRenderer>();
        Player6 = GameObject.Find(Player06).GetComponent<SkinnedMeshRenderer>();
        Player7 = GameObject.Find(Player07).GetComponent<SkinnedMeshRenderer>();
        Player8 = GameObject.Find(Player08).GetComponent<SkinnedMeshRenderer>();
        Player9 = GameObject.Find(Player09).GetComponent<SkinnedMeshRenderer>();
        Player10 = GameObject.Find(Player010).GetComponent<SkinnedMeshRenderer>();
        Player11 = GameObject.Find(Player011).GetComponent<SkinnedMeshRenderer>();
        Player12 = GameObject.Find(Player012).GetComponent<SkinnedMeshRenderer>();
        Player13 = GameObject.Find(Player013).GetComponent<SkinnedMeshRenderer>();
        Player14 = GameObject.Find(Player014).GetComponent<SkinnedMeshRenderer>();
        Player15 = GameObject.Find(Player015).GetComponent<SkinnedMeshRenderer>();

           Player.enabled = false;
           Player1.enabled = false;
           Player2.enabled = false;
           Player3.enabled = false;
           Player4.enabled = false;
           Player5.enabled = false;
           Player6.enabled = false;
           Player7.enabled = false;
           Player8.enabled = false;
           Player9.enabled = false;
           Player10.enabled = false;
           Player11.enabled = false;
           Player12.enabled = false;
           Player13.enabled = false;
           Player14.enabled = false;


        GameObject doorObject = GameObject.Find("Door1");

        // 子オブジェクトの数を取得
        int doorparts = doorObject.transform.childCount;
        for (int j = 0; j < doorparts; j++)
        {
            Transform childTransform = doorObject.transform.GetChild(j);
            GameObject door = childTransform.gameObject;
            door.GetComponent<Renderer>().enabled = false;
        }
        //ItemCanvas.GetComponent<Canvas>().enabled = false;

        Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject Wall in Walls)
        {
            Wall.GetComponent<Renderer>().enabled = false;
        }

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
        GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        parentObject = GameObject.FindGameObjectsWithTag("Item");

        GameObject doorObject = GameObject.Find("Door1");


        if (PlayerOnoff == true)
        {
            PS.onoff = 1;  //見えているから1
            onoff = 1;
            Player.enabled = true;
            Player1.enabled = true;
            Player2.enabled = true;
            Player3.enabled = true;
            Player4.enabled = true;
            Player5.enabled = true;
            Player6.enabled = true;
            Player7.enabled = true;
            Player8.enabled = true;
            Player9.enabled = true;
            Player10.enabled = true;
            Player11.enabled = true;
            Player12.enabled = true;
            Player13.enabled = true;
            Player14.enabled = true;
       
        }

        if (PlayerOnoff == false)
        {
            OnoffTime += Time.deltaTime;
            if (OnoffTime >= 5.0f)
            {
                OnoffTime = 0;

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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            ////keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            //keyOnoff = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    ////keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
                }
            }
        }

        if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (other.CompareTag("Box"))//接触したオブジェクトのタグが"Box"のとき
        {
            other.GetComponent<Renderer>().enabled = true;
        }

        if (other.CompareTag("Door"))
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

        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 0)
            {
                PlayerOnoff = true;
                Player.enabled = true;
                Player1.enabled = true;
                Player2.enabled = true;
                Player3.enabled = true;
                Player4.enabled = true;
                Player5.enabled = true;
                Player6.enabled = true; 
                Player7.enabled = true;
                Player8.enabled = true;
                Player9.enabled = true;
                Player10.enabled = true;
                Player11.enabled = true;
                Player12.enabled = true;
                Player13.enabled = true;
                Player14.enabled = true;
                Player15.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Playerオブジェクトを探す
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //付いているスクリプトを取得
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 1)
            {
                Player.enabled = false;
                onoff = 0;
                Player1.enabled = false;
                Player2.enabled = false;
                Player3.enabled = false;
                Player4.enabled = false;
                Player5.enabled = false;
                Player6.enabled = false;
                Player7.enabled = false;
                Player8.enabled = false;
                Player9.enabled = false;
                Player10.enabled = false;
                Player11.enabled = false;
                Player12.enabled = false;
                Player13.enabled = false;
                Player14.enabled = false;
            }
        }
    }
}
