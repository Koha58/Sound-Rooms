using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class memo : MonoBehaviour
{
    //[SerializeField] private InputActionAsset inputActions;

    //private InputAction openSettingAction;
    //private InputAction moveAction;
    //private InputAction backAction;
    //private InputAction decisionAction;

    //// 設定メニュー関連のオブジェクト
    //[SerializeField] private GameObject SettingButton;  // 設定メニュー全体のオブジェクト
    //[SerializeField] private GameObject SettingSelect;
    //[SerializeField] private GameObject ExplanationSelect;
    //[SerializeField] private GameObject BackPanel;
    //[SerializeField] private GameObject GoTitleSelectParent;
    //[SerializeField] private Image buttonBui;         // BボタンのUI（戻るボタン表示用）
    //[SerializeField] private Image buttonAui;         // AボタンのUI（タイトルへ戻るボタン表示用）
    //[SerializeField] private Image settingCursorUI;   // 設定メニューのカーソル画像

    //// スライダーの管理
    //private List<Slider> sliders; // 設定メニュー内のスライダー（BGM・SE）を管理するリスト
    //public List<GameObject> Menu;
    //public List<GameObject> Cursor;
    //private int currentIndex = 0; // 現在選択中の項目（0: BGM, 1: SE, 2: Aボタン）
    //private int totalOptions;     // 選択可能な項目数（スライダーの数 + AボタンUI）

    //// スティック入力の管理
    //private bool stickNeutral = true; // スティックの入力がニュートラルな状態か（押しっぱなし防止）
    //private bool canMove = true;      // 選択移動が可能か（入力クールダウン管理）

    //// カーソル位置管理
    //private Vector3 lastCursorPosition; // 最後に設定されたカーソルの位置

    //// 定数（数値設定）
    //private const float InputCooldown = 0.1f;           // 連続入力のクールダウン時間
    //private const float StickThreshold = 0.5f;          // スティックの入力閾値
    //private const float MinStickNeutralThreshold = 0.1f;// スティックがニュートラルと見なされる閾値
    //private const float CursorOffsetX = -600f;         // カーソル位置のX軸オフセット

    //// 設定メニューの状態を管理するプロパティ
    //public bool IsSettingActive { get; private set; }  // 設定メニューが開いているか

    //private void OnEnable()
    //{
    //    // InputActionAsset 内の "UI" というアクションマップを取得
    //    var uiMap = inputActions.FindActionMap("UI");

    //    // 各入力アクション（設定を開く、メニュー移動、戻る、決定）を取得
    //    openSettingAction = uiMap.FindAction("OpenSetting");
    //    moveAction = uiMap.FindAction("SelectMenu");
    //    backAction = uiMap.FindAction("Return");
    //    decisionAction = uiMap.FindAction("Decision");

    //    // 各アクションにイベントを登録
    //    openSettingAction.performed += ctx => ToggleSettingMenu(!IsSettingActive); // 設定メニューの開閉
    //    moveAction.performed += OnMove;            // メニュー選択移動処理を呼び出し
    //    backAction.performed += ctx => Close();    // 戻る処理（メニューを閉じる）
    //    decisionAction.performed += OnDecision;    // 決定処理（タイトルに戻るなど）

    //    // 各アクションを有効化（Enable）
    //    openSettingAction.Enable();
    //    moveAction.Enable();
    //    backAction.Enable();
    //    decisionAction.Enable();
    //}

    //private void OnDisable()
    //{
    //    // 各アクションを無効化
    //    openSettingAction.Disable();
    //    moveAction.Disable();
    //    backAction.Disable();
    //    decisionAction.Disable();

    //    // イベント登録を解除（メモリリーク防止のため）
    //    openSettingAction.performed -= ctx => ToggleSettingMenu(!IsSettingActive);
    //    moveAction.performed -= OnMove;
    //    backAction.performed -= ctx => Close();
    //    decisionAction.performed -= OnDecision;
    //}

    //private void OnMove(InputAction.CallbackContext ctx)
    //{
    //    // 設定メニューが開いておらず、または移動操作が無効なら何もしない
    //    if (!IsSettingActive || !canMove) return;

    //    // スティック（または方向キーなど）から入力値を取得
    //    Vector2 input = ctx.ReadValue<Vector2>();

    //    // スティックがニュートラルだった状態から、しきい値以上に倒されたときだけ処理
    //    if (stickNeutral && Mathf.Abs(input.y) > StickThreshold)
    //    {
    //        stickNeutral = false; // 押しっぱなし防止のためフラグをfalseに

    //        if (input.y > 0)
    //        {
    //            // 上に倒された：前の項目へ移動（最小0まで）
    //            currentIndex = Mathf.Max(0, currentIndex - 1);
    //        }
    //        else
    //        {
    //            // 下に倒された：次の項目へ移動（最大 totalOptions - 1 まで）
    //            currentIndex = Mathf.Min(totalOptions - 1, currentIndex + 1);
    //        }

    //        // カーソルの表示位置を更新
    //        UpdateCursorPosition();

    //        // クールダウン時間後に再び移動できるようにする
    //        StartCoroutine(ResetStickNeutralAfterCooldown());
    //    }
    //    else if (stickNeutral && Mathf.Abs(input.x) > StickThreshold)
    //    {
    //        // 右スティックで左・右に倒された場合
    //        if (input.x > 0)
    //        {
    //            // カーソルを右に移動（BGM/SEなどに移動）
    //            currentIndex = Mathf.Min(Menu.Count - 1, currentIndex + 1);
    //            UpdateCursorPosition();
    //        }
    //        else if (input.x < 0)
    //        {
    //            // カーソルを左に移動（BGM/SEなどに移動）
    //            currentIndex = Mathf.Max(0, currentIndex - 1);
    //            UpdateCursorPosition();
    //        }

    //        // クールダウン時間後に再び移動できるようにする
    //        StartCoroutine(ResetStickNeutralAfterCooldown());
    //    }
    //}


    //private IEnumerator ResetStickNeutralAfterCooldown()
    //{
    //    canMove = false;                      // 移動操作を一時的に無効化
    //    yield return new WaitForSeconds(InputCooldown); // 一定時間待機（クールダウン）
    //    canMove = true;                       // 再び移動操作を有効にする
    //    stickNeutral = true;                 // スティックがニュートラルに戻ったと見なす
    //}

    //// Aボタンでスライダーに移動する処理を追加
    //private void OnDecision(InputAction.CallbackContext ctx)
    //{
    //    // メニューが開いていない場合は何もしない
    //    if (!IsSettingActive) return;

    //    // スライダー項目を選択している場合（indexがスライダー数より小さい）
    //    if (currentIndex < sliders.Count)
    //    {
    //        // スライダー項目に移動したらスライダー操作にフォーカスを移動
    //        Slider slider = sliders[currentIndex];
    //        // 必要に応じてスライダーの操作が可能な状態にする処理（例：スライダーを選択状態にする）
    //        slider.Select();
    //    }
    //    // AボタンUIが選択されている状態なら
    //    else if (buttonAui != null && buttonAui.enabled)
    //    {
    //        // Quit(); // タイトルシーンに戻る処理（タイトルに戻る場合）
    //    }
    //}

    //private void Start()
    //{
    //    // 設定メニュー内にあるすべてのSliderコンポーネントを取得しリスト化
    //    sliders = new List<Slider>(SettingButton.GetComponentsInChildren<Slider>());

    //    // 選択可能な項目数（スライダーの数 + AボタンUI）
    //    totalOptions = Menu.Count + 1;
    //}

    ///// <summary>
    ///// 設定メニューを開閉する
    ///// </summary>
    ///// <param name="isActive">設定メニューを表示するかどうかのフラグ</param>
    //public void ToggleSettingMenu(bool isActive)
    //{
    //    // メニューの表示・非表示を制御
    //    SettingButton.SetActive(isActive);
    //    IsSettingActive = isActive;

    //    // ゲームパッドが使用されている場合のみ、UIのBボタンとカーソルを有効にする
    //    bool isUsingGamepad = Gamepad.current != null;
    //    buttonBui.enabled = isUsingGamepad && isActive;
    //    settingCursorUI.enabled = isUsingGamepad && isActive;

    //    // メニューが表示されている場合、カーソル位置を更新
    //    if (isActive)
    //    {
    //        UpdateCursorPosition();
    //    }

    //    sliders = new List<Slider>(SettingButton.GetComponentsInChildren<Slider>());
    //}

    //private void Update()
    //{
    //    // 設定メニューが表示されている場合のみ処理を行う
    //    if (!IsSettingActive) return;

    //    // 現在のシーンがGameSceneの場合、canMoveをtrueに設定
    //    if (SceneManager.GetActiveScene().name == "GameScene")
    //    {
    //        canMove = true;

    //        // ゲームパッドが接続されており、B(East)ボタンが押されたら
    //        if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
    //        {
    //            // 設定メニューを閉じる
    //            Close();
    //        }
    //        // ゲームパッドが接続されており、AボタンUIが表示され、かつA(South)ボタンが押されたら
    //        else if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame && buttonAui.enabled == true)
    //        {
    //            //// タイトルシーンに戻る
    //            //Quit();
    //        }
    //    }

    //    // ゲームパッドが使用されている場合、入力を処理
    //    if (Gamepad.current != null)
    //    {
    //        HandleGamepadInput();
    //    }
    //}

    ///// <summary>
    ///// ゲームパッドの入力を処理する
    ///// </summary>
    //private void HandleGamepadInput()
    //{
    //    // 左スティックの入力を取得
    //    Vector2 input = Gamepad.current.leftStick.ReadValue();

    //    // 入力可能な状態で、スティックの上下入力に応じてスライダーの選択を変更
    //    if (canMove && stickNeutral)
    //    {
    //        if (input.y > StickThreshold) // 上に入力された場合、前のスライダーに移動
    //        {
    //            ChangeSliderSelection(-1);
    //        }
    //        else if (input.y < -StickThreshold) // 下に入力された場合、次のスライダーに移動
    //        {
    //            ChangeSliderSelection(1);
    //        }
    //    }

    //    // スティックのy軸入力がニュートラル領域（しきい値未満）に戻った場合
    //    if (Mathf.Abs(input.y) < MinStickNeutralThreshold)
    //    {
    //        stickNeutral = true;
    //    }
    //}

    ///// <summary>
    ///// スライダーの選択を変更する
    ///// </summary>
    ///// <param name="direction">選択方向（-1: 上、1: 下）</param>
    //private void ChangeSliderSelection(int direction)
    //{
    //    // 選択肢の数に応じてインデックスを循環させる
    //    currentIndex = (currentIndex + direction + totalOptions) % totalOptions;
    //    // カーソルの位置を更新
    //    UpdateCursorPosition();
    //    // 移動処理が終わるまで次の移動を受け付けない
    //    StartCoroutine(ResetMove());
    //    stickNeutral = false;
    //}

    ///// <summary>
    ///// カーソルの位置を現在選択中のスライダーの位置に合わせる
    ///// </summary>
    //private void UpdateCursorPosition()
    //{
    //    Vector3 offset = new Vector3(CursorOffsetX, 0f, 0f);

    //    if (currentIndex < Menu.Count)
    //    {
    //        settingCursorUI.transform.position = new Vector3(370, Menu[currentIndex].transform.position.y + 10.0f, Menu[currentIndex].transform.position.z);
    //        lastCursorPosition = settingCursorUI.transform.position;

    //        if (buttonAui != null)
    //        {
    //            buttonAui.enabled = false;
    //        }

    //        // 表示切替処理を追加（スライダーの選択状況で制御）
    //        SettingSelect.SetActive(currentIndex == 0);           // BGMなど
    //        ExplanationSelect.SetActive(currentIndex == 1);        // SEなど
    //        GoTitleSelectParent.SetActive(false);                 // Aボタンが選ばれてないので非表示
    //        BackPanel.SetActive(true);                            // 常時表示にしてもOK
    //        SettingButton.SetActive(true);                        // メニュー表示時は常時 true

    //        Debug.Log($"カーソルが指しているスライダー: {Menu[currentIndex].name}");
    //    }
    //    else if (buttonAui != null)
    //    {
    //        buttonAui.enabled = true;

    //        settingCursorUI.transform.position = new Vector3(
    //            lastCursorPosition.x,
    //            buttonAui.transform.position.y,
    //            lastCursorPosition.z
    //        );

    //        // Aボタンが選ばれている時の表示切替
    //        SettingSelect.SetActive(false);
    //        ExplanationSelect.SetActive(false);
    //        GoTitleSelectParent.SetActive(true);      // タイトルに戻る選択肢を表示
    //        BackPanel.SetActive(true);
    //        SettingButton.SetActive(true);

    //        Debug.Log("カーソルが指しているのは: AボタンUI（タイトルに戻る）");
    //    }
    //}
    ///// <summary>
    ///// 入力受付をクールダウンする
    ///// </summary>
    //private IEnumerator ResetMove()
    //{
    //    // 入力受付を無効化
    //    canMove = false;
    //    // 一定時間待機
    //    yield return new WaitForSeconds(InputCooldown);
    //    // 入力受付を再度有効化
    //    canMove = true;
    //}

    ///// <summary>
    ///// 設定メニューを閉じる
    ///// </summary>
    //public void Close()
    //{
    //    ToggleSettingMenu(false);
    //}

    ///// <summary>
    ///// Bボタンが押されたときの処理
    ///// </summary>
    ///// <param name="value">Bボタンの入力値</param>
    //private void OnBack(InputValue value)
    //{
    //    Close();
    //}

    ///// <summary>
    ///// タイトルシーンに戻る
    ///// </summary>
    //public void Quit()
    //{
    //    SceneManager.LoadScene("StartScene");
    //}

    ///// <summary>
    ///// 音声パラメータ名を定義するクラス
    ///// </summary>
    //private static class AudioParameterName
    //{
    //    public const string BGM = "BGM"; // BGMのパラメータ名
    //    public const string SE = "SE";   // SEのパラメータ名
    //}
}
