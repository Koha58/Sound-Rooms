using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController1 : MonoBehaviour
{
    public Transform player;

    NavMeshAgent navMeshAgent;

    float chaseRange = 5f;  //Playerを検知する範囲
    float distanceToPlayer = Mathf.Infinity;

    private float realizeTime;
    private float realizeMaxTime=10f;

    private float searchTime;

    public Slider slider;

    public Canvas canvas;

    int i = 0;

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
        patrol,    //巡回
        chase,     //追いかける
        search,    //探す
        discussion,//話し合い
        faind,     //見つける
        Stand,     //立ちはだかる
        restraint, //拘束
        enjoy,     //楽しむ
        anticipate,//先回り(予測)
        shout,     //叫ぶ
        sleep,     //眠る
        rage,      //暴れる
        doNothing//何もしない
    }

    enum BehaviorType
    {
        patrol,    //巡回
        chase,     //追いかける
        search,    //探す
        discussion,//話し合い
        faind,     //見つける
        Stand,     //立ちはだかる
        restraint, //拘束
        enjoy,     //楽しむ
        anticipate,//先回り(予測)
        shout,     //叫ぶ
        sleep,     //眠る
        rage,      //暴れる
        doNothing//何もしない
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
        realizeTime = 0;
        slider.value = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        slider.value = realizeTime/realizeMaxTime;

        canvas.transform.rotation =Camera.main.transform.rotation;

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            behaviors.GetBehavior(BehaviorType.search).value = 2;
            realizeTime += 0.2f;
        }
        else
        {
            realizeTime -= 0.2f;
            ChangeState(enemyState.patrol);
        }

        if (realizeTime == realizeMaxTime||slider.value==1f)
        {
            ChangeState(enemyState.chase);
        }
        else
        {
            behaviors.GetBehavior(BehaviorType.search).value = 1f;
        }

        switch (curretState)
        {
            case enemyState.doNothing:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("何もしない");
                    ChangeState(enemyState.patrol);
                }

                behaviors.SortDesire();//行動パターンをソート

                if (behaviors.behaviorList[0].value >=1)//リストの一番上の1を上回ったら
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
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
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
                    navMeshAgent.SetDestination(this.transform.position);
                    behaviors.GetBehavior(BehaviorType.search).value = 0;
                    searchTime += Time.deltaTime;
                }

                if(searchTime>=10f)
                {
                    searchTime = 0;
                    ChangeState(enemyState.patrol);
                }

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
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.patrol:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("巡回中");
                    navMeshAgent.SetDestination(GameManager.instance.testPos[i].position);
                }

                if (navMeshAgent.remainingDistance <= 0.1f && !navMeshAgent.pathPending)
                {
                    i += 1;

                    if (i > 2)
                    {
                        i = 0;
                    }
                }

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
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
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
                    navMeshAgent.speed = 4.0f;
                    Chase();
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
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.discussion:
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    Debug.Log("話し合っている");
                }

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
                        case BehaviorType.patrol:
                            ChangeState(enemyState.patrol);
                            return;
                        case BehaviorType.discussion:
                            ChangeState(enemyState.discussion);
                            return;
                        case BehaviorType.faind:
                            ChangeState(enemyState.faind);
                            return;
                        case BehaviorType.Stand:
                            ChangeState(enemyState.Stand);
                            return;
                        case BehaviorType.restraint:
                            ChangeState(enemyState.restraint);
                            return;
                        case BehaviorType.enjoy:
                            ChangeState(enemyState.enjoy);
                            return;
                        case BehaviorType.anticipate:
                            ChangeState(enemyState.anticipate);
                            return;
                        case BehaviorType.shout:
                            ChangeState(enemyState.shout);
                            return;
                        case BehaviorType.sleep:
                            ChangeState(enemyState.sleep);
                            return;
                        case BehaviorType.rage:
                            ChangeState(enemyState.rage);
                            return;
                        case BehaviorType.doNothing:
                            ChangeState(enemyState.doNothing);
                            return;
                    }
                }

                #endregion
                break;


        }
    }

    void Chase()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}
