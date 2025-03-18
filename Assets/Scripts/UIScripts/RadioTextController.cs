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
    private float textDisplayTime = 4f;  // 各テキストを表示する時間（秒）

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
        yield return new WaitForSeconds(2f);  // シーン開始から2秒待機
        StartCoroutine(SwitchText());  // テキストを順番に切り替える
    }

    // テキストを時間ごとに切り替えるコルーチン
    IEnumerator SwitchText()
    {
        while (TextCounter <= 12)  // 最大のテキストカウンター（12番目まで）
        {
            // 現在のテキストを設定
            SetText();

            // Textごとに切り替えタイミングを変える
            ChangeTiming();

            // 指定した秒数待つ
            yield return new WaitForSeconds(textDisplayTime);

            // カウンターを進める
            TextCounter++;

            // 12番目のテキストが表示されたら nextText を表示
            if (TextCounter == 13)
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
        if (TextCounter == 0)
        {
            text.text = "聞こえておるか";
        }
        else if (TextCounter == 1)
        {
            text.text = "確かに実験は成功した";
        }
        else if (TextCounter == 2)
        {
            text.text = "だが奴らは我々の想像以上に進化し過ぎた";
        }
        else if (TextCounter == 3)
        {
            text.text = "奴らは目が見えん代わりに耳がよい";
        }
        else if (TextCounter == 4)
        {
            text.text = "よいか　奴らの前では決して音を立ててはならん";
        }
        else if (TextCounter == 5)
        {
            text.text = "お前がそこから脱出するには鍵が必要じゃ";
        }
        else if (TextCounter == 6)
        {
            text.text = "しかし厄介なことに限られた理性で";
        }
        else if (TextCounter == 7)
        {
            text.text = "奴らが鍵と脱出口付近を守っておる";
        }
        else if (TextCounter == 8)
        {
            text.text = "逆を言えば　奴らが多くいる場所ほど";
        }
        else if (TextCounter == 9)
        {
            text.text = "お前が欲しいものが近くにあるということ";
        }
        else if (TextCounter == 10)
        {
            text.text = "しつこいようじゃが　音の出し過ぎには気を付けるんじゃぞ";
        }
        else if (TextCounter == 11)
        {
            text.text = "この音声は奴らから身を守るために使うとよい";
        }
        else if (TextCounter == 12)
        {
            text.text = "幸運を祈る";
        }
        else if (TextCounter == 13)
        {
            text.text = "";  // 最後のテキスト後に空文字を設定
        }
    }

    // Textごとの表示時間を調整する
    void ChangeTiming()
    {
        // TextCounterに応じて表示時間を調整
        if (TextCounter == 0)
        {
            textDisplayTime = 2.0f;
        }
        else if (TextCounter == 1)
        {
            textDisplayTime = 4.0f;
        }
        else if (TextCounter == 2)
        {
            textDisplayTime = 5.0f;
        }
        else if (TextCounter == 4)
        {
            textDisplayTime = 6.0f;
        }
        else if (TextCounter == 6)
        {
            textDisplayTime = 4.0f;
        }
        else if (TextCounter == 11)
        {
            textDisplayTime = 6.0f;
        }
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