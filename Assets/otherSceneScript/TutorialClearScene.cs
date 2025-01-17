using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // "StayTime" -> "elapsedTime" �ɕύX

    [SerializeField] private VideoPlayer tutorialClearVideo;  // "ClearVideo" -> "tutorialClearVideo" �ɕύX
    [SerializeField] private RawImage videoRawImage;  // "RawImage" -> "videoRawImage" �ɕύX
    [SerializeField] private RawImage initialImage;  // "FirstImage" -> "initialImage" �ɕύX

    void OnEnable()
    {
        // ���揀���������̃C�x���g���X�i�[
        tutorialClearVideo.prepareCompleted += OnPrepareCompleted;
        // �t���[���������ł������ɌĂ΂��C�x���g
        tutorialClearVideo.frameReady += OnFrameReady;
    }

    void OnDisable()
    {
        tutorialClearVideo.prepareCompleted -= OnPrepareCompleted;
        tutorialClearVideo.frameReady -= OnFrameReady;
    }

    // ����̏��������������Ƃ��ɌĂ΂��
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        if (videoPlayer != tutorialClearVideo) { return; }
        StartCoroutine(PlayTutorialClearVideo(videoPlayer));
    }

    // �t���[���������ł������ɌĂ΂��
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // �ŏ��̃t���[���������ł�����RawImage��\��
        if (frameIndex == 0)
        {
            videoRawImage.texture = videoPlayer.texture;
            videoRawImage.enabled = true;
        }
    }

    private IEnumerator PlayTutorialClearVideo(VideoPlayer videoPlayer)
    {
        // �ŏ���1�b�ԁAinitialImage�i�ʂ̉摜�j��\��
        initialImage.enabled = true;

        // 0.5�b���initialImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);
        // 1. ����Đ��O��videoRawImage���\���ɂ���
        videoRawImage.enabled = false;

        // ����̍Đ����J�n
        videoPlayer.Play();

        // ���悪�ŏ��ɏ��������܂őҋ@
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // ����̍ŏ��̃t���[�����`�悳���܂őҋ@
        while (videoPlayer.frame < 1)
        {
            yield return null;
        }

        // ����̍ŏ��̃t���[���������ł�����videoRawImage��L���ɂ��ĕ\��
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    public void PlayTutorialClearVideo()
    {
        // ����v�����[�h
        tutorialClearVideo.Prepare();
    }

    void Start()
    {
        // ����������
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
