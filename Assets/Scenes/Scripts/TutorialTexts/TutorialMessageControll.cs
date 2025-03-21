using System;
using UnityEngine;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// チュートリアル用のUIを管理するクラス
/// </summary>
public class TutorialMessageControll : MonoBehaviour
{
    // チュートリアルメッセージのスライドUIを管理する配列
    [SerializeField]
    private SlideUIControll[] Messages;

    // キーボード専用のチュートリアルメッセージを管理する配列
    [SerializeField]
    private Image[] KeyboardMove;

    // コントローラー専用のチュートリアルメッセージを管理する配列
    [SerializeField]
    private Image[] ControllerMove;

    // チュートリアルメッセージを管理する配列（現在のデバイスに基づいてメッセージを切り替える）
    [SerializeField]
    private Image[] UIDeviceCheck;

    // 現在表示されているメッセージのインデックス
    public int MessageIndex;

    // 入力デバイスがコントローラーかどうかをチェックするフラグ
    private bool isControllerInput;

    // オブジェクト配置を管理するスクリプトへの参照
    [SerializeField]
    private ObjectPlacer objectPlacer;

    // メッセージのインデックスを管理する定数
    private const int FIRST_MESSAGE_INDEX = 0;  // 最初のメッセージ（インデックス0）
    private const int SECOND_MESSAGE_INDEX = 1; // 2番目のメッセージ（インデックス1）
    private const int THIRD_MESSAGE_INDEX = 2;  // 3番目のメッセージ（インデックス2）

    // チュートリアルの最大メッセージ数
    private const int MAX_MESSAGES = 3;

    // キーボードとコントローラーの配列の最大数
    private const int MAX_DEVICE_MESSAGES = 3;

    // キーボード、コントローラー、UIDeviceCheck 配列のインデックスの最大値
    private const int DEVICE_MESSAGE_0_INDEX = 0;  // 0番目のデバイスメッセージ
    private const int DEVICE_MESSAGE_1_INDEX = 1;  // 1番目のデバイスメッセージ
    private const int DEVICE_MESSAGE_2_INDEX = 2;  // 2番目のデバイスメッセージ

    private bool OnPut;

    private bool OnKey;

    // メッセージの最大数とインデックス範囲の定数
    private const int MAX_INDEX = 2; // メッセージのインデックスは 0, 1, 2 なので最大値は 2

    void Start()
    {
        // 各メッセージの状態を初期化
        // Messages 配列の各要素（各メッセージ）の state を 0（非表示）に設定
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[i].state = SlideUIControll.State.Initial; // 各メッセージを非表示に設定
        }

        // 最初のメッセージのインデックスを設定
        MessageIndex = FIRST_MESSAGE_INDEX; // 最初のメッセージを設定（0番目のメッセージ）

        // 最初のメッセージを表示状態に変更
        Messages[MessageIndex].state = SlideUIControll.State.SlideIn; // 最初のメッセージを表示状態に

        // 入力デバイス（キーボード、コントローラー）のメッセージを初期化
        InitializeDeviceMessages(KeyboardMove); // キーボード用のメッセージを非表示
        InitializeDeviceMessages(ControllerMove); // コントローラー用のメッセージを非表示

        // UIDeviceCheck 配列にキーボードメッセージを設定
        for (int i = 0; i < MAX_DEVICE_MESSAGES; i++)
        {
            UIDeviceCheck[i] = KeyboardMove[i]; // UIDeviceCheck 配列をキーボードのメッセージで初期化
        }

