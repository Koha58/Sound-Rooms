using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RadioTextController : MonoBehaviour
{
    public Text text;  // Text UI コンポーネントへの参照
    private int TextCounter = 0;  // テキストカウンター
    private float textDisplayTime = 4f;  // 各テキストを表示する時間（秒）

    // Start is called before the first frame update
    void Start()
    {
        // 2秒後に最初のテキストを表示
        StartCoroutine(StartTextSequence());
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
        }
    }

    // 現在のテキストを設定する
    void SetText()
    {
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
    }

    void ChangeTiming()
    {
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
}