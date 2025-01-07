using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ClearScene : MonoBehaviour
{
    private float stayTime = 0f;

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
        // 3. 動画再生
        vp.Play();

        // 4. フレームを待つ
        while (vp.frame < 1)
        {
            yield return null;
        }

        // 5. 画面に表示
        RawImage.texture = vp.texture;
        RawImage.enabled = true;
    }

    private void SetVideoUrl(string url)
    {
        ClearVideo.source = VideoSource.Url;
        ClearVideo.url = url;
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
        stayTime += Time.deltaTime;

        if (stayTime > 10f)
        {
            SceneManager.LoadScene("StartScene");
        }

    }
}
