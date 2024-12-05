using UnityEngine;
using UnityEngine.UI;

public class ClickToRecordAndVisualize : MonoBehaviour
{
    public RawImage waveformImage;
    public AudioSource audioSource;
    public int recordingDuration = 10;
    private Texture2D waveformTexture;
    private int textureWidth = 512;
    private int textureHeight = 256;
    private float[] audioSamples;
    private string microphoneDevice;
    private bool isRecording = false;
    private bool isPlaying = false;
    private AudioClip recordedClip;
    private float recordingStartTime;

    void Start()
    {
        waveformTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        waveformImage.texture = waveformTexture;
        audioSamples = new float[textureWidth];
        microphoneDevice = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isRecording && recordedClip == null) StartRecording();
            else if (!isRecording && recordedClip != null && !isPlaying) StartPlayback();
        }

        if (isRecording)
        {
            DrawWaveformFromMicrophone();
            if (Time.time - recordingStartTime >= recordingDuration) StopRecording();
        }

        if (isPlaying)
        {
            DrawWaveformFromAudioSource();
            if (!audioSource.isPlaying)
            {
                isPlaying = false;
                ClearWaveform();
            }
        }
    }

    void StartRecording()
    {
        if (isRecording || microphoneDevice == null) return;
        recordedClip = Microphone.Start(microphoneDevice, false, recordingDuration, 44100);
        isRecording = true;
        recordingStartTime = Time.time;
        Debug.Log("ò^âπÇäJénÇµÇ‹ÇµÇΩ");
    }

    void StopRecording()
    {
        if (!isRecording) return;
        Microphone.End(microphoneDevice);
        isRecording = false;
        audioSource.clip = recordedClip;
        Debug.Log("ò^âπÇí‚é~ÇµÇ‹ÇµÇΩ");
    }

    void StartPlayback()
    {
        if (recordedClip == null) return;
        audioSource.Play();
        isPlaying = true;
        Debug.Log("ò^âπÇµÇΩâπê∫Ççƒê∂ÇµÇ‹Ç∑");
    }

    void DrawWaveformFromMicrophone()
    {
        if (recordedClip == null || !Microphone.IsRecording(microphoneDevice)) return;
        int micPosition = Microphone.GetPosition(microphoneDevice);
        if (micPosition <= 0) return;
        float[] tempSamples = new float[audioSamples.Length];
        recordedClip.GetData(tempSamples, Mathf.Max(0, micPosition - tempSamples.Length));
        for (int i = 0; i < audioSamples.Length; i++) audioSamples[i] = tempSamples[i];
        DrawWaveform(audioSamples);
    }

    void DrawWaveformFromAudioSource()
    {
        if (!audioSource.isPlaying) return;
        audioSource.GetOutputData(audioSamples, 0);
        DrawWaveform(audioSamples);
    }

    void DrawWaveform(float[] samples)
    {
        Color[] colors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < colors.Length; i++) colors[i] = Color.black;
        waveformTexture.SetPixels(colors);
        for (int x = 0; x < samples.Length; x++)
        {
            float sample = samples[x];
            int y = Mathf.Clamp((int)((sample + 1.0f) * 0.5f * textureHeight), 0, textureHeight - 1);
            waveformTexture.SetPixel(x, y, Color.green);
        }
        waveformTexture.Apply();
    }

    void ClearWaveform()
    {
        Color[] colors = new Color[textureWidth * textureHeight];
        for (int i = 0; i < colors.Length; i++) colors[i] = Color.black;
        waveformTexture.SetPixels(colors);
        waveformTexture.Apply();
    }
}
