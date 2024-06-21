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

    //�͈͓��̃A�C�e���̉����E�s����
        public int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

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
            // �q�I�u�W�F�N�g�̐����擾
            int childCount1 = Key1.transform.childCount;
            for (int i = 0; i < childCount1; i++)
            {
                Transform childTransform = Key1.transform.GetChild(i);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = false;
            }
            GameObject Key2 = parentObject[1];
            // �q�I�u�W�F�N�g�̐����擾
            int childCount2 = Key2.transform.childCount;
            for (int a = 0; a < childCount2; a++)
            {
                Transform childTransform = Key2.transform.GetChild(a);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = false;
            }
            GameObject Key3 = parentObject[2];
            // �q�I�u�W�F�N�g�̐����擾
            int childCount3 = Key3.transform.childCount;
            for (int b = 0; b < childCount3; b++)
            {
                Transform childTransform = Key3.transform.GetChild(b);
                GameObject childObject = childTransform.gameObject;
                childObject.GetComponent<Renderer>().enabled = false;
            }
            GameObject Key4 = parentObject[3];
            // �q�I�u�W�F�N�g�̐����擾
            int childCount4 = Key4.transform.childCount;
            for (int c = 0; c < childCount4; c++)
            {
                Transform childTransform = Key4.transform.GetChild(c);
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
                //Rigidbody���擾
                var rb = Box.GetComponent<Rigidbody>();
                //�ړ�����]�����Ȃ��悤�ɂ���
                rb.constraints = RigidbodyConstraints.FreezeAll;
                Box.GetComponent<Renderer>().enabled = false;
            }
        }

    private void Update()
    {
        GameObject eobj = GameObject.FindWithTag("Enemy");
        EnemyController EC = eobj.GetComponent<EnemyController>(); //Enemy�ɕt���Ă���X�N���v�g���擾

        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        parentObject = GameObject.FindGameObjectsWithTag("Item");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");
        ISe = isobj.GetComponent<ItemSearch>(); //�t���Ă���X�N���v�g���擾

        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾



        if (EC.ONoff == 1)
        {
            SeenArea.GetComponent<Collider>().enabled = false;//������i�L���j
            onoff = 0;  //�����Ă��邩��1
        }
        //�����o���Ɣ͈͓�������
        if (EC.ONoff == 1)
        {
            SeenArea.GetComponent<Collider>().enabled = true;//������i�L���j
            onoff = 1;  //�����Ă��邩��1
        }
        /*
             //�����o�Ă��Ȃ���΁A�͈͓��̉������ł��Ȃ�����
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
                {
                    
                    if (ISe.count != 4)
                    {
                        if (parentObject[0] != null)
                        {
                            GameObject Key1 = parentObject[0];
                            // �q�I�u�W�F�N�g�̐����擾
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
                            // �q�I�u�W�F�N�g�̐����擾
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
                    
                    SeenArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j

                    foreach (GameObject Wall in Walls)
                    {
                        Wall.GetComponent<Renderer>().enabled = false;
                    }

                    // �q�I�u�W�F�N�g�̐����擾
                    int doorparts = doorObject.transform.childCount;
                    for (int j = 0; j < doorparts; j++)
                    {
                        Transform childTransform = doorObject.transform.GetChild(j);
                        GameObject door = childTransform.gameObject;
                        door.GetComponent<Renderer>().enabled = false;
                    }

                    foreach (GameObject Box in Boxes)
                    {
                        //Rigidbody���擾
                        var rb = Box.GetComponent<Rigidbody>();
                        //�ړ�����]�����Ȃ��悤�ɂ���
                        rb.constraints = RigidbodyConstraints.FreezeAll;
                        Box.GetComponent<Renderer>().enabled = false;
                    }
                    onoff = 0;  //�����Ă��Ȃ�����0
                }
            }
            */
    }

        void OnTriggerEnter(Collider other)
        {
            parentObject = GameObject.FindGameObjectsWithTag("Item");
            GameObject doorObject = GameObject.Find("Door1");
            objName = other.gameObject.name;
            //�ڐG�����I�u�W�F�N�g�̃^�O��"Item"�̂Ƃ�
            if (objName == "key 1 (1)")
            {
                if (parentObject[0] != null)
                {
                    GameObject Key1 = parentObject[0];
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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
                    // �q�I�u�W�F�N�g�̐����擾
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

            else if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
            {
                Debug.Log("1");
                other.gameObject.GetComponent<Renderer>().enabled = true;
            }
            else if (other.CompareTag("Box"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Box"�̂Ƃ�
            {
                other.GetComponent<Renderer>().enabled = true;
            }

            else if (other.CompareTag("Door"))
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


            if (other.CompareTag("Enemy"))
            {

                GameObject eobj = GameObject.FindWithTag("Enemy");
                EnemyController EC = eobj.GetComponent<EnemyController>(); //�t���Ă���X�N���v�g���擾

                if (EC.ONoff == 0)
                {
                    var childTransforms = EC._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));

                    foreach (var item in childTransforms)
                    {
                        //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }

                    EC.ONoff = 1;
                    EC.SoundTime = 0.0f;
                    // EC.Sphere.SetActive(true);//���g��\�����\��
                }


                EC.ONoff = 1;
                EC.SoundTime = 0.0f;
                // EC.Sphere.SetActive(true);//���g��\�����\��
            }


            else if (other.CompareTag("EnemyG"))
            {
                EnemyGController EGC;
                EGC = other.gameObject.GetComponent<EnemyGController>(); //�t���Ă���X�N���v�g���擾
                if (EGC.ONoff == 0)
                {
                    var childTransforms = EGC._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("EnemyParts"));
                    foreach (var item in childTransforms)
                    {
                        //�^�O��"EnemyParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        item.gameObject.GetComponent<Renderer>().enabled = true;
                    }
                    EGC.ONoff = 1;
                    EGC.SoundTime = 0.0f;
                    // EsG.Sphere.SetActive(true);//���g��\�����\��
                }
            }
        }
}






