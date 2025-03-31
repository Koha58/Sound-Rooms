using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InputDeviceManager;

/// <summary>
/// StartSceneのボタン管理クラス
/// </summary>
public class StartManager : MonoBehaviour
{
    // Imageコンポーネントを参照
    Image SelectButtonImage;
    Image BackDesktopButtonImage;

    // ボタンのGameObject参照
    [SerializeField] private GameObject SelectButton;
    [SerializeField] private GameObject BackDesktopButton;

    // ボタンの移動方向管理用フラグ
    bool UPDOWN;

    // 元の位置を保存するための変数
    Vector3 originalSelectButtonPosition;
    Vector3 originalBackDesktopButtonPosition;

    // Selectボタンが押されたときの音を再生するAudioSource
    [SerializeField] AudioSource SelectSound;  // AudioSourceをSerializeFieldとしてインスペクターから設定

    // 入力デバイスの種類を判定するフラグ
    bool deviceCheck;

    // Start is called before the first frame update
    void Start()
    {
        // ボタンのImageコンポーネントを取得
        SelectButtonImage = SelectButton.GetComponent<Image>();
        BackDesktopButtonImage = BackDesktopButton.GetComponent<Image>();

        // Selectボタンは色を黒に、BackDesktopボタンは色を薄く設定
        SelectButtonImage.color = new Color32(0, 0, 0, 255);
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // ボタンの元の位置を保存
        originalSelectButtonPosition = SelectButton.GetComponent<RectTransform>().localPosition;
        originalBackDesktopButtonPosition = BackDesktopButton.GetComponent<RectTransform>().localPosition;

        // Selectボタンを少し左に移動
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-20f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButtonは移動しない

        UPDOWN = true;

        // AudioSource コンポーネントを取得
        SelectSound = GetComponent<AudioSource>();

        deviceCheck = false; // キーボードが使用されている
    }

    void Update()
    {
        // 入力デバイスの種類を確認し、フラグを設定
        if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Xbox)
        {
            deviceCheck = true; // コントローラーが使用されている
        }
        else if (InputDeviceManager.Instance.CurrentDeviceType == InputDeviceType.Keyboard)
        {
            deviceCheck = false; // キーボードが使用されている
        }

        if (deviceCheck)
        {
            GamePadUIController();
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
        // Unityエディターの場合、ゲームプレイを終了
        UnityEditor.EditorApplication.isPlaying = false; // ゲームプレイ終了
#else
        // ビルドされたゲームの場合、アプリケーションを終了
        Application.Quit(); // ゲームプレイ終了
#endif
    }

    // Selectボタンにカーソルが入ったときの処理
    public void EnterSelectButton()
    {
        // Selectボタンを少し左に移動
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition + new Vector3(-40f, 0f, 0f);
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition; // BackDesktopButtonは移動しない

        // Selectボタンの色を黒に変更
        SelectButtonImage.color = new Color32(0, 0, 0, 255);

        // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Selectボタンからカーソルが出たときの処理
    public void ExitSelectButton()
    {
        // Selectボタンの色を薄く設定
        SelectButtonImage.color = new Color32(0, 0, 0, 120);

        // Selectボタンを元の位置に戻す
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition;
    }

    // BackDesktopボタンにカーソルが入ったときの処理
    public void EnterBackDesktopButton()
    {
        // BackDesktopボタンを少し左に移動
        SelectButton.GetComponent<RectTransform>().localPosition = originalSelectButtonPosition; // SelectButtonは移動しない
        BackDesktopButton.GetComponent<RectTransform>().localPosition = originalBackDesktopButtonPosition + new Vector3(-40f, 0f, 0f);

        // BackDesktopボタンの色を黒に変更
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);

        // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
        EventSystem.current.SetSelectedGameObject(null);
    }

    // BackDesktopボタンからカーソルが出たときの処理
    public void ExitBackDesktopButton()
    {
        // BackDesktopボタンの色を薄く設定
        BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);

        // BackDesktopボタンを元の位置に戻す
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

    public void GamePadUIController()
    {
        // 選択中のUI取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == SelectButton)
        {
            // Selectボタンの色を黒に変更
            SelectButtonImage.color = new Color32(0, 0, 0, 255);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 120);
        }
        else if (selectedGameObject == BackDesktopButton)
        {
            // Selectボタンの色を薄く設定
            SelectButtonImage.color = new Color32(0, 0, 0, 120);
            BackDesktopButtonImage.color = new Color32(0, 0, 0, 255);
        }
        else if(selectedGameObject == null)
        {
            // selectedGameObjectがnullの場合、settingButtonにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(SelectButton);
        }
    }
}