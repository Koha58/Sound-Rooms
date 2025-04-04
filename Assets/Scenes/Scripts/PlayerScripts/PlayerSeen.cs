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
    // プレイヤーの可視状態を管理
    public bool isVisible = false;  // 判定用（プレイヤーが見えていない時：false / 見えている時：true）

    // プレイヤーの親オブジェクト
    [SerializeField] private Transform _parentTransform;

    // 音量を測定するスクリプトのインスタンス
    private LevelMeter levelMeter;

    // ピアノ部屋での挙動判定フラグ
    public bool piano;

    // ピアノ部屋の挙動に関するカウンタ
    private int pianocnt;

    // ピアノ部屋の音量がゼロかどうかを判定するフラグ
    private bool zero;

    // 音量設定を管理するスクリプト
    private AudioSetting AS;

    // 敵参照用プレイヤーが見えるかどうかの状態
    public bool isVisualization;

    // BGMのミュートとする値（音量）
    private int muteBGM = -80;

    // マイクの入力判定用閾値
    private float muteLevel = 0.0f;

    // 偶数判定用定数
    const int EvenNumber = 2;

    void Start()
    {
        // 初期状態の設定
        isVisible = false;  // プレイヤーは最初見えていない
        isVisualization = false;  // 可視化の状態は初期は不可視
        piano = false;  // ピアノ部屋でない
        pianocnt = 0;  // ピアノ部屋の挙動カウント
        zero = false;  // 音量ゼロフラグは初期はオフ
    }

    public void Update()
    {
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeterスクリプトを取得

        // 音を出すことでプレイヤーが見えるようになる
        if (levelMeter.nowdB > muteLevel && !piano)
        {
            isVisible = true;  // 見えている状態に変更
        }

        if (!isVisualization)
        {
            // 音を出していない場合、プレイヤーを見えなくする
            if (isVisible)
            {
                if (levelMeter.nowdB <= muteLevel && !piano)
                {
                    isVisible = false;  // 見えていない状態に変更
                }
            }
        }

        // ピアノ部屋の挙動管理
        if (piano)
        {
            isVisible = true;  // ピアノ部屋ではプレイヤーは見える

            GameObject Setting = GameObject.Find("EventSystem");
            AS = Setting.GetComponent<AudioSetting>(); // AudioSettingスクリプトを取得

            // 音量が最小（-80）の場合、ピアノ部屋の挙動を終了
            if (AS.BGMSlider.value == muteBGM)
            {
                zero = true;  // 音量ゼロを検出
                piano = false;  // ピアノ部屋終了
                isVisible = false;  // 見えない状態に戻す
            }
            else
            {
                piano = true;  // ピアノ部屋
                zero = false;  // 音量ゼロフラグオフ
                isVisible = true;  // プレイヤーは見える
            }
        }
        else
        {
            zero = false;  // ピアノ部屋でない場合
            // ピアノ部屋の挙動カウンタが奇数ならピアノ部屋状態にする
            if (pianocnt % EvenNumber != 0 && AS.BGMSlider.value != muteBGM)
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
                if (pianocnt % EvenNumber == 0)
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
            isVisible = false;  // プレイヤーは見えない状態に戻す
        }
    }
}