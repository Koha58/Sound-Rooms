using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

/// <summary>
/// オプション画面で音量やマウス感度などを調整するための機能を提供するクラス
/// </summary>
public class AudioSetting : MonoBehaviour
{
    // 定数定義
    private const float MouseSensitivityDivisor = 50f;  // Y軸の感度調整用
    private const float MouseSensitivityMultiplier = 50f;  // X軸の感度調整用
    private const float DefaultMicVolume = 0.5f;  // 初期のマイク音量（デフォルト値）

    // オーディオミキサー、マイクオブジェクト、ボリューム設定用の変数
    [SerializeField] AudioMixer audioMixer;      // オーディオミキサー
    [SerializeField] GameObject micObject;       // マイクのオブジェクト
    public float volume;                         // ボリューム

    // CinemachineFreeLook カメラ（マウス感度設定用）
    public CinemachineFreeLook VCamera;

    // UIのスライダー（各音量調整用）
    [SerializeField] Slider MicSlider;          // マイク音量用スライダー
    [SerializeField] public Slider BGMSlider;   // BGM音量用スライダー
    [SerializeField] Slider SESlider;           // SE音量用スライダー
    [SerializeField] Slider MouseSlider;        // マウス感度用スライダー

    // Start is called before the first frame update
    private void Start()
    {
        // マイクのAudioSourceコンポーネントを取得
        AudioSource Mic = micObject.GetComponent<AudioSource>();

        // マイク音量をスライダーに反映（デフォルト値を使用）
        MicSlider.value = DefaultMicVolume;

        // マウス感度の設定（VCameraのY軸の最大速度をスライダー値に基づいて設定）
        MouseSlider.value = VCamera.m_YAxis.m_MaxSpeed;

        // VCameraのX軸の最大速度を設定（固定値）
        VCamera.m_XAxis.m_MaxSpeed = MouseSensitivityMultiplier;

        // オーディオミキサーのBGMのボリュームをスライダーに設定
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;

        // オーディオミキサーのSEのボリュームをスライダーに設定
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }

    // BGM音量を設定するメソッド
    public void SetBGM(float volume)
    {
        // オーディオミキサーでBGMの音量を設定
        audioMixer.SetFloat("BGM", volume);
    }

    // SE音量を設定するメソッド
    public void SetSE(float volume)
    {
        // オーディオミキサーでSEの音量を設定
        audioMixer.SetFloat("SE", volume);
    }

    // マイク音量を設定するメソッド
    public void SetMic(float volume)
    {
        // マイクのAudioSourceコンポーネントを取得
        AudioSource Mic = micObject.GetComponent<AudioSource>();

        // マイクの音量をスライダーの値に設定
        Mic.volume = MicSlider.value;
    }

    // マウス感度を設定するメソッド
    public void SetMouse(float level)
    {
        // VCameraのY軸の最大速度をスライダー値を基に調整
        VCamera.m_YAxis.m_MaxSpeed = level / MouseSensitivityDivisor;

        // VCameraのX軸の最大速度をスライダー値を基に調整
        VCamera.m_XAxis.m_MaxSpeed = level * MouseSensitivityMultiplier;
    }
}