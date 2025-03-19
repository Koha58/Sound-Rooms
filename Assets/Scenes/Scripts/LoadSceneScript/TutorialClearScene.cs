using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// TutorialScene����J�ڂ���N���A��ʂł̓���̍Đ����Ǘ�����N���X
/// </summary>
public class TutorialClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // �o�ߎ��ԁB�w�肵�����Ԍ�ɃV�[���J�ڂ��s�����߂Ɏg�p

    [SerializeField] private VideoPlayer tutorialClearVideo;  // �`���[�g���A���N���A������Đ�����VideoPlayer
    [SerializeField] private RawImage videoRawImage;  // �����\�����邽�߂�RawImage
    [SerializeField] private RawImage initialImage;  // �����ɕ\�������摜�i����Đ��O�ɕ\�������j

    // �I�u�W�F�N�g���L���ɂȂ�ۂɌĂяo�����
    void OnEnable()
    {
        // ���悪�������������ۂɌĂ΂��C�x���g���X�i�[
        tutorialClearVideo.prepareCompleted += OnPrepareCompleted;
        // �t���[���������ł������ɌĂ΂��C�x���g���X�i�[
        tutorialClearVideo.frameReady += OnFrameReady;
    }

    // �I�u�W�F�N�g�������ɂȂ�ۂɌĂяo�����
    void OnDisable()
    {
        // �C�x���g���X�i�[���������āA���������[�N��h��
        tutorialClearVideo.prepareCompleted -= OnPrepareCompleted;
        tutorialClearVideo.frameReady -= OnFrameReady;
    }

    // ����̏��������������ۂɌĂ΂��
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // �Ώۂ�VideoPlayer����v�����ꍇ�ɍĐ����J�n����
        if (videoPlayer != tutorialClearVideo) { return; }
        StartCoroutine(PlayTutorialClearVideo(videoPlayer));  // ����Đ��������R���[�`���ŊJ�n
    }

    // ����̃t���[���������ł������ɌĂ΂��
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // �ŏ��̃t���[���������ł����ꍇ��RawImage��\��
        if (frameIndex == 0)
        {
            videoRawImage.texture = videoPlayer.texture;  // ����̃e�N�X�`����RawImage�ɐݒ�
            videoRawImage.enabled = true;  // RawImage��\��
        }
    }

    // �`���[�g���A���N���A������Đ�����R���[�`��
    private IEnumerator PlayTutorialClearVideo(VideoPlayer videoPlayer)
    {
        // ���߂ɕ\�������摜��1�b�ԕ\��
        initialImage.enabled = true;

        // 0.5�b���initialImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);

        // ����Đ��O��RawImage���\���ɂ���
        videoRawImage.enabled = false;

        // ����̍Đ����J�n
        videoPlayer.Play();

        // ���悪���������̂�ҋ@
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // ����̍ŏ��̃t���[�����`�悳���܂őҋ@
        while (videoPlayer.frame < 1)
        {
            yield return null;  // �t���[�������������܂ő҂�
        }

        // �ŏ��̃t���[���������ł�����RawImage��\��
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    // ����̃v�����[�h���J�n
    public void PlayTutorialClearVideo()
    {
        tutorialClearVideo.Prepare();  // ������v�����[�h���ď����𐮂���
    }

    void Update()
    {
        // �o�ߎ��Ԃ����Z
        elapsedTime += Time.deltaTime;

        // �o�ߎ��Ԃ�12�b�𒴂����玟�̃V�[���ɑJ��
        if (elapsedTime > 12f)
        {
            SceneManager.LoadScene("Stage1");  // "Stage1"�V�[���ɑJ��
        }
    }
}