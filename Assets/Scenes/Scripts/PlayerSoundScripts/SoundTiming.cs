using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの音波エフェクトを管理するクラス
/// </summary>
public class SoundTiming : MonoBehaviour
{
    // パーティクルシステムの参照（音に合わせてパーティクルを制御）
    ParticleSystem SoundParticle;

    // LevelMeterスクリプトの参照（音の大きさを取得するため）
    LevelMeter levelMeter;

    void Start()
    {
        // "SoundVolume"という名前のGameObjectをシーンから取得
        GameObject soundobj = GameObject.Find("SoundVolume");

        // LevelMeterコンポーネントを取得（音量情報を取得するため）
        levelMeter = soundobj.GetComponent<LevelMeter>(); // LevelMeterスクリプトを取得

        // "SoundParticle"という名前のGameObjectをシーンから取得
        GameObject SoundEffect = GameObject.Find("SoundParticle");

        // ParticleSystemコンポーネントを取得（パーティクルを制御するため）
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // 初期状態ではパーティクルを停止
        SoundParticle.Stop();
    }

    void Update()
    {
        // "SoundVolume"という名前のGameObjectを再度取得
        GameObject soundobj = GameObject.Find("SoundVolume");

        // LevelMeterコンポーネントを再度取得（音量情報が変動する可能性があるため毎フレーム確認）
        levelMeter = soundobj.GetComponent<LevelMeter>();

        // "SoundParticle"という名前のGameObjectを再度取得
        GameObject SoundEffect = GameObject.Find("SoundParticle");

        // ParticleSystemコンポーネントを再度取得
        SoundParticle = SoundEffect.GetComponent<ParticleSystem>();

        // 音の大きさ（dB）に基づいてパーティクルの状態を制御
        if (levelMeter.nowdB > 0.0f) // 音量が0以上の場合
        {
            // パーティクルが再生中でなければ再生開始
            if (!SoundParticle.isPlaying)
            {
                SoundParticle.Play();
            }
        }
        else // 音量が0未満の場合
        {
            // パーティクルが再生中であれば停止
            if (SoundParticle.isPlaying)
            {
                SoundParticle.Stop();
            }
        }
    }
}