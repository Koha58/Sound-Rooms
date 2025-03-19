using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// GameSceneから遷移するクリア画面での動画の再生を管理するクラス
/// </summary>
public class ClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // 経過時間。シーンの表示時間を計測。

    // 動画プレイヤーとUIを設定
    [SerializeField] private VideoPlayer clearSceneVideo;  // クリアシーンの動画を再生するためのVideoPlayer
    [SerializeField] private RawImage videoRawImage;  // 動画を表示するためのRawImage
    [SerializeField] private RawImage initialImage;  // 動画再生前に表示する最初の画像

    // このオブジェクトが有効になったときに呼ばれる
    void OnEnable()
    {
        // 動画の準備が完了したときに呼ばれるイベントを登録
        clearSceneVideo.prepareCompleted += OnPrepareCompleted;

        // フレームが準備できたときに呼ばれるイベントを登録
        clearSceneVideo.frameReady += OnFrameReady;
    }

    // このオブジェクトが無効になったときに呼ばれる
    void OnDisable()
    {
        // イベントを解除して、メモリリークを防ぐ
        clearSceneVideo.prepareCompleted -= OnPrepareCompleted;
        clearSceneVideo.frameReady -= OnFrameReady;
    }

    // 動画の準備が完了したときに呼ばれる
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // 動画プレイヤーが期待するものでない場合は何もしない
        if (videoPlayer != clearSceneVideo) { return; }

        // 動画の再生を開始するコルーチンを呼び出す
        StartCoroutine(PlayClearSceneVideo(videoPlayer));
    }

    // フレームが準備できた時に呼ばれる
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // 最初のフレームが準備できたらRawImageを表示
        if (frameIndex == 0)
        {
            // RawImageに動画のテクスチャを設定し、表示する
            videoRawImage.texture = videoPlayer.texture;
            videoRawImage.enabled = true;
        }
    }

    // クリアシーンの動画を再生するコルーチン
    private IEnumerator PlayClearSceneVideo(VideoPlayer videoPlayer)
    {
        // 最初の1秒間、initialImage（別の画像）を表示
        initialImage.enabled = true;

        // 0.5秒待ってからinitialImageを非表示にする
        yield return new WaitForSeconds(0.5f);

        // 動画再生前にvideoRawImageを非表示にする
        videoRawImage.enabled = false;

        // 動画の再生を開始
        videoPlayer.Play();

        // 動画の準備が完了するまで待機
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // 動画の最初のフレームが描画されるまで待機
        while (videoPlayer.frame < 1)
        {
            yield return null;
        }

        // 動画の最初のフレームが準備できたらRawImageに動画を設定し、表示する
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    // 動画プリロードを開始
    public void PlayClearSceneVideo()
    {
        // 動画を事前に準備（非同期で準備）
        clearSceneVideo.Prepare();
    }

    // Updateは毎フレーム呼ばれる
    void Update()
    {
        // 経過時間を加算
        elapsedTime += Time.deltaTime;

        // 10秒経過したら、StartSceneに遷移
        if (elapsedTime > 10f)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}