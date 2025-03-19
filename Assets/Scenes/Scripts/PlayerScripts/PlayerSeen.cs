using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// プレイヤーの可視化・不可視化を管理するクラス
/// </summary>
public class PlayerSeen : MonoBehaviour
{
    public int onoff = 0;  // 判定用（プレイヤーが見えていない時：0 / 見えている時：1）

    [SerializeField] public Transform _parentTransform; // プレイヤーの親オブジェクト
    LevelMeter levelMeter; // 音量を測定するスクリプト
    public bool piano; // ピアノ部屋での挙動判定フラグ
    int pianocnt; // ピアノ部屋の挙動に関するカウンタ
    public bool zero; // ピアノ部屋の音量がゼロかどうかを判定するフラグ
    AudioSetting AS; // 音量設定を管理するスクリプト

    public bool Visualization; // プレイヤーが見えるかどうかの状態

    void Start()
    {
        // 初期状態の設定
        onoff = 0;  // プレイヤーは最初見えていない
        Visualization = false;  // 可視化の状態は初期は不可視
        piano = false;  // ピアノ部屋でない
        pianocnt = 0;  // ピアノ部屋の挙動カウント
        zero = false;  // 音量ゼロフラグは初期はオフ
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeterスクリプトを取得

        // 音を出すことでプレイヤーが見えるようになる
        if (levelMeter.nowdB > 0.0f && !piano)
        {
            onoff = 1;  // 見えている状態に変更
        }

        if (Visualization == false)
        {
            // 音を出していない場合、プレイヤーを見えなくする
            if (onoff == 1)
            {
                if (levelMeter.nowdB <= 0.0f && !piano)
                {
                    onoff = 0;  // 見えていない状態に変更
                }
            }
        }

        // ピアノ部屋の挙動管理
        if (piano)
        {
            onoff = 1;  // ピアノ部屋ではプレイヤーは見える

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>(); // AudioSettingスクリプトを取得

            // 音量が最小（-80）の場合、ピアノ部屋の挙動を終了
            if (AS.BGMSlider.value == -80)
            {
                zero = true;  // 音量ゼロを検出
                piano = false;  // ピアノ部屋終了
                onoff = 0;  // 見えない状態に戻す
            }
            else
            {
                piano = true;  // ピアノ部屋
                zero = false;  // 音量ゼロフラグオフ
                onoff = 1;  // プレイヤーは見える
            }
        }
        else
        {
            zero = false;  // ピアノ部屋でない場合
            // ピアノ部屋の挙動カウンタが奇数ならピアノ部屋状態にする
            if (pianocnt % 2 != 0 && AS.BGMSlider.value != -80)
            {
                piano = true;
            }
        }
    }

    // プレイヤーが「PianoCheck」タグのオブジェクトと衝突した時
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            pianocnt++;  // ピアノ部屋挙動カウンタを増加
            if (!zero)
            {
                piano = true;  // ピアノ部屋状態にする

                // カウントが偶数ならピアノ部屋を終了
                if (pianocnt % 2 == 0)
                {
                    piano = false;
                }
            }
        }
    }

    // プレイヤーが「RoomOut」タグのオブジェクト内にいるとき
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RoomOut"))
        {
            onoff = 0;  // プレイヤーは見えない状態に戻す
        }
    }
}