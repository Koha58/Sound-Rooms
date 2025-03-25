using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ピアノの音をミュートにさせるクラス
/// </summary>
public class PianoMute : MonoBehaviour
{
    private AudioSource audioSource;  // AudioSource コンポーネントを格納する変数
    PlayerSeen PS;  // PlayerSeen スクリプトを格納する変数

    // Start はスクリプトが開始されるときに最初に実行される
    private void Start()
    {
        // AudioSource コンポーネントをこのオブジェクトから取得
        audioSource = GetComponent<AudioSource>();

        // "Player" オブジェクトをシーンから検索して取得
        GameObject Player = GameObject.Find("Player");

        // "Player" オブジェクトにアタッチされている PlayerSeen スクリプトを取得
        PS = Player.GetComponent<PlayerSeen>();

        // 初期状態で音声をミュートに設定
        audioSource.mute = true;
    }

    // Update は毎フレーム呼び出される
    void Update()
    {
        // PlayerSeen スクリプト内の piano 変数が false の場合、音声をミュート
        if (PS.piano == false)
        {
            audioSource.mute = true;  // ミュート
        }
        else
        {
            // piano が true の場合、音声をミュート解除
            audioSource.mute = false;  // ミュート解除
        }
    }
}