        // オブジェクト配置のコンポーネントを初期化
        objectPlacer.GetComponent<ObjectPlacer>(); // ObjectPlacer コンポーネントを取得（後で使う）
    }

    void Update()
    {
        // 現在の入力デバイスタイプを確認して、コントローラーかキーボードかを判断
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            isControllerInput = true; // コントローラーが使用されている場合はフラグを true に
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            isControllerInput = false; // キーボードが使用されている場合はフラグを false に
        }

        // 現在のメッセージ状態を更新
        StayMessage();

        // 入力デバイスに基づいて表示するメッセージを切り替え
        ControllerCheck();

        // 現在のメッセージを表示状態に設定
        Messages[MessageIndex].state = SlideUIControll.State.SlideIn; // 現在表示されているメッセージを表示状態に
    }

    // メッセージの切り替え条件を確認するメソッド
    void StayMessage()
    {
        // 最初のメッセージ（インデックス 0）の場合
        if (MessageIndex == FIRST_MESSAGE_INDEX)
        {
            // objectPlacer が設定地点にある場合、次のメッセージに切り替え
            if (objectPlacer.isOnSettingPoint) // オブジェクトが設定地点にある場合
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // 現在のメッセージを非表示
                MessageIndex++; // 次のメッセージへインデックスを変更
            }
        }
        // 2番目のメッセージ（インデックス 1）の場合
        else if (MessageIndex == SECOND_MESSAGE_INDEX)
        {
            // "EnemyAttackArea" を探して、その状態に基づいてメッセージを切り替え
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea"); // "EnemyAttackArea" オブジェクトを探す
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects スクリプトを取得
            if (impactObjects.count == 1) // impactObjects のカウントが 1 の場合
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // 現在のメッセージを非表示
                MessageIndex++; // 次のメッセージに移動
            }
        }
        // 3番目のメッセージ（インデックス 2）の場合
        else if (MessageIndex == THIRD_MESSAGE_INDEX)
        {
            // objectPlacer が設定地点にない場合、次のメッセージに切り替え
            if (!objectPlacer.isOnSettingPoint) // オブジェクトが設定地点にない場合
            {
                Messages[MessageIndex].state = SlideUIControll.State.Initial; // 現在のメッセージを非表示
                MessageIndex++; // 次のメッセージに移動
            }
        }
    }

    // 入力デバイスに応じたメッセージ処理を行うメソッド
    void ControllerCheck()
    {

        // "EnemyAttackArea" を探して、その状態に基づいてメッセージを切り替え
        GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea"); // "EnemyAttackArea" オブジェクトを探す
        ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // ImpactOnObjects スクリプトを取得

        if (isControllerInput) // コントローラーが使用されている場合
        {
            // キーボードメッセージを非表示にし、コントローラーのメッセージを表示
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX] = KeyboardMove[DEVICE_MESSAGE_0_INDEX]; // キーボードメッセージを非表示
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX].enabled = false; // 0番目のデバイスメッセージを非表示

            // メッセージインデックスが最初の場合、コントローラー用のメッセージを表示
            if (impactObjects.count != 1 && OnPut)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX] = ControllerMove[DEVICE_MESSAGE_1_INDEX]; // コントローラーの1番目のメッセージを表示
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = true; // コントローラーの1番目のメッセージを表示
            }
            // メッセージインデックスが2番目の場合、コントローラーの1番目のメッセージを非表示
            else if (MessageIndex == SECOND_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1番目のメッセージを非表示
            }
            // メッセージインデックスが3番目の場合、コントローラーの2番目のメッセージを表示
            else if (MessageIndex == THIRD_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1番目のメッセージを非表示
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX] = ControllerMove[DEVICE_MESSAGE_2_INDEX]; // コントローラーの2番目のメッセージを表示
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = true; // コントローラーの2番目のメッセージを表示
            }
            else
            {
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = false; // それ以外はメッセージを非表示
            }
        }
        else // キーボードが使用されている場合
        {
            // キーボードメッセージを表示
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX] = KeyboardMove[DEVICE_MESSAGE_0_INDEX]; // キーボードの0番目のメッセージを表示
            UIDeviceCheck[DEVICE_MESSAGE_0_INDEX].enabled = true; // 0番目のメッセージを表示

            // メッセージインデックスが最初の場合、キーボードの1番目のメッセージを表示
            if (impactObjects.count != 1 && OnPut)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX] = KeyboardMove[DEVICE_MESSAGE_1_INDEX]; // キーボードの1番目のメッセージを表示
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = true; // キーボードの1番目のメッセージを表示
            }
            // メッセージインデックスが2番目の場合、キーボードの1番目のメッセージを非表示
            else if (MessageIndex == SECOND_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1番目のメッセージを非表示
            }
            // メッセージインデックスが3番目の場合、キーボードの2番目のメッセージを表示
            else if (MessageIndex == THIRD_MESSAGE_INDEX)
            {
                UIDeviceCheck[DEVICE_MESSAGE_1_INDEX].enabled = false; // 1番目のメッセージを非表示
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX] = KeyboardMove[DEVICE_MESSAGE_2_INDEX]; // キーボードの2番目のメッセージを表示
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = true; // キーボードの2番目のメッセージを表示
            }
            else
            {
                UIDeviceCheck[DEVICE_MESSAGE_2_INDEX].enabled = false; // それ以外はメッセージを非表示
            }
        }
    }

    // デバイスメッセージを初期化するヘルパーメソッド（各メッセージを非表示にする）
    private void InitializeDeviceMessages(Image[] deviceMessages)
    {
        // 配列の各メッセージを非表示にする
        for (int i = 0; i < deviceMessages.Length; i++)
        {
            deviceMessages[i].enabled = false; // すべてのメッセージを非表示
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SettingPoint"))
        {
            OnPut = true;
        }
        else
        {
            OnPut = false;
        }

        if (other.CompareTag("KeyCheck"))
        {
            OnKey = true;
        }
        else
        {
            OnKey = false;
        }
    }

}