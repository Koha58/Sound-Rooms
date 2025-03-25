using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// GameOver時のUIを接続デバイスによって変更するクラス
/// </summary>
public class GameOverDeviceCheck : MonoBehaviour
{
    // リトライボタン（ゲームパッドまたはキーボードに応じて表示/非表示）
    [SerializeField] private GameObject RetryKey;
    [SerializeField] private GameObject RetryButton;
    // 日本語のバックボタン（ゲームパッドまたはキーボードに応じて表示/非表示）
    [SerializeField] private GameObject JapaneseBackButton;
    [SerializeField] private GameObject JapaneseBackKey;

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態では、すべてのUIイメージを非表示にする
        RetryKey.GetComponent<Image>().enabled = false;
        RetryButton.GetComponent<Image>().enabled = false;
        JapaneseBackButton.GetComponent<Image>().enabled = false;
        JapaneseBackKey.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 入力デバイスがXboxの場合、リトライボタンを表示し、キーボードのリトライキーを非表示にする
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && RetryButton != null)
        {
            RetryButton.GetComponent<Image>().enabled = true;  // Xbox用のボタン表示
            RetryKey.GetComponent<Image>().enabled = false;   // キーボード用のキー非表示
        }
        // 入力デバイスがキーボードの場合、リトライキーを表示し、Xboxのリトライボタンを非表示にする
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && RetryKey != null)
        {
            RetryKey.GetComponent<Image>().enabled = true;     // キーボード用のキー表示
            RetryButton.GetComponent<Image>().enabled = false; // Xboxのリトライボタン非表示
        }

        // 入力デバイスがXboxの場合、日本語のバックボタンを表示し、キーボードのバックキーを非表示にする
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && JapaneseBackButton != null)
        {
            JapaneseBackButton.GetComponent<Image>().enabled = true;  // Xbox用のバックボタン表示
            JapaneseBackKey.GetComponent<Image>().enabled = false;   // キーボード用のバックキー非表示
        }
        // 入力デバイスがキーボードの場合、日本語のバックキーを表示し、Xboxのバックボタンを非表示にする
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && JapaneseBackKey != null)
        {
            JapaneseBackKey.GetComponent<Image>().enabled = true;     // キーボード用のバックキー表示
            JapaneseBackButton.GetComponent<Image>().enabled = false; // Xboxのバックボタン非表示
        }
    }
}