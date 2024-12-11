using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private UIInputActions _uiInputActions;

    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    public GameObject SelectButton;
    public GameObject BackDesktopButton;


    bool UPDOWN;

    Vector3 originalSelectButtonPosition; // 元の位置を保存するための変数
    Vector3 originalBackDesktopButtonPosition; // 元の位置を保存するための変数

    [SerializeField] AudioSource SelectSound;  // AudioSourceをSerializeFieldとしてインスペクターから設定

    // Start is called before the first frame update
    void Start()
    {

        _uiInputActions = new UIInputActions();
        _uiInputActions.Enable();

        SelectButtonImage = SelectButton.GetComponent<Image>();
        BackDesktopButtonImage = BackDesktopButton.GetComponent<Image>();

        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // ボタンの元の位置を保存
        originalSelectButtonPosition = SelectButton.GetComponent<RectTransform>().localPosition;
        originalBackDesktopButtonPosition = BackDesktopButton.GetComponent<RectTransform>().localPosition;

        // Selectボタンを少しマイナスX方向に移動
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButtonは移動しない

        UPDOWN = true;

        // AudioSource コンポーネントを取得
        SelectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_uiInputActions.SettingUI.MainSelsectUp.triggered)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 255);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
            UPDOWN = true;

            // Selectボタンを少しマイナスX方向に移動
            SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
            BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButtonは移動しない
        }
        else if (_uiInputActions.SettingUI.MainSelsectDown.triggered)
        {
            SelectButtonImage.color = new Color32(0, 0, 0, 120);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
            UPDOWN = false;

            // BackDesktopボタンを少しマイナスX方向に移動
            BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-20f, 0f, 0f);
            SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButtonは移動しない
        }

        // ボタンが選択された状態でボタンが押された場合の処理
        if (UPDOWN == true)
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
                // 音を再生してシーン遷移するコルーチンを開始
                StartCoroutine(PlaySelectSoundAndLoadScene());
                SceneManager.LoadScene("StageSelectScene");
            }
        }
        else
        {
            if (Input.GetKeyDown("joystick button 0"))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // ゲームプレイ終了
#else
                Application.Quit(); // ゲームプレイ終了
#endif
            }
        }

    }

    // Selectボタンが選択されたときにシーン遷移
    public void OnSelect()
    {
        // 音を再生してシーン遷移するコルーチンを開始
        StartCoroutine(PlaySelectSoundAndLoadScene());
    }

    // BackDesktopボタンが選択されたときにゲームを終了
    public void OnBackDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ゲームプレイ終了
#else
        Application.Quit(); // ゲームプレイ終了
#endif
    }

    public void EnterSelectButton()
    {
        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        // Selectボタンを少しマイナスX方向に移動
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButtonは移動しない
    }

    public void ExitSelectButton()
    {
        SelectButtonImage.color = new Color32(0, 0, 0, 120);
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition;
    }

    public void EnterBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
        // BackDesktopボタンを少しマイナスX方向に移動
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-20f, 0f, 0f);
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButtonは移動しない
    }

    public void ExitBackDesktopButton()
    {
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition;
    }

    // 音を再生してシーン遷移するコルーチン
    private IEnumerator PlaySelectSoundAndLoadScene()
    {
        // 音を再生
        SelectSound.PlayOneShot(SelectSound.clip);

        // 音が再生されるのを待機 (音の長さ分だけ待機)
        yield return new WaitForSeconds(SelectSound.clip.length);

        // 音が終了した後にシーンを遷移
        SceneManager.LoadScene("StageSelectScene");
    }
}
