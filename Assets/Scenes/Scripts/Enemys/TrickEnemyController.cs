using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// TrickEnemyの制御（移動　アニメーション　サウンド）クラス
/// </summary>
public class TrickEnemyController : MonoBehaviour
{
    // キャラクターのID（敵キャラクターを一意に識別するための番号）
    public int characterID = -1;

    // ナビメッシュエージェントの参照（NavMeshを使用した自動経路探索・移動処理に使用）
    NavMeshAgent navMeshAgent;

    // パトロールポイントマネージャーの参照（巡回ポイントの管理を担当）
    private PatrolPointManager patrolPointManager;

    // 「待機 → 笑う」動作を制御するコルーチンの参照（多重実行を防止）
    private Coroutine idleLaughCoroutine = null;

    // アニメーターの参照（アニメーション制御用）
    [SerializeField] Animator animator;

    // サウンド再生関連の変数群
    [SerializeField] private AudioSource audioSourse;  // AudioSource（音を鳴らすためのコンポーネント）
    [SerializeField] private AudioClip searchClip;     // 探しているときの音声クリップ
    [SerializeField] private AudioClip laughClip;      // 笑い声の音声クリップ
    [SerializeField] private AudioClip runClip;        // 走っているときの足音クリップ

    // 探索時の音を再生する処理
    void Idle() { PlayClipIfNotPlaying(searchClip); }

    // 笑い声を再生する処理
    void Laugh() { PlayClipIfNotPlaying(laughClip); }

    // 走行時の音を再生する処理
    void Run() { PlayClipIfNotPlaying(runClip); }

    /// <summary>
    ///  巡回（パトロール）関連
    /// </summary>

    // 巡回ポイントの位置情報（Transform）を格納するリスト
    private List<Transform> patrolPoints;

    // 現在の巡回ポイントのインデックス（リスト内の何番目か）
    private int currentPatrolPointIndex;

    // 通常の歩行速度
    private float walkSpeed = 1.0f;

    // Start時に記録しておく初期位置と回転（復帰用）
    private Vector3 originalPosition;
    private Quaternion originalRotation;

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
    float minSpeed = 2.0f;  // プレイヤーが近い時の最小速度
    float maxSpeed = 8.0f;  // プレイヤーが遠い時の最大速度

    // 待機・探索・聞き取り中などのときに使用する速度（基本的に停止状態）
    private float idleSpeed = 0.0f;

    /// <summary>
    /// ラジオカセットに反応する行動関連
    /// </summary>

    // ラジオカセットなどの音源を検知する範囲（この距離内の音に反応）
    public float detectionRange = 10f;

    // 音源（ラジオなど）の位置
    public Vector3 soundPosition;

    // 音に反応して移動中かどうかのフラグ
    private bool isMovingToSound = false;

    #region ステートベースAI（敵の行動状態を管理するAIの仕組み）

    // 敵の現在の行動状態を表す列挙型（ステート）
    enum enemyState
    {
        back,      // 元の位置へ戻る
        chase,     // プレイヤーを追いかける
        search,    // プレイヤーを探す
        hear,      // 音を聞く
        near,      // 音に反応して移動する
        doNothing  // 何もしない（待機状態）
    }

    // 行動の種類を表す列挙型（行動の候補や種類として利用）
    enum BehaviorType
    {
        back,      // 元の位置へ戻る
        chase,     // プレイヤーを追いかける
        search,    // プレイヤーを探す
        hear,      // 音を聞く
        near,      // 音に反応して移動する
        doNothing  // 何もしない（待機行動）
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

        // 初期位置と初期回転を記録（戻る処理などに使える）
        originalPosition = patrolPoints[currentPatrolPointIndex].transform.position;
        originalRotation = transform.rotation;

        // 行動管理システムで、「search（探索）」行動の優先度を初期設定（重要度：2）
        behaviors.GetBehavior(BehaviorType.search).value = 2;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        // プレイヤーの位置を確認し、追跡すべきか巡回を続けるべきかを判断
        PatrolAndChaseAI();

        // ラジオカセットの音に反応する処理を実行
        PutOnRange();

        // 現在の行動ステート（AI状態）に応じた振る舞いを実行
        CurretStateStatus();

