using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // "StayTime" -> "elapsedTime" に変更

    [SerializeField] private VideoPlayer tutorialClearVideo;  // "ClearVideo" -> "tutorialClearVideo" に変更
    [SerializeField] private RawImage videoRawImage;  // "RawImage" -> "videoRawImage" に変更
    [SerializeField] private RawImage initialImage;  // "FirstImage" -> "initialImage" に変更

    void OnEnable()
    {
        // 動画準備完了時のイベントリスナー
        tutorialClearVideo.prepareCompleted += OnPrepareCompleted;
        // フレームが準備できた時に呼ばれるイベント
        tutorialClearVideo.frameReady += OnFrameReady;
    }

    void OnDisable()
    {
        tutorialClearVideo.prepareCompleted -= OnPrepareCompleted;
        tutorialClearVideo.frameReady -= OnFrameReady;
    }

    // 動画の準備が完了したときに呼ばれる
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        if (videoPlayer != tutorialClearVideo) { return; }
        StartCoroutine(PlayTutorialClearVideo(videoPlayer));
    }

    // フレームが準備できた時に呼ばれる
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // 最初のフレームが準備できたらRawImageを表示
        if (frameIndex == 0)
        {
            videoRawImage.texture = videoPlayer.texture;
            videoRawImage.enabled = true;
        }
    }

    private IEnumerator PlayTutorialClearVideo(VideoPlayer videoPlayer)
    {
        // 最初の1秒間、initialImage（別の画像）を表示
        initialImage.enabled = true;

        // 0.5秒後にinitialImageを非表示にする
        yield return new WaitForSeconds(0.5f);
        // 1. 動画再生前にvideoRawImageを非表示にする
        videoRawImage.enabled = false;

        // 動画の再生を開始
        videoPlayer.Play();

        // 動画が最初に準備されるまで待機
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // 動画の最初のフレームが描画されるまで待機
        while (videoPlayer.frame < 1)
        {
            yield return null;
        }

        // 動画の最初のフレームが準備できたらvideoRawImageを有効にして表示
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    public void PlayTutorialClearVideo()
    {
        // 動画プリロード
        tutorialClearVideo.Prepare();
    }

    void Start()
    {
        // 初期化処理
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 12f)
        {
            SceneManager.LoadScene("Stage1");
        }
    }
}
