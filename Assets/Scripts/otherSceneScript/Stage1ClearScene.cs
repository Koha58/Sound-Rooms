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
    [SerializeField] private RawImage firstImage; // 0.5�b�ԕ\������ʂ̉摜

    void OnEnable()
    {
        // ���揀���������̃C�x���g���X�i�[
        clearVideo.prepareCompleted += PrepareCompleted;
        // �t���[���������ł������ɌĂ΂��C�x���g
        clearVideo.frameReady += FrameReady;
    }

    void OnDisable()
    {
        clearVideo.prepareCompleted -= PrepareCompleted;
        clearVideo.frameReady -= FrameReady;
    }

    // ����̏��������������Ƃ��ɌĂ΂��
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != clearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    // �t���[���������ł������ɌĂ΂��
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // �ŏ��̃t���[���������ł�����RawImage��\��
        if (frameIndex == 0)
        {
            rawImage.texture = vp.texture;
            rawImage.enabled = true;
        }
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // �ŏ���1�b�ԁAFirstImage�i�ʂ̉摜�j��\��
        firstImage.enabled = true;

        // 0.5�b���FirstImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);
        // 1. ����Đ��O��RawImage���\���ɂ���
        rawImage.enabled = false;

        // ����̍Đ����J�n
        vp.Play();

        // ���悪�ŏ��ɏ��������܂őҋ@
        yield return new WaitUntil(() => vp.isPrepared);

        // ����̍ŏ��̃t���[�����`�悳���܂őҋ@
        while (vp.frame < 1)
        {
            yield return null;
        }

        // ����̍ŏ��̃t���[���������ł�����RawImage��L���ɂ��ĕ\��
        rawImage.texture = vp.texture;
        rawImage.enabled = true;
    }

    public void Play()
    {
        // ����v�����[�h
        clearVideo.Prepare();
    }

    void Start()
    {
        // ����������
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
