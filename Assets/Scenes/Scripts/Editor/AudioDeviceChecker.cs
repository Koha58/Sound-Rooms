using UnityEditor;
using UnityEngine;
using System.Linq;

// Unityエディタが起動したタイミングでこのクラスを初期化する
[InitializeOnLoad]

/// <summary>
/// マイク、スピーカーが接続されているか判定するスクリプト
/// </summary>
public class AudioDeviceMonitor
{
    // 前回のマイク接続状態を保持
    private static bool lastMicrophoneState = false;

    // 前回のスピーカー接続状態を保持
    private static bool lastSpeakersState = false;

    // クラスが読み込まれたときに実行される静的コンストラクタ
    static AudioDeviceMonitor()
    {
        // エディタの毎フレーム更新に合わせてデバイスチェックを実行
        EditorApplication.update += CheckAudioDevices;
    }

    // マイクとスピーカーの接続状態を確認し、状態に応じてログを出力
    private static void CheckAudioDevices()
    {
        // 現在のマイク・スピーカーの接続状態を取得
        bool isMicrophoneConnected = CheckMicrophone();
        bool isSpeakersConnected = CheckSpeakers();

        // マイクの状態に変化があった場合にログ出力
        if (isMicrophoneConnected != lastMicrophoneState)
        {
            if (isMicrophoneConnected)
            {
                Debug.Log("マイクが接続されました。");
            }
            else
            {
                Debug.LogWarning("マイクが切断されました！");
            }
            lastMicrophoneState = isMicrophoneConnected;
        }

        // スピーカーの状態に変化があった場合にログ出力
        if (isSpeakersConnected != lastSpeakersState)
        {
            if (isSpeakersConnected)
            {
                Debug.Log("スピーカーが接続されました。");
            }
            else
            {
                Debug.LogWarning("スピーカーが切断されました！");
            }
            lastSpeakersState = isSpeakersConnected;
        }

        // 両方とも接続されていないときの注意ログ（補足）
        if (!isMicrophoneConnected && !isSpeakersConnected)
        {
            Debug.LogWarning("マイクとスピーカーの両方が接続されていません！");
        }
        else if (!isMicrophoneConnected)
        {
            Debug.LogWarning("マイクが接続されていません！");
        }
        else if (!isSpeakersConnected)
        {
            Debug.LogWarning("スピーカーが接続されていません！");
        }
    }

    // マイクの接続確認
    private static bool CheckMicrophone()
    {
        // Microphone.devices は接続されているマイクの一覧を返す
        // 空でない名前のデバイスがあれば「接続されている」と判断
        return Microphone.devices.Any(device => !string.IsNullOrEmpty(device));
    }

    // スピーカーの接続確認（簡易的な音出力チェック）
    private static bool CheckSpeakers()
    {
        // 一時的な GameObject を作って AudioSource を追加
        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();

        // 1サンプルだけのダミーAudioClipを作成
        AudioClip clip = AudioClip.Create("test", 1, 1, 44100, false);
        audioSource.clip = clip;
        audioSource.Play();

        // 再生が成功しているかどうかでスピーカーの接続を判定
        bool isPlaying = audioSource.isPlaying;

        // エディタ上では Destroy ではなく DestroyImmediate を使用
        Object.DestroyImmediate(audioSource.gameObject);

        return isPlaying;
    }
}
