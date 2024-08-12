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

//�͈͓��̃A�C�e���̉����E�s����
public class ItemSeen : MonoBehaviour
{
    public int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

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

        // �q�I�u�W�F�N�g�̐����擾
        int doorparts = doorObject.transform.childCount;
        for (int j = 0; j < doorparts; j++)
        {
            Transform childTransform = doorObject.transform.GetChild(j);
            GameObject door = childTransform.gameObject;
            door.GetComponent<Renderer>().enabled = false;
        }

        //�ŏ��͌����Ȃ����
        SeenArea.GetComponent<Collider>().enabled = false;
        //ItemCanvas.GetComponent<Canvas>().enabled = false;

        Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject Wall in Walls)
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }

        //GameObject BoxSeen = GameObject.FindWithTag("BoxJudge");
        //BoxSeen.SetActive(true);

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (GameObject Box in Boxes)
        {
            //Rigidbody���擾
            var rb = Box.GetComponent<Rigidbody>();
            //�ړ�����]�����Ȃ��悤�ɂ���
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Box.GetComponent<Renderer>().enabled = true;
        }

        Objects = GameObject.FindGameObjectsWithTag("Object");

        foreach (GameObject Object in Objects)
        {
            Object.GetComponent<Renderer>().enabled = true;
            Object.GetComponent<Collider>().enabled = true;
        }

        Doors = GameObject.FindGameObjectsWithTag("Door");

        foreach (GameObject Door in Doors)
        {
            Door.GetComponent<Collider>().enabled = true;
        }
    }

    private void Update()
    {
        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

        //�����o���Ɣ͈͓���s����
        if (levelMeter.nowdB > 0.0f)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//������i�L���j

            onoff = 1;  //�����Ă��邩��1
        }

        //�����o�Ă��Ȃ���΁A�͈͓��̕�������
        if (onoff == 1)
        {
            if (levelMeter.nowdB <= 0.0f)
            {
                SeenArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j

                foreach (GameObject Wall in Walls)
                {
                    Wall.GetComponent<Renderer>().enabled = true;
                }

                // �q�I�u�W�F�N�g�̐����擾
                int doorparts = doorObject.transform.childCount;
                for (int j = 0; j < doorparts; j++)
                {
                    Transform childTransform = doorObject.transform.GetChild(j);
                    GameObject door = childTransform.gameObject;
                    door.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject Box in Boxes)
                {
                    //Rigidbody���擾
                    var rb = Box.GetComponent<Rigidbody>();
                    //�ړ�����]�����Ȃ��悤�ɂ���
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    Box.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject Object in Objects)
                {
                    Object.GetComponent<Renderer>().enabled = true;
                    Object.GetComponent<Collider>().enabled = true;
                }

                foreach (GameObject Door in Doors)
                {
                    Door.GetComponent<Collider>().enabled = true;
                }
                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject doorObject = GameObject.Find("Door1");
        objName = other.gameObject.name;

        if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
        {
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (other.CompareTag("Box"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Box"�̂Ƃ�
        {
            other.GetComponent<Renderer>().enabled = false;
        }
        else if(other.CompareTag("Object"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Object"�̂Ƃ�
        {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
        }
        else if (other.CompareTag("Door"))
        {
            other.GetComponent<Collider>().enabled = false;
        }

        else if (objName == "Door1")
        {
            // �q�I�u�W�F�N�g�̐����擾
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