        // カプセルコライダー（当たり判定）のON/OFFをチェックし更新
        UpdateCapsuleCollider();
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
                // 「back（巡回に戻る）」行動の重要度を高める（追跡中止）
                behaviors.GetBehavior(BehaviorType.back).value = 2;
            }
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
            if (Vector3.Distance(this.transform.position, soundPosition) < 2.5f)
            {
                // 「hear（音を聞いた）」行動の優先度を最大に設定（音の場所に到達）
                behaviors.GetBehavior(BehaviorType.hear).value = 3;
                isMovingToSound = false; // 音に対する移動を終了
            }
            // 音源に向かって移動している途中
            else if (Vector3.Distance(this.transform.position, soundPosition) >= 2.5f && OP.isParticle)
            {
                // 「near（音に近づいている）」行動の優先度を設定
                behaviors.GetBehavior(BehaviorType.near).value = 3;
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

                    // 次の行動候補として探索の優先度を上げる
                    behaviors.GetBehavior(BehaviorType.search).value = 2;   
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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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
            case enemyState.back: //位置へ戻る
                #region
                if (stateEnter)
                {
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる

                    // 位置へ戻るときの行動の優先度をリセット
                    behaviors.GetBehavior(BehaviorType.back).value = 0;

                    // 可視化表示をオフ（プレイヤー視認不可）
                    PS.isVisualization = false;

                    // 位置へ戻るの速度に設定
                    navMeshAgent.speed = walkSpeed;

                    // 元の位置へ戻るの巡回ポイントへ移動
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
                }

                Run();  // 走行サウンド再生

                // 到着処理
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {
                    // 回転を徐々に元の向きに戻す
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, Time.deltaTime * 180f); // ゆっくり向きを戻す

                    // 元の位置に到着したら探索行動へ
                    if (Quaternion.Angle(transform.rotation, originalRotation) < 3.0f)
                    {
                        ChangeState(enemyState.search);
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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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

                    // 探索行動を終了（優先度をリセット）
                    behaviors.GetBehavior(BehaviorType.search).value = 0;

                    // 移動速度を待機モードに設定
                    navMeshAgent.speed = idleSpeed;

                    // アニメーション設定：走行をオフ、待機をオン
                    animator.SetBool("Run", false);
                    animator.SetBool("Idle", true);

                    // 現在の巡回ポイントへ移動
                    navMeshAgent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

                    // 向きを元に戻す
                    transform.rotation = originalRotation;
                }

                IdleThenLaugh(); // 待機中に笑う動作などの演出

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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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

                    behaviors.GetBehavior(BehaviorType.chase).value = 0; // 追跡行動を終了

                    animator.SetBool("Run", true);  // 走行アニメーション開始
                    animator.SetBool("Idle", false); // 待機アニメーション停止

                    navMeshAgent.speed = 0.0f; // 初期速度をゼロに設定（徐々に上げるため）
                }

                PS.isVisible = true;       // プレイヤーが見えるように設定
                PS.isVisualization = true; // 可視化処理をオン（視認されている演出）

                // カプセルコライダー（当たり判定）のON/OFFをチェックし更新
                UpdateCapsuleCollider();

                Run();  // 走る音を再生

                transform.LookAt(player.transform); // プレイヤーの方向を向く
                navMeshAgent.SetDestination(player.transform.position); // プレイヤーへ向かって移動

                // プレイヤーとの距離に応じて速度を補間設定（近いほど速く）
                float t = Mathf.Clamp01(distanceToPlayer / chaseRange);
                navMeshAgent.speed = Mathf.Lerp(minSpeed, maxSpeed, t);

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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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
                    behaviors.GetBehavior(BehaviorType.hear).value = 0;// 聞く行動を終了
                    animator.SetBool("Run", true);   // 走行アニメーションを停止
                    animator.SetBool("Idle", false);   // 待機アニメーションを開始
                }

                IdleThenLaugh();

                // ラジオカセットの音に反応して移動する
                if (OP.isParticle)
                {
                    navMeshAgent.SetDestination(this.transform.position); // 音源に移動
                    navMeshAgent.speed = walkSpeed;                        // 移動速度設定
                }
                else if (!OP.isParticle)
                {
                    isMovingToSound = false;                           　// 音源が消えたら移動停止
                    behaviors.GetBehavior(BehaviorType.back).value = 2; // 巡回に戻す
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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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
                    stateEnter = false; // 初回遷移処理が完了したのでフラグを下げる
                    behaviors.GetBehavior(BehaviorType.near).value = 0; // 音源に近づく行動を終了
                    PS.isVisualization = false; // プレイヤーの可視化をオフ
                }

                animator.SetBool("Run", true);  // 走行アニメーションを停止
                animator.SetBool("Idle", false); // 待機アニメーションを停止

                Run();  // 走る音を再生

                navMeshAgent.speed = walkSpeed;             // 通常の歩行速度に設定
                navMeshAgent.SetDestination(soundPosition); // 音源に向かって移動

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

                        case BehaviorType.back: //元の位置へ戻るときの行動
                            ChangeState(enemyState.back); // 位置へ戻る状態に遷移
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
            audioSourse.pitch = 1.0f;       // 再生速度を落とす（音程も下がる）
            audioSourse.Play();             // クリップを再生
        }
    }

    /// <summary>
    /// カプセルコライダーの状態を更新する関数
    /// </summary>
    void UpdateCapsuleCollider()
    {
        // このゲームオブジェクトにアタッチされているCapsuleColliderを取得
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();

        // カプセルコライダーが存在する場合のみ処理を実行
        if (capsule != null)
        {
            // 現在の敵の状態が「追跡（chase）」であるかを判定し、
            // その状態に応じてカプセルコライダーの有効/無効を切り替える
            capsule.enabled = (curretState == enemyState.chase);
        }
    }

    /// <summary>
    /// 「待機 → 笑う」処理の開始関数
    /// </summary>
    void IdleThenLaugh()
    {
        // まだコルーチンが開始されていない場合のみ開始する（重複防止）
        if (idleLaughCoroutine == null)
        {
            // コルーチンを開始し、参照を保持
            idleLaughCoroutine = StartCoroutine(PlayIdleThenLaughCoroutine());
        }
    }

    /// <summary>
    /// 「待機 → 一定時間後に笑う」動作を実行するコルーチン
    /// </summary>
    IEnumerator PlayIdleThenLaughCoroutine()
    {
        // 待機中の音（探している音）を再生する関数
        Idle();

        // 探索音の再生時間だけ待機
        yield return new WaitForSeconds(searchClip.length);

        // 笑い声を再生する関数
        Laugh();

        // コルーチン終了後、参照をクリアして再実行できるようにする
        idleLaughCoroutine = null;
    }

}