using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Enemyの制御（移動　アニメーション　サウンド）クラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    // キャラクターのID (敵キャラクターを一意に識別するため)
    public int characterID;

    // ナヴィメッシュエージェントの参照 (移動に使用するNavMeshAgent)
    NavMeshAgent navMeshAgent;

    // PatrolPointManagerへの参照 (巡回ポイントを管理)
    private PatrolPointManager patrolPointManager;

    // アニメーターの参照 (アニメーション制御用)
    [SerializeField] Animator animator;

    // サウンド関連の変数
    [SerializeField] AudioSource audioSourse; //オーディオソース取得
    [SerializeField] AudioClip searchClip;    //探す音
    [SerializeField] AudioClip runClip;       //走る音
    [SerializeField] AudioClip walkClip;      //歩く音

    void Idle() { audioSourse.PlayOneShot(searchClip); }     //探す音を再生
    void Run() { audioSourse.PlayOneShot(runClip); }         //走る音を再生
    void Walk() { audioSourse.PlayOneShot(walkClip); }       //歩く音を再生

    //巡回
    private List<Transform> patrolPoints;     // 巡回ポイントリスト
    private int currentPatrolPointIndex = 0;  // 現在の巡回ポイントのインデックス
    private bool isPatrolling = false;      　// 巡回中かどうか

    //追跡
    public Transform player;                  //プレイヤーの位置
    float distanceToPlayer = Mathf.Infinity;  // プレイヤーとの距離
    float chaseRange = 7f;        　　　　　　//Playerを検知する範囲

    //ラジオカセット
    public float detectionRange = 10f;   　// 音を聞き取れる範囲
    public Vector3 soundPosition;        　//ラジオカセットの置かれているポイント
    private bool isMovingToSound = false;　//ラジオカセットに反応して移動する


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
        public float value;                             //行動パターン変化を表す値

        // コンストラクタ
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

        // 行動パターンの重要度順にソート
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
    bool stateEnter = true;                      //ステートの変化時に一回だけ特殊な処理をさせたいときに使用

    // ステート変更用メソッド
    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;
        stateEnter = true;
    }

    #endregion

    // 初期化処理
    private void Start()
    {
        // コンポーネントの取得
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        // PatrolPointManagerのインスタンスを取得
        patrolPointManager = FindObjectOfType<PatrolPointManager>();

        // そのIDに対応する巡回ポイントを取得
        patrolPoints = patrolPointManager.GetPatrolPoints(characterID);

        // 巡回ポイントが存在すれば巡回を開始
        if (patrolPoints != null && patrolPoints.Count > 0)
        {
            isPatrolling = true;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);  // 最初の巡回ポイントに向かう
        }

        // 行動リストの巡回の重要度を初期設定
        behaviors.GetBehavior(BehaviorType.patrol).value = 2;
    }

    private void Update()
    {
        GameObject obj = GameObject.Find("Player");      //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();  //付いているスクリプトを取得

        #region　プレイヤーの位置を確認し、追跡・巡回を判断
        Vector3 Position = player.position - transform.position;      // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定

        distanceToPlayer = Vector3.Distance(player.position, transform.position); // プレイヤーとの距離を計算

        if (isFront && !isMovingToSound && PS.onoff == 1)
        {
            if (distanceToPlayer <= chaseRange)
            {
                PS.onoff = 1;
                PS.Visualization = true;
                behaviors.GetBehavior(BehaviorType.chase).value = 2; // プレイヤーを追跡する
            }
            else if (distanceToPlayer >= chaseRange)
            {
                behaviors.GetBehavior(BehaviorType.patrol).value = 2;   // プレイヤーが範囲外の場合、巡回に戻る
                isPatrolling = true;
                PS.Visualization = false; // プレイヤーの可視化をオフ
            }
        }
        else
        {
            behaviors.GetBehavior(BehaviorType.patrol).value = 2;   // プレイヤーが範囲外の場合、巡回に戻る
            isPatrolling = true;
            PS.Visualization = false; // プレイヤーの可視化をオフ
        }
        #endregion

        // ラジオカセットの音に反応して移動する
        if (isMovingToSound)
        {
            isPatrolling = false;
            // 目的地に近づいたら停止
            if (Vector3.Distance(this.transform.position, soundPosition) < 1f)
            {
                behaviors.GetBehavior(BehaviorType.hear).value = 2; // 音の元に到達
                isMovingToSound = false;
            }
            else
            {
                behaviors.GetBehavior(BehaviorType.near).value = 2; // 音に近づいている
            }
        }

        // 現在のステートに基づいた処理
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
                    PS.Visualization = true;
                    PS.onoff = 1;
                    Debug.Log("追いかけいるよ");
                }

                Run();  // 走る音

                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                animator.SetBool("Idle", false);

                transform.LookAt(player.transform);

                navMeshAgent.SetDestination(player.transform.position);
                navMeshAgent.speed = 3.5f;

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

    // 音に反応したときに呼ばれる
    public void OnSoundHeard(Vector3 position)
    {
        // 範囲内の場合のみ音に反応
        if (Vector3.Distance(transform.position, position) <= detectionRange)
        {
            soundPosition = position;   // 音の位置を保存
            isMovingToSound = true;      // 音に移動する
        }
    }
}
