using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static Unity.VisualScripting.Metadata;

//�͈͓��̃A�C�e���̉����E�s����
public class ItemSeen : MonoBehaviour
{
    public int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    public float seentime = 0.0f; //�o�ߎ��ԋL�^�p
    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject Wall;
    public static GameObject Box;
    public static GameObject Box1;
    void Start()
    {
        GameObject parentObject = GameObject.FindWithTag("Item");

        // �q�I�u�W�F�N�g�̐����擾
        int childCount = parentObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = parentObject.transform.GetChild(i);
            GameObject childObject = childTransform.gameObject;
            childObject.GetComponent<Renderer>().enabled = false;
        }

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
        //���N���b�N�Ŕ͈͓�������
        if (Input.GetMouseButtonUp(0))
        {
            SeenArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }

        //�w�肵�����Ԃ��o�߂�����͈͓��̉������ł��Ȃ�����
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 10.0f)
            {
                if (parentObject != null)
                {
                    // �q�I�u�W�F�N�g�̐����擾
                    int childCount = parentObject.transform.childCount;
                    for (int i = 0; i < childCount; i++)
                    {
                        Transform childTransform = parentObject.transform.GetChild(i);
                        GameObject childObject = childTransform.gameObject;
                        childObject.GetComponent<Renderer>().enabled = false;
                    }
                    ItemCanvas.GetComponent<Canvas>().enabled = false;
                }
                SeenArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                Wall.GetComponent<Renderer>().enabled = false;
                // �q�I�u�W�F�N�g�̐����擾
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
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
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
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Item"�̂Ƃ�
        if (other.CompareTag("Item") && parentObject != null)
        {
            // �q�I�u�W�F�N�g�̐����擾
            int childCount = parentObject.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = parentObject.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = true;
            }
        }
        else if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }
        else if (other.CompareTag("BoxJudge"))//�ڐG�����I�u�W�F�N�g�̃^�O��"BoxJudge"�̂Ƃ�
        {
            Box.SetActive(true);
            Box1.SetActive(true);
            BoxSeen.GetComponent<Collider>().enabled = false;
        }
        
        else if(other.CompareTag("Door"))
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


        else if(other.CompareTag("Enemy"))
        {
            EnemySeen ES;
            /*
            GameObject eobj = GameObject.Find("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            */
            GameObject eobj = GameObject.FindWithTag("Enemy");
            ES = eobj.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾

            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }

        }
        else if(other.CompareTag("Enemy1"))
        {
            EnemySeen ES;
            GameObject eobj1 = GameObject.FindWithTag("Enemy1");
            ES = eobj1.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }
        }
        else if (other.CompareTag("EnemyG1"))
        {
            EnemySeen ES;
            GameObject eobj2 = GameObject.FindWithTag("EnemyG1");
            ES = eobj2.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }
        }
        else if (other.CompareTag("EnemyG2"))
        {
            EnemySeen ES;
            GameObject eobj3 = GameObject.FindWithTag("EnemyG2");
            ES = eobj3.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }
        }
        else if (other.CompareTag("EnemyG3"))
        {
            EnemySeen ES;
            GameObject eobj4 = GameObject.FindWithTag("EnemyG3");
            ES = eobj4.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }
        }
        else if (other.CompareTag("EnemyG4"))
        {
            EnemySeen ES;
            GameObject eobj5 = GameObject.FindWithTag("EnemyG4");
            ES = eobj5.GetComponent<EnemySeen>(); //�t���Ă���X�N���v�g���擾
            if (ES.ONoff == 0)
            {
                var childTransforms = ES._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                foreach (var item in childTransforms)
                {
                    //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    item.gameObject.GetComponent<Renderer>().enabled = true;
                }
                ES.ONoff = 1;
                ES.SoundTime = 0.0f;
                ES.Sphere.SetActive(true);//���g��\�����\��
            }
        }
    }
}
