using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    [Serializable]
    public enum Type//Itemタイプの種類、ポケモンでいう、みずタイプやくさタイプとか
    {
        Enemy1, Enemy2,Enemy3,Boss
    }

    public Type type;//タイプ決め
    //可視化
    public float ONOFF = 0;//(0が見えない；１が見える状態）
    [SerializeField] float ONTime;
    [SerializeField] float OFFTime;
    [SerializeField] float VisualizationRandom;//可視化時間をランダム

    //Playerを追跡
    [SerializeField] float ChaseSpeed = 0.25f;//Playerを追いかけるスピード
    [SerializeField] bool ChaseONOFF;

    //Destroyの判定
    public bool DestroyONOFF;//(DestroyON： true/DestroyOFF: false)

    //Wallに当たった時
    [SerializeField] private bool TouchWall;
    [SerializeField] float WallONOFF = 0.0f;


    public Enemy(Type type)
    {
        this.type = type;
    }

}
