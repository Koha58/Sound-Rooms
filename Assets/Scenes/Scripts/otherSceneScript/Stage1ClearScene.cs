using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Stage1ClearScene : MonoBehaviour
{
    private float StayTime = 0f;

    [SerializeField] private VideoPlayer clearVideo;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private RawImage firstImage; // 0.5秒間表示する別の画像

    void OnEnable()
    {
        // 動画準備完了時のイベントリスナー
        clearVideo.prepareCompleted += PrepareCompleted;
        // フレームが準備できた時に呼ばれるイベント
        clearVideo.frameReady += FrameReady;
    }

    void OnDisable()
    {
        clearVideo.prepareCompleted -= PrepareCompleted;
        clearVideo.frameReady -= FrameReady;
    }

    // 動画の準備が完了したときに呼ばれる
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != clearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    // フレームが準備できた時に呼ばれる
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // 最初のフレームが準備できたらRawImageを表示
        if (frameIndex == 0)
        {
            rawImage.texture = vp.texture;
            rawImage.enabled = true;
        }
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // 最初の1秒間、FirstImage（別の画像）を表示
        firstImage.enabled = true;

        // 0.5秒後にFirstImageを非表示にする
        yield return new WaitForSeconds(0.5f);
        // 1. 動画再生前にRawImageを非表示にする
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
        rawImage.texture = vp.texture;
        rawImage.enabled = true;
    }

    public void Play()
    {
        // 動画プリロード
        clearVideo.Prepare();
    }

    void Start()
    {
        // 初期化処理
    }

    void Update()
    {
        StayTime += Time.deltaTime;

        if (StayTime > 8f)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
