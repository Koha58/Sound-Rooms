using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    enum enemyState
    { 
        walk,
        chase,
        search,
        doNothing
    }

    enemyState curretState = enemyState.doNothing;//現在のステートは何もしていない
    bool stateEnter = true;                    　 //ステートの変化時に一回だけ特殊な処理をさせたいときに使用

    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    private void Update()
    {
        //if(curretState != enemyState.walk) 
        //{ 

        //}

        switch(curretState)
        {
            case enemyState.doNothing:
                if(stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("何もしない");
                }
                break; 

        }
    }

    //アニメーション
    [SerializeField] Animator animator;　//アニメーター取得

    //サウンド
    [SerializeField] AudioSource audioSourse; //オーディオソース取得
    [SerializeField] AudioClip searchClip;    //探す音
    [SerializeField] AudioClip runClip;       //走る音
    [SerializeField] AudioClip walkClip;      //歩く音

}
