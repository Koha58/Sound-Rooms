using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// TutorialSceneでのライフ管理クラス
/// </summary>
public class TutorialGameOver : MonoBehaviour
{
    public int LifeCount;  // プレイヤーの残りライフ数
    [SerializeField] GameObject Life1;  // ライフ1のUIオブジェクト
    [SerializeField] GameObject Life2;  // ライフ2のUIオブジェクト
    [SerializeField] GameObject Life3;  // ライフ3のUIオブジェクト
    [SerializeField] GameObject Life4;  // ライフ4のUIオブジェクト
    [SerializeField] GameObject Life5;  // ライフ5のUIオブジェクト
    [SerializeField] GameObject LostLife1;  // 失われたライフ1のUIオブジェクト
    [SerializeField] GameObject LostLife2;  // 失われたライフ2のUIオブジェクト
    [SerializeField] GameObject LostLife3;  // 失われたライフ3のUIオブジェクト
    [SerializeField] GameObject LostLife4;  // 失われたライフ4のUIオブジェクト
    [SerializeField] GameObject LostLife5;  // 失われたライフ5のUIオブジェクト

    private float Timer;  // タイマー (ライフを失ってから次の処理までの時間を計測)
    private float Count;  // カウント (ライフを失った後の待機フラグ)

    // Start is called before the first frame update
    void Start()
    {
        // 初期設定として、ライフのUIを全て表示
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // 失われたライフのUIは最初は非表示
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ライフ数を5に設定
        LifeCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーがライフを失った後、待機している状態
        if (Count == 1)
        {
            Timer += Time.deltaTime;  // 経過時間を加算
            if (Timer >= 3)  // 3秒経過したら
            {
                Timer = 0;  // タイマーをリセット
                Count = 0;  // 待機状態を解除
            }
        }
    }

    // トリガーに入ったときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        PlayerSeen PS;
        GameObject gobj = GameObject.Find("Player");  // プレイヤーオブジェクトを探す
        PS = gobj.GetComponent<PlayerSeen>();  // PlayerSeenスクリプトを取得

        // プレイヤーのライフに応じてUIを変更
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // ライフ5を非表示
            LostLife5.GetComponent<Image>().enabled = true;  // 失われたライフ5を表示
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // ライフ4を非表示
            LostLife4.GetComponent<Image>().enabled = true;  // 失われたライフ4を表示
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // ライフ3を非表示
            LostLife3.GetComponent<Image>().enabled = true;  // 失われたライフ3を表示
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // ライフ2を非表示
            LostLife2.GetComponent<Image>().enabled = true;  // 失われたライフ2を表示
        }
        else if (LifeCount == 0)
        {
            Life1.GetComponent<Image>().enabled = false;  // ライフ1を非表示
            LostLife1.GetComponent<Image>().enabled = true;  // 失われたライフ1を表示
            SceneManager.LoadScene("GameOver_Tutorial");  // ゲームオーバー画面に遷移
        }
    }
}