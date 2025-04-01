using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMを管理するコード
/// </summary>
public class BGMController : MonoBehaviour
{
    // BGM用のAudioSourceをインスペクターで設定できるようにする
    [SerializeField] AudioSource BGM;

    // Startは最初に1回だけ呼ばれる
    void Start()
    {
        // AudioSource コンポーネントを取得
        BGM = GetComponent<AudioSource>();

        // PlaySound メソッドを0秒後に開始し、以降10秒ごとに繰り返し実行
        InvokeRepeating("PlaySound", 0f, 10f);
    }

    // Updateは毎フレーム実行されるが、現在は何も行っていない
    void Update()
    {
        // 特に更新処理は必要ないため、このメソッドは空でも問題なし
    }

    // PlaySoundメソッド：BGMを再生する
    public void PlaySound()
    {
        // BGM音声を再生
        BGM.Play();
    }
}