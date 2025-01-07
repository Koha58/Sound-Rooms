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

    // 2. ���[�h��҂�
    private void PrepareCompleted(VideoPlayer vp)
    {
        if (vp != ClearVideo) { return; }
        StartCoroutine(ShowMovie(vp));
    }

    private IEnumerator ShowMovie(VideoPlayer vp)
    {
        // 1. ����Đ��O��RawImage���\���ɂ���
        RawImage.enabled = false;

        // 2. ����Đ�
        vp.Play();

        // 3. ����̍Đ���1�b�ҋ@
        yield return new WaitForSeconds(1f);

        // 4. ����̃e�N�X�`����ݒ�
        RawImage.texture = vp.texture;

        // 5. ����̍ŏ��̃t���[�����i�ނ܂őҋ@
        while (vp.frame < 1)
        {
            yield return null;
        }

        // 6. ����̍ŏ��̃t���[���������ł�����RawImage��L���ɂ��ĕ\��
        RawImage.enabled = true;
    }


    public void Play()
    {
        // 1. ����v�����[�h
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
