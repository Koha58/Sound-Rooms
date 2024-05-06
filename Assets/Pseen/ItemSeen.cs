using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static Unity.VisualScripting.Metadata;

//範囲内のアイテムの可視化・不可視化
public class ItemSeen : MonoBehaviour
{
    public int onoff = 0;  //判定用（見えていない時：0/見えている時：1）

    public float seentime = 0.0f; //経過時間記録用
    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject Wall;
    public static GameObject Box;
    public static GameObject Box1;
    void Start()
    {
        GameObject parentObject = GameObject.FindWithTag("Item");

        // 子オブジェクトの数を取得
        int childCount = parentObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentObject.transform.GetChild(i);
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
        Wall.GetComponent<Renderer>().enabled = false;
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        BoxSeen.SetActive(true);
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box.SetActive(false);
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        Box1.SetActive(false);
    }

    private void Update()
    {
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        GameObject parentObject = GameObject.FindWithTag("Item");
        GameObject doorObject = GameObject.Find("Door1");
        //左クリックで範囲内を可視化
        if (Input.GetMouseButtonUp(0))
        {
            SeenArea.GetComponent<Collider>().enabled = true;//見える（有効）
            onoff = 1;  //見えているから1
        }

        //指定した時間が経過したら範囲内の可視化をできなくする
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                if (parentObject != null)
                {
                    // 子オブジェクトの数を取得
                    int childCount = parentObject.transform.childCount;
                    for (int i = 0; i < childCount; i++)
                    {
                        Transform childTransform = parentObject.transform.GetChild(i);
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<Renderer>().enabled = false;
                    }
                    ItemCanvas.GetComponent<Canvas>().enabled = false;
                }
                SeenArea.GetComponent<Collider>().enabled = false;//見えない（無効）
                Wall.GetComponent<Renderer>().enabled = false;
                // 子オブジェクトの数を取得
                int doorparts = doorObject.transform.childCount;
                for (int j = 0; j < doorparts; j++)
                {
                    Transform childTransform = doorObject.transform.GetChild(j);
                    GameObject door = childTransform.gameObject;
                    door.GetComponent<Renderer>().enabled = false;
                }
                if (Box.activeSelf== true)
                {
                    Box.SetActive(false);
                    Box1.SetActive(false);
                    BoxSeen.GetComponent<Collider>().enabled = true;
                }
                onoff = 0;  //見えていないから0
                seentime = 0.0f;    //経過時間をリセット
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        Box = BoxSeen.transform.Find("cardboard (1)").gameObject;
        Box1 = BoxSeen.transform.Find("cardboard").gameObject;
        GameObject parentObject = GameObject.FindWithTag("Item");
        GameObject doorObject = GameObject.Find("Door1");
        //接触したオブジェクトのタグが"Item"のとき
        if (other.CompareTag("Item") && parentObject != null)
        {
            // 子オブジェクトの数を取得
            int childCount = parentObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = true;
            }
        }
        else if (other.CompareTag("Wall"))//接触したオブジェクトのタグが"Wall"のとき
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }
        else if (other.CompareTag("BoxJudge"))//接触したオブジェクトのタグが"BoxJudge"のとき
        {
            Box.SetActive(true);
            Box1.SetActive(true);
            BoxSeen.GetComponent<Collider>().enabled = false;
        }
        
        else if(other.CompareTag("Door"))
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


        else if(other.CompareTag("Enemy"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            */
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //付いているスクリプトを取得

            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
        else if(other.CompareTag("Enemy1"))
        {
            EnemySeen ES;
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            ES = eobj1.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
        else if (other.CompareTag("EnemyG1"))
        {
            EnemySeen ES;
            GameObject eobj2 = GameObject.FindWithTag("EnemyG1");
            ES = eobj2.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
        else if (other.CompareTag("EnemyG2"))
        {
            EnemySeen ES;
            GameObject eobj3 = GameObject.FindWithTag("EnemyG2");
            ES = eobj3.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
        else if (other.CompareTag("EnemyG3"))
        {
            EnemySeen ES;
            GameObject eobj4 = GameObject.FindWithTag("EnemyG3");
            ES = eobj4.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
        else if (other.CompareTag("EnemyG4"))
        {
            EnemySeen ES;
            GameObject eobj5 = GameObject.FindWithTag("EnemyG4");
            ES = eobj5.GetComponent<EnemySeen>(); //付いているスクリプトを取得
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
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
    }
}
