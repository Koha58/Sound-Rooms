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
        //�ŏ��͌����Ȃ����
        SeenArea.GetComponent<Collider>().enabled = false;

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

                onoff = 0;  //�����Ă��Ȃ�����0
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        objName = other.gameObject.name;

        if (other.CompareTag("Box"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Box"�̂Ƃ�
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

        else if (other.CompareTag("Shelf"))
        {
            other.GetComponent<Renderer>().enabled = false;
            other.GetComponent<Collider>().enabled = false;
            // �q�I�u�W�F�N�g�̐����擾
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
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g�������Ȃ�����
                capsuleParts.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }

    }
}