using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Stage1Sceneでのライフ管理クラス
/// </summary>
public class Stage1GameOver : MonoBehaviour
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
        // プレイヤーの初期ライフアイコンを全て表示
        Life1.GetComponent<Image>().enabled = true;
        Life2.GetComponent<Image>().enabled = true;
        Life3.GetComponent<Image>().enabled = true;
        Life4.GetComponent<Image>().enabled = true;
        Life5.GetComponent<Image>().enabled = true;

        // 失ったライフのアイコンは初期状態で非表示
        LostLife1.GetComponent<Image>().enabled = false;
        LostLife2.GetComponent<Image>().enabled = false;
        LostLife3.GetComponent<Image>().enabled = false;
        LostLife4.GetComponent<Image>().enabled = false;
        LostLife5.GetComponent<Image>().enabled = false;

        // ライフ数の初期値を5に設定
        LifeCount = 5;
        Count = 0;  // カウントを0にリセット
    }

    // Update is called once per frame
    void Update()
    {
        // カウントが1の場合、5秒間の待機タイムを設定
        if (Count == 1)
        {
            Timer += Time.deltaTime;  // タイマーを経過時間で更新
            if (Timer >= 5.0f)  // 5秒経過したら
            {
                Timer = 0;  // タイマーをリセット
                Count = 0;  // カウントを0に戻す
            }
        }
    }

    // トリガーに入ったときに呼び出される
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーオブジェクトを取得
        GameObject gobj = GameObject.Find("Player");
        PlayerSeen PS = gobj.GetComponent<PlayerSeen>();

        // 敵に接触した場合の処理
        if (other.CompareTag("Enemy"))
        {
            // プレイヤーが見えている状態（onoff == 1）のときだけライフが減る
            if (PS.onoff == 1)
            {
                if (Count == 0)  // まだライフを減らしていない場合
                {
                    LifeCount--;  // ライフを1減らす
                    Count = 1;  // 1回目のライフ減少をカウント
                }
            }
        }

        // ライフ数に応じて表示するアイコンを変更
        if (LifeCount == 4)
        {
            Life5.GetComponent<Image>().enabled = false;  // 5番目のライフアイコンを非表示
            LostLife5.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示
        }
        else if (LifeCount == 3)
        {
            Life4.GetComponent<Image>().enabled = false;  // 4番目のライフアイコンを非表示
            LostLife4.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示
        }
        else if (LifeCount == 2)
        {
            Life3.GetComponent<Image>().enabled = false;  // 3番目のライフアイコンを非表示
            LostLife3.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示
        }
        else if (LifeCount == 1)
        {
            Life2.GetComponent<Image>().enabled = false;  // 2番目のライフアイコンを非表示
            LostLife2.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示
        }
        else if (LifeCount == 0)
        {
            // 最後のライフを失った場合
            Life1.GetComponent<Image>().enabled = false;  // 1番目のライフアイコンを非表示
            LostLife1.GetComponent<Image>().enabled = true;  // 失われたライフアイコンを表示
            SceneManager.LoadScene("GameOver_Stage1");  // ゲームオーバー画面へ遷移
        }
    }
}
