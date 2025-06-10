using System.Collections;
using UnityEngine;

/// <summary>
/// UI段階（ステージ）を時間経過に応じて自動的に切り替えるクラス
/// - 5段階のUI（例：警戒レベルやボスの怒り状態など）を2分おきに切り替える。
/// - 最終段階（5段階目）に到達した後、1分経過すると最初の段階にリセットされる。
/// - UIは GameObject のアクティブ状態で制御され、1段階だけが常に表示される。
/// </summary>
public class BossCounter : MonoBehaviour
{
    // 各段階のUIオブジェクト（ステージ0～4を想定）
    public GameObject[] uiStages;

    // UI切り替え時に再生する効果音
    [SerializeField] private AudioClip stageChangeClip;

    // 効果音再生に使用する AudioSource（このオブジェクトにアタッチされている想定）
    private AudioSource audioSource;

    // 現在の段階インデックス（0～4）
    private int currentStage = 0;

    // 時間をカウントするためのタイマー
    private float timer = 0f;

    // 各段階の継続時間（秒）：2分（120秒）
    private float stageDuration = 10f;

    // 最終段階到達後のリセットまでの待機時間：1分（60秒）
    private float resetDelay = 10f;

    // 最終段階に到達したかを示すフラグ
    private bool isFinalStage = false;

    void Start()
    {
        // AudioSource を取得
        audioSource = GetComponent<AudioSource>();

        // 初期段階のUIを表示（他は非表示）
        UpdateUIStage();
    }

    void Update()
    {
        // 経過時間を加算
        timer += Time.deltaTime;

        // 最終段階でない場合に限り、一定時間ごとに段階を進める
        if (!isFinalStage && timer >= stageDuration)
        {
            timer = 0f; // タイマーリセット
            currentStage++; // 段階を1つ進める

            // 段階が配列の長さ以上になった場合（最終段階を超えたら）
            if (currentStage >= uiStages.Length)
            {
                // 最終段階に固定
                currentStage = uiStages.Length - 1;
                isFinalStage = true;

                // 指定秒数後に最初の段階へリセット
                StartCoroutine(ResetAfterDelay());
            }

            // UIを更新
            UpdateUIStage();
        }
    }

    /// <summary>
    /// 現在の段階以外のUIは非表示にし、現在段階のUIのみを表示
    /// </summary>
    void UpdateUIStage()
    {
        for (int i = 0; i < uiStages.Length; i++)
        {
            uiStages[i].SetActive(i == currentStage);
        }

        // 効果音を再生（AudioClip が設定されていて、AudioSource もある場合）
        if (stageChangeClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageChangeClip);
        }
    }

    /// <summary>
    /// 最終段階に到達してから一定時間後に最初に戻す処理
    /// </summary>
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay); // 指定秒数待機
        currentStage = 0;        // 最初の段階に戻す
        isFinalStage = false;    // 最終段階フラグをリセット
        timer = 0f;              // タイマーをリセット
        UpdateUIStage();         // UIを更新
    }
}
