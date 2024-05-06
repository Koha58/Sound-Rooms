using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���o�����g

public class PlayerSound : MonoBehaviour
{
    public float angle = 45.0f;

    SphereCollider sc;
    GameObject Player;  //�I�u�W�F�N�g���͓K�X�ύX

    int onoff = 0;  //����p�i�����F0/�����o���F1�j

    private float seentime = 0.0f; //�o�ߎ��ԋL�^�p

    void Start()
    {
        //�ŏ��͖���
        sc = GetComponent<SphereCollider>();
        sc.enabled = false; //����
    }

    void Update()
    {
        //���N���b�N�ŉ����o��
        if (Input.GetMouseButtonDown(0))
        {
            sc.enabled = true;  //�����o��
            onoff = 1;  //�����o������1

            OnTriggerStay(sc);

        }

        //�w�肵�����Ԃ��o�߂����疳���ɖ߂�
        if (onoff == 1)
        {
            seentime += Time.deltaTime;
            if (seentime >= 0.1f)
            {
                sc.enabled = false; //����
                onoff = 0;  //����������0
                seentime = 0.0f;    //�o�ߎ��Ԃ����Z�b�g

                Debug.Log("");

            }
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "obj")
        {
            Vector3 posDelta = other.transform.position - transform.position;
            float objangle = Vector3.Angle(transform.forward, posDelta);
            if (objangle < angle)
            {
                Debug.Log("!!!");
            }
        }
    }

}
