using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// BossEnemyの制御（移動　アニメーション　サウンド）クラス
/// </summary>
public class BossEnemyController : MonoBehaviour
{
    // キャラクターのID（敵キャラクターを一意に識別するための番号）
    public int characterID = -1;

    // ナビメッシュエージェントの参照（NavMeshを使用した自動経路探索・移動処理に使用）
    NavMeshAgent navMeshAgent;

    // パトロールポイントマネージャーの参照（巡回ポイントの管理を担当）
    private PatrolPointManager patrolPointManager;

    // アニメーターの参照（アニメーション制御用）
    [SerializeField] Animator animator;

    // サウンド再生関連の変数群
    [SerializeField] private AudioSource audioSourse; // AudioSource（音を鳴らすためのコンポーネント）
    [SerializeField] private AudioClip searchClip;    // 探しているときの音声クリップ
    [SerializeField] private AudioClip walkClip;      //歩くときの音声クリップ


    // 探索時の音を再生する処理
    void Idle() { audioSourse.PlayOneShot(searchClip); }

    //歩行の音を再生する処理
    void Move() { PlayClipIfNotPlaying(walkClip); }       

 /// <summary>
    ///  巡回（パトロール）関連
    /// </summary>

    // 巡回ポイントの位置情報（Transform）を格納するリスト
    private List<Transform> patrolPoints;

    // 現在の巡回ポイントのインデックス（リスト内の何番目か）
    private int currentPatrolPointIndex;

    // 巡回中かどうかのフラグ
    private bool isPatrolling = false;      　

    // 巡回時の移動速度（通常の歩行速度）
    private float walkSpeed = 2.0f;

    // 巡回移動開始時刻
    private float patrolMoveStartTime;

    // ワープタイムアウト（秒）
    private float patrolTimeout = 60f;

    // 巡回開始からの経過時間
    float elapsed;

    /// <summary>
    /// プレイヤー追跡関連
    /// </summary>

    // プレイヤーのTransform参照（位置取得用）
    public Transform player;

    // プレイヤーとの現在の距離（毎フレーム更新）
    private float distanceToPlayer = Mathf.Infinity;

    // プレイヤーを検知できる最大距離（この範囲内に入ると追跡を開始）
    private float chaseRange = 10f;

    // 距離に応じて追跡スピードを変化させるための設定値
    float minSpeed = 5.5f;  // プレイヤーが近い時の最小速度
    float maxSpeed = 6.5f;  // プレイヤーが遠い時の最大速度

    // 待機・探索・聞き取り中などのときに使用する速度（基本的に停止状態）
    private float idleSpeed = 0.0f;

    // 探す状態を維持する時間
    private float searchTimer = 0f;

    /// <summary>
    /// ラジオカセットに反応する行動関連
    /// </summary>

    // ラジオカセットなどの音源を検知する範囲（この距離内の音に反応）
    public float detectionRange = 10f;

    // 音源（ラジオなど）の位置
    public Vector3 soundPosition;

    // 音に反応して移動中かどうかのフラグ
    private bool isMovingToSound = false;

    #region ステートベースAI
    enum enemyState
    {
        patrol,    // 巡回
        chase,     // プレイヤーを追いかける
        search,    // プレイヤーを探す
        hear,      // 音を聞く
        near,      // 音に反応して移動する
        doNothing  // 何もしない（待機状態）
    }

    enum BehaviorType
    {
        patrol,    // 巡回
        chase,     // プレイヤーを追いかける
        search,    // プレイヤーを探す
        hear,      // 音を聞く
        near,      // 音に反応して移動する
        doNothing  // 何もしない（待機状態）
    }

    // 各行動タイプと、その行動に対する「欲求値（優先度）」を持つクラス
    class Behavior
    {
        // 行動の種類（変更不可）
        public BehaviorType type { get; private set; }

        // 行動の優先度（数値が高いほど優先される）
        public float value;

        // コンストラクタ：行動タイプを指定して初期化
        public Behavior(BehaviorType _type)
        {
            type = _type;  // 行動タイプを設定
            value = 0f;    // 初期状態では欲求値は0
        }
    }

    // 敵の全行動候補（Behavior）を保持し、欲求値によって選択・管理するクラス
    class Behaviors
    {
        // 行動リスト（すべての行動候補を保持）
        public List<Behavior> behaviorList { get; private set; } = new List<Behavior>();

