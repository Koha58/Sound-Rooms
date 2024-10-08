using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip audioClip;
    public OverflowHandler overflowHandler; // �I�[�o�[�t���[�����p�̃N���X�ւ̎Q��

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);

        audioSource.clip = audioClip;
        audioSource.Play();
    }

    private void OnAudioRead(float[] data)
    {
        // �I�[�f�B�I�f�[�^���������鏈��
        // �����ɃI�[�f�B�I�f�[�^�̐�����擾�̃R�[�h���L�q
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (data.Length > 1024) // �C�ӂ̃T�C�Y���w��
        {
            Debug.Log("OK"); 
            HandleBufferOverflow((uint)data.Length);
        }
    }

    private void HandleBufferOverflow(uint overflow)
    {
        Debug.LogWarning($"�o�b�t�@�I�[�o�[�t���[���������܂���: {overflow} samples discarded.");

        // OverflowHandler�̃��\�b�h���Ăяo��
        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);
        }
    }

    void OnDestroy()
    {
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}
