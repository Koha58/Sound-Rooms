using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// StartSceneのUI表示をデバイスによって切り替えるクラス
/// </summary>
public class StartDeviceCheck : MonoBehaviour
{
    GameObject Cursor; // カーソルオブジェクトを格納するための変数

    // Start is called before the first frame update
    void Start()
    {
        // "Cursor"という名前のGameObjectをシーンから取得し、Cursor変数に格納
        Cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の入力デバイスがXboxの場合、カーソルを表示
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = true; // カーソルのImageコンポーネントを有効にして表示
        }
        // 現在の入力デバイスがキーボードの場合、カーソルを非表示
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard && Cursor != null)
        {
            Cursor.GetComponent<Image>().enabled = false; // カーソルのImageコンポーネントを無効にして非表示
        }
    }
}
