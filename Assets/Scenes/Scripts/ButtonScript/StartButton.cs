using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム開始ボタンの挙動を管理するクラス
/// </summary>
public class StartButton : MonoBehaviour
{
    public static GameObject Startbutton; // ゲーム開始ボタンのGameObjectを保持する静的変数
    AudioSource StartSound; // ボタンが押されたときに鳴るサウンドのAudioSource

    // Start is called before the first frame update
    void Start()
    {
        // シーン内から「StartButton」オブジェクトを探して、Startbutton変数に格納
        Startbutton = GameObject.Find("StartButton");

        // ゲーム開始ボタンを初期状態では非表示にする
        Startbutton.SetActive(false);

        // AudioSourceコンポーネントを取得し、StartSoundに格納
        StartSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 特に更新する処理はないので、空のまま
    }

    /// <summary>
    /// ゲーム開始ボタンがクリックされたときに呼び出されるメソッド
    /// </summary>
    public void OnStart()
    {
        // ゲーム開始音を再生
        StartSound.PlayOneShot(StartSound.clip);

        // シーンを「GameScene」に遷移させる
        SceneManager.LoadScene("GameScene");
    }
}
