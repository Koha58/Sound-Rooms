using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private Transform player;

    //アニメーション
    [SerializeField] Animator animator;　//アニメーター取得

    //サウンド
    [SerializeField] AudioSource audioSourse; //オーディオソース取得
    [SerializeField] AudioClip searchClip;    //探す音
    [SerializeField] AudioClip runClip;       //走る音
    [SerializeField] AudioClip walkClip;      //歩く音

    //ステートベースAI
    #region
    enum enemyState
    {
        walk,
        chase,
        search,
        doNothing
    }

    float walking = 0;//歩いている　0～１;

    float chasing = 0;//追いかけている　0～１;

    float searching = 0;//探している　0～１;

    enemyState curretState = enemyState.doNothing;//現在のステートは何もしていない
    bool stateEnter = true;                    　 //ステートの変化時に一回だけ特殊な処理をさせたいときに使用

    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;　//プレイヤーの位置を取得
    }

    private void Update()
    {
        //if(curretState != enemyState.walk) 
        //{ 

        //}

        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("何もしない");
                }



                #endregion
                break;
            case enemyState.search:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("どこにいるかな？");
                }



                #endregion
                break;
            case enemyState.walk:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("歩いている");
                }



                #endregion
                break;
            case enemyState.chase:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("追いかけいるよ");
                }



                #endregion
                break;


        }
    }
}
