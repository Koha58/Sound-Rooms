using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    [Serializable]
    public enum Type//Item�^�C�v�̎�ށA�|�P�����ł����A�݂��^�C�v�₭���^�C�v�Ƃ�
    {
        Enemy1, Enemy2,Enemy3,Boss
    }

    public Type type;//�^�C�v����
    //����
    public float ONOFF = 0;//(0�������Ȃ��G�P���������ԁj
    [SerializeField] float ONTime;
    [SerializeField] float OFFTime;
    [SerializeField] float VisualizationRandom;//�������Ԃ������_��

    //Player��ǐ�
    [SerializeField] float ChaseSpeed = 0.25f;//Player��ǂ�������X�s�[�h
    [SerializeField] bool ChaseONOFF;

    //Destroy�̔���
    public bool DestroyONOFF;//(DestroyON�F true/DestroyOFF: false)

    //Wall�ɓ���������
    [SerializeField] private bool TouchWall;
    [SerializeField] float WallONOFF = 0.0f;


    public Enemy(Type type)
    {
        this.type = type;
    }

}
