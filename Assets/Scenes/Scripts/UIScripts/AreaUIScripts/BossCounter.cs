using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ボス戦に関連するステージ演出・UI更新・不気味演出を管理するクラス。
/// ステージ進行に応じてUIを切り替え、最終段階で特殊演出やオーバーレイ演出を実行する。
/// </summary>
public class BossCounter : MonoBehaviour
{
    // === UI・演出素材関連 ===
    public GameObject[] uiStages; // 各ステージごとに用意されたUIオブジェクト（順にアクティブ化）
    [SerializeField] private GameObject screenEerieOverlay; // 最終ステージで表示される不気味な演出用オーバーレイ
    [SerializeField] private AudioClip stageChangeClip; // ステージ切り替え時に再生するSE
    private AudioSource audioSource; // SE再生用のAudioSource

    // === ステージ進行管理 ===
    private int currentStage = 0; // 現在のステージ番号（配列indexとしても使う）
    private float timer = 0f; // 経過時間を測るタイマー
    private float stageDuration = 120f; // 各ステージの持続時間（秒単位）
    private float resetDelay = 60f; // 最終ステージ演出後にリセットするまでの時間
    private bool isFinalStage = false; // 現在が最終ステージかどうかのフラグ

    // === カメラ演出関連 ===
    [Header("演出カメラ")]
    [SerializeField] private Camera mainCamera; // 通常プレイ用カメラ
    [SerializeField] private Camera eventCamera; // イベントシーン用の演出カメラ
    [SerializeField] private Transform eventCameraStartPoint; // 通常演出の開始位置
    [SerializeField] private Transform eventCameraEndPoint; // 通常演出の終了位置
    [SerializeField] private Transform frontEventCameraStartPoint; // 正面（壁が後ろにある場合）演出の開始位置
    [SerializeField] private Transform frontEventCameraEndPoint; // 正面演出の終了位置

    private float wallCheckDistance = 10f; // 壁との距離をチェックするための射程
    private float minWallDistance = 2f; // 追加：壁が近すぎると判定する最短距離

    // === カメラ演出設定 ===
    private float cameraMoveDuration = 5f; // カメラ演出の移動時間
    private float eventDuration = 5f; // 演出後に静止する待機時間

    // === プレイヤー・状態管理 ===
    [SerializeField] private GameObject player; // プレイヤーオブジェクトへの参照

    private bool isEventPlaying = false; // カメラ演出中の制御フラグ
    private bool isEerieEffectActive = false; // 不気味演出がアクティブかどうかのフラグ

    private Vector3 mainCameraOriginalPosition; // 演出終了後にメインカメラを戻すための位置保存
    private Quaternion mainCameraOriginalRotation; // 同じく回転の保存

    void Start()
    {
        // AudioSourceの取得（SE再生用）
        audioSource = GetComponent<AudioSource>();

        Debug.Log("[Start] 初期化: currentStage=0");

        // 最初のステージUIのみを表示
        UpdateUIStage();

        // 不気味オーバーレイは最初は非表示にしておく
        if (screenEerieOverlay != null)
            screenEerieOverlay.SetActive(false);
    }

    void Update()
    {
        // イベント演出中は通常の進行を停止
        if (isEventPlaying) return;

        // 経過時間をカウント
        timer += Time.deltaTime;

        // 通常ステージ進行：一定時間ごとに次のステージに移行
        if (!isFinalStage && timer >= stageDuration)
        {
            timer = 0f;
            currentStage++;

            // 最終ステージに到達したかチェック
            if (currentStage >= uiStages.Length - 1)
            {
                currentStage = uiStages.Length - 1;
                isFinalStage = true;

                // 最終ステージ用UIを表示
                UpdateUIStage();

                // イベント未実行なら演出コルーチンを開始
                if (!isEventPlaying)
                {
                    isEventPlaying = true;
                    StartCoroutine(PlayFinalStageEvent());
                }
            }
            else
            {
                // 通常ステージのUI更新
                UpdateUIStage();
            }
        }

        // 不気味演出のオーバーレイ表示更新（フラグに応じて）
        if (screenEerieOverlay != null)
        {
            screenEerieOverlay.SetActive(isEerieEffectActive);
        }
    }

