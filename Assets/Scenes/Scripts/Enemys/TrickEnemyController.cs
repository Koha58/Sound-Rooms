using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// TrickEnemyの制御（移動　アニメーション　サウンド）クラス
/// </summary>
public class TrickEnemyController : MonoBehaviour
{
    // キャラクターのID (敵キャラクターを一意に識別するため)
    public int characterID = -1;

    // ナヴィメッシュエージェントの参照 (移動に使用するNavMeshAgent)
    NavMeshAgent navMeshAgent;

    // PatrolPointManagerへの参照 (巡回ポイントを管理)
    private PatrolPointManager patrolPointManager;

    // アニメーターの参照 (アニメーション制御用)
    [SerializeField] Animator animator;

    // サウンド関連の変数
    [SerializeField] private AudioSource audioSourse; //オーディオソース取得
    [SerializeField] private AudioClip searchClip;    //探す音
    [SerializeField] private AudioClip runClip;       //走る音

    void Idle() { PlayClipIfNotPlaying(searchClip); }     //探す音を再生
    void Run() { PlayClipIfNotPlaying(runClip); }         //走る音を再生

    //巡回
    private List<Transform> patrolPoints;     // 巡回ポイントリスト
    private int currentPatrolPointIndex;      // 現在の巡回ポイントのインデックス
    private bool isPatrolling = false;      　// 巡回中かどうか
    private float walkSpeed = 1.0f;           // 巡回速度設定

    // Startで記録
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    //追跡
    public Transform player;                          //プレイヤーの位置
    private float distanceToPlayer = Mathf.Infinity;  // プレイヤーとの距離
    private float chaseRange = 7f;                    //Playerを検知する範囲

    // 距離に応じた速度を設定（距離が近いほど遅く、遠いほど速い）
    float minSpeed = 3.5f;  // 最低速度
    float maxSpeed = 6.0f;  // 最大速度

    //探す・聞く・何もしない
    private float idleSpeed = 0.0f; // 探す・聞く・何もしない時の速度設定
    private float searchTimer = 0f; // 探す状態を維持する時間

    //ラジオカセット
    public float detectionRange = 10f;   　// 音を聞き取れる範囲
    public Vector3 soundPosition;        　//ラジオカセットの置かれているポイント
    private bool isMovingToSound = false;  //ラジオカセットに反応して移動する

    #region ステートベースAI
    enum enemyState
    {
        back,      //戻る
        chase,     //追いかける
        search,    //探す
        hear,      //聞く
        near,      //近づく
        doNothing  //何もしない
    }

    enum BehaviorType
    {
        back,      //戻る
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

    public void Laugh()
    {
        // アニメーションイベントの処理（例えば笑う効果音を鳴らすなど）
        Debug.Log("Laugh animation event triggered");
    }

    // 初期化処理
    private void Start()
    {
        Debug.Log($"{gameObject.name} に ID {characterID} を割り当てました");

        // コンポーネントの取得
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        // PatrolPointManagerのインスタンスを取得
        patrolPointManager = FindObjectOfType<PatrolPointManager>();

        // そのIDに対応する巡回ポイントを取得
        patrolPoints = patrolPointManager.GetPatrolPoints(characterID);

        // 巡回ポイントが存在すれば巡回を開始
        if (patrolPoints != null && patrolPoints.Count >= 0)
        {
            isPatrolling = true;
            currentPatrolPointIndex = 0;
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);  // 最初の巡回ポイントに向かう
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] 巡回ポイントが取得できていません。characterID: {characterID}");
        }

        originalPosition = patrolPoints[currentPatrolPointIndex].transform.position;
        originalRotation =transform.rotation;

