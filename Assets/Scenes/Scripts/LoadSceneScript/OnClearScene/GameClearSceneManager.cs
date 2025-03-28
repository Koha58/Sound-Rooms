using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// ゲームクリアシーンの処理を行うクラス
/// </summary>
public class GameClearSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image[] backgroundImages; // 背景画像の配列
    [SerializeField] private Text typewriterText; // タイプライター効果用のテキスト
    [SerializeField] private Text normalText; // 普通に表示するテキスト

    [Header("UI for Each Scene")]
    [SerializeField] private Sprite[][] backgroundSets; // 各シーンごとの背景セット（2次元配列）

    [SerializeField] private string[] typewriterTextContent; // 表示するテキスト内容（タイプライター効果用）
    [SerializeField] private string[] normalTextContent; // 表示するテキスト内容（普通に表示用）

    private int currentIndex = 0; // 現在のテキストのインデックス
    private bool isTyping = false; // 入力中かどうか

    // 入力が終了した後に遷移するシーン名
    private string nextScene;

    // 定数定義
    private const int TUTORIAL_SCENE_INDEX = 0; // チュートリアルシーンのインデックス
    private const int STAGE1_SCENE_INDEX = 1;  // Stage1シーンのインデックス
    private const int GAME_SCENE_INDEX = 2;    // GameSceneシーンのインデックス

    private const string TUTORIAL_SCENE = "TutorialScene"; // チュートリアルシーンの名前
    private const string STAGE1_SCENE = "Stage1";         // Stage1シーンの名前
    private const string GAME_SCENE = "GameScene";         // GameSceneシーンの名前
    private const string START_SCENE = "StartScene";       // StartSceneシーンの名前

    private const string PREVIOUS_SCENE_KEY = "PreviousScene"; // PlayerPrefsキー名

    private const float TYPEWRITER_DELAY = 0.3f; // タイプライター効果の遅延時間
    private const float SCENE_TRANSITION_DELAY = 1f; // シーン遷移までの待機時間

    // 背景画像のインデックス用定数
    private const int BACKGROUND_TUTORIAL = TUTORIAL_SCENE_INDEX;
    private const int BACKGROUND_STAGE1 = STAGE1_SCENE_INDEX;
    private const int BACKGROUND_GAME = GAME_SCENE_INDEX;

    // インデックスの定数
    private const int NORMAL_TEXT_INDEX = 0;

    void Start()
    {
        // 前のシーン名をPlayerPrefsから取得する
        string previousScene = PlayerPrefs.GetString(PREVIOUS_SCENE_KEY, START_SCENE);

        // 前のシーン名をデバッグログで表示
        Debug.Log("Previous Scene: " + previousScene);

        // シーンに基づいて表示するテキストを設定
        SetTextBasedOnScene(previousScene);

        // UIを切り替える
        SwitchUI(previousScene);

        // nextSceneが設定されているかチェック
        if (string.IsNullOrEmpty(nextScene))
        {
            Debug.LogError("次のシーンが設定されていません。");
            return; // nextSceneが設定されていない場合は処理を中断
        }

        // normalTextに表示するテキストはすぐに表示
        normalText.text = normalTextContent[NORMAL_TEXT_INDEX]; // 普通に表示するテキスト

        // typewriterTextにはタイプライター効果を使用してテキストを表示
        StartCoroutine(TypeText());
    }

    // シーンに基づいてテキストを設定
    void SetTextBasedOnScene(string previousScene)
    {
        // 前のシーンが"TutorialScene"だった場合、特定のテキストを設定
        if (previousScene == TUTORIAL_SCENE)
        {
            normalTextContent = new string[] { "TUTORIAL" }; //シーン名を表示
            typewriterTextContent = new string[] { "CLEAR\nNEXT STAGE1" }; // タイプライター効果用
            nextScene = STAGE1_SCENE; // TutorialSceneからStage1に遷移
        }
        // 前のシーンが"Stage1"だった場合、特定のテキストを設定
        else if (previousScene == STAGE1_SCENE)
        {
            normalTextContent = new string[] { "STAGE1" }; // シーン名を表示
            typewriterTextContent = new string[] { "CLEAR\nNEXT STAGE2" }; // タイプライター効果用
            nextScene = GAME_SCENE; // Stage1からGameSceneに遷移
        }
        // 前のシーンが"GameScene"だった場合、特定のテキストを設定
        else if (previousScene == GAME_SCENE)
        {
            normalTextContent = new string[] { "STAGE2" }; // シーン名を表示
            typewriterTextContent = new string[] { "CLEAR\nBACK TO TITLE" }; // タイプライター効果用
            nextScene = START_SCENE; // GameSceneからStartSceneに遷移
        }
        else
        {
            // 予期しないシーンの場合のデフォルト処理
            typewriterTextContent = new string[] { "予期しないシーンから遷移しました。" }; // タイプライター効果用
            nextScene = START_SCENE; // デフォルトでStartSceneに戻す
        }

        // 次に遷移するシーン名をデバッグログで表示
        Debug.Log("Next Scene: " + nextScene);
    }

    // UIをシーンに基づいて切り替える
    void SwitchUI(string previousScene)
    {
        // まず、すべての背景画像を非表示にする
        foreach (Image img in backgroundImages)
        {
            img.enabled = false; // 非表示にする
        }

        // シーンに基づいて背景とテキストを切り替え
        if (previousScene == TUTORIAL_SCENE)
        {
            // チュートリアル用の背景を表示
            backgroundImages[BACKGROUND_TUTORIAL].enabled = true;
        }
        else if (previousScene == STAGE1_SCENE)
        {
            // Stage1用の背景を表示
            backgroundImages[BACKGROUND_STAGE1].enabled = true;
        }
        else
        {
            // GameScene用の背景を表示
            backgroundImages[BACKGROUND_GAME].enabled = true;
        }
    }

    // タイプライター効果を実現するコルーチン
    IEnumerator TypeText()
    {
        // 1つ目のテキスト（タイプライター効果）
        foreach (char letter in typewriterTextContent[currentIndex].ToCharArray())
        {
            typewriterText.text += letter;
            yield return new WaitForSeconds(TYPEWRITER_DELAY); // 文字を打つ間隔
        }

        // 文字の入力が終了したら、次のシーンへ遷移
        isTyping = false;
        yield return new WaitForSeconds(SCENE_TRANSITION_DELAY); // 少し待ってから遷移

        // nextSceneが設定されている場合のみ遷移
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene); // 遷移先のシーンに移動
        }
        else
        {
            Debug.LogError("無効なシーン名です。");
        }
    }
}
