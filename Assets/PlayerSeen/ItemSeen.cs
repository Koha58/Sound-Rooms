using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.ProBuilder.MeshOperations;

//範囲内のアイテムの可視化・不可視化
public class ItemSeen : MonoBehaviour
{
    public int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    [SerializeField] public GameObject SeenArea;
    public GameObject[] Walls;
    public GameObject[] Boxes;
    public GameObject[] Objects;
    public GameObject[] Doors;
    public static GameObject[] parentObject;
    private string objName;

    LevelMeter levelMeter;

    void Start()
    {
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
        //ItemCanvas.GetComponent<Canvas>().enabled = false;

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

        Objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (GameObject Object in Objects)
        {
            Object.GetComponent<Renderer>().enabled = false;
            Object.GetComponent<Collider>().enabled = false;
        }

        Doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject Door in Doors)
        {
            Door.GetComponent<Collider>().enabled = false;
        }
    }

    private void Update()
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //音を出すと範囲内を可視化
        if (levelMeter.nowdB > 0.0f)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）

            foreach (GameObject Object in Objects)
            {
                Object.GetComponent<Collider>().enabled = true;
            }

            onoff = 1;  //見えているから1
        }

        //音が出ていなければ、範囲内の可視化をできなくする
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
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

                foreach (GameObject Object in Objects)
                {
                    Object.GetComponent<Renderer>().enabled = false;
                    Object.GetComponent<Collider>().enabled = false;
                }

                foreach (GameObject Door in Doors)
                {
                    Door.GetComponent<Collider>().enabled = false;
                }
                onoff = 0;  //見えていないから0
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject doorObject = GameObject.Find("Door1");
        objName = other.gameObject.name;

        if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
        else if (other.CompareTag("Box"))//接触したオブジェクトのタグが"Box"のとき
        {
            other.GetComponent<Renderer>().enabled = true;
        }
        else if(other.CompareTag("Object"))//接触したオブジェクトのタグが"Object"のとき
        {
            other.GetComponent<Renderer>().enabled = true;
        }
        else if (other.CompareTag("Door"))
        {
            other.GetComponent<Collider>().enabled = true;
        }

        else if (objName == "Door1")
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

    }
}