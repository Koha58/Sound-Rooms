using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int characterID;       　// キャラクターのID

    NavMeshAgent navMeshAgent;      //ナヴィメッシュを取得

    private PatrolPointManager patrolPointManager;  // PatrolPointManagerへの参照

    private List<Transform> patrolPoints;  // 巡回ポイントリスト
    private int currentPatrolPointIndex = 0;  // 現在の巡回ポイントのインデックス

    private bool isPatrolling = false;  // 巡回中かどうか

    public Transform player;      //プレイヤーの位置
    float distanceToPlayer = Mathf.Infinity;
    float chaseRange = 7f;  //Playerを検知する範囲

    public float detectionRange = 10f; // 音を聞き取れる範囲
    public Vector3 soundPosition;
    private bool isMovingToSound = false;//ラジオカセットに反応して移動する

    //アニメーション
    [SerializeField] Animator animator; //アニメーター取得

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

        patrolPointManager = FindObjectOfType<PatrolPointManager>();  // PatrolPointManagerのインスタンスを取得
        patrolPoints = patrolPointManager.GetPatrolPoints(characterID);  // そのIDに対応する巡回ポイントを取得

        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            isPatrolling = true;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);  // 最初の巡回ポイントに向かう
        }

        behaviors.GetBehavior(BehaviorType.patrol).value = 2;
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得

        #region
        Vector3 Position = player.position - transform.position;                          // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;                      // ターゲットが自身の前方にあるかどうか判定

        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            if (isFront && !isMovingToSound && PS.onoff == 1)
            {
                behaviors.GetBehavior(BehaviorType.chase).value = 2;
            }
            else if(isFront && !isMovingToSound)
            {
                isPatrolling = false;
                behaviors.GetBehavior(BehaviorType.search).value = 2;
            }
        }
        else if (distanceToPlayer >= chaseRange && !isMovingToSound)
        {
            behaviors.GetBehavior(BehaviorType.patrol).value = 2;
            isPatrolling =true;
            PS.Visualization = false;
        }
        #endregion

        if (isMovingToSound)
        {
            isPatrolling = false;
            // 目的地に近づいたら停止
            if (Vector3.Distance(this.transform.position, soundPosition) < 1f)
            {
                behaviors.GetBehavior(BehaviorType.hear).value = 2;
                isMovingToSound = false;
            }
            else
            {
                behaviors.GetBehavior(BehaviorType.near).value = 2;
            }
        }

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
                    behaviors.GetBehavior(BehaviorType.patrol).value = 1;
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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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
                }

                Walk(); // 歩く音

                if (isPatrolling)
                {
                    animator.SetBool("Walk", true);
                    animator.SetBool("Run", false);
                    animator.SetBool("Idle", false);

                    navMeshAgent.speed = 1.0f;

                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

                    // 巡回ポイントに到達したかチェック
                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.5f)
                    {
                        // 次の巡回ポイントに移動
                        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);

                Idle();

                navMeshAgent.SetDestination(this.transform.position);

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                Run();  // 走る音

                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);

                navMeshAgent.SetDestination(player.transform.position);

                navMeshAgent.speed = 3.5f;

                PS.Visualization = true;
                PS.onoff = 1;

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                Idle();

                animator.SetBool("Walk",false);
                animator.SetBool("Run",false);
                animator.SetBool("Idle",true);

                navMeshAgent.speed = 1.0f;

                navMeshAgent.SetDestination(this.transform.position);

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
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

                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("Idle", false);

                navMeshAgent.speed = 2.0f;

                navMeshAgent.SetDestination(soundPosition);

                Walk(); // 歩く音

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
                        case BehaviorType.hear:
                            ChangeState(enemyState.hear);
                            return;
                        case BehaviorType.near:
                            ChangeState(enemyState.near);
                            return;
                    }
                }

                #endregion
                break;
        }
    }

    public void OnSoundHeard(Vector3 position)
    {
        // 範囲内の場合のみ音に反応
        if (Vector3.Distance(transform.position, position) <= detectionRange)
        {
            soundPosition = position;
            isMovingToSound = true;
        }
    }
}
