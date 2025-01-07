using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Stage1ClearScene : MonoBehaviour
{
    private float StayTime = 0f;

    [SerializeField] private VideoPlayer ClearVideo;

    [SerializeField] private RawImage RawImage;

    void OnEnable()
    {
        ClearVideo.prepareCompleted += PrepareCompleted;
    }

    void OnDisable()
    {
        ClearVideo.prepareCompleted -= PrepareCompleted;
    }

    // 2. ロードを待つ
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != ClearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // 1. 動画再生前にRawImageを非表示にする
        RawImage.enabled = false;

        // 2. 動画再生
        vp.Play();

        // 3. 動画の再生後1秒待機
        yield return new WaitForSeconds(1f);

        // 4. 動画のテクスチャを設定
        RawImage.texture = vp.texture;

        // 5. 動画の最初のフレームが進むまで待機
        while (vp.frame < 1)
        {
            yield return null;
        }

        // 6. 動画の最初のフレームが準備できたらRawImageを有効にして表示
        RawImage.enabled = true;
    }


    public void Play()
    {
        // 1. 動画プリロード
        ClearVideo.Prepare();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StayTime += Time.deltaTime;

        if (StayTime > 8f)
        {
            SceneManager.LoadScene("GameScene");
        }

    }
}
