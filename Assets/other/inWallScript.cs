using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class inWallScript : MonoBehaviour
{
    BoxCollider bc;

    int onoff = 0;  //����p�i�v���C���[�������Ă��Ȃ����F0/�v���C���[�������Ă��鎞�F1�j

    LevelMeter levelMeter;

    MeshRenderer Wall;

    float WallCount;

    void Start()
    {
        Wall = GetComponent<MeshRenderer>();

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject obj = GameObject.Find("Player"); //Player�I�u�W�F�N�g��T��
            PlayerSeen PS = obj.GetComponent<PlayerSeen>(); //�t���Ă���X�N���v�g���擾
            if (PS.onoff == 1)
            {
                Wall.enabled = true;
                bc.enabled = true;
            }
        }
    }
}
