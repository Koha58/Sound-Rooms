using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ClearScene : MonoBehaviour
{
    private float StayTime = 0f;

    [SerializeField] private VideoPlayer ClearVideo;
    [SerializeField] private RawImage RawImage;
    [SerializeField] private RawImage FirstImage; // 0.5秒間表示する別の画像

    void OnEnable()
    {
        // 動画準備完了時のイベントリスナー
        ClearVideo.prepareCompleted += PrepareCompleted;
        // フレームが準備できた時に呼ばれるイベント
        ClearVideo.frameReady += FrameReady;
    }

    void OnDisable()
    {
        ClearVideo.prepareCompleted -= PrepareCompleted;
        ClearVideo.frameReady -= FrameReady;
    }

    // 動画の準備が完了したときに呼ばれる
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != ClearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    // フレームが準備できた時に呼ばれる
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // 最初のフレームが準備できたらRawImageを表示
        if (frameIndex == 0)
        {
            RawImage.texture = vp.texture;
            RawImage.enabled = true;
        }
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // 最初の1秒間、FirstImage（別の画像）を表示
        FirstImage.enabled = true;

        // 0.5秒後にFirstImageを非表示にする
        yield return new WaitForSeconds(0.5f);
        // 1. 動画再生前にRawImageを非表示にする
        RawImage.enabled = false;

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
        RawImage.texture = vp.texture;
        RawImage.enabled = true;
    }

    public void Play()
    {
        // 動画プリロード
        ClearVideo.Prepare();
    }

    void Start()
    {
        // 初期化処理
    }

    // Update is called once per frame
    void Update()
    {
        StayTime += Time.deltaTime;

        if (StayTime > 10f)
        {
            SceneManager.LoadScene("StartScene");
        }

    }
}
