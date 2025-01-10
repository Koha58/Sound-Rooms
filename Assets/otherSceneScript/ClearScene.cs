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
    [SerializeField] private RawImage FirstImage; // 0.5�b�ԕ\������ʂ̉摜

    void OnEnable()
    {
        // ���揀���������̃C�x���g���X�i�[
        ClearVideo.prepareCompleted += PrepareCompleted;
        // �t���[���������ł������ɌĂ΂��C�x���g
        ClearVideo.frameReady += FrameReady;
    }

    void OnDisable()
    {
        ClearVideo.prepareCompleted -= PrepareCompleted;
        ClearVideo.frameReady -= FrameReady;
    }

    // ����̏��������������Ƃ��ɌĂ΂��
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != ClearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    // �t���[���������ł������ɌĂ΂��
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // �ŏ��̃t���[���������ł�����RawImage��\��
        if (frameIndex == 0)
        {
            RawImage.texture = vp.texture;
            RawImage.enabled = true;
        }
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // �ŏ���1�b�ԁAFirstImage�i�ʂ̉摜�j��\��
        FirstImage.enabled = true;

        // 0.5�b���FirstImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);
        // 1. ����Đ��O��RawImage���\���ɂ���
        RawImage.enabled = false;

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
        RawImage.texture = vp.texture;
        RawImage.enabled = true;
    }

    public void Play()
    {
        // ����v�����[�h
        ClearVideo.Prepare();
    }

    void Start()
    {
        // ����������
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