        // 指定された行動タイプのBehaviorインスタンスを取得する
        public Behavior GetBehavior(BehaviorType type)
        {
            // リストの中から該当するタイプを探す
            foreach (Behavior behaviour in behaviorList)
            {
                if (behaviour.type == type)
                {
                    return behaviour;  // 該当タイプのインスタンスを返す
                }
            }
            return null;  // 該当しない場合はnull
        }

        // 欲求値が高い順にリストを並び替える（降順）
        public void SortDesire()
        {
            behaviorList.Sort((behaviour1, behaviour2) => behaviour2.value.CompareTo(behaviour1.value));
            // ※昇順にしたい場合は behaviour1.value.CompareTo(behaviour2.value)
        }

        // コンストラクタ：すべてのBehaviorTypeに対応するBehaviorを初期化
        public Behaviors()
        {
            // 列挙型BehaviorTypeの総数を取得
            int BehaviorNum = System.Enum.GetNames(typeof(BehaviorType)).Length;

            // 各行動タイプごとにBehaviorインスタンスを生成し、リストに追加
            for (int i = 0; i < BehaviorNum; i++)
            {
                // 列挙値のインデックスをBehaviorTypeに変換
                BehaviorType type = (BehaviorType)System.Enum.ToObject(typeof(BehaviorType), i);

                // 対応するBehaviorインスタンスを作成
                Behavior newBehavior = new Behavior(type);

                // 行動リストに追加
                behaviorList.Add(newBehavior);
            }
        }
    }

    // Behaviorsクラスのインスタンス（行動候補を管理する実体）
    Behaviors behaviors = new Behaviors();

    // 現在の敵の状態（初期状態は「何もしない」）
    enemyState curretState = enemyState.doNothing;

    // ステートが切り替わった瞬間に一度だけ処理したいときに使用するフラグ
    bool stateEnter = true;

    // 敵のステート（状態）を切り替える処理
    void ChangeState(enemyState newEnemyState)
    {
        curretState = newEnemyState;  // 新しいステートを設定
        stateEnter = true;            // ステート移行直後の処理を許可
    }

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        // デバッグ用ログ：このGameObjectに対して割り当てられたcharacterIDを出力
        Debug.Log($"{gameObject.name} に ID {characterID} を割り当てました");

        // NavMeshAgentとAudioSourceコンポーネントを取得（移動や音声再生に使用）
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSourse = GetComponent<AudioSource>();

        // 巡回ポイントの管理クラス(PatrolPointManager)をシーン内から取得
        patrolPointManager = FindObjectOfType<PatrolPointManager>();

        // このキャラクターIDに対応する巡回ポイントのリストを取得
        patrolPoints = patrolPointManager.GetPatrolPoints(characterID);

