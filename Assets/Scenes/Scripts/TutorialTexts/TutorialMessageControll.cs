using System;
using System.Collections;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.UI;

/// <summary>
/// TutorialSceneの説明UIを表示させるクラス
/// </summary>
public class TutorialMessageControll : MonoBehaviour
{
    // チュートリアルメッセージのスライドUIを管理する配列
    [SerializeField]
    private SlideUIControll[] Messages;

    // コントローラー専用のチュートリアルメッセージを管理する配列
    [SerializeField]
    private SlideUIControll[] ControllerMessages;

    public int Message; // 現在のメッセージのインデックス

    bool deviceCheck; // 入力デバイスがコントローラーかをチェックするフラグ

    // オブジェクト配置を管理するスクリプトへの参照
    [SerializeField]
    private ObjectPlacer OP;

    // --- キーボード用UI ---

    [SerializeField]
    private Image TutorialMoveUI;

    [SerializeField]
    private Image TutorialSpaceUI;

    [SerializeField]
    private Image TutorialEUI;

    // --- コントローラー用UI ---

    [SerializeField]
    private Image xButtonUI;

    [SerializeField]
    private Image yButtonUI;


    // Startは最初のフレームが更新される前に一度だけ呼び出される
    void Start()
    {
        // 全てのメッセージの状態を初期化
        for (int i = 0; i < Messages.Length; i++)
        {
            Messages[i].state = 0;
        }
        Message = 1; // 最初のメッセージを設定
        Messages[Message - 1].state = 1;

        // 各コンポーネントの初期化
        OP.GetComponent<ObjectPlacer>();

        TutorialMoveUI.GetComponent<Image>().enabled = false;
        TutorialSpaceUI.GetComponent<Image>().enabled = false;
        TutorialEUI.GetComponent<Image>().enabled = false;
        xButtonUI.GetComponent<Image>().enabled = false;
        yButtonUI.GetComponent<Image>().enabled = false;
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

        StayMessage();
    }

    // メッセージの切り替え条件を確認
    void StayMessage()
    {
        if(Message == 1)
        {
            if (OP.isOnSettingPoint)
            {
                Message++;
            }
        }
        else if(Message == 2)
        {
            GameObject impactObjectsArea = GameObject.Find("EnemyAttackArea");
            ImpactOnObjects impactObjects = impactObjectsArea.GetComponent<ImpactOnObjects>(); // スクリプトを取得
            if (impactObjects.count == 1)
            {
                Message++;
            }
        }
        else if (Message == 3)
        {
            if (!OP.isOnSettingPoint)
            {
                Message++;
            }
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