using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スピーカーが接続されていない場合に、エラー表示をさせるクラス
/// </summary>
public class AudioOutputChecker : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource コンポーネント
    private AudioClip audioClip;  // カスタムオーディオクリップ
    private bool overflowOccurred = false; // オーバーフロー発生フラグ
    private OverflowHandler overflowHandler;  // オーバーフロー処理を担当するハンドラ

    [SerializeField] private GameObject SpeakerConnectionBadUI;  // スピーカー接続エラーUI

    // Start is called before the first frame update
    void Start()
    {
        // AudioSource コンポーネントを追加
        audioSource = gameObject.AddComponent<AudioSource>();

        // カスタムオーディオクリップを作成（44100サンプル、1チャンネル、44100Hzで再生）
        audioClip = AudioClip.Create("CustomAudioClip", 44100, 1, 44100, true, OnAudioRead);

        // AudioSource にクリップを設定
        audioSource.clip = audioClip;

        // オーディオソースをミュートに設定（再生しても音は出さない）
        audioSource.mute = true;

        // 初期状態でオーバーフローが発生していると設定
        overflowOccurred = true;

        // オーディオソースを再生開始
        audioSource.Play();

        // スピーカー接続エラーUIを表示
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 3秒後に実行する処理を開始（ただし現在は何もしていない）
        StartCoroutine(WaitAndExecute());

        // スピーカー接続エラーUIを非表示にする
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = false;

        // オーバーフローが発生している場合、Overflow メソッドを実行
        if (overflowOccurred)
        {
            Overflow();
        }
    }

    // 3秒待ってから実行する処理を開始するコルーチン
    private System.Collections.IEnumerator WaitAndExecute()
    {
        // 3秒間待機
        yield return new WaitForSeconds(3f);
    }

    // オーディオデータを生成するメソッド
    private void OnAudioRead(float[] data)
    {
        // サイン波（440Hz）をオーディオデータとして生成
        for (int i = 0; i < data.Length; i++)
        {
            // 440Hzのサイン波を生成してデータに設定
            data[i] = Mathf.Sin(2 * Mathf.PI * 440 * (i / 44100f)); // 440Hzのサイン波
        }
    }

    // オーディオフィルタの読み取りメソッド（オーバーフローの検出）
    private void OnAudioFilterRead(float[] data, int channels)
    {
        // オーディオデータの長さが1024サンプルより大きい場合、オーバーフローが発生していないと判断
        if (data.Length > 1024)
        {
            overflowOccurred = false;  // オーバーフローが発生していないのでフラグをfalseに
            //Debug.Log("OK");  // 正常終了のログ
        }
        else
        {
            // オーバーフローが発生した場合、フラグを立てて処理を行う
            overflowOccurred = true;
            HandleBufferOverflow((uint)data.Length);  // バッファオーバーフローの処理を行う
        }
    }

    // バッファオーバーフローが発生した場合の処理
    private void HandleBufferOverflow(uint overflow)
    {
        // オーバーフローが発生した際の警告ログ
        Debug.LogWarning($"バッファオーバーフローが発生しました: {overflow} samples discarded.");

        // オーバーフローが発生した場合はフラグを立てる
        overflowOccurred = true;

        // OverflowHandler が設定されていれば、その処理を呼び出す
        if (overflowHandler != null)
        {
            overflowHandler.OnBufferOverflow(overflow);  // オーバーフロー処理を実行
        }
    }

    // オーバーフローが発生した場合に表示する処理
    public void Overflow()
    {
        // スピーカー接続エラーの警告ログ
        Debug.LogWarning("スピーカーが接続されていません");

        // スピーカー接続エラーUIを再度表示
        SpeakerConnectionBadUI.GetComponent<Image>().enabled = true;
    }

    // オブジェクトが破棄されるときの処理
    void OnDestroy()
    {
        // AudioSource が存在すれば、リソースを解放
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
    }
}