        // 巡回ポイントが1つ以上存在する場合に巡回を開始
        if (patrolPoints != null && patrolPoints.Count >= 0)
        {
            currentPatrolPointIndex = 0;              // 最初の巡回ポイントを設定
            navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position); // 最初の巡回地点へ移動開始
        }
        else
        {
            // 巡回ポイントが見つからない場合、エラーを出力
            Debug.LogError($"[{gameObject.name}] 巡回ポイントが取得できていません。characterID: {characterID}");
        }

        // 行動管理システムで、「patrol（巡回）」行動の優先度を初期設定（重要度：2）
        behaviors.GetBehavior(BehaviorType.patrol).value = 2;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        // 現在のステートが「巡回復帰状態」で、目的地にまだ到達していない場合
        if (curretState == enemyState.patrol && !(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance))
        {
            // 経過時間を加算し、ワープタイムアウトを監視
            elapsed += Time.deltaTime;
            if (elapsed >= patrolTimeout)
            {
                // ワープ実行：指定の巡回ポイントへ瞬間移動
                navMeshAgent.Warp(patrolPoints[currentPatrolPointIndex].position);
                patrolMoveStartTime = Time.time;
            }
        }
        else
        {
            // タイマーリセット（正常に巡回中）
            elapsed = 0.0f;
        }

        // プレイヤーの位置を確認し、追跡すべきか巡回を続けるべきかを判断
        PatrolAndChaseAI();

        // ラジオカセットの音に反応する処理を実行
        PutOnRange();

        // 現在の行動ステート（AI状態）に応じた振る舞いを実行
        CurretStateStatus();

    }

    /// <summary>
    /// プレイヤーの位置に応じて追跡 or 巡回に切り替える処理
    /// </summary>
    public void PatrolAndChaseAI()
    {
        // プレイヤーオブジェクトを取得
        GameObject obj = GameObject.Find("Player");

        // プレイヤーの視認状態を管理するスクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // ラジオなどのオブジェクト設置状況を管理するスクリプトを取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>();

        // プレイヤーの方向ベクトル（プレイヤーと敵との位置差分）
        Vector3 Position = player.position - transform.position;

        // プレイヤーが敵の前方にいるかを判定（Dot積を使用）
        bool isFront = Vector3.Dot(Position, transform.forward) > 0;

        // プレイヤーと敵との距離を算出
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // プレイヤーが前方にいて、ラジオの音を聞いて移動しておらず、視界に見えている場合
        if (isFront && !isMovingToSound && PS.isVisible)
        {
            // プレイヤーが追跡範囲内にいる場合
            if (distanceToPlayer <= chaseRange)
            {
                // 「chase（追跡）」行動の重要度を高める（追跡開始）
                behaviors.GetBehavior(BehaviorType.chase).value = 2;
            }
            // プレイヤーが追跡範囲外にいる場合
            else if (distanceToPlayer >= chaseRange)
            {
                // 「patrol（巡回に戻る）」行動の重要度を高める（追跡中止）
                behaviors.GetBehavior(BehaviorType.patrol).value = 2;
                isPatrolling = true;// 巡回ポイントを移動を開始                                
            }
        }
        // 巡回中の処理
        else if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.5f)
        {
            behaviors.GetBehavior(BehaviorType.search).value = 2;   // 巡回ポイント到達後、周辺を探索する
        }
    }

    /// <summary>
    /// ラジオカセットが置かれた際に、音の発生位置へ向かう処理
    /// </summary>
    public void PutOnRange()
    {
        // プレイヤーオブジェクトを取得
        GameObject obj = GameObject.Find("Player");

        // プレイヤーの視認状態を管理するスクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // オブジェクト設置状況を管理するスクリプトを取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>();

        // ラジオの音に反応して移動中かつ、エフェクト（音源）が出ている場合
        if (isMovingToSound && OP.isParticle)
        {
            // 音源の位置にある程度近づいた場合
            if (Vector3.Distance(this.transform.position, soundPosition) < 7.0f)
            {
                // 「hear（音を聞いた）」行動の優先度を最大に設定（音の場所に到達）
                behaviors.GetBehavior(BehaviorType.hear).value = 2;
                isMovingToSound = false; // 音に対する移動を終了
            }
            // 音源に向かって移動している途中
            else if (Vector3.Distance(this.transform.position, soundPosition) >= 7.0f && OP.isParticle)
            {
                // 「near（音に近づいている）」行動の優先度を設定
                behaviors.GetBehavior(BehaviorType.near).value = 2;
            }
        }
    }

    /// <summary>
    /// 現在のステートに基づいた処理を実行するメソッド。
    /// 敵の状態（待機・巡回・探索・追跡など）に応じて、それぞれの行動を定義。
    /// </summary>
    public void CurretStateStatus()
    {
        // プレイヤーオブジェクトを取得
        GameObject obj = GameObject.Find("Player");

        // プレイヤーの視認状態を管理するスクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // オブジェクト設置状況を管理するスクリプトを取得
        ObjectPlacer OP = obj.GetComponent<ObjectPlacer>();

        switch (curretState)
        {
            case enemyState.doNothing: //何もしない
                #region
                if (stateEnter)
                {
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる

                    // 現在の行動（何もしない）を終了させる
                    behaviors.GetBehavior(BehaviorType.doNothing).value = 0;

                    // 次の行動候補として巡回の優先度を上げる
                    behaviors.GetBehavior(BehaviorType.patrol).value = 2;
                }

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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
            case enemyState.patrol: //巡回
                #region
                if (stateEnter)
                {
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる

                    // 巡回行動の優先度をリセット
                    behaviors.GetBehavior(BehaviorType.patrol).value = 0;

                    // 可視化表示をオフ（プレイヤー視認不可）
                    PS.isVisualization = false;

                    animator.SetBool("Move", true);     // 歩行アニメーションを開始
                    animator.SetBool("Idle", false);    // 待機アニメーションを停止

                    patrolMoveStartTime = Time.time; // 巡回開始時刻を記録
                }

                Move(); // 歩く音を再生

                // 巡回中であればナビメッシュで目的地に向かう
                if (isPatrolling)
                {
                    navMeshAgent.speed = walkSpeed; // 巡回用の速度に設定

                    // 現在の巡回ポイントへ移動
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

                    // 巡回ポイントに到着したら次のポイントへ
                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 1.5f)
                    {
                        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
                    }
                }

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる

                    // 探索行動を終了
                    behaviors.GetBehavior(BehaviorType.search).value = 0;

                    navMeshAgent.speed = idleSpeed; // 探索時は移動しない or 非アクティブ速度

                    animator.SetBool("Move", false);   // 走行アニメーションを停止
                    animator.SetBool("Idle", true);   // 待機アニメーションを開始
                }

                Idle(); // 待機音・探す動作の音などを再生

                // 移動を停止（現在位置に留まる）
                navMeshAgent.SetDestination(this.transform.position);

                // 2.5秒経過後に巡回に戻る
                searchTimer += Time.deltaTime;
                if (searchTimer >= 2.5f)
                {
                    // 探す状態に入ったらタイマーをリセット
                    searchTimer = 0f;

                    // 巡回行動の優先度を上げる
                    behaviors.GetBehavior(BehaviorType.patrol).value = 2;
                    isPatrolling = true;

                    // 可視化をオフ
                    PS.isVisualization = false;
                }

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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
            case enemyState.chase: //追いかける
                #region
                if (stateEnter)
                {
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる

                    // 追跡行動を終了
                    behaviors.GetBehavior(BehaviorType.chase).value = 0;

                    animator.SetBool("Move", true);    // 走行アニメーションを開始
                    animator.SetBool("Idle", false);  // 待機アニメーションを停止
                }

                PS.isVisible = true;       // プレイヤーを可視化
                PS.isVisualization = true; // プレイヤーの可視化をオン

                float distance = Vector3.Distance(transform.position, player.transform.position); // プレイヤーとの距離を取得
                float stopDistance = 4.0f; // 追いかけるのを止める距離（これ以下には近づかない）

                if (distance > stopDistance)
                {
                    transform.LookAt(player.transform); // プレイヤーに向かって回転
                    navMeshAgent.SetDestination(player.transform.position); // プレイヤーに向かって移動
                }
                else
                {
                    navMeshAgent.SetDestination(transform.position); // 一定距離内ならその場で停止
                    navMeshAgent.speed = 0f;
                }

                float t = Mathf.Clamp01(distance / chaseRange); // 0〜1の範囲に正規化
                navMeshAgent.speed = Mathf.Lerp(minSpeed, maxSpeed, t); // 線形補間で速度を設定

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる
                    // 聞く行動を終了
                    behaviors.GetBehavior(BehaviorType.hear).value = 0;

                    animator.SetBool("Move", false);   // 走行アニメーションを停止
                    animator.SetBool("Idle", true);   // 待機アニメーションを開始
                }

                Idle();// アイドル（探す）音を再生

                if (OP.isParticle) // 音のエフェクトが出ている（ラジオなどが鳴っている）場合
                {
                    transform.LookAt(soundPosition);
                    navMeshAgent.SetDestination(this.transform.position); // 現在位置に留まる
                    navMeshAgent.speed = walkSpeed; // 歩行速度設定
                }
                else // 音が止まった場合
                {
                    isMovingToSound = false; // 音源への移動をキャンセル
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position); // 巡回ポイントへ戻る
                    behaviors.GetBehavior(BehaviorType.patrol).value = 2; // 巡回へ戻す
                }

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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

                animator.SetBool("Walk", true);  // 歩行アニメーションを開始
                animator.SetBool("Run", false);  // 走行アニメーションを停止
                animator.SetBool("Idle", false); // 待機アニメーションを停止

                navMeshAgent.speed = walkSpeed;// 移動速度設定
                navMeshAgent.SetDestination(soundPosition); // 音源に向かって移動

                Move(); // 歩く音を再生

                behaviors.SortDesire();//行動パターンをソート

                // 行動リストを優先度順にソート
                behaviors.SortDesire();

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

                        case BehaviorType.patrol: // パトロール行動
                            ChangeState(enemyState.patrol); // パトロール状態に遷移
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

    /// <summary>
    /// 音に反応したときに呼ばれる
    /// </summary>
    public void OnSoundHeard(Vector3 position)
    {
        // 範囲内の場合のみ音に反応
        if (Vector3.Distance(transform.position, position) <= detectionRange)
        {
            soundPosition = position;   // 音の位置を保存
            isMovingToSound = true;     // 音に移動する
        }
    }

    /// <summary>
    /// 指定されたAudioClipが再生されていない場合に再生するメソッド
    /// </summary>
    void PlayClipIfNotPlaying(AudioClip clip)
    {
        // 現在のクリップが指定されたものと異なる、または再生されていない場合に再生処理を行う
        if (audioSourse.clip != clip || !audioSourse.isPlaying)
        {
            audioSourse.clip = clip;        // 指定されたクリップをセット
            audioSourse.pitch = 0.7f;       // 再生速度を落とす（音程も下がる）
            audioSourse.Play();             // クリップを再生
        }
    }

    /// <summary>
    /// サウンドエフェクトを発生させる処理。
    /// 主に「プレイヤーを一時的に可視化する」演出に使われる。
    /// 音量や距離減衰などのAudio設定も一時的に変更される。
    /// </summary>
    public void ShowSoundEffect()
    {
        // シーン内から "Player" という名前のGameObjectを探す
        GameObject obj = GameObject.Find("Player");

        // Playerオブジェクトにアタッチされている PlayerSeen スクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // 行動AIのうち「search（探索）」行動の優先度を3に設定（強制的に探索を促す）
        behaviors.GetBehavior(BehaviorType.search).value = 3;

        // AudioSourceの設定を変更：音が遠くまで届くように調整
        audioSourse.maxDistance = 500f;                          // 音が聞こえる最大距離を大きくする
        audioSourse.volume = 0.05f;                              // 音量を一時的に下げる（環境音的に）
        audioSourse.rolloffMode = AudioRolloffMode.Linear;       // 音量の減衰をリニア方式に設定

        // プレイヤーを見える状態に設定する（例：シルエット表示などの演出）
        PS.isVisible = true;       // 敵AIにとって「見えている状態」にする
        PS.isVisualization = true; // 表示的に「可視化中」の状態（ビジュアル用フラグ）

        // 可視化を10秒後に自動でオフにするコルーチンを開始
        StartCoroutine(ResetVisibility(PS));
    }

    /// <summary>
    /// サウンドエフェクトを発生させる処理。
    /// 主に「プレイヤーを一時的に可視化する」演出に使われる。
    /// 音量や距離減衰などのAudio設定も一時的に変更される。
    /// </summary>
    public void SkipShowSoundEffect()
    {
        // シーン内から "Player" という名前のGameObjectを探す
        GameObject obj = GameObject.Find("Player");

        // Playerオブジェクトにアタッチされている PlayerSeen スクリプトを取得
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();

        // 行動AIのうち「search（探索）」行動の優先度を3に設定（強制的に探索を促す）
        behaviors.GetBehavior(BehaviorType.search).value = 3;

        // AudioSourceの設定を変更：音が遠くまで届くように調整
        audioSourse.maxDistance = 500f;                          // 音が聞こえる最大距離を大きくする
        audioSourse.volume = 0.05f;                              // 音量を一時的に下げる（環境音的に）
        audioSourse.rolloffMode = AudioRolloffMode.Linear;       // 音量の減衰をリニア方式に設定

        // プレイヤーを見える状態に設定する（例：シルエット表示などの演出）
        PS.isVisible = true;       // 敵AIにとって「見えている状態」にする
        PS.isVisualization = true; // 表示的に「可視化中」の状態（ビジュアル用フラグ）

        // 可視化を10秒後に自動でオフにするコルーチンを開始
        StartCoroutine(ResetVisibility(PS));
    }

    /// <summary>
    /// 一時的に可視化されたプレイヤー状態を10秒後に元に戻すコルーチン。
    /// 音量や音の届く距離も元に戻す。
    /// </summary>
    private IEnumerator ResetVisibility(PlayerSeen ps)
    {
        // 10秒間待機
        yield return new WaitForSeconds(10f);

        // 音が聞こえる範囲を通常の距離（短距離）に戻す
        audioSourse.maxDistance = 20f;

        // 音量を元に戻す（通常の大きさ）
        audioSourse.volume = 1.0f;
    }
}
