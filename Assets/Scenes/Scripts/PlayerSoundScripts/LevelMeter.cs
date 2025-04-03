using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音量レベルを示すメーター（レベルメーター）を管理するクラス
/// </summary>
public class LevelMeter : MonoBehaviour
{
    // 更新する対象のlevelMeter (uGUI Image)
    Image levelMeterImage = null;

    // このdBでlevelMeter表示の下限に到達する
    [SerializeField]
    private float dB_Min = -60.0f;  // 最小dB（音量の下限）

    // このdBでlevelMeter表示の上限に到達する
    [SerializeField]
    private float dB_Max = -0.0f;   // 最大dB（音量の上限）

    // dBを取得する対象のmicAudioSource
    [SerializeField]
    public MicAudioSource micAS = null;  // マイク音声のデータを取得するMicAudioSource

    public float nowdB;  // 現在のdB値

    // ゲームオブジェクトがアクティブになる前に呼ばれる
    void Awake()
    {
        // 更新する対象のImage（レベルメーターのUI）を取得
        levelMeterImage = GetComponent<Image>();
    }

    void Start()
    {
        // MicAudioSourceコンポーネントをシーンから取得
        micAS = FindObjectOfType<MicAudioSource>();
    }

    void Update()
    {
        // micASから現在のdB値を取得し、それをfillAmountに変換
        float fillAmountValue = dB_ToFillAmountValue(micAS.now_dB);

        // レベルメーターのfillAmountを更新（表示の進捗具合）
        this.levelMeterImage.fillAmount = fillAmountValue;

        // 現在のdB値を格納
        nowdB = fillAmountValue;

        // dBが0より大きければ、レベルメーターの色を変更（音量が大きい場合）
        if (nowdB > 0f)
        {
            // レベルメーターが音量に応じて色を変更
            levelMeterImage.color = new Color32(255, 255, 255, 154);
        }
    }

    /// <summary>
    /// dB_MinとdB_Maxに基づいてdBをfillAmount値に変換
    /// </summary>
    /// <param name="dB">現在のdB値</param>
    /// <returns>fillAmount値（0.0fから1.0fの範囲）</returns>
    float dB_ToFillAmountValue(float dB)
    {
        // 入力されたdBをdB_MaxとdB_Min値で切り捨て（範囲内に収める）
        float modified_dB = dB;
        if (modified_dB > dB_Max) { modified_dB = dB_Max; }   // dBが最大値より大きければ最大値に設定
        else if (modified_dB < dB_Min) { modified_dB = dB_Min; }  // dBが最小値より小さければ最小値に設定

        // dBをfillAmount（0.0fから1.0f）の範囲に変換
        // dB_Min = 0.0f, dB_Max = 1.0f という変換式
        float fillAountValue = 1.0f + (modified_dB / (dB_Max - dB_Min));
        return fillAountValue;  // 計算したfillAmount値を返す
    }
}