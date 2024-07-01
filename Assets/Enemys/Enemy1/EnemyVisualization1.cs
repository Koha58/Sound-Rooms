using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyVisualization1 : MonoBehaviour
{
    // public int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�jVisualization

    [SerializeField] public GameObject Ring;
    //public GameObject ItemCanvas;
    public GameObject[] Walls;
    public GameObject[] Boxes;
    public static GameObject[] parentObject;
    private string objName;

    ItemSearch ISe;
    //LevelMeter levelMeter;
    [SerializeField] public Transform _parentTransform;

    bool PlayerOnoff;
    float OnoffTime;

    LevelMeter levelMeter;

    private void Start()
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
        Ring.GetComponent<Collider>().enabled = false;
        //ItemCanvas.GetComponent<Canvas>().enabled = false;

        Walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject Wall in Walls)
        {
            Wall.GetComponent<Renderer>().enabled = false;
        }

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
        GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
        var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));

        Walls = GameObject.FindGameObjectsWithTag("Wall");

        Boxes = GameObject.FindGameObjectsWithTag("Box");

        parentObject = GameObject.FindGameObjectsWithTag("Item");

        GameObject doorObject = GameObject.Find("Door1");

        GameObject isobj = GameObject.Find("Player");
        ISe = isobj.GetComponent<ItemSearch>(); //�t���Ă���X�N���v�g���擾


        if (PlayerOnoff == true)
        {
            PS.onoff = 1;  //�����Ă��邩��1
            foreach (var playerParts in childTransforms)
            {
                //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                playerParts.gameObject.GetComponent<Renderer>().enabled = true;
            }
        }


        if (PlayerOnoff == false)
        {
            GameObject soundobj = GameObject.Find("SoundVolume");
            levelMeter = soundobj.GetComponent<LevelMeter>(); //�t���Ă���X�N���v�g���擾

            //�v���C���[�������Ă��鎞
            if (levelMeter.nowdB > 0.0f)
            {
                PS.onoff = 1;  //�����Ă��邩��1
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }

            //�v���C���[�������Ă��Ȃ��Ƃ�
            if (PS.onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f)
                {
                    PS.onoff = 0;  //�����Ă��Ȃ�����0
                    foreach (var playerParts in childTransforms)
                    {
                        //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                        playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }
        }

        if (PS.onoff == 1)
        {
            OnoffTime += Time.deltaTime;
            if (OnoffTime >= 5.0f)
            {
                PlayerOnoff = false;
                foreach (var playerParts in childTransforms)
                {
                    //�^�O��"PlayerParts"�ł���q�I�u�W�F�N�g��������悤�ɂ���
                    playerParts.gameObject.GetComponent<Renderer>().enabled = false;
                }
                OnoffTime = 0;


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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                            childObject.GetComponent<Renderer>().enabled = false;
                            //keyOnoff = true;
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
                        // �q�I�u�W�F�N�g�̐����擾
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
                        // �q�I�u�W�F�N�g�̐����擾
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
                        // �q�I�u�W�F�N�g�̐����擾
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
                        // �q�I�u�W�F�N�g�̐����擾
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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
                    //keyOnoff = true;
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

        if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
        if (other.CompareTag("Box"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Box"�̂Ƃ�
        {
            other.GetComponent<Renderer>().enabled = true;
        }

        if (other.CompareTag("Door"))
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

        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 0)
            {
                PlayerOnoff = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            var childTransforms = PS._parentTransform.GetComponentsInChildren<Transform>().Where(t => t.CompareTag("PlayerParts"));
            if (PS.onoff == 1)
            {
                PlayerOnoff = false;
            }
        }
    }
}