    /// <summary>
    /// 現在のステージUIのみ表示し、他は全て非表示にする。
    /// さらに不気味演出オーバーレイやSEの処理も制御。
    /// </summary>
    void UpdateUIStage()
    {
        // UIステージ全体をループして、現在のステージだけ表示
        for (int i = 0; i < uiStages.Length; i++)
        {
            uiStages[i].SetActive(i == currentStage);
        }

        // 不気味演出が有効でないならオーバーレイは非表示
        if (screenEerieOverlay != null && !isEerieEffectActive)
        {
            screenEerieOverlay.SetActive(false);
        }

        // ステージ変更SEを再生
        if (stageChangeClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageChangeClip);
        }
    }

    /// <summary>
    /// 最終ステージに到達したときに再生されるイベント演出。
    /// 壁の有無によって演出の可否を判断し、適切なパターンを再生。
    /// </summary>
    IEnumerator PlayFinalStageEvent()
    {
        Debug.Log("[PlayFinalStageEvent] 演出開始");

        // プレイヤーの近くにボスをワープさせる関数
        WarpBossNearPlayer();

        // プレイヤー動作停止のために時間を止める（イベント演出中はゲームが一時停止）
        Time.timeScale = 0f;

        // プレイヤーの向きと位置から前後の壁をRayで検出
        Vector3 pos = player.transform.position;
        Vector3 forward = player.transform.forward;
        Vector3 back = -forward;

        // 壁検出＋距離測定
        bool hasWallBehind = Physics.Raycast(pos, back, out RaycastHit backHit, wallCheckDistance);
        bool hasWallInFront = Physics.Raycast(pos, forward, out RaycastHit frontHit, wallCheckDistance);

        // 壁が近すぎる場合の判定を追加
        bool wallTooClose = (hasWallBehind && backHit.distance <= minWallDistance) ||
                            (hasWallInFront && frontHit.distance <= minWallDistance);

        // ボスを取得
        GameObject boss = GameObject.Find("BossEnemy");

        // 両方向とも壁、または壁が近すぎる場合は演出スキップ
        if ((hasWallBehind && hasWallInFront) || wallTooClose)
        {
            Debug.Log("[PlayFinalStageEvent] 前後に壁、または壁が近すぎる → 演出スキップ");

            isEerieEffectActive = true;
            screenEerieOverlay?.SetActive(true);

            //// ボスの一時停止処理をコルーチンで呼び出す
            //if (boss != null)
            //{
            //    StartCoroutine(TemporarilyStopBoss(boss, 3f)); // ここで3秒停止
            //}

            // ボス側にSEやエフェクトのトリガーを送る（演出強化）
            boss?.GetComponent<BossEnemyController>()?.ShowSoundEffect();

            Time.timeScale = 1f;
            StartCoroutine(ResetAfterDelay());
            isEventPlaying = false;
            yield break;
        }

        // 使用するカメラ演出開始点と終了点を決定（背面が塞がれている場合は正面パターン）
        Transform startPoint = hasWallBehind ? frontEventCameraStartPoint : eventCameraStartPoint;
        Transform endPoint = hasWallBehind ? frontEventCameraEndPoint : eventCameraEndPoint;

        // メインカメラの位置・回転を保存しておく（演出終了後に復元）
        mainCameraOriginalPosition = mainCamera.transform.position;
        mainCameraOriginalRotation = mainCamera.transform.rotation;

        // イベントカメラの位置・角度を初期化
        eventCamera.transform.position = startPoint.position;
        eventCamera.transform.rotation = startPoint.rotation;

        // カメラ切り替え：演出用カメラON、メインカメラOFF
        mainCamera.enabled = false;
        eventCamera.enabled = true;

        // ボス側にSEやエフェクトのトリガーを送る（演出強化）
        boss?.GetComponent<BossEnemyController>()?.ShowSoundEffect();

        // カメラを一定時間かけて移動させる（滑らかな演出）
        float elapsed = 0f;
        while (elapsed < cameraMoveDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / cameraMoveDuration);

            // 線形補間と球面線形補間で位置と回転を更新
            eventCamera.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            eventCamera.transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // 終点を念のため再設定して精度保証
        eventCamera.transform.position = endPoint.position;
        eventCamera.transform.rotation = endPoint.rotation;

        Debug.Log("[PlayFinalStageEvent] カメラ演出終了、待機");

        // イベントの余韻を数秒間表示したままにする
        yield return new WaitForSecondsRealtime(eventDuration);

        // カメラを元の状態に戻す
        eventCamera.enabled = false;
        mainCamera.enabled = true;
        mainCamera.transform.position = mainCameraOriginalPosition;
        mainCamera.transform.rotation = mainCameraOriginalRotation;

        // ゲームを再開
        Time.timeScale = 1f;

        Debug.Log("[PlayFinalStageEvent] 不気味演出へ移行");

        // 不気味なオーバーレイ表示を有効化
        isEerieEffectActive = true;
        screenEerieOverlay?.SetActive(true);

        // 一定時間後にステージをリセットするコルーチン開始
        StartCoroutine(ResetAfterDelay());

        isEventPlaying = false;
    }

    /// <summary>
    /// 最終ステージの演出後、一定時間待ってステージを初期化する処理。
    /// </summary>
    IEnumerator ResetAfterDelay()
    {
        Debug.Log("[ResetAfterDelay] 待機中...");
        yield return new WaitForSeconds(resetDelay);

        // ステージと状態をリセット
        currentStage = 0;
        isFinalStage = false;
        timer = 0f;

        Debug.Log("[ResetAfterDelay] 初期化完了 → currentStage=0");

        // 不気味演出のオーバーレイ非表示
        isEerieEffectActive = false;
        screenEerieOverlay?.SetActive(false);

        // UIもリセットして最初の状態に戻す
        UpdateUIStage();
    }

    /// <summary>
    /// プレイヤーの近くにボスをワープさせる関数
    /// </summary>
    void WarpBossNearPlayer()
    {
        // "BossEnemy"という名前のGameObjectをシーンから探す
        GameObject boss = GameObject.Find("BossEnemy");

        // ボスまたはプレイヤーが存在しない場合は処理を中断
        if (boss == null || player == null) return;

        // ボスのNavMeshAgentコンポーネントを取得
        NavMeshAgent agent = boss.GetComponent<NavMeshAgent>();

        // NavMeshAgentが存在しない場合は警告を出して処理を中断
        if (agent == null)
        {
            Debug.LogWarning("BossにNavMeshAgentがアタッチされていません。");
            return;
        }

        // プレイヤーの現在位置
        Vector3 playerPos = player.transform.position;

        // プレイヤーの前方ベクトル（向いている方向）
        Vector3 forward = player.transform.forward;

        // プレイヤーの右方向ベクトル
        Vector3 right = player.transform.right;

        // プレイヤーの前方に壁があるかをRaycastでチェック
        bool hasWallFront = Physics.Raycast(playerPos, forward, wallCheckDistance);

        // プレイヤーの背後に壁があるかをRaycastでチェック
        bool hasWallBack = Physics.Raycast(playerPos, -forward, wallCheckDistance);

        Vector3 direction; // ボスをワープさせる方向を決定する変数

        // 前方と背後の両方に壁がある場合
        if (hasWallFront && hasWallBack)
        {
            // 前後に壁があるため、プレイヤーの横方向（ここでは右）にワープさせる
            direction = right.normalized;
        }
        // 背後にのみ壁がある場合
        else if (hasWallBack)
        {
            // 背後がふさがっているため、前方にワープ
            direction = forward.normalized;
        }
        // 前方にのみ壁がある場合
        else if (hasWallFront)
        {
            // 前方がふさがっているため、背後にワープ
            direction = -forward.normalized;
        }
        // 前後に壁がない場合
        else
        {
            // 安全のため、背後にワープさせる
            direction = -forward.normalized;
        }

        // ワープさせたい目標位置をプレイヤー位置 + 方向 * 8m で算出
        Vector3 desiredPosition = playerPos + direction * 8f;

        NavMeshHit hit; // 実際にワープさせるNavMesh上の位置を格納する変数

        // 指定位置の周囲半径5m以内でNavMesh上の適切な地点を探す
        if (NavMesh.SamplePosition(desiredPosition, out hit, 5f, NavMesh.AllAreas))
        {
            // NavMeshAgentを探した地点にワープさせる
            agent.Warp(hit.position);

            // ボスのy座標が変わってしまう場合に備え、元のyを維持（高さを変更しない）
            Vector3 pos = agent.transform.position;
            pos.y = boss.transform.position.y;
            agent.transform.position = pos;

            // ワープ後、ボスをプレイヤーの方向に向ける
            agent.transform.LookAt(playerPos);
        }
        else
        {
            // NavMesh上に適切な地点が見つからなかった場合は警告を出力
            Debug.LogWarning("適切なワープ先が見つかりませんでした。");
        }
    }

    /// <summary>
    /// ボスの動きを一時的に止め、一定時間後に再開するコルーチン。
    /// </summary>
    IEnumerator TemporarilyStopBoss(GameObject boss, float stopDuration)
    {
        // ボスにアタッチされているNavMeshAgentコンポーネントを取得（移動停止用）
        var nav = boss.GetComponent<NavMeshAgent>();

        // ボスにアタッチされているAI制御スクリプト（例: 独自の行動制御）の参照を取得
        var ai = boss.GetComponent<BossEnemyController>(); // 存在しない場合はnull

        // NavMeshAgentが存在していれば移動停止
        if (nav != null)
        {
            nav.isStopped = true; // ボスの経路移動を停止する
        }

        // AIスクリプトが存在していれば一時的に無効化
        if (ai != null)
        {
            ai.enabled = false; // AIの処理ループや行動制御を一時的に無効にする
        }

        // 指定時間だけ待機（リアルタイムでの停止を保証、ゲームのTimeScaleに影響されない）
        yield return new WaitForSecondsRealtime(stopDuration);

        // NavMeshAgentが存在していれば移動再開
        if (nav != null)
        {
            nav.isStopped = false;
        }

        // AIスクリプトが存在していれば再び有効化
        if (ai != null)
        {
            ai.enabled = true;
        }

        // デバッグログに再始動の通知を出力（デバッグ用）
        Debug.Log($"[TemporarilyStopBoss] ボスが{stopDuration}秒後に再始動");
    }

}
