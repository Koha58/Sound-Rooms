using System.Collections;
using UnityEngine;

/// <summary>
/// ボスイベントを管理するクラス
/// 段階ごとのUI切り替えや最終ステージでのカメラ演出、
/// 不気味なオーバーレイ表示などを制御。
/// </summary>

public class BossCounter : MonoBehaviour
{
    // UI関連
    public GameObject[] uiStages;               // 各ステージのUIオブジェクト配列
    [SerializeField] private GameObject screenEerieOverlay; // 不気味オーバーレイ用ゲームオブジェクト
    [SerializeField] private AudioClip stageChangeClip;    // ステージ切り替え時の効果音
    private AudioSource audioSource;                        // 効果音再生用

    // ステージ管理
    private int currentStage = 0;           // 現在のステージ番号
    private float timer = 0f;               // ステージ経過時間管理用タイマー
    private float stageDuration = 120f;     // 各ステージの継続時間（秒）
    private float resetDelay = 60f;        // 最終ステージ後のリセット待機時間（秒）
    private bool isFinalStage = false;     // 最終ステージ到達フラグ

    [Header("演出カメラ")]
    [SerializeField] private Camera mainCamera;          // 通常プレイ用カメラ
    [SerializeField] private Camera eventCamera;         // 演出用カメラ
    [SerializeField] private Transform eventCameraStartPoint; // 演出カメラ開始位置
    [SerializeField] private Transform eventCameraEndPoint;   // 演出カメラ終了位置

    // 前方演出用（背後に壁がある場合）
    [SerializeField] private Transform frontEventCameraStartPoint; // 壁あり時の演出カメラ開始位置
    [SerializeField] private Transform frontEventCameraEndPoint;   // 壁あり時の演出カメラ終了位置
    private float wallCheckDistance = 10f;           // 壁判定用レイの距離

    // 演出設定
    private float cameraMoveDuration = 5f;           // カメラ移動にかける時間（秒）
    private float eventDuration = 5f;                 // 演出中の待機時間（秒）

    // 参照
    [SerializeField] private GameObject player;       // プレイヤーオブジェクト参照

    // 状態管理
    private bool isEventPlaying = false;              // 演出中フラグ
    private bool isEerieEffectActive = false;        // 不気味オーバーレイ表示フラグ

    // MainCameraの元の位置・回転を保持（演出後に戻すため）
    private Vector3 mainCameraOriginalPosition;
    private Quaternion mainCameraOriginalRotation;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log("[Start] 初期化: currentStage=0");

        // 最初のUI更新（ステージ0のUI表示）
        UpdateUIStage();

