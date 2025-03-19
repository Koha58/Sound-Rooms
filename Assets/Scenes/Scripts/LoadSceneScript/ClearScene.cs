using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// GameScene����J�ڂ���N���A��ʂł̓���̍Đ����Ǘ�����N���X
/// </summary>
public class ClearScene : MonoBehaviour
{
    private float elapsedTime = 0f;  // �o�ߎ��ԁB�V�[���̕\�����Ԃ��v���B

    // ����v���C���[��UI��ݒ�
    [SerializeField] private VideoPlayer clearSceneVideo;  // �N���A�V�[���̓�����Đ����邽�߂�VideoPlayer
    [SerializeField] private RawImage videoRawImage;  // �����\�����邽�߂�RawImage
    [SerializeField] private RawImage initialImage;  // ����Đ��O�ɕ\������ŏ��̉摜

    // ���̃I�u�W�F�N�g���L���ɂȂ����Ƃ��ɌĂ΂��
    void OnEnable()
    {
        // ����̏��������������Ƃ��ɌĂ΂��C�x���g��o�^
        clearSceneVideo.prepareCompleted += OnPrepareCompleted;

        // �t���[���������ł����Ƃ��ɌĂ΂��C�x���g��o�^
        clearSceneVideo.frameReady += OnFrameReady;
    }

    // ���̃I�u�W�F�N�g�������ɂȂ����Ƃ��ɌĂ΂��
    void OnDisable()
    {
        // �C�x���g���������āA���������[�N��h��
        clearSceneVideo.prepareCompleted -= OnPrepareCompleted;
        clearSceneVideo.frameReady -= OnFrameReady;
    }

    // ����̏��������������Ƃ��ɌĂ΂��
    private void OnPrepareCompleted(VideoPlayer videoPlayer)
    {
        // ����v���C���[�����҂�����̂łȂ��ꍇ�͉������Ȃ�
        if (videoPlayer != clearSceneVideo) { return; }

        // ����̍Đ����J�n����R���[�`�����Ăяo��
        StartCoroutine(PlayClearSceneVideo(videoPlayer));
    }

    // �t���[���������ł������ɌĂ΂��
    private void OnFrameReady(VideoPlayer videoPlayer, long frameIndex)
    {
        // �ŏ��̃t���[���������ł�����RawImage��\��
        if (frameIndex == 0)
        {
            // RawImage�ɓ���̃e�N�X�`����ݒ肵�A�\������
            videoRawImage.texture = videoPlayer.texture;
            videoRawImage.enabled = true;
        }
    }

    // �N���A�V�[���̓�����Đ�����R���[�`��
    private IEnumerator PlayClearSceneVideo(VideoPlayer videoPlayer)
    {
        // �ŏ���1�b�ԁAinitialImage�i�ʂ̉摜�j��\��
        initialImage.enabled = true;

        // 0.5�b�҂��Ă���initialImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);

        // ����Đ��O��videoRawImage���\���ɂ���
        videoRawImage.enabled = false;

        // ����̍Đ����J�n
        videoPlayer.Play();

        // ����̏�������������܂őҋ@
        yield return new WaitUntil(() => videoPlayer.isPrepared);

        // ����̍ŏ��̃t���[�����`�悳���܂őҋ@
        while (videoPlayer.frame < 1)
        {
            yield return null;
        }

        // ����̍ŏ��̃t���[���������ł�����RawImage�ɓ����ݒ肵�A�\������
        videoRawImage.texture = videoPlayer.texture;
        videoRawImage.enabled = true;
    }

    // ����v�����[�h���J�n
    public void PlayClearSceneVideo()
    {
        // ��������O�ɏ����i�񓯊��ŏ����j
        clearSceneVideo.Prepare();
    }

    // Update�͖��t���[���Ă΂��
    void Update()
    {
        // �o�ߎ��Ԃ����Z
        elapsedTime += Time.deltaTime;

        // 10�b�o�߂�����AStartScene�ɑJ��
        if (elapsedTime > 10f)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}