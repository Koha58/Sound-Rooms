using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeadMsg : MonoBehaviour
{
    //���b�Z�[�W��\������Q�[���I�u�W�F�N�g�������邽�߂̃t�B�[���h
    //������ꂽ�I�u�W�F�N�g�����b�Z�[�W���Ǐ]����
    public Transform targetTran;
    //public Renderer item;

    //���[���h���W�i3D�I�u�W�F�N�g�̍��W�j����X�N���[���̍��W�ɕϊ����āA�����Ɉړ�����
    //��P�����ɃJ�����I�u�W�F�N�g�A��Q�����Ƀ��b�Z�[�W��\���������I�u�W�F�N�g�̍��W��n��(����͓���ɕ\���������̂�Y�������ɂP�グ���ꏊ���w��)
    //Vector3.up��new Vector3(0, 1, 0)�𓯂�(ex.���0.5�グ������΁Avector3.up * 0.5�Ƃ���΂���)
    void Update()
    {
        //if (item.enabled == true)
        //{
            transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTran.position + Vector3.up);
       // }
    }
}