        // 不気味オーバーレイは最初は非表示にしておく
        if (screenEerieOverlay != null)
            screenEerieOverlay.SetActive(false);
    }

    void Update()
    {
        // 演出中は通常のステージ進行処理を止める
        if (isEventPlaying) return;

        timer += Time.deltaTime; // タイマーを進める

        if (!isFinalStage && timer >= stageDuration)
        {
            // ステージ時間経過でステージ進行
            timer = 0f;
            currentStage++;

            if (currentStage >= uiStages.Length - 1)
            {
                // 最終ステージ到達時の処理
                currentStage = uiStages.Length - 1;
                isFinalStage = true;
                UpdateUIStage();

                if (!isEventPlaying)
                {
                    // 演出開始
                    isEventPlaying = true;
                    StartCoroutine(PlayFinalStageEvent());
                }
            }
            else
            {
                // 通常ステージ更新
                UpdateUIStage();
            }
        }

        // 不気味オーバーレイのON/OFFをフラグで制御
        if (screenEerieOverlay != null)
        {
            screenEerieOverlay.SetActive(isEerieEffectActive);
        }
    }

    // ステージに応じてUIを切り替える関数
    void UpdateUIStage()
    {
        for (int i = 0; i < uiStages.Length; i++)
        {
            uiStages[i].SetActive(i == currentStage);
        }

        // 不気味オーバーレイはUpdate()で制御しているためここではOFFにする
        if (screenEerieOverlay != null && !isEerieEffectActive)
        {
            screenEerieOverlay.SetActive(false);
        }

        // ステージ切り替え音の再生
        if (stageChangeClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageChangeClip);
        }
    }

    // 最終ステージ到達時の演出処理
    IEnumerator PlayFinalStageEvent()
    {
        Debug.Log("[PlayFinalStageEvent] 演出開始");

        // ゲームの時間を止める（演出用に）
        Time.timeScale = 0f;

        // プレイヤーの背後に壁があるかをチェック
        Vector3 back = -player.transform.forward;
        Ray ray = new Ray(player.transform.position, back);
        bool hasWallBehind = Physics.Raycast(ray, wallCheckDistance);

        // 壁の有無によって演出カメラのスタート・エンド位置を切り替え
        Transform startPoint = hasWallBehind ? frontEventCameraStartPoint : eventCameraStartPoint;
        Transform endPoint = hasWallBehind ? frontEventCameraEndPoint : eventCameraEndPoint;

        // MainCameraの元位置・回転を保存（演出後に戻すため）
        mainCameraOriginalPosition = mainCamera.transform.position;
        mainCameraOriginalRotation = mainCamera.transform.rotation;

        // 演出用カメラの初期位置・回転を設定
        eventCamera.transform.position = startPoint.position;
        eventCamera.transform.rotation = startPoint.rotation;

        // 通常カメラをOFF、演出カメラをONに切り替え
        mainCamera.enabled = false;
        eventCamera.enabled = true;

        // ボスのサウンドエフェクトを再生（存在する場合）
        GameObject boss = GameObject.Find("BossEnemy");
        boss?.GetComponent<BossEnemyController>()?.ShowSoundEffect();

        // カメラをスムーズに移動させる
        float elapsed = 0f;
        while (elapsed < cameraMoveDuration)
        {
            float t = Mathf.SmoothStep(0, 1, elapsed / cameraMoveDuration);
            eventCamera.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            eventCamera.transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);

            elapsed += Time.unscaledDeltaTime; // 時間停止中でも進む時間で計算
            yield return null;
        }

        // 最終的な位置・回転に補正
        eventCamera.transform.position = endPoint.position;
        eventCamera.transform.rotation = endPoint.rotation;

        Debug.Log("[PlayFinalStageEvent] 演出中...待機");
        yield return new WaitForSecondsRealtime(eventDuration); // 演出時間だけ待機（リアルタイム）

        // 演出カメラをOFF、通常カメラをONに戻す
        eventCamera.enabled = false;
        mainCamera.enabled = true;

        // カメラの位置・回転を元に戻す
        mainCamera.transform.position = mainCameraOriginalPosition;
        mainCamera.transform.rotation = mainCameraOriginalRotation;

        // 時間を通常に戻す
        Time.timeScale = 1f;

        Debug.Log("[PlayFinalStageEvent] 演出終了、不気味演出開始");

        // 不気味演出ON（オーバーレイ表示）
        isEerieEffectActive = true;
        if (screenEerieOverlay != null)
            screenEerieOverlay.SetActive(true);

        // リセット処理開始
        StartCoroutine(ResetAfterDelay());

        isEventPlaying = false;
    }

    // 不気味演出表示後、リセットするまで待機してリセットする
    IEnumerator ResetAfterDelay()
    {
        Debug.Log("[ResetAfterDelay] リセット待機開始");
        yield return new WaitForSeconds(resetDelay); // リセットまでの待機

        // ステージ情報を初期化
        currentStage = 0;
        isFinalStage = false;
        timer = 0f;

        Debug.Log("[ResetAfterDelay] リセット完了 currentStage=0");

        // 不気味演出OFF（オーバーレイ非表示）
        isEerieEffectActive = false;
        if (screenEerieOverlay != null)
            screenEerieOverlay.SetActive(false);

        // UI更新
        UpdateUIStage();
    }
}
