using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�s�[�J�[���ڑ�����Ă��Ȃ��ꍇ�ɁA�G���[�\����������N���X
/// </summary>
public class AudioOutputChecker : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource �R���|�[�l���g
    private AudioClip audioClip;  // �J�X�^���I�[�f�B�I�N���b�v
    private bool overflowOccurred = false; // �I�[�o�[�t���[�����t���O
    private OverflowHandler overflowHandler;  // �I�[�o�[�t���[������S������n���h��

    [SerializeField] private GameObject SpeakerConnectionBadUI;  // �X�s�[�J�[�ڑ��G���[UI

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource �R���|�[�l���g��ǉ�
        audioSource = gameObject.AddComponent<AudioSource>();

        // �J�X�^���I�[�f�B�I�N���b�v���쐬�i44100�T���v���A1�`�����l���A44100Hz�ōĐ��j
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);

        // AudioSource �ɃN���b�v��ݒ�
        audioSource.clip = audioClip;

        // �I�[�f�B�I�\�[�X���~���[�g�ɐݒ�i�Đ����Ă����͏o���Ȃ��j
        audioSource.mute = true;

        // ������ԂŃI�[�o�[�t���[���������Ă���Ɛݒ�
        overflowOccurred = true;

        // �I�[�f�B�I�\�[�X���Đ��J�n
        audioSource.Play();

        // �X�s�[�J�[�ڑ��G���[UI��\��
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 3�b��Ɏ��s���鏈�����J�n�i���������݂͉������Ă��Ȃ��j
        StartCoroutine(WaitAndExecute());

        // �X�s�[�J�[�ڑ��G���[UI���\���ɂ���
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = false;

        // �I�[�o�[�t���[���������Ă���ꍇ�AOverflow ���\�b�h�����s
        if (overflowOccurred)
        {
            Overflow();
        }
    }

    // 3�b�҂��Ă�����s���鏈�����J�n����R���[�`��
    private System.Collections.IEnumerator WaitAndExecute()
    {
        // 3�b�ԑҋ@
        yield return new WaitForSeconds(3f);
    }

    // �I�[�f�B�I�f�[�^�𐶐����郁�\�b�h
    private void OnAudioRead(float[] data)
    {
        // �T�C���g�i440Hz�j���I�[�f�B�I�f�[�^�Ƃ��Đ���
        for (int i = 0; i < data.Length; i++)
        {
            // 440Hz�̃T�C���g�𐶐����ăf�[�^�ɐݒ�
            data[i] = Mathf.Sin(2 * Mathf.PI * 440 * (i / 44100f)); // 440Hz�̃T�C���g
        }
    }

    // �I�[�f�B�I�t�B���^�̓ǂݎ�胁�\�b�h�i�I�[�o�[�t���[�̌��o�j
    private void OnAudioFilterRead(float[] data, int channels)
    {
        // �I�[�f�B�I�f�[�^�̒�����1024�T���v�����傫���ꍇ�A�I�[�o�[�t���[���������Ă��Ȃ��Ɣ��f
        if (data.Length > 1024)
        {
            overflowOccurred = false;  // �I�[�o�[�t���[���������Ă��Ȃ��̂Ńt���O��false��
            //Debug.Log("OK");  // ����I���̃��O
        }
        else
        {
            // �I�[�o�[�t���[�����������ꍇ�A�t���O�𗧂Ăď������s��
            overflowOccurred = true;
            HandleBufferOverflow((uint)data.Length);  // �o�b�t�@�I�[�o�[�t���[�̏������s��
        }
    }

    // �o�b�t�@�I�[�o�[�t���[�����������ꍇ�̏���
    private void HandleBufferOverflow(uint overflow)
    {
        // �I�[�o�[�t���[�����������ۂ̌x�����O
        Debug.LogWarning($"�o�b�t�@�I�[�o�[�t���[���������܂���: {overflow} samples discarded.");

        // �I�[�o�[�t���[�����������ꍇ�̓t���O�𗧂Ă�
        overflowOccurred = true;

        // OverflowHandler ���ݒ肳��Ă���΁A���̏������Ăяo��
        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);  // �I�[�o�[�t���[���������s
        }
    }

    // �I�[�o�[�t���[�����������ꍇ�ɕ\�����鏈��
    public void Overflow()
    {
        // �X�s�[�J�[�ڑ��G���[�̌x�����O
        Debug.LogWarning("�X�s�[�J�[���ڑ�����Ă��܂���");

        // �X�s�[�J�[�ڑ��G���[UI���ēx�\��
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    // �I�u�W�F�N�g���j�������Ƃ��̏���
    void OnDestroy()
    {
        // AudioSource �����݂���΁A���\�[�X�����
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}