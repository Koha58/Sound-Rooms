using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

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
        public BehaviorType type { get; private set; }　//行動パターン（書き換えできない）
        public float value;　　　　　　　　　　　　　　//行動パターン変化を表す値

        public Behavior(BehaviorType _type) 
        { 
            //各変数の初期化
            type = _type;
            value = 0f;
        }
    }

    class Behaviors
    { 
        public List<Behavior> behaviorList { get; private set; }=new List<Behavior>();　//行動パターンの種類を表す変数

        //BehaviorTypeを引数に、該当するBehaviorクラスを参照する
        public Behavior GetBehavior(BehaviorType type)
        {
            foreach (Behavior behaviour in behaviorList)// behaviorListを一個ずつ確認
            {
                if (behaviour.type == type)
                {
                    return behaviour;
                }
            }
            return null;
        }

        public void SortDesire()
        {
            //要素を降順でソートしていく
            behaviorList.Sort((behaviour1, behaviour2) => behaviour2.value.CompareTo(behaviour1.value));
            //昇順にしたい場合は behaviour1.value.CompareTo(behaviour2.value)
        }

        //コンストラクタ
        public Behaviors()
        {
            //列挙型を文字列の配列に変換、Lengthで要素数を取得
            int BehaviorNum = System.Enum.GetNames(typeof(BehaviorType)).Length;

            // Behaviorクラスを生成初期化、リストに追加していく
            for (int i=0; i< BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType),i);//列挙型をインデックスで取得する
                Behavior newBehavior=new Behavior(type);　　　　　　　　　　　　　　　　　　　 //初期化　　　　　　　　　　　　　　　　　　　

                behaviorList.Add(newBehavior);//追加
            }
        }
    }

    Behaviors behaviors = new Behaviors();//クラスの実態


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
            behaviors.GetBehavior(BehaviorType.search).value += Time.deltaTime;
        }

        if (curretState != enemyState.walk)//現在のステートがwalkじゃなかったら
        {
            behaviors.GetBehavior(BehaviorType.walk).value += Time.deltaTime / 3;
        }

        if (curretState != enemyState.chase)//現在のステートがchaseじゃなかったら
        {
            behaviors.GetBehavior(BehaviorType.chase).value += Time.deltaTime / 5;
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

                behaviors.SortDesire();//行動パターンをソート

                if (behaviors.behaviorList[0].value >=1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch(behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.search:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("どこにいるかな？");
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
                }

                //behaviors.GetBehavior(BehaviorType.search).value += Time.deltaTime;

                behaviors.SortDesire();
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.walk:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("歩いている");
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
                }

               // behaviors.GetBehavior(BehaviorType.walk).value += Time.deltaTime / 3;

                behaviors.SortDesire();
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.chase:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("追いかけいるよ");
                    behaviors.GetBehavior(BehaviorType.search).value = 1;
                }

                //behaviors.GetBehavior(BehaviorType.chase).value += Time.deltaTime / 5;

                behaviors.SortDesire();
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search:
                            ChangeState(enemyState.search);
                            return;
                        case BehaviorType.chase:
                            ChangeState(enemyState.chase);
                            return;
                        case BehaviorType.walk:
                            ChangeState(enemyState.walk);
                            return;
                    }
                }

                #endregion
                break;


        }
    }
}
