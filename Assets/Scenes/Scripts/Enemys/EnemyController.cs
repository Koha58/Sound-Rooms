using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int characterID;       　// キャラクターのID
    public Transform player;    　　//プレイヤーの位置

    NavMeshAgent navMeshAgent;      //ナヴィメッシュを取得

    public PatrolPointManager patrolPointManager;  // PatrolPointManagerの参照

    //アニメーション
    //[SerializeField] Animator animator;　//アニメーター取得

    //サウンド
    [SerializeField] AudioSource audioSourse; //オーディオソース取得
    [SerializeField] AudioClip searchClip;    //探す音
    [SerializeField] AudioClip runClip;       //走る音
    [SerializeField] AudioClip walkClip;      //歩く音

    void Idle() { audioSourse.PlayOneShot(searchClip); }
    void Run() { audioSourse.PlayOneShot(runClip); }
    void Walk() { audioSourse.PlayOneShot(walkClip); }


    //ステートベースAI
    #region
    enum enemyState
    {
        patrol,    //巡回
        chase,     //追いかける
        search,    //探す
        hear,      //聞く
        near,      //近づく
        doNothing  //何もしない
    }

    enum BehaviorType
    {
        patrol,    //巡回
        chase,     //追いかける
        search,    //探す
        hear,      //聞く
        near,      //近づく
        doNothing　//何もしない
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
        public List<Behavior> behaviorList { get; private set; } = new List<Behavior>();　//行動パターンの種類を表す変数

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
            for (int i = 0; i < BehaviorNum; i++)
            {
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType), i);//列挙型をインデックスで取得する
                Behavior newBehavior = new Behavior(type);　　　　　　　　　　　　　　　　　　　 //初期化　　　　　　　　　　　　　　　　　　　

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
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        ChangeState(enemyState.patrol);
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得

        if (audioSourse.clip != null)
        {
            audioSourse.Play();
        }

        switch (curretState)
        {
            case enemyState.doNothing: //何もしない
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.doNothing).value = 0;
                    Debug.Log("何もしない");
                }

                behaviors.SortDesire();//行動パターンをソート

                if (behaviors.behaviorList[0].value >= 1)//リストの一番上の1を上回ったら
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
            case enemyState.patrol:　//巡回
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.patrol).value = 0;
                    Debug.Log("巡回中");


                    // キャラクターIDに基づいて次の巡回ポイントを取得
                    Transform nextPatrolPoint = patrolPointManager.GetNextPatrolPoint(characterID);
                    if (nextPatrolPoint != null)
                    {
                        navMeshAgent.SetDestination(nextPatrolPoint.position);  // 次の巡回先に向けて移動
                    }
                    else
                    {
                        Debug.LogError("巡回ポイントが見つかりません");
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
            case enemyState.search: //探す
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.search).value = 0;
                    Debug.Log("どこにいるかな？");
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
            case enemyState.chase:　//追いかける
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.chase).value = 0;
                    Debug.Log("追いかけいるよ");
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
            case enemyState.hear:　//聞く
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.hear).value = 0;
                    Debug.Log("聞く");
                }

                behaviors.SortDesire();//行動パターンをソート

                if (behaviors.behaviorList[0].value >= 1)//リストの一番上の1を上回ったら
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
            case enemyState.near:　//音に近づく
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.near).value = 0;
                    Debug.Log("近づく");
                }

                behaviors.SortDesire();//行動パターンをソート

                if (behaviors.behaviorList[0].value >= 1)//リストの一番上の1を上回ったら
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
        }
    }
}
