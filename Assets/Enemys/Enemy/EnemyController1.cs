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
    //[SerializeField] AudioClip searchClip;    //探す音
    //[SerializeField] AudioClip runClip;       //走る音
    //[SerializeField] AudioClip walkClip;      //歩く音

    //ステートベースAI
    #region
    enum enemyState
    {
        walk,    //歩く
        chase,   //追いかける
        search,  //探す
        doNothing//何もしない
    }

    enum BehaviorType
    {
        walk,    //歩く
        chase,   //追いかける
        search,  //探す
    }

    class Behavior
    { 
        public BehaviorType type { get; private set; }
        public float value;

        public Behavior(BehaviorType _type) 
        { 
            type = _type;
            value = 0f;
        }
    }

    class Behaviors
    { 
        public List<Behavior> behaviorList { get; private set; }=new List<Behavior>();
        //public Behavior GetBehavior(BehaviorType type)
        //{
        //    foreach (Behaviour behaviour in behaviorList)
        //    {
        //        if (behaviour.type == type)
        //        {
        //            return behaviour;
        //        }
        //    }
        //    return null;
        //}


        //コンストラクタ
        public Behaviors()
        {
            int BehaviorNum = System.Enum.GetNames(typeof(BehaviorType)).Length;

            for(int i=0; i< BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType),i);
                Behavior newBehavior=new Behavior(type);

                behaviorList.Add(newBehavior);
            }
        }
    }



    float doNothingTime;//何もしない時間

    float walking = 0;//歩いている　0～１;
    float walkingTime;//歩いている時間

    float chasing = 0;//追いかけている　0～１;
    float chaseTime; //追いかけている時間（ステートの切り替えだけにある時間）

    float searching = 0;//探している　0～１;
    float searchTime;//探している時間

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
        if (curretState != enemyState.search)//現在のステートがsearchじゃなかったら
        {
            searchTime += Time.deltaTime;
        }

        if (curretState != enemyState.walk)//現在のステートがwalkじゃなかったら
        {
            walkingTime += Time.deltaTime/3;
        }

        if (curretState != enemyState.chase)//現在のステートがchaseじゃなかったら
        {
            chaseTime += Time.deltaTime/5;
        }

        if(curretState != enemyState.doNothing)
        {
            doNothingTime += Time.deltaTime/5;
        }


        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("何もしない");
                }

                if (searchTime <= 1)
                {
                    ChangeState(enemyState.search);
                    return;
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

                if (walkingTime >= 3)
                {
                    ChangeState(enemyState.walk);
                    return;
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

                if (chaseTime >= 5)
                {
                    ChangeState(enemyState.chase);
                    return;
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

                if (doNothingTime >= 10)
                {
                    ChangeState(enemyState.doNothing);
                    return;
                }

                #endregion
                break;


        }
    }
}
