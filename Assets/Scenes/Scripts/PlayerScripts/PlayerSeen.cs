using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーの可視・不可視状態を管理するクラス
/// 音量やピアノ部屋の状態によって可視化を切り替える
/// </summary>
public class PlayerSeen : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが見えているかどうか（true: 見えている / false: 見えていない）
    /// </summary>
    public bool isVisible = false;

    /// <summary>
    /// 音量測定スクリプト
    /// </summary>
    private LevelMeter levelMeter;

    /// <summary>
    /// 音量設定スクリプト
    /// </summary>
    private AudioSetting AS;

    /// <summary>
    /// プレイヤーがピアノ部屋にいるかどうか
    /// </summary>
    public bool piano;

    /// <summary>
    /// 敵がプレイヤーを視認できるかどうか
    /// </summary>
    public bool isVisualization;

    /// <summary>
    /// BGMをミュートと判定する値（dB）
    /// </summary>
    private int muteBGM = -80;

    /// <summary>
    /// マイク入力を無音とする閾値（dB）
    /// </summary>
    private float muteLevel = 0.0f;

    public bool overrideVisibility = false; //自動制御を一時停止する

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        isVisible = false;         // 初期状態は見えない
        isVisualization = false;   // 敵からも初期は見えない
        piano = false;             // ピアノ部屋にいない
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        if (overrideVisibility)
        {
            // 強制状態なので、他のロジックをスキップ
            return;
        }

        // 必要なら LevelMeter を探す
        if (levelMeter == null)
        {
            var soundobj = GameObject.Find("SoundVolume");
            if (soundobj != null)
                levelMeter = soundobj.GetComponent<LevelMeter>();
        }

        // 必要なら AudioSetting を探す
        if (AS == null)
        {
            var Setting = GameObject.Find("EventSystem");
            if (Setting != null)
                AS = Setting.GetComponent<AudioSetting>();
        }

        // --- ピアノ部屋以外の場合の処理 ---
        if (!piano)
        {
            // 音量が閾値を超えていれば可視化
            if (levelMeter != null && levelMeter.nowdB > muteLevel)
            {
                isVisible = true;
            }
            // 音がないかつ敵が視認できない設定なら不可視化
            else if (!isVisualization)
            {
                isVisible = false;
            }
        }

        // --- ピアノ部屋の場合の処理 ---
        if (piano)
        {
            isVisible = true;  // ピアノ部屋にいる間は必ず可視化

            // BGMがミュートなら部屋終了処理
            if (AS != null && AS.BGMSlider.value == muteBGM)
            {
                isVisible = false; // 可視化をオフ
            }
            else
            {
                isVisible = true; // 可視化をオン
            }
        }
    }

    /// <summary>
    /// PianoCheck タグの Trigger に入ったとき呼ばれる
    /// （部屋に入った判定）
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            piano = true;  // ピアノ部屋状態にする
            Debug.Log("ピアノ部屋に入った！");
        }
    }

    /// <summary>
    /// PianoCheck タグの Trigger から出たとき呼ばれる
    /// （部屋から出た判定）
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PianoCheck"))
        {
            piano = false;      // ピアノ部屋状態を解除
            isVisible = false;  // 可視化をオフ
            Debug.Log("ピアノ部屋から出た！");
        }
    }
}
