using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// TutorialSceneから遷移するクリア画面での動画の再生を管理するクラス
/// </summary>
public class TutorialClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // 経過時間。指定した時間後にシーン遷移を行うために使用

    [SerializeField] private VideoPlayer tutorialClearVideo;  // チュートリアルクリア動画を再生するVideoPlayer
    [SerializeField] private RawImage videoRawImage;  // 動画を表示するためのRawImage
    [SerializeField] private RawImage initialImage;  // 初期に表示される画像（動画再生前に表示される）

    // オブジェクトが有効になる際に呼び出される
    void OnEnable()
    {
        // 動画が準備完了した際に呼ばれるイベントリスナー
        tutorialClearVideo.prepareCompleted += OnPrepareCompleted;
        // フレームが準備できた時に呼ばれるイベントリスナー
        tutorialClearVideo.frameReady += OnFrameReady;
    }

    // オブジェクトが無効になる際に呼び出される
    void OnDisable()
    {
        // イベントリスナーを解除して、メモリリークを防ぐ
        tutorialClearVideo.prepareCompleted -= OnPrepareCompleted;
        tutorialClearVideo.frameReady -= OnFrameReady;
    }

    // 動画の準備が完了した際に呼ばれる
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // 対象のVideoPlayerが一致した場合に再生を開始する
        if (videoPlayer != tutorialClearVideo) { return; }
        StartCoroutine(PlayTutorialClearVideo(videoPlayer));  // 動画再生処理をコルーチンで開始
    }

    // 動画のフレームが準備できた時に呼ばれる
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // 最初のフレームが準備できた場合にRawImageを表示
        if (frameIndex == 0)
        {
            videoRawImage.texture = videoPlayer.texture;  // 動画のテクスチャをRawImageに設定
            videoRawImage.enabled = true;  // RawImageを表示
        }
    }

    // チュートリアルクリア動画を再生するコルーチン
    private IEnumerator PlayTutorialClearVideo(VideoPlayer videoPlayer)
    {
        // 初めに表示される画像を1秒間表示
        initialImage.enabled = true;

        // 0.5秒後にinitialImageを非表示にする
        yield return new WaitForSeconds(0.5f);

        // 動画再生前にRawImageを非表示にする
        videoRawImage.enabled = false;

        // 動画の再生を開始
        videoPlayer.Play();

        // 動画が準備されるのを待機
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // 動画の最初のフレームが描画されるまで待機
        while (videoPlayer.frame < 1)
        {
            yield return null;  // フレームが準備されるまで待つ
        }

        // 最初のフレームが準備できたらRawImageを表示
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    // 動画のプリロードを開始
    public void PlayTutorialClearVideo()
    {
        tutorialClearVideo.Prepare();  // 動画をプリロードして準備を整える
    }

    void Update()
    {
        // 経過時間を加算
        elapsedTime += Time.deltaTime;

        // 経過時間が12秒を超えたら次のシーンに遷移
        if (elapsedTime > 12f)
        {
            SceneManager.LoadScene("Stage1");  // "Stage1"シーンに遷移
        }
    }
}