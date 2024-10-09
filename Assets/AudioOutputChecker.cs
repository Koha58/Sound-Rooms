using UnityEngine;

public class AudioOutputChecker : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioClip;
    public bool overflowOccurred = false; // �I�[�o�[�t���[�����t���O
    public OverflowHandler overflowHandler;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);
        audioSource.clip = audioClip;
        audioSource.mute = true;
        overflowOccurred = true; // �I�[�o�[�t���[�����t���O
        audioSource.Play();
    }

    void Update()
    {
        // 1�b�҂��Ă�����s���鏈�����J�n
        StartCoroutine(WaitAndExecute());

        // ���t���[���I�[�o�[�t���[���������Ă��Ȃ����m�F
        if (overflowOccurred)
        {
            Overflow();
        }
    }

    private System.Collections.IEnumerator WaitAndExecute()
    {
        // 1�b�ҋ@
        yield return new WaitForSeconds(3f);
    }

    private void OnAudioRead(float[] data)
    {
        // �I�[�f�B�I�f�[�^�𐶐����鏈��
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * 440 * (i / 44100f)); // 440Hz�̃T�C���g
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (data.Length > 1024) // �C�ӂ̃T�C�Y���w��
        {
            overflowOccurred = false;
            Debug.Log("OK");
        }
        else
        {
            overflowOccurred = true; // �I�[�o�[�t���[���������Ȃ������ꍇ�̓t���O�����Z�b�g
            HandleBufferOverflow((uint)data.Length);
        }
    }

    private void HandleBufferOverflow(uint overflow)
    {
        Debug.LogWarning($"�o�b�t�@�I�[�o�[�t���[���������܂���: {overflow} samples discarded.");
        overflowOccurred = true; // �I�[�o�[�t���[�����������ꍇ�t���O�𗧂Ă�

        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);
        }
    }

    public void Overflow()
    {
        // �I�[�o�[�t���[���������Ȃ������ꍇ�Ɏ��s���鏈��
        Debug.LogError("�X�s�[�J�[���ڑ�����Ă��܂���");
    }

    void OnDestroy()
    {
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}
