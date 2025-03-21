using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マイクの接続を管理するクラス
/// </summary>
public class GetDeviceCheck : MonoBehaviour
{
    // マイク接続チェック用フラグ
    bool micCheck = false;

    // マイク接続エラーメッセージを表示するUIオブジェクト
    [SerializeField] private GameObject MicConnectionBadUI;

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態ではマイクが接続されていないと仮定
        micCheck = false;

        // 初期状態ではエラーメッセージUIを非表示
        MicConnectionBadUI.GetComponent<Image>().enabled = false;

        // マイクデバイスを検出して、デバイス名をログに出力
        foreach (string device in Microphone.devices)
        {
            // 各マイクのデバイス名をログに表示
            Debug.Log("Name: " + device);

            // マイクデバイスが見つかったら、接続フラグをtrueに設定
            micCheck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレーム、マイクが接続されているか再チェック
        micCheck = false;

        // マイクデバイスのリストを再度確認
        foreach (string device in Microphone.devices)
        {
            //// 各マイクのデバイス名をログに表示
            //Debug.Log("Name: " + device);

            // マイクが接続されている場合、接続フラグをtrueに設定
            micCheck = true;

            // マイクが接続されていればエラーメッセージUIを非表示に
            MicConnectionBadUI.GetComponent<Image>().enabled = false;
        }

        // マイクが接続されていない場合、エラーメッセージUIを表示
        if (!micCheck)
        {
            //// マイクが接続されていないことをログに表示
            //Debug.Log("マイクが接続されていません");

            // エラーメッセージUIを表示
            MicConnectionBadUI.GetComponent<Image>().enabled = true;
        }
    }
}
