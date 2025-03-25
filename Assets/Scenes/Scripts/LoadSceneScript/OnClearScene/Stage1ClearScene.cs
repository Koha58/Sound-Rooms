using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Stage1����J�ڂ���N���A��ʂł̓���̍Đ����Ǘ�����N���X
/// </summary>
public class Stage1ClearScene : MonoBehaviour
{
    private float StayTime = 0f; // �V�[���J�ڂ܂ł̌o�ߎ��Ԃ�ێ�����ϐ�

    [SerializeField] private VideoPlayer clearVideo; // �X�e�[�W�N���A������Đ����邽�߂�VideoPlayer
    [SerializeField] private RawImage rawImage; // �����\������RawImage
    [SerializeField] private RawImage firstImage; // �ŏ���1�b�ԕ\������ʂ̉摜

    // OnEnable���Ă΂��Ƃ��A�C�x���g���X�i�[��o�^
    void OnEnable()
    {
        // ���揀���������̃C�x���g���X�i�[
        clearVideo.prepareCompleted += PrepareCompleted;
        // �t���[���������ł������ɌĂ΂��C�x���g
        clearVideo.frameReady += FrameReady;
    }

    // OnDisable���Ă΂��Ƃ��A�C�x���g���X�i�[������
    void OnDisable()
    {
        clearVideo.prepareCompleted -= PrepareCompleted;
        clearVideo.frameReady -= FrameReady;
    }

    // ����̏��������������Ƃ��ɌĂ΂��
    private void PrepareCompleted(VideoPlayer vp)
    {
        // ������VideoPlayer���������ݒ肵��clearVideo�łȂ��ꍇ�͉������Ȃ�
        if (vp != clearVideo) { return; }
        // ������Đ�����R���[�`�����J�n
        StartCoroutine(ShowMovie(vp));
    }

    // �t���[���������ł������ɌĂ΂��
    private void FrameReady(VideoPlayer vp, long frameIndex)
    {
        // �ŏ��̃t���[���������ł�����RawImage��\��
        if (frameIndex == 0)
        {
            rawImage.texture = vp.texture; // ����̃e�N�X�`����RawImage�ɐݒ�
            rawImage.enabled = true; // RawImage��\��
        }
    }

    // ����̕\�����s���R���[�`��
    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // �ŏ���1�b�ԁAFirstImage�i�ʂ̉摜�j��\��
        firstImage.enabled = true;

        // 0.5�b���FirstImage���\���ɂ���
        yield return new WaitForSeconds(0.5f);
        // ����Đ��O��RawImage���\���ɂ���
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
        rawImage.texture = vp.texture; // ����̃e�N�X�`����RawImage�ɐݒ�
        rawImage.enabled = true; // RawImage��\��
    }

    // ������Đ����邽�߂̏������J�n
    public void Play()
    {
        // ������v�����[�h����
        clearVideo.Prepare();
    }

    // Start�͏����������Ɏg�p�����
    void Start()
    {
        // �����������i���݂͋�̃��\�b�h�j
    }

    // Update�͖��t���[���Ă΂�郁�\�b�h
    void Update()
    {
        StayTime += Time.deltaTime; // ���Ԃ��o�߂��邽�т�StayTime�𑝉�������

        // 8�b�o�߂�����A���̃V�[���ɑJ��
        if (StayTime > 8f)
        {
            SceneManager.LoadScene("GameScene"); // "GameScene"�ɑJ��
        }
    }
}
