using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Stage1から遷移するクリア画面での動画の再生を管理するクラス
/// </summary>
public class Stage1ClearScene : MonoBehaviour
{
    private float StayTime = 0f; // シーン遷移までの経過時間を保持する変数

    [SerializeField] private VideoPlayer clearVideo; // ステージクリア動画を再生するためのVideoPlayer
    [SerializeField] private RawImage rawImage; // 動画を表示するRawImage
    [SerializeField] private RawImage firstImage; // 最初の1秒間表示する別の画像

    // OnEnableが呼ばれるとき、イベントリスナーを登録
    void OnEnable()
    {
        // 動画準備完了時のイベントリスナー
        clearVideo.prepareCompleted += PrepareCompleted;
        // フレームが準備できた時に呼ばれるイベント
        clearVideo.frameReady += FrameReady;
    }

    // OnDisableが呼ばれるとき、イベントリスナーを解除
    void OnDisable()
    {
        clearVideo.prepareCompleted -= PrepareCompleted;
        clearVideo.frameReady -= FrameReady;
    }

    // 動画の準備が完了したときに呼ばれる
    private void PrepareCompleted(VideoPlayer vp)
    {
        // 引数のVideoPlayerが自分が設定したclearVideoでない場合は何もしない
        if (vp != clearVideo) { return; }
        // 動画を再生するコルーチンを開始
        StartCoroutine(ShowMovie(vp));
    }

    // フレームが準備できた時に呼ばれる
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // 最初のフレームが準備できたらRawImageを表示
        if (frameIndex == 0)
        {
            rawImage.texture = vp.texture; // 動画のテクスチャをRawImageに設定
            rawImage.enabled = true; // RawImageを表示
        }
    }

    // 動画の表示を行うコルーチン
    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // 最初の1秒間、FirstImage（別の画像）を表示
        firstImage.enabled = true;

        // 0.5秒後にFirstImageを非表示にする
        yield return new WaitForSeconds(0.5f);
        // 動画再生前にRawImageを非表示にする
        rawImage.enabled = false;

        // 動画の再生を開始
        vp.Play();

        // 動画が最初に準備されるまで待機
        yield return new WaitUntil(() => vp.isPrepared);

        // 動画の最初のフレームが描画されるまで待機
        while (vp.frame < 1)
        {
            yield return null;
        }

        // 動画の最初のフレームが準備できたらRawImageを有効にして表示
        rawImage.texture = vp.texture; // 動画のテクスチャをRawImageに設定
        rawImage.enabled = true; // RawImageを表示
    }

    // 動画を再生するための準備を開始
    public void Play()
    {
        // 動画をプリロードする
        clearVideo.Prepare();
    }

    // Startは初期化処理に使用される
    void Start()
    {
        // 初期化処理（現在は空のメソッド）
    }

    // Updateは毎フレーム呼ばれるメソッド
    void Update()
    {
        StayTime += Time.deltaTime; // 時間が経過するたびにStayTimeを増加させる

        // 8秒経過したら、次のシーンに遷移
        if (StayTime > 8f)
        {
            SceneManager.LoadScene("GameScene"); // "GameScene"に遷移
        }
    }
}
