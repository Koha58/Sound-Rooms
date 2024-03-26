using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�͈͓��̃A�C�e���̉����E�s����
public class ItemSeen : MonoBehaviour
{
    int onoff = 0;  //����p�i�����Ă��Ȃ����F0/�����Ă��鎞�F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    public GameObject Key1;
    public GameObject Key2;
    [SerializeField] public GameObject SeenArea;
    public GameObject ItemCanvas;
    public GameObject Wall;


    void Start()
    {
        //�ŏ��͌����Ȃ����
        SeenArea.GetComponent<Collider>().enabled = false;
        Key1.GetComponent<Renderer>().enabled = false;
        Key2.GetComponent<Renderer>().enabled = false;
        ItemCanvas.GetComponent<Canvas>().enabled = false;
        Wall.GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        //���N���b�N�Ŕ͈͓�������
        if (Input.GetMouseButtonDown(0))
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
                if (Key1 != null)
                {
                    SeenArea.GetComponent<Collider>().enabled = false;//�����Ȃ��i�����j
                    Key1.GetComponent<Renderer>().enabled = false;
                    Key2.GetComponent<Renderer>().enabled = false;
                    ItemCanvas.GetComponent<Canvas>().enabled = false;
                }
                Wall.GetComponent<Renderer>().enabled = false;
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //�ڐG�����I�u�W�F�N�g�̃^�O��"Item"�̂Ƃ�
        if (other.CompareTag("Item") && Key1 != null)
        {
            Key1.GetComponent<Renderer>().enabled = true;
            Key2.GetComponent<Renderer>().enabled = true;
            ItemCanvas.GetComponent<Canvas>().enabled = true;
        }
        else if (other.CompareTag("Wall"))//�ڐG�����I�u�W�F�N�g�̃^�O��"Wall"�̂Ƃ�
        {
            Wall.GetComponent<Renderer>().enabled = true;
        }
    }
}
