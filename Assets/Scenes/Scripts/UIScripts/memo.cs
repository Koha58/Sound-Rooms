using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class memo : MonoBehaviour
{

///// <summary>
///// ゲーム中に設定（BGMやSE、操作説明など）を制御するスクリプト
///// </summary>
//public class UIController : MonoBehaviour
//{
//    [SerializeField] private InputActionAsset inputActions;

//    private InputAction openSettingAction;
//    private InputAction moveAction;
//    private InputAction backAction;
//    private InputAction decisionAction;

//    // 設定メニュー関連のオブジェクト
//    [SerializeField] private GameObject SettingButton;  // 設定メニュー全体のオブジェクト
//    [SerializeField] private GameObject SettingSelect;
//    [SerializeField] private GameObject ExplanationSelect;
//    [SerializeField] private GameObject BackPanel;
//    [SerializeField] private GameObject GoTitleSelectParent;
//    [SerializeField] private Image buttonBui;         // BボタンのUI（戻るボタン表示用）
//    [SerializeField] private Image buttonAui;         // AボタンのUI（タイトルへ戻るボタン表示用）
//    [SerializeField] private Image settingCursorUI;   // 設定メニューのカーソル画像
//    private const float VolumeAdjustMultiplierMax = 5f; // スティック入力強度による音量調整倍率
//    private const float VolumeAdjustStep = 0.1f;        // 音量変更の基本ステップ
//    [SerializeField] private AudioMixer audioMixer;   // オーディオミキサー（音量制御）


//    [SerializeField] GameObject micObject;    // マイクオブジェクト
//    public CinemachineFreeLook VCamera;       // Cinemachineカメラ

//    private const float MouseMaxSpeedDivisor = 50f; // Y軸のスピード設定の割り算に使う定数
//    private const float MouseMaxSpeedMultiplier = 50f; // X軸のスピード設定の掛け算に使う定数

//    private const float TimeScalePaused = 0f;  // ゲーム進行を一時停止するための定数
//    private const float TimeScaleRunning = 1f; // ゲーム進行を再開するための定数

//    // スライダーの管理
//    private List<Slider> sliders; // 設定メニュー内のスライダー（BGM・SE）を管理するリスト
//    public List<GameObject> Menu;
//    public List<GameObject> Cursor;
//    private int currentIndex = 0; // 現在選択中の項目（0: BGM, 1: SE, 2: Aボタン）
//    private int totalOptions;     // 選択可能な項目数（スライダーの数 + AボタンUI）

//    // スティック入力の管理
//    private bool stickNeutral = true; // スティックの入力がニュートラルな状態か（押しっぱなし防止）
//    private bool canMove = true;      // 選択移動が可能か（入力クールダウン管理）

//    // カーソル位置管理
//    private Vector3 lastCursorPosition; // 最後に設定されたカーソルの位置

//    // 定数（数値設定）
//    private const float InputCooldown = 0.1f;           // 連続入力のクールダウン時間
//    private const float StickThreshold = 0.5f;          // スティックの入力閾値
//    private const float MinStickNeutralThreshold = 0.1f;// スティックがニュートラルと見なされる閾値
//    private const float CursorOffsetX = -600f;         // カーソル位置のX軸オフセット

//    // 設定メニューの状態を管理するプロパティ
//    public bool IsSettingActive { get; private set; }  // 設定メニューが開いているか


//    private bool isSliderMode = false; // Aボタンでスライダー操作モードに入っているか

//    private void OnEnable()
//    {
//        // InputActionAsset 内の "UI" というアクションマップを取得
//        var uiMap = inputActions.FindActionMap("UI");

//        // 各入力アクション（設定を開く、メニュー移動、戻る、決定）を取得
//        openSettingAction = uiMap.FindAction("OpenSetting");
//        moveAction = uiMap.FindAction("SelectMenu");
//        backAction = uiMap.FindAction("Return");
//        decisionAction = uiMap.FindAction("Decision");

//        // 各アクションにイベントを登録
//        openSettingAction.performed += ctx => ToggleSettingMenu(!IsSettingActive); // 設定メニューの開閉
//        moveAction.performed += OnMove;            // メニュー選択移動処理を呼び出し
//        backAction.performed += ctx => Close();    // 戻る処理（メニューを閉じる）
//        decisionAction.performed += OnDecision;    // 決定処理（タイトルに戻るなど）

//        // 各アクションを有効化（Enable）
//        openSettingAction.Enable();
//        moveAction.Enable();
//        backAction.Enable();
//        decisionAction.Enable();
//    }

//    private void OnDisable()
//    {
//        // 各アクションを無効化
//        openSettingAction.Disable();
//        moveAction.Disable();
//        backAction.Disable();
//        decisionAction.Disable();

//        // イベント登録を解除（メモリリーク防止のため）
//        openSettingAction.performed -= ctx => ToggleSettingMenu(!IsSettingActive);
//        moveAction.performed -= OnMove;
//        backAction.performed -= ctx => Close();
//        decisionAction.performed -= OnDecision;
//    }

//    private void OnMove(InputAction.CallbackContext ctx)
//    {
//        // 設定メニューが開いておらず、または移動操作が無効なら何もしない
//        if (!IsSettingActive || !canMove) return;

//        // スティック（または方向キーなど）から入力値を取得
//        Vector2 input = ctx.ReadValue<Vector2>();

//        // スティックがニュートラルだった状態から、しきい値以上に倒されたときだけ処理
//        if (stickNeutral && Mathf.Abs(input.y) > StickThreshold)
//        {
//            stickNeutral = false; // 押しっぱなし防止のためフラグをfalseに

//            if (input.y > 0)
//            {
//                // 上に倒された：前の項目へ移動（最小0まで）
//                currentIndex = Mathf.Max(0, currentIndex - 1);
//            }
//            else
//            {
//                // 下に倒された：次の項目へ移動（最大 totalOptions - 1 まで）
//                currentIndex = Mathf.Min(totalOptions - 1, currentIndex + 1);
//            }

//            // カーソルの表示位置を更新
//            UpdateCursorPosition();

//            // クールダウン時間後に再び移動できるようにする
//            StartCoroutine(ResetStickNeutralAfterCooldown());
//        }
//        else if (stickNeutral && Mathf.Abs(input.x) > StickThreshold)
//        {
//            // 右スティックで左・右に倒された場合
//            if (input.x > 0)
//            {
//                // カーソルを右に移動（BGM/SEなどに移動）
//                currentIndex = Mathf.Min(Menu.Count - 1, currentIndex + 1);
//                UpdateCursorPosition();
//            }
//            else if (input.x < 0)
//            {
//                // カーソルを左に移動（BGM/SEなどに移動）
//                currentIndex = Mathf.Max(0, currentIndex - 1);
//                UpdateCursorPosition();
//            }

//            // クールダウン時間後に再び移動できるようにする
//            StartCoroutine(ResetStickNeutralAfterCooldown());
//        }
//    }


//    private IEnumerator ResetStickNeutralAfterCooldown()
//    {
//        canMove = false;                      // 移動操作を一時的に無効化
//        yield return new WaitForSeconds(InputCooldown); // 一定時間待機（クールダウン）
//        canMove = true;                       // 再び移動操作を有効にする
//        stickNeutral = true;                 // スティックがニュートラルに戻ったと見なす
//    }

//    // Aボタンでスライダーに移動する処理を追加
//    private void OnDecision(InputAction.CallbackContext ctx)
//    {
//        if (!IsSettingActive) return;

//        if (currentIndex < sliders.Count)
//        {
//            isSliderMode = true; // 操作モード突入
//            sliders[currentIndex].Select(); // フォーカス
//            Debug.Log($"スライダー操作モードへ: {sliders[currentIndex].name}");
//        }
//        else if (buttonAui != null && buttonAui.enabled)
//        {
//            //Quit();
//        }
//    }

//    private void Start()
//    {
//        // 設定メニュー内にあるすべてのSliderコンポーネントを取得しリスト化
//        sliders = new List<Slider>(SettingButton.GetComponentsInChildren<Slider>());

//        // 選択可能な項目数（スライダーの数 + AボタンUI）
//        totalOptions = Menu.Count + 1;
//    }

//    /// <summary>
//    /// 設定メニューを開閉する
//    /// </summary>
//    /// <param name="isActive">設定メニューを表示するかどうかのフラグ</param>
//    public void ToggleSettingMenu(bool isActive)
//    {
//        // メニューの表示・非表示を制御
//        SettingButton.SetActive(isActive);
//        IsSettingActive = isActive;

//        // ゲームパッドが使用されている場合のみ、UIのBボタンとカーソルを有効にする
//        bool isUsingGamepad = Gamepad.current != null;
//        buttonBui.enabled = isUsingGamepad && isActive;
//        settingCursorUI.enabled = isUsingGamepad && isActive;

//        // メニューが表示されている場合、カーソル位置を更新
//        if (isActive)
//        {
//            UpdateCursorPosition();
//        }

//        sliders = new List<Slider>(SettingButton.GetComponentsInChildren<Slider>());
//    }

//    private void Update()
//    {
//        if (!IsSettingActive) return;

//        if (isSliderMode)
//        {
//            HandleSliderControl();
//            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame) // Bボタン
//            {
//                ExitSliderMode();
//            }
//            return; // スライダー操作中は他の処理を行わない
//        }

//        if (SceneManager.GetActiveScene().name == "GameScene")
//        {
//            canMove = true;

//            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
//            {
//                Close();
//            }
//            else if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame && buttonAui.enabled == true)
//            {
//                //Quit();
//            }
//        }

//        if (Gamepad.current != null)
//        {
//            HandleGamepadInput();
//        }
//    }

//    /// <summary>
//    /// ゲームパッドの入力を処理する
//    /// </summary>
//    private void HandleGamepadInput()
//    {
//        // 左スティックの入力を取得
//        Vector2 input = Gamepad.current.leftStick.ReadValue();

//        // 入力可能な状態で、スティックの上下入力に応じてスライダーの選択を変更
//        if (canMove && stickNeutral)
//        {
//            if (input.y > StickThreshold) // 上に入力された場合、前のスライダーに移動
//            {
//                ChangeSliderSelection(-1);
//            }
//            else if (input.y < -StickThreshold) // 下に入力された場合、次のスライダーに移動
//            {
//                ChangeSliderSelection(1);
//            }
//        }

//        // スティックのy軸入力がニュートラル領域（しきい値未満）に戻った場合
//        if (Mathf.Abs(input.y) < MinStickNeutralThreshold)
//        {
//            stickNeutral = true;
//        }

//        // 左右入力で音量調整
//        if (input.x > StickThreshold)
//        {
//            AdjustVolume(VolumeAdjustStep); // 音量を増加
//        }
//        else if (input.x < -StickThreshold)
//        {
//            AdjustVolume(-VolumeAdjustStep); // 音量を減少
//        }
//    }

//    /// <summary>
//    /// スライダーの選択を変更する
//    /// </summary>
//    /// <param name="direction">選択方向（-1: 上、1: 下）</param>
//    private void ChangeSliderSelection(int direction)
//    {
//        // 選択肢の数に応じてインデックスを循環させる
//        currentIndex = (currentIndex + direction + totalOptions) % totalOptions;
//        // カーソルの位置を更新
//        UpdateCursorPosition();
//        // 移動処理が終わるまで次の移動を受け付けない
//        StartCoroutine(ResetMove());
//        stickNeutral = false;
//    }

//    /// <summary>
//    /// カーソルの位置を現在選択中のスライダーの位置に合わせる
//    /// </summary>
//    private void UpdateCursorPosition()
//    {
//        Vector3 offset = new Vector3(CursorOffsetX, 0f, 0f);

//        if (currentIndex < Menu.Count)
//        {
//            settingCursorUI.transform.position = new Vector3(370, Menu[currentIndex].transform.position.y + 10.0f, Menu[currentIndex].transform.position.z);
//            lastCursorPosition = settingCursorUI.transform.position;

//            if (buttonAui != null)
//            {
//                buttonAui.enabled = false;
//            }

//            // 表示切替処理を追加（スライダーの選択状況で制御）
//            SettingSelect.SetActive(currentIndex == 0);           // BGMなど
//            ExplanationSelect.SetActive(currentIndex == 1);        // SEなど
//            GoTitleSelectParent.SetActive(false);                 // Aボタンが選ばれてないので非表示
//            BackPanel.SetActive(true);                            // 常時表示にしてもOK
//            SettingButton.SetActive(true);                        // メニュー表示時は常時 true

//            Debug.Log($"カーソルが指しているスライダー: {Menu[currentIndex].name}");
//        }
//        else if (buttonAui != null)
//        {
//            buttonAui.enabled = true;

//            settingCursorUI.transform.position = new Vector3(
//                lastCursorPosition.x,
//                buttonAui.transform.position.y,
//                lastCursorPosition.z
//            );

//            // Aボタンが選ばれている時の表示切替
//            SettingSelect.SetActive(false);
//            ExplanationSelect.SetActive(false);
//            GoTitleSelectParent.SetActive(true);      // タイトルに戻る選択肢を表示
//            BackPanel.SetActive(true);
//            SettingButton.SetActive(true);

//            Debug.Log("カーソルが指しているのは: AボタンUI（タイトルに戻る）");
//        }
//    }

//    /// <summary>
//    /// 現在選択されているスライダーの音量を調整し、設定を保存する。
//    /// </summary>
//    /// <param name="adjustment">音量の増減値（正: 増加, 負: 減少）</param>
//    private void AdjustVolume(float adjustment)
//    {
//        // Aボタン UI が選択されている場合は音量調整を行わない
//        if (currentIndex >= sliders.Count) return;

//        // 現在選択されているスライダーを取得
//        Slider selectedSlider = sliders[currentIndex];

//        // スティックの入力強度を取得し、音量変更速度を調整
//        float inputStrength = Mathf.Abs(Gamepad.current.leftStick.x.ReadValue());
//        float speedMultiplier = Mathf.Lerp(1f, VolumeAdjustMultiplierMax, inputStrength);

//        // スライダーの値を変更（範囲を超えないように Clamp）
//        selectedSlider.value = Mathf.Clamp(selectedSlider.value + adjustment * speedMultiplier, selectedSlider.minValue, selectedSlider.maxValue);

//        // オーディオミキサーに適用するパラメータ名を判定
//        string parameterName = currentIndex == 0 ? AudioParameterName.BGM : AudioParameterName.SE;

//        // オーディオミキサーに新しい音量を適用
//        audioMixer.SetFloat(parameterName, selectedSlider.value);

//        // 設定を PlayerPrefs に保存
//        PlayerPrefs.SetFloat(parameterName, selectedSlider.value);
//        PlayerPrefs.Save();
//    }
//    /// <summary>
//    /// 入力受付をクールダウンする
//    /// </summary>
//    private IEnumerator ResetMove()
//    {
//        // 入力受付を無効化
//        canMove = false;
//        // 一定時間待機
//        yield return new WaitForSeconds(InputCooldown);
//        // 入力受付を再度有効化
//        canMove = true;
//    }

//    /// <summary>
//    /// 設定メニューを閉じる
//    /// </summary>
//    public void Close()
//    {
//        ToggleSettingMenu(false);
//    }

//    /// <summary>
//    /// Bボタンが押されたときの処理
//    /// </summary>
//    /// <param name="value">Bボタンの入力値</param>
//    private void OnBack(InputValue value)
//    {
//        Close();
//    }

//    /// <summary>
//    /// タイトルシーンに戻る
//    /// </summary>
//    public void Quit()
//    {
//        SceneManager.LoadScene("StartScene");
//    }

//    /// <summary>
//    /// 音声パラメータ名を定義するクラス
//    /// </summary>
//    private static class AudioParameterName
//    {
//        public const string BGM = "BGM"; // BGMのパラメータ名
//        public const string SE = "SE";   // SEのパラメータ名
//    }

//    private void HandleSliderControl()
//    {
//        if (currentIndex >= sliders.Count) return;

//        float xInput = Gamepad.current.rightStick.x.ReadValue();

//        if (Mathf.Abs(xInput) > 0.1f)
//        {
//            Slider slider = sliders[currentIndex];
//            float delta = xInput * Time.deltaTime * 5f; // 値調整スピードは適宜調整
//            slider.value = Mathf.Clamp(slider.value + delta, slider.minValue, slider.maxValue);
//        }
//    }

//    private void ExitSliderMode()
//    {
//        isSliderMode = false;
//        EventSystem.current.SetSelectedGameObject(null);
//        UpdateCursorPosition();
//        Debug.Log("スライダー操作モード終了");
//    }

//    // BGMの音量を設定するメソッド
//    // 引数として渡されたbgmVolumeを使用して、オーディオミキサーでBGMの音量を調整する
//    public void SetBGM(float bgmVolume)
//    {
//        // BGMの音量を設定
//        audioMixer.SetFloat("BGM", bgmVolume); // オーディオミキサーで"BGM"の音量を設定
//    }

//    // SEの音量を設定するメソッド
//    // 引数として渡されたseVolumeを使用して、オーディオミキサーでSEの音量を調整する
//    public void SetSE(float seVolume)
//    {
//        // SEの音量を設定
//        audioMixer.SetFloat("SE", seVolume); // オーディオミキサーで"SE"の音量を設定
//    }

//    // マイクの音量を設定するメソッド
//    // 引数として渡されたmicVolumeを使用して、マイクの音量を調整する
//    public void SetMic(float micVolume)
//    {
//        // マイク音量を設定
//        AudioSource micAudioSource = micObject.GetComponent<AudioSource>(); // micObjectからAudioSourceコンポーネントを取得
//        micAudioSource.volume = micVolume; // 引数のmicVolumeに基づいて、マイクの音量を設定
//    }

//    // マウス感度を設定するメソッド
//    // 引数として渡されたmouseSensitivityを基に、CinemachineカメラのX軸とY軸の感度を調整する
//    public void SetMouse(float mouseSensitivity)
//    {
//        // マウスの感度を設定（Y軸の移動速度）
//        VCamera.m_YAxis.m_MaxSpeed = mouseSensitivity / MouseMaxSpeedDivisor; // Y軸の感度を設定（割り算を使用）

//        // マウスの感度を設定（X軸の移動速度）
//        VCamera.m_XAxis.m_MaxSpeed = mouseSensitivity * MouseMaxSpeedMultiplier; // X軸の感度を設定（掛け算を使用）
//    }
}