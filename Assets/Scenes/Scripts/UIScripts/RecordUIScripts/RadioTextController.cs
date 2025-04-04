using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン遷移用
using static InputDeviceManager;

/// <summary>
/// GetRecorderシーンのテキストを管理するクラス
/// </summary>
public class RadioTextController : MonoBehaviour
{
    [SerializeField] private Text text;  // Text UI コンポーネントへの参照
    [SerializeField] private Text nextText;  // 次のシーンへ進む指示を出すテキスト
    [SerializeField] private FadeController fadeController;  // フェード用のスクリプト参照
    private int textCounter = 0;  // テキストカウンター

    // 各テキストに対応する表示時間（秒）
    private static readonly float[] TextDisplayTimes = {
        2.0f,  // 聞こえておるか
        4.0f,  // 確かに実験は成功した
        5.0f,  // だが奴らは我々の想像以上に進化し過ぎた
        5.0f,  // 奴らは目が見えん代わりに耳がよい
        6.0f,  // よいか　奴らの前では決して音を立ててはならん
        5.0f,  // お前がそこから脱出するには鍵が必要じゃ
        4.0f,  // しかし厄介なことに限られた理性で
        5.0f,  // 奴らが鍵と脱出口付近を守っておる
        4.5f,  // 逆を言えば　奴らが多くいる場所ほど
        3.5f,  // お前が欲しいものが近くにあるということ
        5.0f,  // しつこいようじゃが　音の出し過ぎには気を付けるんじゃぞ
        5.0f,  // この音声は奴らから身を守るために使うとよい
        3.5f   // 幸運を祈る（最後）
    };

    // 各テキストの内容
    private static readonly string[] Texts = {
        "聞こえておるか",
        "確かに実験は成功した",
        "だが奴らは我々の想像以上に進化し過ぎた",
        "奴らは目が見えん代わりに耳がよい",
        "よいか　奴らの前では決して音を立ててはならん",
        "お前がそこから脱出するには鍵が必要じゃ",
        "しかし厄介なことに限られた理性で",
        "奴らが鍵と脱出口付近を守っておる",
        "逆を言えば　奴らが多くいる場所ほど",
        "お前が欲しいものが近くにあるということ",
        "しつこいようじゃが　音の出し過ぎには気を付けるんじゃぞ",
        "この音声は奴らから身を守るために使うとよい",
        "幸運を祈る"
    };

    // シーン開始から待機する時間（秒）
    private const float InitialWaitTime = 2.0f;

    // デバイス（Xbox/Keyboard）のチェックフラグ
    private bool deviceCheck;

    // Start is called before the first frame update
    void Start()
    {
        // 2秒後に最初のテキストを表示
        StartCoroutine(StartTextSequence());
        nextText.enabled = false;  // 次のシーンへの進行メッセージは初期状態で非表示
    }

    // 2秒後にテキストシーケンスを開始する
    IEnumerator StartTextSequence()
    {
        yield return new WaitForSeconds(InitialWaitTime);  // シーン開始から2秒待機
        StartCoroutine(SwitchText());  // テキストを順番に切り替える
    }

    // テキストを時間ごとに切り替えるコルーチン
    IEnumerator SwitchText()
    {
        while (textCounter < Texts.Length)  // 最大のテキストカウンター（配列の長さまで）
        {
            // 現在のテキストを設定
            SetText();

            // 指定した秒数待つ
            yield return new WaitForSeconds(TextDisplayTimes[textCounter]);

            // カウンターを進める
            textCounter++;

            // 12番目のテキストが表示されたら nextText を表示
            if (textCounter == Texts.Length)
            {
                text.enabled = false;  // 現在のテキストを非表示
                if(deviceCheck)
                {
                    nextText.text = "X：ラジオを拾う";  // 次のシーンに進むメッセージ
                }
                else
                {
                    nextText.text = "E：ラジオを拾う";  // 次のシーンに進むメッセージ
                }            
                nextText.enabled = true;  // nextTextを表示
            }
        }
    }

    // 現在のテキストを設定する
    void SetText()
    {
        // TextCounterに応じて表示するテキストを設定
        text.text = Texts[textCounter];
    }

    // Eボタンが押されたときの処理
    void Update()
    {
        // 現在使用している入力デバイスをチェック
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            // Xboxコントローラーが使用されている場合
            deviceCheck = true;
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            // キーボードが使用されている場合
            deviceCheck = false;
        }

        // Xboxコントローラーが使用されている場合
        if (deviceCheck)
        {
            // Xボタンが押され、次のテキストが表示されている場合
            if (Input.GetKeyDown("joystick button 2") && nextText.enabled)
            {
                // フェードアウトを開始
                StartCoroutine(FadeAndLoadScene());
            }
        }
        // キーボードが使用されている場合
        else
        {
            // Eキーが押され、次のテキストが表示されている場合
            if (Input.GetKeyDown(KeyCode.E) && nextText.enabled)
            {
                // フェードアウトを開始
                StartCoroutine(FadeAndLoadScene());
            }
        }
    }

    // フェードアウトとシーン遷移
    IEnumerator FadeAndLoadScene()
    {
        // フェードアウトを開始
        yield return StartCoroutine(fadeController.FadeOut());

        // フェードアウトが完了したらシーン遷移
        SceneManager.LoadScene("TutorialScene");  // シーン名は適宜変更
    }
}
