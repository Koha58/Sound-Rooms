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
    public GameObject[] Boxes;
    public GameObject[] Objects;
    public GameObject[] Shelfs;
    public GameObject[] InShelfs;
    public GameObject[] Capsules;
    public GameObject[] CapsuleParts;
    public static GameObject[] parentObject;
    private string objName;

    LevelMeter levelMeter;

    GameObject haveChildren;

    void Start()
    {
        //最初は見えない状態
        SeenArea.GetComponent<Collider>().enabled = false;

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject Box in Boxes)
        {
            //Rigidbodyを取得
            var rb = Box.GetComponent<Rigidbody>();
            //移動も回転もしないようにする
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Box.GetComponent<Renderer>().enabled = true;
        }

        Objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (GameObject Object in Objects)
        {
            Object.GetComponent<Renderer>().enabled = true;
            Object.GetComponent<Collider>().enabled = true;
        }

        Shelfs = GameObject.FindGameObjectsWithTag("Shelf");

        foreach (GameObject Shelf in Shelfs)
        {
            Shelf.GetComponent<Renderer>().enabled = true;
            Shelf.GetComponent<Collider>().enabled = true;
        }

        InShelfs = GameObject.FindGameObjectsWithTag("InShelf");

        foreach (GameObject InShelf in InShelfs)
        {
            InShelf.GetComponent<Renderer>().enabled = true;
        }

        Capsules = GameObject.FindGameObjectsWithTag("Capsule");

        foreach (GameObject Capsule in Capsules)
        {
            Capsule.GetComponent<Collider>().enabled = true;
        }

        CapsuleParts = GameObject.FindGameObjectsWithTag("CapsuleParts");

        foreach (GameObject CapsulePartss in CapsuleParts)
        {
            CapsulePartss.GetComponent<Renderer>().enabled = true;
        }
    }

    private void Update()
    {
        Boxes = GameObject.FindGameObjectsWithTag("Box");

        Shelfs = GameObject.FindGameObjectsWithTag("Shelf");

        InShelfs = GameObject.FindGameObjectsWithTag("InShelf");

        Capsules = GameObject.FindGameObjectsWithTag("Capsule");

        CapsuleParts = GameObject.FindGameObjectsWithTag("CapsuleParts");

        GameObject isobj = GameObject.Find("Player");

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //付いているスクリプトを取得

        //音を出すと範囲内を不可視化
        if (levelMeter.nowdB > 0.0f)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）

            onoff = 1;  //見えているから1
        }

        //音が出ていなければ、範囲内の物を可視化
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
                SeenArea.GetComponent<Collider>().enabled = false;//見えない（無効）

                foreach (GameObject Shelf in Shelfs)
                {
                    Shelf.GetComponent<Renderer>().enabled = true;
                    Shelf.GetComponent<Collider>().enabled = true;
                }

                foreach (GameObject InShelf in InShelfs)
                {
                    InShelf.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject Capsule in Capsules)
                {
                    Capsule.GetComponent<Collider>().enabled = true;
                }

                foreach (GameObject CapsulePartss in CapsuleParts)
                {
                    CapsulePartss.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject Box in Boxes)
                {
                    //Rigidbodyを取得
                    var rb = Box.GetComponent<Rigidbody>();
                    //移動も回転もしないようにする
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    Box.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject Object in Objects)
                {
                    Object.GetComponent<Renderer>().enabled = true;
                    Object.GetComponent<Collider>().enabled = true;
                }

                onoff = 0;  //見えていないから0
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        objName = other.gameObject.name;

        if (other.CompareTag("Box"))//接触したオブジェクトのタグが"Box"のとき
        {
            other.GetComponent<Renderer>().enabled = false;
        }
        else if(other.CompareTag("Object"))//接触したオブジェクトのタグが"Object"のとき
        {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
        }
        else if (other.CompareTag("Door"))
        {
            other.GetComponent<Collider>().enabled = false;
        }

        else if (other.CompareTag("Shelf"))
        {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            // 子オブジェクトの数を取得
            int Shelfparts = other.transform.childCount;
            for (int i = 0; i < Shelfparts; i++)
            {
                Transform childTransform = other.transform.GetChild(i);
                GameObject Inshelf = childTransform.gameObject;
                Inshelf.GetComponent<Renderer>().enabled = false;
            }
        }
        else if(other.CompareTag("Capsule"))
        {
            other.GetComponent<Collider>().enabled = false;

            var childTransforms = other.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("CapsuleParts"));
            foreach (var capsuleParts in childTransforms)
            {
                //タグが"PlayerParts"である子オブジェクトを見えなくする
                capsuleParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }

    }
}