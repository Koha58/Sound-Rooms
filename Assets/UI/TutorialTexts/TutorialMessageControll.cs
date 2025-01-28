using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialMessageControll : MonoBehaviour
{
    // チュートリアルメッセージのスライドUIを管理する配列
    [SerializeField]
    private SlideUIControll[] Messages;

    // コントローラー専用のチュートリアルメッセージを管理する配列
    [SerializeField]
    private SlideUIControll[] ControllerMessages;

    // メッセージが切り替わる際のサウンド
    [SerializeField] AudioSource MessageSound;

    float timeCnt; // タイマーとして使用されるカウンタ
    public int Message; // 現在のメッセージのインデックス
    PlayerSeen PS; // プレイヤーが見られている状態を管理するスクリプト

    bool deviceCheck; // 入力デバイスがコントローラーかをチェックするフラグ

    // オブジェクト配置を管理するスクリプトへの参照
    public ObjectPlacer OP;

    // ボタンのイメージコンポーネント
    public Image LeftButton;

    // Startは最初のフレームが更新される前に一度だけ呼び出される
    void Start()
    {
        // 全てのメッセージの状態を初期化
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[Message].state = 0;
        }
        Message = 1; // 最初のメッセージを設定
        Messages[Message - 1].state = 1;

        // 各コンポーネントの初期化
        OP.GetComponent<ObjectPlacer>();
        LeftButton.GetComponent<Image>().enabled = false;
    }

    // Updateは毎フレーム呼び出される
    void Update()
    {
        // 現在の入力デバイスを確認
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false;
        }

        // タイマーが7秒を超え、かつメッセージが22未満なら次のメッセージへ進む
        if (timeCnt >= 7.0f && Message < 22)
        {
            Messages[Message - 1].state = 0; // 現在のメッセージを非表示に
            Controller(); // 入力デバイスに応じた処理
            Messages[Message].state = 1; // 次のメッセージを表示
            Message++;
            timeCnt = 0f; // タイマーをリセット
            MessageSound.PlayOneShot(MessageSound.clip); // サウンドを再生
        }

        MoveWait(); // メッセージ切り替え条件の確認

        Messages[Message - 1].state = 1; // 現在のメッセージを表示
    }

    // メッセージの切り替え条件を確認
    void MoveWait()
    {
        if (Message == 1)
        {
            Messages[0].state = 0;
            Message++;
        }

        if (Message == 2)
        {
            StartCoroutine(FlashButton()); // ボタン点滅を開始
            if (OP.isOnSettingPoint)
            {
                Messages[1].state = 0;
                Message++;
                StopCoroutine(FlashButton()); // 点滅を停止
                SetButtonColor(Color.white); // ボタンの色を白に戻す
                LeftButton.GetComponent<Image>().enabled = false; // ボタンを非表示に
            }
        }

        if (Message == 3)
        {
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // スクリプトを取得
            if (impactObjects.count == 1)
            {
                Messages[2].state = 0;
                Message++;
            }
        }

        if (Message == 4)
        {
            //if (OP.Recorder.activeSelf == true)
            //{
            //    Messages[3].state = 0;
            //    Message++;
            //}
        }
    }

    // ボタンを赤と白に点滅させるコルーチン
    private IEnumerator FlashButton()
    {
        while (Message == 2)
        {
            LeftButton.GetComponent<Image>().enabled = true;
            SetButtonColor(Color.red); // ボタンを赤に
            yield return new WaitForSeconds(0.5f); // 0.5秒待機
            SetButtonColor(Color.white); // ボタンを白に
            yield return new WaitForSeconds(0.5f); // 0.5秒待機
        }
    }

    // ボタンの色を設定するメソッド
    private void SetButtonColor(Color color)
    {
        if (LeftButton != null)
        {
            LeftButton.color = color;
        }
    }

    // 入力デバイスに応じたメッセージ処理
    void Controller()
    {
        if (deviceCheck) // コントローラーの場合
        {
            if (Message == 1) Messages[Message] = ControllerMessages[1];
            else if (Message == 2) Messages[Message] = ControllerMessages[2];
            else if (Message == 3) Messages[Message] = ControllerMessages[3];
            else if (Message == 4) timeCnt += 5.0f; // 追加時間を加算
        }
        else // キーボードの場合
        {
            if (Message == 1) Messages[Message] = Messages[0];
            else if (Message == 2) Messages[Message] = Messages[1];
            else if (Message == 3) Messages[Message] = Messages[2];
            else if (Message == 4) Messages[Message] = Messages[3];
        }
    }
}