        // 行動リストの巡回の重要度を初期設定
        behaviors.GetBehavior(BehaviorType.search).value = 2;
    }

    private void Update()
    {
        // 状態遷移前の状態ログ（必要に応じて）
        Debug.Log($"[Update] Current State: {curretState}, DistanceToPlayer: {distanceToPlayer:F2}, IsPatrolling: {isPatrolling}, IsMovingToSound: {isMovingToSound}");

        //プレイヤーの位置を確認し、追跡・巡回を判断
        PatrolAndChaseAI();

        // ラジオカセットが置かれた時に反応し、状態偏移を行う
        PutOnRange();

        // 現在のステートに基づいた処理
        CurretStateStatus();
    }

    //プレイヤーの位置を確認し、追跡・巡回を判断
    public void PatrolAndChaseAI()
    {
        GameObject obj = GameObject.Find("Player");         //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();     //付いているスクリプト(PlayerSeen)を取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>(); //付いているスクリプト( ObjectPlacer)を取得

        Vector3 Position = player.position - transform.position;      // ターゲットの位置と自身の位置の差を計算
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;  // ターゲットが自身の前方にあるかどうか判定

        distanceToPlayer = Vector3.Distance(player.position, transform.position); // プレイヤーとの距離を計算

        // プレイヤーが前方にいるかつラジオが範囲内におかれていないかつ視界内にいる場合
        if (isFront && !isMovingToSound && PS.isVisible)
        {
            //追跡範囲内
            if (distanceToPlayer <= chaseRange)
            {
                behaviors.GetBehavior(BehaviorType.chase).value = 2; // プレイヤーを追跡する
            }
            //追跡範囲外
            else if (distanceToPlayer >= chaseRange)
            {
                behaviors.GetBehavior(BehaviorType.back).value = 2;// プレイヤーが範囲外の場合、巡回に戻る
            }
        }
    }

    // ラジオカセットが置かれた時に反応し、状態偏移を行う
    public void PutOnRange()
    {
        GameObject obj = GameObject.Find("Player");         //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();     //付いているスクリプト(PlayerSeen)を取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>(); //付いているスクリプト( ObjectPlacer)を取得

        // ラジオカセットの音に反応して移動する
        if (isMovingToSound && OP.isParticle)
        {
            isPatrolling = false;   // 目的地に向かって移動中は巡回停止

            // 目的地に近づいたら停止
            if (Vector3.Distance(this.transform.position, soundPosition) < 2.5f)
            {
                behaviors.GetBehavior(BehaviorType.hear).value = 3; // 音の元に到達
                isMovingToSound = false;                            // 移動停止
            }
            // 音源に向かっている途中で、一定距離まで接近
            else if (Vector3.Distance(this.transform.position, soundPosition) >= 2.5f && OP.isParticle)
            {
                behaviors.GetBehavior(BehaviorType.near).value = 3; // 音に近づいている
            }
        }
    }

    // 現在のステートに基づいた処理
    public void CurretStateStatus()
    {
        GameObject obj = GameObject.Find("Player");         //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();     //付いているスクリプト(PlayerSeen)を取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>(); //付いているスクリプト( ObjectPlacer)を取得

        switch (curretState)
        {
            case enemyState.doNothing: //何もしない
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.doNothing).value = 0;//何もしない行動を終了
                    behaviors.GetBehavior(BehaviorType.search).value = 2;   
                }

                behaviors.SortDesire();//行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search: // 探索行動
                            ChangeState(enemyState.search); // 探索状態に遷移
                            return;

                        case BehaviorType.chase: // 追跡行動
                            ChangeState(enemyState.chase); // 追跡状態に遷移
                            return;

                        case BehaviorType.back: // パトロール行動
                            ChangeState(enemyState.back); // パトロール状態に遷移
                            return;

                        case BehaviorType.hear: // 音を聞いたときの行動
                            ChangeState(enemyState.hear); // 音源に反応する状態に遷移
                            return;

                        case BehaviorType.near: // プレイヤーに接近されたときの行動
                            ChangeState(enemyState.near); // 接近状態に遷移
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.back: //巡回
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.back).value = 0; // 巡回行動を終了
                    PS.isVisualization = false;                           // プレイヤーの可視化をオフ
                    navMeshAgent.speed = walkSpeed;
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
                }

                Run();  // 走る音を再生

                // 到着処理
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    navMeshAgent.ResetPath(); // 停止
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, Time.deltaTime * 180f); // ゆっくり向きを戻す

                    if (Quaternion.Angle(transform.rotation, originalRotation) < 1f)
                    {
                        ChangeState(enemyState.search);
                    }
                }

                behaviors.SortDesire();// 行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search: // 探索行動
                            ChangeState(enemyState.search); // 探索状態に遷移
                            return;

                        case BehaviorType.chase: // 追跡行動
                            ChangeState(enemyState.chase); // 追跡状態に遷移
                            return;

                        case BehaviorType.back: // パトロール行動
                            ChangeState(enemyState.back); // パトロール状態に遷移
                            return;

                        case BehaviorType.hear: // 音を聞いたときの行動
                            ChangeState(enemyState.hear); // 音源に反応する状態に遷移
                            return;

                        case BehaviorType.near: // プレイヤーに接近されたときの行動
                            ChangeState(enemyState.near); // 接近状態に遷移
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
                    behaviors.GetBehavior(BehaviorType.search).value = 0; // 探索行動を終了
                    navMeshAgent.speed = idleSpeed;
                    animator.SetBool("Run", false);   // 走行アニメーションを停止
                    animator.SetBool("Idle", true);   // 待機アニメーションを開始

                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position); 
                    transform.rotation = originalRotation;      // 回転修正
                }

                Idle();// アイドル（探す）音を再生

                behaviors.SortDesire();// 行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
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
                        case BehaviorType.back:
                            ChangeState(enemyState.back);
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
            case enemyState.chase: //追いかける
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.chase).value = 0;// 追跡行動を終了

                    animator.SetBool("Run", true);    // 走行アニメーションを開始
                    animator.SetBool("Idle", false);  // 待機アニメーションを停止
                    navMeshAgent.speed = 0.0f;        // 初期速度設定
                }

                PS.isVisible = true;       // プレイヤーを可視化
                PS.isVisualization = true; // プレイヤーの可視化をオン

                Run();  // 走る音を再生

                transform.LookAt(player.transform); // プレイヤーに向かって回転
                navMeshAgent.SetDestination(player.transform.position); // プレイヤーに向かって移動

                float t = Mathf.Clamp01(distanceToPlayer / chaseRange); // 0〜1の範囲に正規化
                navMeshAgent.speed = Mathf.Lerp(minSpeed, maxSpeed, t); // 線形補間で速度を設定

                behaviors.SortDesire();// 行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search: // 探索行動
                            ChangeState(enemyState.search); // 探索状態に遷移
                            return;

                        case BehaviorType.chase: // 追跡行動
                            ChangeState(enemyState.chase); // 追跡状態に遷移
                            return;

                        case BehaviorType.back: // パトロール行動
                            ChangeState(enemyState.back); // パトロール状態に遷移
                            return;

                        case BehaviorType.hear: // 音を聞いたときの行動
                            ChangeState(enemyState.hear); // 音源に反応する状態に遷移
                            return;

                        case BehaviorType.near: // プレイヤーに接近されたときの行動
                            ChangeState(enemyState.near); // 接近状態に遷移
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.hear: //聞く
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.hear).value = 0;// 聞く行動を終了
                    animator.SetBool("Run", true);   // 走行アニメーションを停止
                    animator.SetBool("Idle", false);   // 待機アニメーションを開始
                }

                Idle();// アイドル（探す）音を再生

                // ラジオカセットの音に反応して移動する
                if (OP.isParticle)
                {
                    navMeshAgent.SetDestination(this.transform.position); // 音源に移動
                    navMeshAgent.speed = walkSpeed;                        // 移動速度設定
                }
                else if (!OP.isParticle)
                {
                    isMovingToSound = false;                                                     // 音源が消えたら移動停止
                    behaviors.GetBehavior(BehaviorType.back).value = 2;                          // 巡回に戻す
                }

                behaviors.SortDesire();//行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search: // 探索行動
                            ChangeState(enemyState.search); // 探索状態に遷移
                            return;

                        case BehaviorType.chase: // 追跡行動
                            ChangeState(enemyState.chase); // 追跡状態に遷移
                            return;

                        case BehaviorType.back: // パトロール行動
                            ChangeState(enemyState.back); // パトロール状態に遷移
                            return;

                        case BehaviorType.hear: // 音を聞いたときの行動
                            ChangeState(enemyState.hear); // 音源に反応する状態に遷移
                            return;

                        case BehaviorType.near: // プレイヤーに接近されたときの行動
                            ChangeState(enemyState.near); // 接近状態に遷移
                            return;
                    }
                }

                #endregion
                break;
            case enemyState.near: //音に近づく
                #region
                if (stateEnter)
                {
                    stateEnter = false;
                    behaviors.GetBehavior(BehaviorType.near).value = 0; // 音源に近づく行動を終了
                    PS.isVisualization = false; // プレイヤーの可視化をオフ
                }

                animator.SetBool("Run", true);  // 走行アニメーションを停止
                animator.SetBool("Idle", false); // 待機アニメーションを停止

                Run();  // 走る音を再生

                navMeshAgent.speed = walkSpeed;// 移動速度設定
                navMeshAgent.SetDestination(soundPosition); // 音源に向かって移動

                behaviors.SortDesire();//行動パターンをソート

                //行動リストの中で最も優先度の高い行動を選択
                //リストの一番上の1を上回ったら
                if (behaviors.behaviorList[0].value >= 1)
                {
                    Behavior behavior = behaviors.behaviorList[0];
                    switch (behavior.type)
                    {
                        case BehaviorType.search: // 探索行動
                            ChangeState(enemyState.search); // 探索状態に遷移
                            return;

                        case BehaviorType.chase: // 追跡行動
                            ChangeState(enemyState.chase); // 追跡状態に遷移
                            return;

                        case BehaviorType.back: // パトロール行動
                            ChangeState(enemyState.back); // パトロール状態に遷移
                            return;

                        case BehaviorType.hear: // 音を聞いたときの行動
                            ChangeState(enemyState.hear); // 音源に反応する状態に遷移
                            return;

                        case BehaviorType.near: // プレイヤーに接近されたときの行動
                            ChangeState(enemyState.near); // 接近状態に遷移
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
            isMovingToSound = true;     // 音に移動する
        }
    }

    // 指定されたAudioClipが再生されていない場合に再生するメソッド
    void PlayClipIfNotPlaying(AudioClip clip)
    {
        // 現在のクリップが指定されたものと異なる、または再生されていない場合に再生処理を行う
        if (audioSourse.clip != clip || !audioSourse.isPlaying)
        {
            audioSourse.clip = clip;        // 指定されたクリップをセット
            audioSourse.pitch = 1.0f;       // 再生速度を落とす（音程も下がる）
            audioSourse.Play();             // クリップを再生
        }
    }
}