using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScript : MonoBehaviour
{
    MeshRenderer mr;
    CapsuleCollider cc;
    GameObject Obj;

    int onoff = 0;  //����p�i���g���������ĂȂ����F0/���g�������������F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    void Start()
    {
        //���g���������ĂȂ���
        mr = GetComponent<MeshRenderer>();
        mr.enabled = false; //�����Ȃ��i�����j
        cc = GetComponent<CapsuleCollider>();
        cc.enabled = false; //�ʂ蔲���\
    }

    void Update()
    {
        //���g������������
        if (Input.GetMouseButtonDown(0))
        {
            //OnTriggerStay(cc);
            mr.enabled = true;  //������i�L���j
            cc.enabled = true;  //�ʂ蔲���s��
            onoff = 1;  //�����Ă��邩��1
        }

        //OnTriggerStay(cc);

        //�w�肵�����Ԃ��o�߂����猩���Ȃ��Ȃ�
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 3.0f)
            {
                mr.enabled = false; //�����Ȃ��i�����j
                cc.enabled = false; //�ʂ蔲���\
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }
    }

    /*
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "player")
        {
            mr.enabled = true;  //������i�L���j
            cc.enabled = true;  //�ʂ蔲���s��
            onoff = 1;  //�����Ă��邩��1
        }
        else if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 3.0f)
            {
                mr.enabled = false; //�����Ȃ��i�����j
                cc.enabled = false; //�ʂ蔲���\
                onoff = 0;  //�����Ă��Ȃ�����0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g
            }
        }
    }*/
}
