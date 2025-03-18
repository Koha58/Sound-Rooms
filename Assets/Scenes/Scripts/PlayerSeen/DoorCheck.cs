using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドアの操作を管理するクラス
public class DoorCheck : MonoBehaviour
{
    LevelMeter levelMeter;  // 音量を管理するLevelMeterスクリプトの参照

    bool OnOff; // ドアが有効かどうかを管理するフラグ

    GameObject Rote; // 回転するドアオブジェクト

    public float rotateAngle; // ドアの回転角度
    public float rotateSpeed; // ドアの回転速度

    public bool Right; // 右回転しているかどうかのフラグ

    [SerializeField] AudioSource RollingDoorSound; // 回転ドアの音を管理するAudioSource

    ParticleSystem EF; // ドアのエフェクト（パーティクル）

    void Start()
    {
        // ドアの初期状態を設定
        GetComponent<Collider>().enabled = false; // コライダーを無効化してドアを見えなくする
        OnOff = false; // ドアが表示されない状態にする
        Right = false; // 右回転していない状態にする

        // ドアのパーティクルエフェクトを初期化
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();
        EF.Stop(); // 最初はエフェクトを停止しておく
    }

    private void Update()
    {
        // SoundVolumeオブジェクトを検索し、音量を管理しているLevelMeterスクリプトを取得
        GameObject soundobj = GameObject.Find("SoundVolume");
        levelMeter = soundobj.GetComponent<LevelMeter>();

        // ドアのパーティクルエフェクトを更新
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        // 音量がゼロより大きいときにドアを回転可能にする
        if (levelMeter.nowdB > 0.0f)
        {
            GetComponent<Collider>().enabled = true; // コライダーを有効にしてドアを回転可能にする
            OnOff = true; // ドアを回転可能にする
        }

        // 音量がゼロに戻ったときにドアを回転不可にし、エフェクトと音を停止
        if (OnOff == true)
        {
            if (levelMeter.nowdB == 0.0f)
            {
                GetComponent<Collider>().enabled = false; // コライダーを無効化してドアを回転不可にする
                OnOff = false; // ドアを回転不可にする
                EF.Stop(); // パーティクルエフェクトを停止
                RollingDoorSound.Stop(); // 回転ドアの音を停止
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 右または左のドアがトリガー内に入ったときにエフェクトと音を開始
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        if (other.CompareTag("Right"))//右回転
        {
            EF.Play(); // パーティクルエフェクトを再生
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip); // 回転ドアの音を再生
        }
        else if (other.CompareTag("Left") && !Right)//左回転
        {
            EF.Play(); // パーティクルエフェクトを再生
            RollingDoorSound.PlayOneShot(RollingDoorSound.clip); // 回転ドアの音を再生
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Right"))
        {
            // 右方向に回転するドアを操作
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0); // 左回転
            Right = true; // 右回転中であることを示す
        }
        else if (other.CompareTag("Left") && !Right)
        {
            // 左方向に回転するドアを操作
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, rotateAngle * Time.deltaTime * rotateSpeed, 0); // 右回転
            Right = false; // 左回転中であることを示す
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // トリガーから出たときに回転を停止し、エフェクトも停止
        GameObject RotationDoorEffect = GameObject.Find("Dark Lvl 1");
        EF = RotationDoorEffect.GetComponent<ParticleSystem>();

        if (other.CompareTag("Right"))
        {
            EF.Stop(); // エフェクトを停止
            Rote = other.transform.parent.gameObject;
            Rote.transform.Rotate(0, -rotateAngle * Time.deltaTime * rotateSpeed, 0); // 回転を止める（元の位置に戻す）
            Right = false; // 右回転していないことを示す
        }
    }
}
