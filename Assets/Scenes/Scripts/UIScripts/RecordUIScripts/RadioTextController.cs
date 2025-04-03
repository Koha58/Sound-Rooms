using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // シーン遷移用

/// <summary>
/// GetRecorderシーンのテキストを管理するクラス
/// </summary>
public class RadioTextController : MonoBehaviour
{
    [SerializeField] private Text text;  // Text UI コンポーネントへの参照
    [SerializeField] private Text nextText;  // 次のシーンへ進む指示を出すテキスト
    [SerializeField] private FadeController fadeController;  // フェード用のスクリプト参照
    private int TextCounter = 0;  // テキストカウンター

    // 各テキストに対応する表示時間（秒）
    private static readonly float[] TextDisplayTimes = {
        2.0f,  // テキスト0
        4.0f,  // テキスト1
        5.0f,  // テキスト2
        4.0f,  // テキスト3
        6.0f,  // テキスト4
        4.0f,  // テキスト5
        4.0f,  // テキスト6
        4.0f,  // テキスト7
        4.0f,  // テキスト8
        4.0f,  // テキスト9
        4.0f,  // テキスト10
        6.0f,  // テキスト11
        4.0f   // テキスト12（最後）
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
        while (TextCounter < Texts.Length)  // 最大のテキストカウンター（配列の長さまで）
        {
            // 現在のテキストを設定
            SetText();

            // 指定した秒数待つ
            yield return new WaitForSeconds(TextDisplayTimes[TextCounter]);

            // カウンターを進める
            TextCounter++;

            // 12番目のテキストが表示されたら nextText を表示
            if (TextCounter == Texts.Length)
            {
                text.enabled = false;  // 現在のテキストを非表示
                nextText.text = "E：ラジオを拾う";  // 次のシーンに進むメッセージ
                nextText.enabled = true;  // nextTextを表示
            }
        }
    }

    // 現在のテキストを設定する
    void SetText()
    {
        // TextCounterに応じて表示するテキストを設定
        text.text = Texts[TextCounter];
    }

    // Eボタンが押されたときの処理
    void Update()
    {
        // Eキーが押され、次のテキストが表示されている場合
        if (Input.GetKeyDown(KeyCode.E) && nextText.enabled)
        {
            // フェードアウトを開始
            StartCoroutine(FadeAndLoadScene());
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
